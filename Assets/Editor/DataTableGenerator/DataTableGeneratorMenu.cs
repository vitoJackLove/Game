using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using GameFramework;
using GameFramework.Editor.DataTableTools;

namespace Game
{
    public static class DataTableGeneratorMenu
    {
        #region 在这里配置输入输出相关内容

        /// <summary>
        /// 配置生成的表格文本的存放目录
        /// </summary>
        const string OUTPUT_TSV_FILE_PATH = "ABResources/DataTables";

        /// <summary>
        /// 加载Excel表的路径
        /// </summary>
        const string LOADEXCELDATAPATH = "AALoadExcel#.xlsx";

        /// <summary>
        /// ExcelGlobal CS的路径
        /// </summary>
        private const string EXCELGLOBALCSPATH = "Assets/Scripts/Runtime/Base/ExcelGlobal.cs";

        private const string EXCELGLOBALMODEPATH = "Assets/ABResources/Configs/LoadExcelDataPath.txt";
        
        #endregion

        #region 生成实现

        const string EXCEL_EXTENSION = ".xlsx";
        const string TSV_EXTENSION = ".txt";
        const char ADD_FILE_SEPERATOR = '+';

        /// <summary>
        /// 加载DataTable数据路径配合表
        /// </summary>
        /// <returns></returns>
        static string[] DataTablePath2String()
        {
            string filePath = Application.dataPath + "/../Design/" + LOADEXCELDATAPATH;
            if (!File.Exists(filePath))
            {
                Debug.LogError($"缺少Excel:{filePath}");
                return null;
            }

            ExcelUtility excel = new ExcelUtility(filePath);
            if (excel.dataSets.Count == 0)
            {
                Debug.LogError($":{filePath}的Excel没有页签");
                return null;
            }

            List<string> contents = new List<string>();
            DataTable mSheet = excel.dataSets[0].Tables[0];
            for (int i = 0; i < mSheet.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(mSheet.Rows[i][0].ToString()))
                {
                    return contents.ToArray();
                }
                contents.Add(mSheet.Rows[i][0].ToString());
            }

            StringBuilder stringBuilder = new StringBuilder();
            using (FileStream fileStream = new FileStream(EXCELGLOBALMODEPATH, FileMode.Open))
            {
                using (StreamReader stream = new StreamReader(fileStream, Encoding.UTF8))
                {
                    stringBuilder.Append(stream.ReadToEnd());
                }
            }
            
            StringBuilder content = new StringBuilder();
            for (int i = 0; i < contents.Count; i++)
            {
                content.Append('"');
                content.Append(contents[i]);
                content.Append('"');
                content.Append(',');
                content.Append('\n');
                content.Append("      ");
            }
            
            stringBuilder.Replace(" __LOADEXCELPATH__", content.ToString());

            using (FileStream fileStream = new FileStream(EXCELGLOBALCSPATH, FileMode.Create))
            {
                using (StreamWriter stream = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    stream.Write(stringBuilder.ToString());
                }
            }
            return contents.ToArray();
        }

        //[MenuItem("Game Framework/DataTableGenerate/GenerateExcel2Txt")]
        static List<string> Excel2Tsv()
        {
            FileHelp.DeleteAllFile(Application.dataPath + "/" + OUTPUT_TSV_FILE_PATH);
            
            string filePath = GameFramework.Utility.Path.GetCombinePath(Application.dataPath + "/../Design");

            List<string> excelFiles = new List<string>();

            DirectoryInfo d = new DirectoryInfo(filePath);

            DataTableGeneratorTextManager.Init();

            foreach (FileInfo excelFile in d.GetFiles())
            {
                if (excelFile.FullName.Contains("~$") || excelFile.FullName.Contains("#"))
                {
                    continue;
                }

                if (excelFile.Extension.Equals(EXCEL_EXTENSION, StringComparison.OrdinalIgnoreCase))
                {
                    //Debug.LogFormat("正在生成表:{0}", excelFile.Name);

                    string fileName = System.IO.Path.GetFileNameWithoutExtension(excelFile.Name);

                    ExcelUtility excel = new ExcelUtility(excelFile.FullName);

                    DataTableGeneratorTextManager.GetDataSet(excel.dataSets,excelFile.Name);
                }
            }

            foreach (var item in DataTableGeneratorTextManager.textDic)
            {
                excelFiles.Add(item.Key);
                // 输出文件夹
                string output = GameFramework.Utility.Path.GetCombinePath(Application.dataPath, string.Format("{0}/{1}", OUTPUT_TSV_FILE_PATH, item.Key));

                output += TSV_EXTENSION;

                if (ExcelUtility.ConvertListDataSetToTxt(output, item.Value, Encoding.GetEncoding("utf-8")))
                {
                    Debug.Log(output + "导出Text成功！");
                }
                else
                {
                    Debug.LogError(output + "导出Text失败！!!");
                }
            }

            AssetDatabase.Refresh();
            return excelFiles;
        }

        [MenuItem("Game Framework/DataTableGenerate/生成所有的数据文件（策划使用） %&z")]
        static void Excel2TsvAndGenerateDataTables()
        {
            DataTablePath2String();
            DataTableGeneratorEnumCode.ReadExcelData();
            List<string> excelFiles = Excel2Tsv();
            foreach (string dataTableName in excelFiles)
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName ='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }

        //[MenuItem("Game Framework/DataTableGenerate/生成固定二进制文件")]
        private static void GenerateDataFile()
        {
            foreach (string dataTableName in Global.DataTableFileNames)
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName ='{0}'", dataTableName));
                    break;
                }
                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
            }
            AssetDatabase.Refresh();
        }

        //[MenuItem("Game Framework/DataTableGenerate/生成固定类文件")]
        private static void GenerateDataClass()
        {
            foreach (string dataTableName in Global.DataTableFileNames)
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName ='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }

        [MenuItem("Game Framework/DataTableGenerate/一键生成（程序使用）")]
        public static void GenerateExcelClass()
        {
            DataTablePath2String();
            DataTableGeneratorEnumCode.GenerateEnumClass();
            foreach (string dataTableName in Excel2Tsv())
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName ='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }

        [MenuItem("Game Framework/清空本地数据")]
        private static void GenerateDeleteAllObscuredPref()
        {
            PlayerPrefs.DeleteAll();

            Debug.Log("<color=green>清除成功...</color>");
        }

        [MenuItem("Game Framework/DataTableGenerate/删除/删除DataTables数据")]
        private static void GenerateDeleteDataTables()
        {
            FileHelp.DeleteAllFile(Application.dataPath + "/" + OUTPUT_TSV_FILE_PATH);
            FileHelp.DeleteAllFile(DataTableGenerator.CSharpCodePath);
            AssetDatabase.Refresh();
        }
        
        #endregion
    }
}