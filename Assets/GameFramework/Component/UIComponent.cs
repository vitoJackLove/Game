using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loxodon.Framework.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI组件
/// </summary>
public class UIComponent : GameFrameworkComponent
{
    [SerializeField]
        private Transform mInstanceRoot = null;

        [SerializeField]
        private Camera uiCamera;
        
        private const string DefaultGroup = "DEFAULT";
        private const string POPUPGroup = "POPUP";
        private const string PROGRESSGroup = "PROGRESS";
        
        private GlobalWindowManager globalWindowManager;
        
        private Dictionary<string, WindowContainer> uiGroups = new Dictionary<string, WindowContainer>();

        public CanvasScaler Scaler => mInstanceRoot.GetComponent<CanvasScaler>();

        public Camera UICamera => uiCamera;

        public RectTransform RootTransform => mInstanceRoot.GetComponent<RectTransform>();
        
        public override void OnInit()
        {
            if (mInstanceRoot == null)
            {
                mInstanceRoot = (new GameObject("UI Form Instances")).transform;
                mInstanceRoot.SetParent(gameObject.transform);
                mInstanceRoot.localScale = Vector3.one;
            }

            mInstanceRoot.gameObject.layer = LayerMask.NameToLayer("UI");

            globalWindowManager = mInstanceRoot.GetComponent<GlobalWindowManager>();

            if (globalWindowManager == null)
            {
                globalWindowManager = mInstanceRoot.gameObject.AddComponent<GlobalWindowManager>();
            }

            this.AddUIGroup(DefaultGroup);
            this.AddUIGroup(POPUPGroup);
            this.AddUIGroup(PROGRESSGroup);
        }

   

        /// <summary>
        /// 是否存在界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否存在界面组。</returns>
        public bool HasUIGroup(string uiGroupName)
        {
            if (uiGroups.ContainsKey(uiGroupName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否增加界面组成功。</returns>
        public bool AddUIGroup(string uiGroupName)
        {
            if (this.HasUIGroup(uiGroupName))
            {
                return false;
            }
            
            this.uiGroups.Add(uiGroupName , WindowContainer.Create(uiGroupName));

            return true;
        }
        
        /// <summary>
        /// 获取界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>要获取的界面组。</returns>
        public WindowContainer GetUIGroup(string uiGroupName = DefaultGroup)
        {
            this.uiGroups.TryGetValue(uiGroupName, out var uiGroup);

            return uiGroup;
        }
        
        /// <summary>
        /// 加载界面。
        /// </summary>
        /// <param name="uiViewAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public T LoadWindow<T>(string uiViewAssetName) where T : IWindow
        {
            return this.LoadWindow<T>(globalWindowManager,uiViewAssetName);
        }

        /// <summary>
        /// 加载界面。
        /// </summary>
        /// <param name="windowManager">窗口</param>
        /// <param name="uiViewAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public T LoadWindow<T>(IWindowManager windowManager, string uiViewAssetName) where T : IWindow
        {
            if (windowManager == null)
                windowManager = this.globalWindowManager;

            T target = this.DoLoadView<T>(uiViewAssetName);
            if (target != null)
                target.WindowManager = windowManager;

            return target;
        }
        
        /// <summary>
        /// 加载界面。
        /// </summary>
        /// <param name="uiViewAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public T LoadView<T>(string uiViewAssetName) where T : IView
        {
            return DoLoadView<T>(uiViewAssetName);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public T OpenWindow<T>(string uiFormAssetName) where T : IWindow
        {
            return this.OpenWindow<T>(uiFormAssetName, DefaultGroup , null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="userData">玩家数据</param>
        /// <returns>界面</returns>
        public T OpenWindow<T>(string uiFormAssetName , object userData) where T : IWindow
        {
            IBundle bundle = new Bundle();
            bundle.Put(Constant.UiConstant.WINDOW_DATA , userData);
            return this.OpenWindow<T>(uiFormAssetName, DefaultGroup , bundle);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="data">界面数据</param>
        /// <returns>界面</returns>
        public T OpenWindow<T>(string uiFormAssetName , IBundle data) where T : IWindow
        {
            return this.OpenWindow<T>(uiFormAssetName, DefaultGroup , data);
        }
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称</param>
        /// <param name="data">界面数据</param>
        /// <returns>界面</returns>
        public T OpenWindow<T>(string uiFormAssetName , string uiGroupName , IBundle data) where T : IWindow
        {
            T window = LoadWindow<T>(this.GetUIGroup(uiGroupName) , uiFormAssetName);

            if (window == null)
            {
                Debuger.LogError($"load ui window error.  window : {typeof(T)}");
            }
            
            window.Create(data);
            window.Show();
            return window;
        }
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenUIWindow<T>(string uiFormAssetName) where T : IWindow
        {
            return await this.OpenUIWindow<T>(uiFormAssetName, DefaultGroup , null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="userData"></param>
        /// <returns>界面</returns>
        public async Task<T> OpenUIWindow<T>(string uiFormAssetName , object userData) where T : IWindow
        {
            IBundle bundle = new Bundle();
            bundle.Put(Constant.UiConstant.WINDOW_DATA , userData);
            return await this.OpenUIWindow<T>(uiFormAssetName, DefaultGroup , bundle);
        }
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenUIWindow<T>(string uiFormAssetName , object userData, object parentWindow) where T : IWindow
        {
            IBundle bundle = new Bundle();
            bundle.Put(Constant.UiConstant.WINDOW_DATA , userData);
            bundle.Put(Constant.UiConstant.PARENT_WINDOW , parentWindow);
            return await this.OpenUIWindow<T>(uiFormAssetName, DefaultGroup , bundle);
        } 
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenPopupWindow<T>(string uiFormAssetName) where T : IWindow
        {
            return await this.OpenPopupWindow<T>(uiFormAssetName , null);
        }
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenPopupWindow<T>(string uiFormAssetName  , object userData) where T : IWindow
        {
            IBundle bundle = new Bundle();

            if (userData != null)
            {
                bundle.Put(Constant.UiConstant.WINDOW_DATA , userData);
            }
           
            return await this.OpenUIWindow<T>(uiFormAssetName, POPUPGroup , bundle);
        }
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenProgressWindow<T>(string uiFormAssetName) where T : IWindow
        {
            return await this.OpenProgressWindow<T>(uiFormAssetName , null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="userData"></param>
        /// <returns>界面</returns>
        public async Task<T> OpenProgressWindow<T>(string uiFormAssetName  , object userData) where T : IWindow
        {
            IBundle bundle = new Bundle();

            if (userData != null)
            {
                bundle.Put(Constant.UiConstant.WINDOW_DATA , userData);
            }
           
            return await this.OpenUIWindow<T>(uiFormAssetName, PROGRESSGroup , bundle);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenView<T>(string uiFormAssetName) where T : IUIView
        {
            return await this.OpenUIView<T>(uiFormAssetName);
        } 
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenView<T>(string uiFormAssetName,Transform parentTransform) where T : IUIView
        {
            return await this.OpenUIView<T>(uiFormAssetName,parentTransform);
        } 
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="data">界面数据</param>
        /// <returns>界面</returns>
        public async Task<T> OpenUIWindow<T>(string uiFormAssetName , IBundle data) where T : IWindow
        {
            return await this.OpenUIWindow<T>(uiFormAssetName, DefaultGroup , data);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称</param>
        /// <param name="data">界面数据</param>
        /// <returns>界面</returns>
        public async Task<T> OpenUIWindow<T>(string uiFormAssetName , string uiGroupName , IBundle data) where T : IWindow
        {
            T window = await LoadWindowAsync<T>(this.GetUIGroup(uiGroupName) , uiFormAssetName);

            if (window == null)
            {
                Debuger.LogError($"load ui window error.  window : {typeof(T)}");
            }
            
            window.Create(data);
            window.Show();
            return window;
        }
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenUIView<T>(string uiFormAssetName) where T : IView
        {
            T view = await LoadViewAsync<T>(uiFormAssetName);

            if (view == null)
            {
                Debuger.LogError($"load ui view error.  view : {typeof(T)}");
            }
            
            return view;
        }
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面</returns>
        public async Task<T> OpenUIView<T>(string uiFormAssetName,Transform parentTransform) where T : IView
        {
            T view = await LoadViewAsync<T>(uiFormAssetName,parentTransform);

            if (view == null)
            {
                Debuger.LogError($"load ui view error.  view : {typeof(T)}");
            }
            
            return view;
        }
        
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        private async Task<T> LoadWindowAsync<T>(string uiFormAssetName) where T : IWindow
        {
            return await LoadWindowAsync<T>(null, uiFormAssetName);
        }

        private async Task<T> LoadWindowAsync<T>(IWindowManager windowManager, string uiFormAssetName) where T : IWindow
        {
            if (windowManager == null)
                windowManager = this.globalWindowManager;

            T target = await this.DoLoadWindowAsync<T>(uiFormAssetName);
            if (target != null)
                target.WindowManager = windowManager;

            return target;
        }

        private async Task<T> LoadViewAsync<T>(string uiFormAssetName) where T : IView
        {
            T target = await this.DoLoadViewAsync<T>(uiFormAssetName);
            return target;
        }
        
        private async Task<T> LoadViewAsync<T>(string uiFormAssetName,Transform parentTransform) where T : IView
        {
            T target = await this.DoLoadViewAsync<T>(uiFormAssetName,parentTransform);
            return target;
        }

        private async Task<T> DoLoadViewAsync<T>(string uiFormAssetName) where T : IView
        {
            GameObject viewTemplateGo = await GameEnter.Resources.LoadAssetAsync<GameObject>(uiFormAssetName);

            if (viewTemplateGo == null)
            {
                Debuger.LogError($"load ui window object error. assetName : {uiFormAssetName}.");
            }

            GameObject go = Instantiate(viewTemplateGo,mInstanceRoot);
            go.name = viewTemplateGo.name;
            go.SetActive(false);
            T view = go.GetComponent<T>();
            if (view == null && go != null)
                Destroy(go);
            return view;
        }
        
        private async Task<T> DoLoadViewAsync<T>(string uiFormAssetName,Transform parentTransform) where T : IView
        {
            GameObject viewTemplateGo = await GameEnter.Resources.LoadAssetAsync<GameObject>(uiFormAssetName);

            if (viewTemplateGo == null)
            {
                Debuger.LogError($"load ui window object error. assetName : {uiFormAssetName}.");
            }

            GameObject go = Instantiate(viewTemplateGo,parentTransform);
            go.name = viewTemplateGo.name;
            go.SetActive(false);
            T view = go.GetComponent<T>();
            if (view == null && go != null)
                Destroy(go);
            return view;
        }
        
        private async Task<T> DoLoadWindowAsync<T>(string uiFormAssetName) where T : IWindow
        {
            GameObject viewTemplateGo = await GameEnter.Resources.LoadAssetAsync<GameObject>(uiFormAssetName);

            if (viewTemplateGo == null)
            {
               Debuger.LogError($"load ui window object error. assetName : {uiFormAssetName}.");
            }

            GameObject go = Instantiate(viewTemplateGo,mInstanceRoot);
            go.name = viewTemplateGo.name;
            T view = go.GetComponent<T>();
            if (view == null && go != null)
                Destroy(go);
            return view;
        }
        
        private T DoLoadView<T>(string uiFormAssetName)
        {
            GameObject viewTemplateGo = GameEnter.Resources.LoadAsset<GameObject>(uiFormAssetName);

            if (viewTemplateGo == null)
            {
                Debuger.LogError($"load ui view object error. assetName : {uiFormAssetName}.");
            }

            GameObject go = Instantiate(viewTemplateGo);
            go.name = viewTemplateGo.name;
            T view = go.GetComponent<T>();
            if (view == null && go != null)
                Destroy(go);
            return view;
        }
        
        public override void OnShutDown()
        {
            
        }
}