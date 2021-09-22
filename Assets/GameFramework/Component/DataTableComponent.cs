using System;
using System.Collections.Generic;
using Loxodon.Framework.Asynchronous;
using GameFramework.DataTable;
using UnityEngine;

/// <summary>
/// 加载方式。
/// </summary>
public enum LoadType
{
    /// <summary>
    /// 按文本加载。
    /// </summary>
    Text = 0,

    /// <summary>
    /// 按二进制流加载。
    /// </summary>
    Bytes,
}

namespace GameFramework
{
    /// <summary>
    /// 数据表组件
    /// </summary>
    [DisallowMultipleComponent]
    public class DataTableComponent : GameFrameworkComponent
    {
        private IDataTableManager m_DataTableManager = null;
        
        [SerializeField]
        private string m_DataTableHelperTypeName = "UnityGameFramework.DataTable.DefaultDataTableHelper";

        [SerializeField]
        private DataTableHelperBase m_CustomDataTableHelper = null;

        /// <summary>
        /// 初始加载配置文件列表
        /// </summary>
        public readonly string[] DataTableNames = new string[]
        {
        };
        
        /// <summary>
        /// 怪物受击行为树配置
        /// int key = 怪物Id
        /// string key = "冲击力-霸体等级" Value = 行为树Id
        /// </summary>
        private Dictionary<int, Dictionary<string, int>> monsterHitConfig = new Dictionary<int, Dictionary<string, int>>();
        
        public override void OnInit()
        {
            ResourcesComponent resourceComponent = GameEnter.Resources;

            m_DataTableManager = new DataTableManager();
            
            m_DataTableManager.SetResourceManager(resourceComponent);
            
            DataTableHelperBase dataTableHelper = Helper.CreateHelper(m_DataTableHelperTypeName, m_CustomDataTableHelper);
            
            if (dataTableHelper == null)
            {
                Debug.LogError("Can not create data table helper.");
                return;
            }
            
            dataTableHelper.Init(this , resourceComponent);
            dataTableHelper.name = "Data Table Helper";
            Transform helpTsm = dataTableHelper.transform;
            helpTsm.SetParent(this.transform);
            helpTsm.localScale = Vector3.one;
            m_DataTableManager.SetDataTableHelper(dataTableHelper);
        }

        // <summary>
        /// 获取数据表数量。
        /// </summary>
        public int Count => m_DataTableManager.Count;

        /// <summary>
        /// 获取所有数据表。
        /// </summary>
        public DataTableBase[] GetAllDataTables()
        {
            return m_DataTableManager.GetAllDataTables();
        }
        
        /// <summary>
        /// 加载数据表。
        /// </summary>
        /// <param name="dataRowType">数据表行的类型。</param>
        /// <param name="dataTableName">数据表名称。</param>
        /// <param name="dataTableNameInType">数据表类型下的名称。</param>
        /// <param name="dataTableAssetName">数据表资源名称。</param>
        /// <param name="loadType">数据表加载方式。</param>
        /// <param name="promise"></param>
        /// <param name="userData">用户自定义数据。</param>
        public void LoadDataTable(Type dataRowType, string dataTableName, string dataTableNameInType, string dataTableAssetName, LoadType loadType , IPromise promise , object userData)
        {
            if (dataRowType == null)
            {
                Debug.LogError("Data row type is invalid.");
                return;
            }

            if (string.IsNullOrEmpty(dataTableName))
            {
                Debug.LogError("Data table name is invalid.");
                return;
            }

            m_DataTableManager.LoadDataTable(dataTableAssetName, loadType , promise , LoadDataTableInfo.Create(dataRowType, dataTableName, dataTableNameInType, userData));
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
            return m_DataTableManager.CreateDataTable(dataRowType, name, text);
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
            return m_DataTableManager.CreateDataTable(dataRowType, name, bytes);
        }
        
        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        /// <returns>要获取的数据表。</returns>
        public IDataTable<T> GetDataTable<T>() where T : IDataRow
        {
            return m_DataTableManager.GetDataTable<T>();
        }
        
        /// <summary>
        /// 根据Id获取数据表中的数据。
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        public T GetDataRow<T>(int id) where T : IDataRow
        {
            IDataTable<T> dataRows = GetDataTable<T>();

            if (dataRows == null)
            {
                Debuger.LogError($"没有找到表[{typeof(T)}的配置.[{id}].]");
            }
            
            return dataRows.GetDataRow(id);
        }
        
        /// <summary>
        /// 获取所有表的数据
        /// </summary>
        /// <typeparam name="T">数据表行的类型。</typeparam>
        public T [] GetAllDataRow<T>() where T : IDataRow
        {
            IDataTable<T> dataRows = GetDataTable<T>();

            if (dataRows == null)
            {
                Debuger.LogError($"没有找到表[{typeof(T)}的配置.]");
            }

            return dataRows.GetAllDataRows();
        }
        
        /// <summary>
        /// 获取符合条件的数据表行。
        /// </summary>
        public T GetDataRow<T>(Predicate<T> condition) where T : IDataRow
        {
            IDataTable<T> dataRows = GetDataTable<T>();

            if (dataRows == null)
            {
                Debug.Log($"没有找到表[{typeof(T)}的配置..]");
            }
            
            return dataRows.GetDataRow(condition);
        }

        public override void OnShutDown()
        {
            
        }
    }
}