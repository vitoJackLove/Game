using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 常量
/// </summary>
public static partial class Constant 
{
    /// <summary>
    /// UI数据
    /// </summary>
    public static class UiConstant
    {
        public static string WINDOW_DATA = "WindowData";
        public static string PARENT_WINDOW = "parent_Window";
    }
    
    /// <summary>
    /// 资源路径
    /// </summary>
    public static class ResourcesPath
    {
        private const string Path = "Assets/ABResources";

        public static string GetUiPrefab(string prefabPath)
        {
            return $"{Path}/UI/{prefabPath}.prefab";
        }
    }
}
