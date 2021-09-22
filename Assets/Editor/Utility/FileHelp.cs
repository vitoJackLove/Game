using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public class FileHelp
{
    /// <summary>
    /// 获取指定文件夹大小
    /// </summary>
    public static float GetFolderSize(string folderPath)
    {
        float size = 0;
        DirectoryInfo directoryInfo =new DirectoryInfo(folderPath);
        FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
        foreach (FileInfo info in files)
        {
            size += info.Length;
        }
        size = size / 1024.0f / 1024f;
        return size;
    }
    
    /// <summary>
    /// 删除指定文件目录下的所有文件
    /// </summary>
    /// <param name="fullPath"></param>
    /// <returns></returns>
    public static void DeleteAllFile(string fullPath)
    {
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo directory = new DirectoryInfo(fullPath);
            FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                File.Delete(files[i].FullName);
            }
        }
        else
        {
            Debug.LogError("删除错误,没有该文件夹");
        }
    }

    /// <summary>
    /// 获取指定文件夹的所有子文件夹
    /// </summary>
    public static void GetFolderChild(string fullPath, List<DirectoryInfo> dirNameList )
    {
        DirectoryInfo directoryInfo =new DirectoryInfo(fullPath);
        DirectoryInfo[] infos = directoryInfo.GetDirectories();
        if (infos.Length == 0)
        {
            dirNameList.Add(directoryInfo);
        }
        foreach (var info in infos)
        {
            GetFolderChild(info.FullName, dirNameList);
        }
    }

    /// <summary>
    /// 获取文件名
    /// </summary>
    /// <param name="path">文件路径</param>
    public static string GetFileNameByFilePath(string path)
    {
        string[] fullPath = path.Split('/');
        return fullPath[fullPath.Length - 1].ToLower().Split('.')[0];
    }

    /// <summary>
    /// 获取指定目录下的所有文件路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<string> GetDirAllFilePath(string path)
    {
        List<string> pathList = new List<string>();
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        FileInfo[] fileInfos =  directoryInfo.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < fileInfos.Length; i++)
        {
            if (!fileInfos[i].FullName.Contains(".meta"))
            {
                pathList.Add(fileInfos[i].FullName);
            }
        }
        return pathList;
    }

    /// <summary>
    /// 设置AB包的名字
    /// </summary>
    /// <param name="name"></param>
    /// <param name="path"></param>
    public static void SetABName(string path, string name)
    {
        AssetImporter assetImporter = AssetImporter.GetAtPath(path);
        if (assetImporter == null) 
        {
            Debug.LogError(string.Format("该路径没有资源!:{0},路径为{1}",name, path));
        }
        else
        {
            assetImporter.assetBundleName = name;
        }
    }

    /// <summary>
    /// 设置AB包的名字
    /// </summary>
    /// <param name="name"></param>
    /// <param name="path"></param>
    public static void SetABName(string name, List<string> paths)
    {
        foreach (var item in paths)
        {
            AssetImporter assetImporter = AssetImporter.GetAtPath(item);
            if (assetImporter == null)
            {
                Debug.LogError("该路径没有资源!");
            }
            else
            {
                assetImporter.assetBundleName = name;
            }
        }
    }

    /// <summary>
    /// 剔除多余路径 Asset/.....
    /// </summary>
    public static string RejectPath(string path,bool isAssetFileStart = true)
    {
        string dataPath = Application.dataPath;

        path = ModifyPathSymbol(path);

        if (!string.IsNullOrEmpty(path))
        {
            if (path.StartsWith(dataPath))
            {
                if (isAssetFileStart)
                {
                    return "Assets/" + path.Substring(dataPath.Length + 1);
                }
                else
                {
                    return path.Substring(dataPath.Length + 1);
                }
            }
            else
            {
                Debug.LogError(string.Format("不能在Assets目录之外!:{0}", path));
            }
        }
        return null;
    }
    
    /// <summary>
    /// 修改字符“\” "/"
    /// </summary>
    public static string ModifyPathSymbol(string path)
    {
        return path.Replace('\\','/');
    }

    /// <summary>
    /// 强制清除之前设置好的AB包名
    /// </summary>
    public static void ClearOldABName()
    {
        string[] oldABName = AssetDatabase.GetAllAssetBundleNames();
        foreach (var item in oldABName)
        {
            AssetDatabase.RemoveAssetBundleName(item, true);
        }
    }
    
    /// <summary>
    /// 检测路径是否包含
    /// </summary>
    /// <param name="path"></param>
    /// <param name="targetPathList"></param>
    public static string CheckIncludePath(string path,ref List<string> targetPathList)
    {
       return targetPathList.Find(a => a.StartsWith(path)); 
    }
}

#endif