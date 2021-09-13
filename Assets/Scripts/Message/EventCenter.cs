using System;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter 
{
    static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType,Delegate>();

    /// <summary>
    /// 添加无参事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="callBack"></param>
    public static void AddEvent(EventType type,CallBack callBack) 
    {
        if (!m_EventTable.ContainsKey(type))
        {
            m_EventTable.Add(type,null);
        }
        Delegate d = m_EventTable[type];
        if (d != null && d.GetType() != callBack.GetType())
        {
            Debug.LogError("事件类型不匹配！！！");
        }
        m_EventTable[type] = (CallBack)m_EventTable[type] + callBack;
    }
 
    /// <summary>
    /// 移除事件
    /// </summary>
    public static void RemoveEvent(EventType type,CallBack callBack)
    {
        if (m_EventTable.ContainsKey(type))
        {
            Delegate d = null;   
            if (!m_EventTable.TryGetValue(type, out d))
            {
                Debug.LogError("移除的监听不存在!!!");
            }
            else
            {
                m_EventTable[type] =(CallBack) d - callBack;
            }
        }
        else
        {
            Debug.LogError("不存在事件码！！！");
        }
    }

    /// <summary>
    /// 广播事件
    /// </summary>
    public static void BrocaEvent(EventType type)
    {
        if (m_EventTable.ContainsKey(type))
        {
            Delegate d = null;
            if(m_EventTable.TryGetValue(type, out d))
            {
                CallBack callBack = (CallBack)d;
                callBack();
            }
            else
            {
                Debug.LogError("没有该事件！！！");
            }
        }
        else
        {
            Debug.LogError("没有该事件码！！！");
        }
    }
}
