using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Loxodon.Framework.Asynchronous;

/// <summary>
/// 资源加载组件
/// </summary>
public class ResourcesComponent : GameFrameworkComponent
{
    public override void OnInit()
    {
        /*ResourcesManager.Instance.Init(this);
        ObjectManager.Instance.Init(m_RecycleObjPool,m_DontDesObjPool);*/
    }

    public T LoadAsset<T>(string assetPath) where T : Object
    {
        return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        //return ResourcesManager.Instance.LoadResource<T>(assetPath);
    }
    
    public async Task<T> LoadAssetAsync<T>(string assetName) where T : Object
    {
        ResourceRequest request  = Resources.LoadAsync<T>(assetName);

        await request;
        
        return (T)request.asset;
    }

    public override void OnShutDown()
    {
        
    }
}
