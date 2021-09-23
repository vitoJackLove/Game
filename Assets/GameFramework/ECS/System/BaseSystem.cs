using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 系统基类
/// </summary>
public abstract class BaseSystem : IInit,IStart,IUpdate,IFixUpdate,IDispose
{
    protected BaseWorld baseWorld;

    public virtual void OnInit(object obj = null)
    {
        baseWorld = (BaseWorld) obj;
    }

    public abstract void OnStart(object obj = null);

    public abstract void OnUpdate(float deltaTime);

    public abstract void OnFixUpdate(float deltaTime);

    public T GetSystem<T>() where T : BaseSystem
    {
        return baseWorld.GetSystem<T>();
    }

    public abstract void OnDispose();
}
