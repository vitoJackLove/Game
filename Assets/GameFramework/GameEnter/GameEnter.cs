﻿using System;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Contexts;
using Loxodon.Framework.Messaging;
using Loxodon.Framework.Services;
using Loxodon.Framework.Views;
using UnityEngine;
using Loxodon.Framework.Asynchronous;

/// <summary>
/// 游戏入口
/// </summary>
public partial class GameEnter : MonoBehaviour
{
    private ApplicationContext context;

    private BaseWorld world;
    
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
        
        world = new BaseWorld();
        
        world.InitWorld();

        world.OnStart();
    }

    public void Update()
    {
        world.OnUpdate(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        world.OnFixUpdate(Time.deltaTime);
    }

    public void OnDisable()
    {
        world.OnDispose();
    }

    private async void LoadGameStartWindow()
    {
        await new PreloadDataCtrl().LoadData();
        
        UI.OpenWindow<TestStartupWindow>(Constant.ResourcesPath.GetUiWindowPrefab("Startup"));
    }
}
