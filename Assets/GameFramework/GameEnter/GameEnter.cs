﻿using System.Threading.Tasks;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Messaging;
using Loxodon.Framework.Services;
using Loxodon.Framework.Views;
using UnityEngine;

/// <summary>
/// 游戏入口
/// </summary>
public partial class GameEnter : MonoBehaviour
{
    private ApplicationContext context;
    public void Start()
    {
        GlobalWindowManager windowManager = FindObjectOfType<GlobalWindowManager>();
        if (windowManager == null)
            throw new NotFoundException("Not found the GlobalWindowManager.");
            
        context = Context.GetApplicationContext();

        IServiceContainer container = context.GetContainer();

        //初始化VM绑定服务
        BindingServiceBundle bundle = new BindingServiceBundle(container);
        bundle.Start();
            
        container.Register<IMessenger>(new Messenger());
        //container.Register<IRoleService>(new RoleService());
        
        InitGameFrameworkComponent();

        LoadGameStartWindow();
    }

    private void LoadGameStartWindow()
    {
        UI.OpenWindow<TestStartupWindow>("Assets/UI/Startup/Startup.prefab");
    }
}
