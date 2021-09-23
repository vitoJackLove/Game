using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 实体状态
/// </summary>
public enum EntityStateType
{
    Survive,
    Die,
}

/// <summary>
/// 实体基类
/// </summary>
public abstract class BaseEntity : BaseLifeCycle, IReference
{
    /// <summary>
    /// 实体ID （唯一）
    /// </summary>
    protected int entityId;

    /// <summary>
    /// 实体配置数据
    /// </summary>
    protected EntityConfigData configData;

    public EntityConfigData ConfigData => configData;

    /// <summary>
    /// 实体系统
    /// </summary>
    protected EntitySystem entitySystem;

    /// <summary>
    /// 实体组件
    /// </summary>
    protected readonly Dictionary<Type, BaseComponent> entityComponents  = new Dictionary<Type, BaseComponent>();
    
    /// <summary>
    /// 实体组件
    /// </summary>
    /// <returns></returns>
    protected abstract Type[] GetEntityComponents();

    /// <summary>
    /// 实体视图
    /// </summary>
    /// <returns></returns>
    public abstract Type[] GetEntityViews();

    public T GetSystem<T>() where T : BaseSystem
    {
        return entitySystem.GetSystem<T>();
    }
    
    public T GetComponent<T>() where T : BaseComponent
    {
        return (T)entityComponents[typeof(T)];
    }

    //TODO
    public T GetView<T>() where T : BaseEntityView
    {
        return null;
    }
    
    public override void OnInit(object obj = null)
    {
        CommandEntityData data = (CommandEntityData) obj;

        entityId = data.entityId;

        entitySystem = data.entitySystem;
        
        foreach (var type in GetEntityComponents())
        {
            BaseComponent component = (BaseComponent)Activator.CreateInstance(type);
            entityComponents.Add(type,component);
            
            component.OnInit(this);
        }
        
        ReferencePool.Release(data);
    }
    
    public override async Task OnEnter(object obj = null)
    {
        foreach (var component in entityComponents.Values)
        {
           await component.OnEnter();
        }
    }

    public override void OnStart(object obj = null)
    {
        foreach (var component in entityComponents.Values)
        {
            component.OnStart();
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var component in entityComponents.Values)
        {
            component.OnUpdate(deltaTime);
        }
    }

    public override void OnFixUpdate(float deltaTime)
    {
        foreach (var component in entityComponents.Values)
        {
            component.OnFixUpdate(deltaTime);
        }
    }

    public override void OnLateUpdate(float deltaTime)
    {
        foreach (var component in entityComponents.Values)
        {
            component.OnLateUpdate(deltaTime);
        }
    }

    public override void OnDispose()
    {
        foreach (var component in entityComponents.Values)
        {
            component.OnDispose();
        }
    }

    public void Clear()
    {
        
    }
}
