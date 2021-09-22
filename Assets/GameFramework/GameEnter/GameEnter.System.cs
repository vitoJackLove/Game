using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化系统类
/// </summary>
public partial class GameEnter : MonoBehaviour
{
    public static Type[] GetSystemTypes()
    {
        return new Type[]
        {
            //实体系统
            typeof(EntitySystem),
        };
    }
}
