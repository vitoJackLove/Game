using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 实体视图组件 （主要维护实体的视图）
/// </summary>
public class EntityViewComponent : BaseComponent
{
    private Dictionary<Type,BaseEntityView> entityViews = new Dictionary<Type, BaseEntityView>();
    
    public override void OnInit(object obj = null)
    {
        LoadPrefabAsset();
    }

    public override void OnStart(object obj = null)
    {
        
    }
    
    /// <summary>
    /// 加载视图 TODO 这里需要加个CacheSystem
    /// </summary>
    private async void LoadPrefabAsset()
    {
         Task<GameObject> task = GameEnter.Resources.LoadAssetAsync<GameObject>(entity.ConfigData.assetsPath);
         await task;
         GameObject go = task.Result;
         foreach (var type in entity.GetEntityViews())
         {
             BaseEntityView view = (BaseEntityView)go.GetOrAddComponent(type);
             entityViews.Add(type,view);
         }
    }

    public override void OnDispose()
    {
        
    }
}
