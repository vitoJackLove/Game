using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Reflection;
using System;
using ExcelDataReader;
using UnityEngine;

namespace Game
{
    public class ExcelUtility
    {
        /// <summary>
        /// 表格数据集合 单个页签
        /// </summary>
        private DataSet mResultSet;

        /// <summary>
        /// 页签集合
        /// </summary>
        private List<DataSet> _dataSets;

        public List<DataSet> dataSets => _dataSets;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="excelFile">Excel file.</param>
        public ExcelUtility(string excelFile)
        {
            try
            {
                _dataSets = GetDataSet(excelFile);
                mResultSet = _dataSets[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ExcelUtility()
        {
        }

        public static List<DataSet> GetDataSet(string path)
        {
            List<DataSet> dses = new List<DataSet>();
            try
            {
                var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                int CurWritedSheet = 0;
                var ds = new DataSet();
                ds.DataSetName = excelReader.Name;
                do
                {
                    DataTable dt = GetTable(excelReader);
                    ds.Merge(dt);
                    if (excelReader.NextResult())
                    {
                        CurWritedSheet++;
                        dses.Add(ds);
                        ds = new DataSet();
                        ds.DataSetName = excelReader.Name;
                    }
                    else
                    {
                        dses.Add(ds);
                        break;
                    }
                } while (excelReader.ResultsCount > CurWritedSheet);

                excelReader.Close();
                excelReader.Dispose();
                stream.Close();
                stream.Dispose();
                return dses;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static DataTable GetTable(IExcelDataReader excelReader)
        {
            DataTable dt = new DataTable();
            dt.TableName = excelReader.Name;

            bool isInit = false;
            string[] ItemArray = null;
            try
            {
                while (excelReader.Read())
                {
                    if (!isInit)
                    {
                        isInit = true;
                        for (int i = 0; i < excelReader.FieldCount; i++)
                        {
                            dt.Columns.Add("", typeof(string));
                        }

                        ItemArray = new string[excelReader.FieldCount];
                    }

                    /*if (excelReader.IsDBNull(0))
                    {
                        continue;
                    }*/

                    for (int i = 0; i < excelReader.FieldCount; i++)
                    {
                        string value = "";
                        if (!excelReader.IsDBNull(i))
                        {
                            Type celltype = excelReader.GetFieldType(i);
                            if (celltype.Equals(typeof(string)))
                            {
                                value = excelReader.GetString(i);
                            }
                            else if (celltype.Equals(typeof(int)))
                            {
                                value = excelReader.GetInt32(i).ToString();
                            }
                            else if (celltype.Equals(typeof(double)))
                            {
                                value = excelReader.GetDouble(i).ToString();
                            }
                            else if (celltype.Equals(typeof(bool)))
                            {
                                value = excelReader.GetBoolean(i).ToString();
                            }
                        }

                        ItemArray[i] = value;
                    }

                    dt.Rows.Add(ItemArray);
                }
            }
            catch (Exception e)
            {
                excelReader.Close();
                excelReader.Dispose();
                Console.WriteLine(e);
                throw;
            }

            return dt;
        }

        /// <summary>
        /// 转换为实体类列表
        /// </summary>
        public List<T> ConvertToList<T>()
        {
            //判断Excel文件中是否存在数据表
            if (mResultSet.Tables.Count < 1)
                return null;
            //默认读取第一个数据表
            DataTable mSheet = mResultSet.Tables[0];

            //判断数据表内是否存在数据
            if (mSheet.Rows.Count < 1)
                return null;

            //读取数据表行数和列数
            int rowCount = mSheet.Rows.Count;
            int colCount = mSheet.Columns.Count;

            //准备一个列表以保存全部数据
            List<T> list = new List<T>();

            //读取数据
            for (int i = 1; i < rowCount; i++)
            {
                //创建实例
                Type t = typeof(T);
                ConstructorInfo ct = t.GetConstructor(System.Type.EmptyTypes);
                T target = (T) ct.Invoke(null);
                for (int j = 0; j < colCount; j++)
                {
                    //读取第1行数据作为表头字段
                    string field = mSheet.Rows[0][j].ToString();
                    object value = mSheet.Rows[i][j];
                    //设置属性值
                    SetTargetProperty(target, field, value);
                }

                //添加至列表
                list.Add(target);
            }

            return list;
        }

        /// <summary>
        /// 转换为CSV
        /// </summary>
        public void ConvertToCSV(string CSVPath, Encoding encoding)
        {
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

            //创建一个StringBuilder存储数据
            StringBuilder stringBuilder = new StringBuilder();

            //读取数据
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    //使用","分割每一个数值
                    stringBuilder.Append(mSheet.Rows[i][j] + ",");
                }

                //使用换行符分割每一行
                stringBuilder.Append("\r\n");
            }

            //写入文件
            using (FileStream fileStream = new FileStream(CSVPath, FileMode.Create, FileAccess.Write))
            {
                using (TextWriter textWriter = new StreamWriter(fileStream, encoding))
                {
                    textWriter.Write(stringBuilder.ToString());
                }
            }
        }

        /// <summary>
        /// 转换为GF DataTable格式txt
        /// </summary>
        public void ConvertSingleDataSetToTxt(string txtPath, Encoding encoding)
        {
            //Debug.Log("txtPath ：" + txtPath);

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

            //Debug.LogFormat("表格行列数 [{0} , {1}]" , rowCount , colCount);

            int trueColCount = 0;
            int trueRowCount = 0;

            //从1开始 忽略表头
            for (int i = 1; i < rowCount; i++)
            {
                string value = mSheet.Rows[i][1].ToString();

                //Debug.LogFormat("i: {0}  value : {1} " , i , value);

                if (string.IsNullOrEmpty(value))
                {
                    break;
                }

                trueRowCount = i + 1;
            }

            for (int i = 0; i < colCount; i++)
            {
                string value = mSheet.Rows[1][i].ToString();

                //Debug.LogFormat("i: {0}  value : {1} " , i , value);

                if (string.IsNullOrEmpty(value))
                {
                    break;
                }

                trueColCount = i + 1;
            }

            //Debug.LogFormat("真实行列数 [{0} , {1}]" , trueRowCount , trueColCount);

            //创建一个StringBuilder存储数据
            StringBuilder stringBuilder = new StringBuilder();

            List<int> clientData = new List<int>();

            clientData.Add(0);
            for (int i = 0; i < trueColCount; i++)
            {
                string content = mSheet.Rows[1][i].ToString().ToUpper();
                if (string.Equals(content, "C") || string.Equals(content, "CS"))
                {
                    clientData.Add(i);
                }
            }

            //读取数据
            for (int i = 1; i < trueRowCount; i++)
            {
                for (int j = 0; j < clientData.Count; j++)
                {
                    if (i == 3)
                    {
                        //变量名字首字母大写
                        stringBuilder.Append(mSheet.Rows[i][clientData[j]].ToString().Substring(0, 1).ToUpper() + mSheet.Rows[i][clientData[j]].ToString().Substring(1) + "\t");
                    }
                    else
                    {
                        //使用","分割每一个数值
                        stringBuilder.Append(mSheet.Rows[i][clientData[j]] + "\t");
                    }
                }

                //使用换行符分割每一行
                stringBuilder.Append("\r\n");
            }

            //写入文件
            using (FileStream fileStream = new FileStream(txtPath, FileMode.Create, FileAccess.Write))
            {
                using (TextWriter textWriter = new StreamWriter(fileStream, encoding))
                {
                    textWriter.Write(stringBuilder.ToString());
                }
            }
        }

        /// <summary>
        /// 转换为GF DataTable格式txt
        /// </summary>
        public static bool ConvertListDataSetToTxt(string txtPath, List<ExcelData> dataSets, Encoding encoding)
        {
            if (dataSets == null)
            {
                Debug.LogError("读取表格出错.. 数据为空!!");
                return false;
            }

            //创建一个StringBuilder存储数据
            StringBuilder stringBuilder = new StringBuilder();

            //检测是否有重复ID
            List<string> checkList = new List<string>();
            // 检查同种表是否格式一样
            List<int> checkTables = new List<int>();
            //第一次读表需要加上表头
            bool isFirstWrite = true;


            foreach (var itemDartSet in dataSets)
            {
                //判断Excel文件中是否存在数据表
                if (itemDartSet.dataSet.Tables.Count < 1)
                    return false;

                //默认读取第一个数据表
                DataTable mSheet = itemDartSet.dataSet.Tables[0];

                //判断数据表内是否存在数据
                if (mSheet.Rows.Count < 1)
                    return false;

                //读取数据表行数和列数
                int rowCount = mSheet.Rows.Count;
                int colCount = mSheet.Columns.Count;

                List<int> trueRowCount = new List<int>();

                //从1开始 忽略表头
                for (int i = 1; i < rowCount; i++)
                {
                    string value = mSheet.Rows[i][1].ToString();

                    // ID不能为空
                    if (i == 3)
                    {
                        if (string.IsNullOrEmpty(value) || !string.Equals(value, "Id") & !string.Equals(value, "ID") & !string.Equals(value, "id"))
                        {
                            Debug.LogError($"{itemDartSet.excelName}表里的{itemDartSet.dataSet.DataSetName}页签没有ID的属性！！！");
                            return false;
                        }
                    }

                    if (string.IsNullOrEmpty(value))
                    {
                        continue;
                    }

                    trueRowCount.Add(i);
                }

                // 这个是注释行 不加注释也默认有数据
                if (!trueRowCount.Contains(4))
                {
                    trueRowCount.Add(4);
                }

                trueRowCount.Sort();

                List<int> clientData = new List<int>();
                clientData.Add(0);
                for (int i = 1; i < colCount; i++)
                {
                    string content = mSheet.Rows[1][i].ToString().ToUpper();
                    string variableName = mSheet.Rows[3][i].ToString().ToUpper();
                    if ((string.Equals(content, "C") || string.Equals(content, "CS")) && !string.IsNullOrEmpty(variableName))
                    {
                        clientData.Add(i);
                    }
                }

                if (isFirstWrite)
                {
                    checkTables = clientData;
                    //读取数据
                    foreach (var i in trueRowCount)
                    {
                        for (int j = 0; j < clientData.Count; j++)
                        {
                            if (i == 3)
                            {
                                //变量名字首字母大写
                                stringBuilder.Append(mSheet.Rows[i][clientData[j]].ToString().Substring(0, 1).ToUpper() + mSheet.Rows[i][clientData[j]].ToString().Substring(1) + "\t");
                            }
                            else
                            {
                                //使用","分割每一个数值
                                stringBuilder.Append(mSheet.Rows[i][clientData[j]] + "\t");
                            }

                            if (i >= 5 && j == 1)
                            {
                                if (!CheckTextRepeat(mSheet.Rows[i][clientData[j]].ToString(), txtPath, ref checkList))
                                {
                                    return false;
                                }
                            }
                        }

                        //使用换行符分割每一行
                        stringBuilder.Append("\r\n");
                    }
                }
                else
                {
                    if (checkTables.Count != clientData.Count)
                    {
                        Debug.LogError($"{itemDartSet.excelName}表里的{itemDartSet.dataSet.DataSetName}页签和主表格式不匹配！！");
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < checkTables.Count; i++)
                        {
                            if (checkTables[i] != clientData[i])
                            {
                                Debug.LogError($"{itemDartSet.excelName}表里的{itemDartSet.dataSet.DataSetName}页签和主表格式不匹配！！");
                                return false;
                            }
                        }
                    }

                    // 读取数据
                    foreach (var i in trueRowCount)
                    {
                        // 相同的页签后面的只加数据
                        if (i >= 5)
                        {
                            for (int j = 0; j < clientData.Count; j++)
                            {
                                //使用","分割每一个数值
                                stringBuilder.Append(mSheet.Rows[i][clientData[j]] + "\t");

                                if (j == 1)
                                {
                                    if (!CheckTextRepeat(mSheet.Rows[i][clientData[j]].ToString(), txtPath, ref checkList))
                                    {
                                        return false;
                                    }
                                }
                            }
                            //使用换行符分割每一行
                            stringBuilder.Append("\r\n");
                        }
                    }
                }
                isFirstWrite = false;
            }

            //写入文件
            using (FileStream fileStream = new FileStream(txtPath, FileMode.Create, FileAccess.Write))
            {
                using (TextWriter textWriter = new StreamWriter(fileStream, encoding))
                {
                    textWriter.Write(stringBuilder.ToString());
                }
            }

            return true;
        }

        /// <summary>
        /// 检查表是否有重复
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentList"></param>
        /// <param name="excelName"></param>
        private static bool CheckTextRepeat(string content, string excelName, ref List<string> contentList)
        {
            if (contentList.Count == 0)
            {
                contentList.Add(content);
            }
            else
            {
                if (contentList.Contains(content))
                {
                    Debug.LogError(string.Format("{0}表有相同ID：{1}", excelName, content));
                    return false;
                }
                else
                {
                    contentList.Add(content);
                }
            }

            return true;
        }

        /// <summary>
        /// 设置目标实例的属性
        /// </summary>
        private void SetTargetProperty(object target, string propertyName, object propertyValue)
        {
            //获取类型
            Type mType = target.GetType();
            //获取属性集合
            PropertyInfo[] mPropertys = mType.GetProperties();
            foreach (PropertyInfo property in mPropertys)
            {
                if (property.Name == propertyName)
                {
                    property.SetValue(target, Convert.ChangeType(propertyValue, property.PropertyType), null);
                }
            }
        }
    }
}