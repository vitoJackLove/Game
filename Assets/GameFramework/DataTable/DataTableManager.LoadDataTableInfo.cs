//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.DataTable
{
    internal sealed partial class DataTableManager : IDataTableManager
    {
        private sealed class LoadDataTableInfo
        {
            private LoadType m_LoadType;
            private object m_UserData;

            public LoadDataTableInfo()
            {
                m_LoadType = LoadType.Text;
                m_UserData = null;
            }

            public LoadType LoadType => m_LoadType;

            public object UserData => m_UserData;

            public static LoadDataTableInfo Create(LoadType loadType, object userData)
            {
                LoadDataTableInfo loadDataTableInfo = new LoadDataTableInfo();
                loadDataTableInfo.m_LoadType = loadType;
                loadDataTableInfo.m_UserData = userData;
                return loadDataTableInfo;
            }

            public void Clear()
            {
                m_LoadType = LoadType.Text;
                m_UserData = null;
            }
        }

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <param name="text">要解析的数据表文本。</param>
        /// <returns>要创建的数据表。</returns>
        public DataTableBase CreateDataTable(Type dataRowType, string name, string text)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            if (HasDataTable(dataRowType, name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist data table '{0}'.", Utility.Text.GetFullName(dataRowType, name)));
            }

            Type dataTableType = typeof(DataTable<>).MakeGenericType(dataRowType);
            DataTableBase dataTable = (DataTableBase)Activator.CreateInstance(dataTableType, name);
            InternalCreateDataTable(dataTable, text);
            m_DataTables.Add(Utility.Text.GetFullName(dataRowType, name), dataTable);
            return dataTable;
        }

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <param name="bytes">要解析的数据表二进制流。</param>
        /// <returns>要创建的数据表。</returns>
        public DataTableBase CreateDataTable(Type dataRowType, string name, byte[] bytes)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            if (HasDataTable(dataRowType, name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist data table '{0}'.", Utility.Text.GetFullName(dataRowType, name)));
            }

            Type dataTableType = typeof(DataTable<>).MakeGenericType(dataRowType);
            DataTableBase dataTable = (DataTableBase)Activator.CreateInstance(dataTableType, name);
            InternalCreateDataTable(dataTable, bytes);
            m_DataTables.Add(Utility.Text.GetFullName(dataRowType, name), dataTable);
            return dataTable;
        }
        
        /// <summary>
        /// 是否存在数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <returns>是否存在数据表。</returns>
        public bool HasDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalHasDataTable(Utility.Text.GetFullName(dataRowType, name));
        }

        private bool InternalHasDataTable(string fullName)
        {
            return m_DataTables.ContainsKey(fullName);
        }
        
        private void InternalCreateDataTable(DataTableBase dataTable, string text)
        {
            IEnumerable<GameFrameworkSegment<string>> dataRowSegments = null;
            try
            {
                dataRowSegments = m_DataTableHelper.GetDataRowSegments(text);
            }
            catch (Exception exception)
            {
                if (exception is GameFrameworkException)
                {
                    throw;
                }

                throw new GameFrameworkException(Utility.Text.Format("Can not get data row segments with exception '{0}'.", exception.ToString()), exception);
            }

            if (dataRowSegments == null)
            {
                throw new GameFrameworkException("Data row segments is invalid.");
            }

            foreach (GameFrameworkSegment<string> dataRowSegment in dataRowSegments)
            {
                if (!dataTable.AddDataRow(dataRowSegment))
                {
                    throw new GameFrameworkException("Add data row failure.");
                }
            }
        }

        private void InternalCreateDataTable(DataTableBase dataTable, byte[] bytes)
        {
            IEnumerable<GameFrameworkSegment<byte[]>> dataRowSegments = null;
            try
            {
                dataRowSegments = m_DataTableHelper.GetDataRowSegments(bytes);
            }
            catch (Exception exception)
            {
                if (exception is GameFrameworkException)
                {
                    throw;
                }

                throw new GameFrameworkException(Utility.Text.Format("Can not get data row segments with exception '{0}'.", exception.ToString()), exception);
            }

            if (dataRowSegments == null)
            {
                throw new GameFrameworkException("Data row segments is invalid.");
            }

            foreach (GameFrameworkSegment<byte[]> dataRowSegment in dataRowSegments)
            {
                if (!dataTable.AddDataRow(dataRowSegment))
                {
                    throw new GameFrameworkException("Add data row failure.");
                }
            }
        }
        
        /// <summary>
        /// 获取所有数据表。
        /// </summary>
        /// <returns>所有数据表。</returns>
        public DataTableBase[] GetAllDataTables()
        {
            int index = 0;
            DataTableBase[] results = new DataTableBase[m_DataTables.Count];
            foreach (KeyValuePair<string, DataTableBase> dataTable in m_DataTables)
            {
                results[index++] = dataTable.Value;
            }

            return results;
        }

        /// <summary>
        /// 获取所有数据表。
        /// </summary>
        /// <param name="results">所有数据表。</param>
        public void GetAllDataTables(List<DataTableBase> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, DataTableBase> dataTable in m_DataTables)
            {
                results.Add(dataTable.Value);
            }
        }
    }
}
