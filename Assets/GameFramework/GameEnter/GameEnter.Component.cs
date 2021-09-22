using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

public partial class GameEnter
{
    /// <summary>
    /// UI
    /// </summary>
    public static UIComponent UI => _ui;

    private static UIComponent _ui;


    /// <summary>
    /// 资源
    /// </summary>
    public static ResourcesComponent Resources => _resources;
    
    private static ResourcesComponent _resources;

    /// <summary>
    /// 数据表
    /// </summary>
    private static DataTableComponent _dataTable;
    public static DataTableComponent DataTable => _dataTable;


    /// <summary>
    /// 初始化游戏组件
    /// </summary>
    private void InitGameFrameworkComponent()
    {
        _ui = (UIComponent)GameEntryBase.GetComponent<UIComponent>();
        _ui.OnInit();
        
        _resources = (ResourcesComponent)GameEntryBase.GetComponent<ResourcesComponent>();
        _resources.OnInit();

        _dataTable = (DataTableComponent)GameEntryBase.GetComponent<DataTableComponent>();
        _dataTable.OnInit();
        
    }
}
