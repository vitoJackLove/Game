using System;
using GameFramework.Asynchronous;

namespace GameFramework.DataTable
{
    public interface IDataTableManager
    {
        /// <summary>
        /// 获取数据表数量。
        /// </summary>
        int Count
        {
            get;
        }
        
        /// <summary>
        /// 设置资源管理器。
        /// </summary>
        /// <param name="resourceManager">资源管理器。</param>
        void SetResourceManager(ResourcesComponent resourceManager);

        /// <summary>
        /// 设置数据表辅助器。
        /// </summary>
        /// <param name="dataTableHelper">数据表辅助器。</param>
        void SetDataTableHelper(IDataTableHelper dataTableHelper);
        
        /// <summary>
        /// 获取所有数据表。
        /// </summary>
        /// <returns>所有数据表。</returns>
        DataTableBase[] GetAllDataTables();
        
        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <param name="text">要解析的数据表文本。</param>
        /// <returns>要创建的数据表。</returns>
        DataTableBase CreateDataTable(Type dataRowType, string name, string text);

        /// <summary>
        /// 创建数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="name">数据表名称。</param>
        /// <param name="bytes">要解析的数据表二进制流。</param>
        /// <returns>要创建的数据表。</returns>
        DataTableBase CreateDataTable(Type dataRowType, string name, byte[] bytes);
        
        /// <summary>
        /// 加载数据表。
        /// </summary>
        /// <param name="dataTableAssetName">数据表资源名称。</param>
        /// <param name="loadType">数据表加载方式。</param>
        /// <param name="loadAsync"></param>
        /// <param name="userData">用户自定义数据。</param>
        void LoadDataTable(string dataTableAssetName, LoadType loadType, IPromise loadAsync, object userData);
        
        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>要获取的数据表。</returns>
        IDataTable<T> GetDataTable<T>() where T : IDataRow;
    }
}