using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 世界基类
/// </summary>
public class BaseWorld
{
    private Dictionary<Type, BaseSystem> systemDic; 
    
     public void InitWorld()
     {
         systemDic = new Dictionary<Type, BaseSystem>();
         InitSystem();
     }

     private void InitSystem()
     {
         Type [] types = GameEnter.GetSystemTypes();

         foreach (var type in types)
         {
             object t = Activator.CreateInstance(type);
             
             BaseSystem system = (BaseSystem)t;
             
             system.OnInit(this);
             
             systemDic.Add(type, system);
         }
     }

     public void OnStart(object obj = null)
     {
         foreach (var system in systemDic.Values)
         {
             system.OnStart();
         }
     }

     public void OnUpdate(float deltaTime)
     {
         foreach (var system in systemDic.Values)
         {
             system.OnUpdate(deltaTime);
         }
     }

     public void OnFixUpdate(float deltaTime)
     {
         foreach (var system in systemDic.Values)
         {
             system.OnFixUpdate(deltaTime);
         }
     }

     public void OnDispose()
     {
         foreach (var system in systemDic.Values)
         {
             system.OnDispose();
         }
     }

     public T GetSystem<T>() where T : BaseSystem
     {
         return (T)systemDic[typeof(T)];
     }
}
