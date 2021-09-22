using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Views;
using UnityEngine;
using UnityEngine.UI;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Interactivity;

public class TestStartupWindow : Window
{
    public Text progressBarText;

    public Slider slider;

    public Button button;

    public GameObject go;

    protected override void OnCreate(IBundle bundle)
    {
        TestStartupViewModel startupViewModel = new TestStartupViewModel();

        BindingSet<TestStartupWindow, TestStartupViewModel> bindingSet = this.CreateBindingSet(startupViewModel);

        bindingSet.Bind(this.slider).For(v => v.value).To(vm => vm.Progress.Progressbar).OneWay();
        
        //第二种方法绑定
        /*bindingSet.Bind(this.slider).For("value", "onValueChanged").To("Progress.Progressbar").TwoWay();*/

        bindingSet.Bind(this.button).For(v => v.onClick).To(vm => vm.OnButtonClick);

        //这里是一个Lamda表达式
        bindingSet.Bind(this.progressBarText).For(v => v.text).ToExpression(vm => $"{Mathf.FloorToInt(vm.Progress.Progressbar * 100f)}%").OneWay();

        bindingSet.Bind(this.go).For(v => v.activeSelf).To(vm => vm.Progress.Enable);

        bindingSet.Bind().For(v => v.OnLoginRequest).To(vm => vm.LoginRequest);

        bindingSet.Build();

        startupViewModel.Zip();
    }

    private void OnLoginRequest(object sender, InteractionEventArgs args)
    {
        GameEnter.UI.OpenWindow<TestLoginWindow>(Constant.ResourcesPath.GetUiPrefab("Login"));
    }
}