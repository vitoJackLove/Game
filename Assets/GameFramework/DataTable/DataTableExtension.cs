using System;
using System.Collections.Generic;
using GameFramework.Asynchronous;
using GameFramework.DataTable;
using UnityEngine;

namespace GameFramework
{
    public static class DataTableExtension
    {
        internal static readonly char[] DataSplitSeparators = new char[] { '\t' };
        internal static readonly char[] DataTrimSeparators = new char[] { '\"' };
        
        /// <summary>
        /// 配置文件字典前缀
        /// </summary>
        public static string DATA_TABLE_PREFIX_NAME = "Game.DR";
        
        public static void LoadDataTable(this DataTableComponent dataTableComponent, string dataTableName, LoadType loadType , IPromise promise = null, object userData = null)
        {
            if (string.IsNullOrEmpty(dataTableName))
            {
                Debug.LogWarning("Data table name is invalid.");
                return;
            }

            string[] splitNames = dataTableName.Split('_');
            if (splitNames.Length > 2)
            {
                Debug.LogWarning("Data table name is invalid.");
                return;
            }

            string dataRowClassName = DATA_TABLE_PREFIX_NAME + splitNames[0];

            Type dataRowType = Type.GetType(dataRowClassName);
            if (dataRowType == null)
            {
                Debug.LogWarningFormat("Can not get data row type with class name '{0}'.", dataRowClassName);
                return;
            }

            string dataTableNameInType = splitNames.Length > 1 ? splitNames[1] : null;
            
            dataTableComponent.LoadDataTable(dataRowType, dataTableName, dataTableNameInType, AssetUtility.GetDataTableAsset(dataTableName, loadType), loadType , promise , userData);
        }

        public static Vector3 ParseVector3(string content)
        {
            string[] v3Data = content.Split(',');
            if (v3Data.Length<2)
            {
                Debug.LogError("Vector3数据错误！！！");
                return new Vector3();
            }
            return new Vector3(float.Parse(v3Data[0]),float.Parse(v3Data[1]),float.Parse(v3Data[2]));
        }
        
        public static List<int> ParseListInt(string content)
        {
            try
            {
                List<int> listData = new List<int>();
                if (string.IsNullOrEmpty(content))
                {
                    return listData;
                }

                if (content.Contains(","))
                {
                    string[] v3Data = content.TrimStart('(').TrimEnd(')').Split(',');
                    foreach (var t in v3Data)
                    {
                        listData.Add(int.Parse( t));
                    }
                }
                else
                {
                    listData.Add(int.Parse(content));
                }
                return listData;
            }
            catch (Exception e)
            {
                Debug.LogError($"{content} 数据格式不正常！！！");
                return null;
            }
        }
        
        public static List<float> ParseListfloat(string content)
        {
            List<float> listDatas = new List<float>();
            if (string.IsNullOrEmpty(content))
            {
                return listDatas;
            }

            if (content.Contains(","))
            {
                string[] v3Data = content.TrimStart('(').TrimEnd(')').Split(',');
                for (int i = 0; i < v3Data.Length; i++)
                {
                    listDatas.Add(float.Parse( v3Data[i]));
                }
            }
            else
            {
                listDatas.Add(float.Parse(content));
            }
            return listDatas;
        }
        
        public static List<string> ParseListstring(string content)
        {
            List<string> listData = new List<string>();
            if (string.IsNullOrEmpty(content))
            {
                return listData;
            }

            if (content.Contains(","))
            {
                string[] v3Data = content.TrimStart('(').TrimEnd(')').Split(',');
                for (int i = 0; i < v3Data.Length; i++)
                {
                    listData.Add( v3Data[i]);
                }
            }
            else
            {
                listData.Add(content);
            }
            return listData;
        }
        
        public static List<T> ParseListEnum<T>(string readString)
        {
            List<T> listData = new List<T>();
            if (string.IsNullOrEmpty(readString))
            {
                return listData;
            }
            if (readString.Contains(","))
            {
                string[] v3Data = readString.TrimStart('(').TrimEnd(')').Split(',');
                foreach (var t in v3Data)
                {
                    listData.Add((T) Enum.Parse(typeof(T), t));
                }
            }
            else
            {
                listData.Add((T) Enum.Parse(typeof(T), readString));
            }

            return listData;
        }
    }
}