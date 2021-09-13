using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏组件管理
/// </summary>
public static class GameEntryBase 
{
    /// <summary>
    /// 游戏通用组件
    /// </summary>
    private static Dictionary<Type,GameFrameworkComponent> _gameFrameworkComponents = new Dictionary<Type, GameFrameworkComponent>();

    public static void Register(GameFrameworkComponent component)
    {
        Type t = component.GetType();
        
        if (!_gameFrameworkComponents.ContainsKey(t))
        {
            _gameFrameworkComponents.Add(t,component);
        }
    }

    public static GameFrameworkComponent GetComponent<T>() where T : GameFrameworkComponent
    {
        _gameFrameworkComponents.TryGetValue(typeof(T), out var component);
        return component;
    }

    public static void ShutDown()
    {
        foreach (var component in _gameFrameworkComponents.Values)
        {
            component.OnShutDown();
        }
    }
}
