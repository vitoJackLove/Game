using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using Game;
using UnityEditor;
using UnityEngine;

public class DataTableGeneratorEnumCode : Editor
{
    //private static readonly string enumExcelTemplatePath = "Assets/Art/DataTable/Configs/DataTableEnum.txt";
    private static readonly string enumExcelDataPath = "Assets/ABResources/Configs/EnumType.xlsx";
    private static readonly string enumCsFilePath = "Assets/Scripts/Runtime/DataTable/DREnumType.cs";

    private static DataSet mResultSet;

    private static List<EnumData> _enumDatas = new List<EnumData>();

    //[MenuItem("Game Framework/DataTableGenerate/生成枚举配置CS")]
    public static void GenerateEnumClass()
    {
        ReadExcelData();
        WriteCsCode();
    }

    public static void ReadExcelData()
    {
        _enumDatas.Clear();
        if (!File.Exists(enumExcelDataPath))
        {
            Debug.LogError("加载枚举失败，路径不存在！");
            return;
        }

        mResultSet = ExcelUtility.GetDataSet(enumExcelDataPath)[0];
        if (mResultSet == null)
        {
            Debug.LogError("读取表格出错.. 数据为空!!");
            return;
        }

        //判断Excel文件中是否存在数据表
        if (mResultSet.Tables.Count < 1)
            return;

        //默认读取第一个数据表
        DataTable mSheet = mResultSet.Tables[0];

        //判断数据表内是否存在数据
        if (mSheet.Rows.Count < 1)
            return;

        //读取数据表行数和列数
        int rowCount = mSheet.Rows.Count;
        int colCount = mSheet.Columns.Count;

        //读取数据
        for (int i = 0; i < rowCount; i++)
        {
            EnumData data = new EnumData();
            data.enumTypeName = new List<string>();
            for (int j = 0; j < colCount; j++)
            {
                if (mSheet.Rows[i][j].ToString() == "Stop")
                {
                    continue;
                }

                if (j == 0)
                {
                    data.enumName = mSheet.Rows[i][j].ToString();
                }
                else if (j == 1)
                {
                    data.enumDoc = mSheet.Rows[i][j].ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(mSheet.Rows[i][j].ToString()))
                    {
                        data.enumTypeName.Add(mSheet.Rows[i][j].ToString());
                    }
                }
            }

            if (!string.IsNullOrEmpty(data.enumName))
            {
                _enumDatas.Add(data);
            }
        }
    }

    private static void WriteCsCode()
    {
        StringBuilder stringBuilder = new StringBuilder();
        bool firstProperty = true;
        for (int i = 0; i < _enumDatas.Count; i++)
        {
            if (firstProperty)
            {
                firstProperty = false;
            }
            else
            {
                stringBuilder.AppendLine().AppendLine();
            }

            stringBuilder
                .AppendLine("/// <summary>")
                .AppendFormat("/// {0}。", _enumDatas[i].enumDoc).AppendLine()
                .AppendLine("/// </summary>")
                .AppendFormat("public {0} {1}", "enum", _enumDatas[i].enumName).AppendLine()
                .AppendLine("{");

            foreach (var item in _enumDatas[i].enumTypeName)
            {
                stringBuilder.AppendLine(item+",");
            }

            stringBuilder.Append("}");
        }

        using (FileStream fileStream = new FileStream(enumCsFilePath, FileMode.Create))
        {
            using (StreamWriter stream = new StreamWriter(fileStream, Encoding.UTF8))
            {
                stream.Write(stringBuilder.ToString());
            }
        }
    }

    /// <summary>
    /// 得到枚举的默认值
    /// </summary>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static string GetEnumDefaultValue(string enumType)
    {
        return _enumDatas.Find(e => e.enumName.Equals(enumType)).enumTypeName[0];
    }
}

public class EnumData
{
    public string enumName;
    public string enumDoc;
    public List<string> enumTypeName;
}