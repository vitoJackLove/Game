using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体系统
/// </summary>
public class EntitySystem : BaseSystem
{
    private int entityId;
    
    private Dictionary<int,BaseEntity> _entities = new Dictionary<int, BaseEntity>();
    
    public override void OnInit(object obj = null)
    {
        entityId = 0;
        //Debuger.Log("EntitySystem Init");
    }

    public override void OnStart(object obj = null)
    {
        
    }

    public override void OnUpdate(float deltaTime)
    {
        
    }

    public override void OnFixUpdate(float deltaTime)
    {
        
    }

    public BaseEntity CreateEntity<T>() where T : BaseEntity, new()
    {
        BaseEntity entity =  ReferencePool.Acquire<T>();

        CommandEntityData data = ReferencePool.Acquire<CommandEntityData>();
        
        entityId++;

        data.entityId = entityId;

        data.entitySystem = this;
        
        entity.OnInit(data);
        _entities.Add(entityId,entity);
        entity.OnEnter();
        return entity;
    }

    public BaseEntity CreateEntity<T>(BaseEntity parentEntity) where T : BaseEntity ,new ()
    {
        BaseEntity entity =  ReferencePool.Acquire<T>();

        CommandEntityData data = ReferencePool.Acquire<CommandEntityData>();
        
        entityId++;
        
        data.parent = parentEntity;
        
        data.entityId = entityId;
        
        data.entitySystem = this;
        
        entity.OnInit(data);
        _entities.Add(entityId,entity);
        entity.OnEnter();
        return entity;
    }

    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="entityId">实体ID</param>
    /// <returns></returns>
    public BaseEntity GetEntity(int entityId)
    {
        return _entities[entityId];
    }

    public override void OnDispose()
    {
        
    }
}
