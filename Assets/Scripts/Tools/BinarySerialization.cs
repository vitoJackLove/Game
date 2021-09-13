using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 二进制序列化 反序列化
/// </summary>
public class BinarySerialization
{
    /// <summary>
    /// 根据类型序列化
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    public static void Serialize<T>(string path,Object obj) where T : class
    {
        try
        {
            using (FileStream stream =new FileStream(path,FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream,obj);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }
    
    /// <summary>
    /// 序列化到本地
    /// </summary>
    /// <param name="path">路径</param>
    /// <typeparam name="T">序列化的类型</typeparam>
    public static void Serialize(string path,System.Object obj)
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("此类无法转换成二进制 " + obj.GetType() + "," + e);
        }
    }

    /// <summary>
    /// 本地文本反序列化
    /// </summary>
    /// <param name="path">路径</param>
    /// <typeparam name="T">反序列化类型</typeparam>
    /// <returns></returns>
    public static T DeserializeByPath<T>(string path) where  T :class
    {
        using (FileStream stream =new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read))
        {
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream) as T;
        }
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="content">反序列化的内容</param>
    /// <typeparam name="T">类型</typeparam>
    /// <returns></returns>
    public static T Deserialize<T>(string content) where T : class
    {
        BinaryFormatter bf = new BinaryFormatter();  
        using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
        {
            return bf.Deserialize(xmlStream) as T;
        }
    }
}
