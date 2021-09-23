using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 组件基类
/// </summary>
public abstract class BaseComponent : BaseLifeCycle
{
    protected BaseEntity entity;
    
    public async override Task OnEnter(object obj = null)
    {
       
    }

    public override void OnInit(object obj = null)
    {
        entity = (BaseEntity) obj;
    }


    public abstract override void OnStart(object obj = null);
 

    public override void OnUpdate(float deltaTime)
    {
    }

    public override void OnFixUpdate(float deltaTime)
    {
    }

    public override void OnLateUpdate(float deltaTime)
    {
    }

    public abstract override void OnDispose();
}
