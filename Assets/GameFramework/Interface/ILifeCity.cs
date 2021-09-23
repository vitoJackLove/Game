using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 初始化接口
/// </summary>
public interface IInit
{
    void OnInit(object obj = null);
}

/// <summary>
/// 开始接口
/// </summary>
public interface IStart
{
    void OnStart(object obj = null);
}

/// <summary>
/// 更新
/// </summary>
public interface IUpdate
{
    void OnUpdate(float deltaTime);
}

/// <summary>
/// 固定刷新接口
/// </summary>
public interface IFixUpdate
{
    void OnFixUpdate(float deltaTime);
}

public interface ILateUpdate
{
    void OnLateUpdate(float deltaTime);
}

/// <summary>
/// 销毁接口
/// </summary>
public interface IDispose
{
    void OnDispose();
}

/// <summary>
/// 进入接口
/// </summary>
public interface IEnter
{
    /// <summary>
    /// 在初始化后面调用
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    Task OnEnter(object obj = null);
}

/// <summary>
/// 生命周期
/// </summary>
public interface ILifeCycle :IInit, IEnter,IStart,IUpdate,IFixUpdate,ILateUpdate,IDispose
{
        
}

/// <summary>
/// 生命周期基类
/// </summary>
public abstract class BaseLifeCycle : ILifeCycle
{
    public virtual void OnInit(object obj = null) { }

    public async virtual Task OnEnter(object obj = null) { }

    public virtual void OnStart(object obj = null) { }

    public virtual void OnUpdate(float deltaTime) { }

    public virtual void OnFixUpdate(float deltaTime) { }

    public virtual void OnLateUpdate(float deltaTime) {}

    public virtual void OnDispose() { }
}
