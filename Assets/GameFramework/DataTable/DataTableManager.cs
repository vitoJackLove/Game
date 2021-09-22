using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace GameFramework.DataTable
{
    internal sealed partial class DataTableManager : IDataTableManager
    {
        private readonly Dictionary<string, DataTableBase> m_DataTables;
        private IDataTableHelper m_DataTableHelper;

        private ResourcesComponent m_ResourceManager;
        
        public int Count => m_DataTables.Count;
        
        /// <summary>
        /// 初始化数据表管理器的新实例。
        /// </summary>
        public DataTableManager()
        {
            m_DataTables = new Dictionary<string, DataTableBase>();
            m_DataTableHelper = null;
        }
        
        public void SetResourceManager(ResourcesComponent resourceManager)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            m_ResourceManager = resourceManager;
        }

        public void SetDataTableHelper(IDataTableHelper dataTableHelper)
        {
            if (dataTableHelper == null)
            {
                throw new GameFrameworkException("Data table helper is invalid.");
            }
            
            m_DataTableHelper = dataTableHelper;
        }
        
        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="dataTableAssetName"></param>
        /// <param name="loadType"></param>
        /// <param name="loadAsync">加载回调</param>
        /// <param name="userData"></param>
        public async void LoadDataTable(string dataTableAssetName, LoadType loadType, GameFramework.Asynchronous.IPromise loadAsync, object userData)
        {
            if (m_ResourceManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (m_DataTableHelper == null)
            {
                throw new GameFrameworkException("You must set data table helper first.");
            }

            Object asset = await m_ResourceManager.LoadAssetAsync<Object>(dataTableAssetName);
            
            loadAsync?.SetResult(asset);
            
            if (asset == null)
            {
                throw new GameFrameworkException($"加载数据失败:{dataTableAssetName}表格不存在!!!");
            }

            if (!m_DataTableHelper.LoadDataTable(asset, loadType, userData))
            {
                throw new GameFrameworkException(Utility.Text.Format("Load data table failure in helper, asset name '{0}'.", dataTableAssetName));
            }
        }
        
        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>要获取的数据表。</returns>
        public IDataTable<T> GetDataTable<T>() where T : IDataRow
        {
            return (IDataTable<T>)InternalGetDataTable(Utility.Text.GetFullName<T>(string.Empty));
        }
        
        private DataTableBase InternalGetDataTable(string fullName)
        {
            if (m_DataTables.TryGetValue(fullName, out var dataTable))
            {
                return dataTable;
            }

            return null;
        }
    }
}