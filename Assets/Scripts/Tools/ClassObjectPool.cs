using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 类对象池
/// </summary>
public class ClassObjectPool<T> where T : class,new()
{
    /// <summary>
    /// 池
    /// </summary>
    private Stack<T> m_Pool = new Stack<T>();
    /// <summary>
    /// 对象池的最大个数  
    /// </summary>
    private int m_MaxCount = 0;

    //没有回收的个数
    private int noRecycleCount = 0;

    public ClassObjectPool(int maxCount)
    {
        m_MaxCount = maxCount;
        for (int i = 0; i < m_MaxCount; i++)
        {
            m_Pool.Push(new T());
        }
    }

    /// <summary>
    /// 取出
    /// </summary>
    /// <param name="isForce">强制取出</param>
    /// <returns></returns>
    public T Spawn(bool isForce)
    {
        if (m_Pool.Count > 0)
        {
            return m_Pool.Pop();
        }
        else
        {
            if (isForce)
            {
                T t = new T();
                noRecycleCount++;
                return t;
            }
            return null;
        }
    }

    /// <summary>
    /// 回收
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public void Recycle(T obj)
    {
        noRecycleCount--;
        if (m_Pool.Count >= m_MaxCount && m_MaxCount <= 0)
        {
            obj = null;
            return;
        }
        m_Pool.Push(obj);
    }
}
