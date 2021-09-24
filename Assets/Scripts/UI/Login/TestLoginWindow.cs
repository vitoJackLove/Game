using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Views;
using UnityEngine;
using UnityEngine.UI;

public class TestLoginWindow : Window
{
    public InputField account;

    public InputField possWord;

    public Button affirm;
    
    public Button cancel;
    
    protected override void OnCreate(IBundle bundle)
    {
        //这个bundle 可以接受上个页面的传的参
        //bundle.Get<T>("key");
        
        TestLoginViewModel viewModel  = new TestLoginViewModel();
        
        BindingSet<TestLoginWindow, TestLoginViewModel> bindingSet = this.CreateBindingSet(viewModel);

        bindingSet.Bind(this.account).For(v => v.text).To(vm => vm._Account.Name).TwoWay();
        
        bindingSet.Bind(this.possWord).For(v => v.text).To(vm => vm._Account.PossWord).TwoWay();

        bindingSet.Bind(this.affirm).For(v => v.onClick).To(vm => vm.OnAffirmClick);
        
        bindingSet.Bind(this.cancel).For(v => v.onClick).To(vm => vm.Click).TwoWay();

        bindingSet.Bind().For(v => v.OnCancelRequest).To(vm => vm.CancelRequest);
        
        bindingSet.Bind().For(v => v.OnAffirmRequest).To(vm => vm.AffirmRequest);
        
        bindingSet.Build();
    }

    private void OnAffirmRequest(object sender, InteractionEventArgs e)
    {
        GameEnter.UI.OpenWindow<StateRoomWindow>(Constant.ResourcesPath.GetUiWindowPrefab("StateRoomWindow"),e.Context);
    }

    private void OnCancelRequest(object sender, InteractionEventArgs e)
    {
        DoDismiss();
    }
}
