using System.Collections.Generic;
using System.Data;
using UnityEngine;

public static class DataTableGeneratorTextManager
{
    public static Dictionary<string, List<ExcelData>> textDic;

    public static void Init()
    {
        textDic = new Dictionary<string, List<ExcelData>>();
    }

    public static void GetDataSet(List<DataSet> dataSets, string excelName)
    {
        foreach (var item in dataSets)
        {
            if (item.DataSetName.StartsWith("#"))
            {
                continue;
            }
            
            ExcelData data = new ExcelData
            {
                dataSet = item,
                excelName = excelName
            };
            
            if (textDic.Count == 0)
            {
                List<ExcelData> dataSet = new List<ExcelData>();
                dataSet.Add(data);
                textDic.Add(item.DataSetName, dataSet);
            }
            else
            {
                if (textDic.ContainsKey(item.DataSetName))
                {
                    textDic[item.DataSetName].Add(data);
                }
                else
                {
                    List<ExcelData> dataSet = new List<ExcelData>();
                    dataSet.Add(data);
                    textDic.Add(item.DataSetName, dataSet);
                }
            }
        }
    }
}

public class ExcelData
{
    /// <summary>
    /// 页签所在的Excel名字
    /// </summary>
    public string excelName;
    /// <summary>
    /// 表数据
    /// </summary>
    public DataSet dataSet;
}