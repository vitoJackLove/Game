using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 添加实体传的参数
/// </summary>
public class CommandEntityData : IReference
{
    public int entityId;

    /// <summary>
    /// 实体的创建者 有可能是空的
    /// </summary>
    public BaseEntity parent;

    public EntitySystem entitySystem;
    
    public void Clear()
    {
        entityId = 0;
        parent = null;
        entitySystem = null;
    }
}
