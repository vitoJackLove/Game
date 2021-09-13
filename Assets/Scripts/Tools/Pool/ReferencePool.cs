using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 清理
/// </summary>
public interface IReference
{
     void Clear();
}

/// <summary>
/// 引用池
/// </summary>
public static class ReferencePool
{
     public static int ReferenceMaxCount => 100;
     
     private static readonly Dictionary<Type, ReferenceCollection> ReferenceCollectionDic = new Dictionary<Type, ReferenceCollection>();

     /// <summary>
     /// 获取
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <returns></returns>
     public static T Acquire<T>()  where T : class, IReference, new()
     {
          if (ReferenceCollectionDic.ContainsKey(typeof(T)))
          {
               return ReferenceCollectionDic[typeof(T)].Acquire<T>();
          }

          ReferenceCollection collection = new ReferenceCollection(typeof(T));
          ReferenceCollectionDic.Add(typeof(T),collection);
          return collection.Acquire<T>();
     }

     /// <summary>
     /// 移除
     /// </summary>
     /// <param name="reference"></param>
     public static void Release(IReference reference)
     {
          if (ReferenceCollectionDic.TryGetValue(reference.GetType(),out var referenceCollection))
          {
               referenceCollection.Release(reference);
          }
     }
}

/// <summary>
/// 引用类控制器
/// </summary>
public sealed class ReferenceCollection
{
     public ReferenceCollection(Type t)
     {
          _type = t;
          _currCount = 0;
          this._maxCount = ReferencePool.ReferenceMaxCount;
          _references = new Queue<IReference>(_maxCount);
     }
     
     public ReferenceCollection(int maxCount,Type t)
     {
          _type = t;
          _currCount = 0;
          this._maxCount = maxCount;
          _references = new Queue<IReference>(_maxCount);
     }

     /// <summary>
     /// 最大引用数量
     /// </summary>
     private int _maxCount;

     /// <summary>
     /// 当前引用数量
     /// </summary>
     private int _currCount;

     /// <summary>
     /// 类型
     /// </summary>
     private Type _type;

     /// <summary>
     /// 引用对象
     /// </summary>
     private Queue<IReference> _references;

     /// <summary>
     /// 获取
     /// </summary>
     /// <typeparam name="T"></typeparam>
     public T Acquire<T>() where T : class , IReference,new ()
     {
          T t = null;
          if (typeof(T) != _type)
          {
               Debuger.Log($"创建非法对象{typeof(T)}!");
               return null;
          }

          t = (T)_references.Dequeue();

          if ( t != null)
          {
               return t;
          }

          if (_currCount >= _maxCount)
          {
               Debuger.LogWarning($"创建对象{typeof(T)}超出引用池,!");
          }

          _currCount++;

          return new T();
     }


     /// <summary>
     /// 回收
     /// </summary>
     /// <param name="reference"></param>
     public void Release(IReference reference)
     {
          if (reference == null)
          {
               return;
          }
          
          if (reference.GetType() != _type)
          {
               Debuger.LogError($"引用回收错误,类型不匹配{reference.GetType()}  :  {_type}");
               reference.Clear();
          }
          
          _references.Enqueue(reference);

          _currCount--;
     }
}
