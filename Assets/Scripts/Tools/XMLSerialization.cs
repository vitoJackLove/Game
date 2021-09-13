using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// XML序列换 反序列化
/// </summary>
public class XMLSerialization
{
	/// <summary>
	/// 序列化到本地
	/// </summary>
	/// <param name="path">序列化到本地的路径</param>
	/// <param name="obj">序列化的实例</param>
	/// <typeparam name="T">序列化的类型</typeparam>
	public static void Serialize<T>(string path, object obj) where T :class
	{
		using (FileStream stream =new FileStream(path,FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite))
		{
			using (StreamWriter sw = new StreamWriter(stream,Encoding.UTF8))
			{
				XmlSerializer xml = new XmlSerializer(typeof(T));
				xml.Serialize(sw,obj);
			}
		}
	}

	/// <summary>
	/// 序列化到本地
	/// </summary>
	/// <param name="path">序列化到本地的路径</param>
	/// <param name="obj">序列化的实例</param>
	/// <typeparam name="T">序列化的类型</typeparam>
	public static void Serialize(string path, object obj)
	{
		using (FileStream stream = new FileStream(path, FileMode.Create))
		{
			using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
			{
				XmlSerializer xml = new XmlSerializer(obj.GetType());
				xml.Serialize(sw, obj);
			}
		}
	}

	
	/// <summary>
	/// 从本地文本反序列化
	/// </summary>
	/// <param name="path">路径</param>
	/// <typeparam name="T">反序列的类型</typeparam>
	/// <returns></returns>
	public static System.Object DeserializeByPath(string path, Type type)
	{
		System.Object obj = null;
		try
		{
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				XmlSerializer xs = new XmlSerializer(type);
				obj = xs.Deserialize(fs);
			}
		}
		catch (Exception e)
		{
			Debug.LogError("此xml无法转成二进制: " + path + "," + e);
		}
		return obj;
	}

	/// <summary>
	/// 反序列化内容
	/// </summary>
	/// <param name="content">要反序列化的字段</param>
	/// <typeparam name="T">反序列化的内容</typeparam>
	/// <returns></returns>
	public static T Deserialize<T>(string content) where T :class
	{
		using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
		{
			using (XmlReader xr = XmlReader.Create(stream))
			{
				XmlSerializer xml =new XmlSerializer(typeof(T));
				return xml.Deserialize(xr) as T;
			}
		}
	}
}
