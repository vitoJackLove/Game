using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏通用组件基类
/// </summary>
public abstract class GameFrameworkComponent : MonoBehaviour
{
     protected virtual void Awake()
     {
          GameEntryBase.Register(this);
     }

     public abstract void OnInit();

     public abstract void OnShutDown();
}
