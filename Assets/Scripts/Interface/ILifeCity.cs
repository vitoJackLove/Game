using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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
public interface ILifeCycle : IEnter
{
    /// <summary>
    /// 初始化
    /// </summary>
    void OnInit(object obj = null);

    /// <summary>
    /// 开始
    /// </summary>
    void OnStart(object obj = null);

   
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="deltaTime"></param>
    void OnUpdate(float deltaTime);

    /// <summary>
    /// 固定更新
    /// </summary>
    /// <param name="deltaTime"></param>
    void OnFixUpdate(float deltaTime);

    void OnLateUpdate(float deltaTime);

    /// <summary>
    /// 销毁
    /// </summary>
    void OnDispose();
}
