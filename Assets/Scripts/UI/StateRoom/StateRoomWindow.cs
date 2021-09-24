using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.Views;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 大厅窗口
/// </summary>
public class StateRoomWindow : Window
{
    private StateRoomViewModel roomViewModel;

    /// <summary>
    /// 玩家信息
    /// </summary>
    public Button roleDataBtn;

    /// <summary>
    /// 宠物信息
    /// </summary>
    public Button petDataBtn;
    
    protected override void OnCreate(IBundle bundle)
    {
        roomViewModel = bundle.Get<StateRoomViewModel>(Constant.UiConstant.WINDOW_DATA);
        
        BindingSet<StateRoomWindow, StateRoomViewModel> bindingSet = this.CreateBindingSet(roomViewModel);
        
        bindingSet.Bind(this.roleDataBtn).For(v => v.onClick).To(vm => vm.ShowRoleDataWindow);
        
        bindingSet.Bind(this.petDataBtn).For(v => v.onClick).To(vm => vm.ShowPetDataWindow);
        
        bindingSet.Bind().For(v => v.OnShowPetWindow).To(vm => vm.ShowPetDataWindowRequest);
        
        bindingSet.Build();
    }

    private void OnShowPetWindow(object sender, InteractionEventArgs e)
    {
        GameEnter.UI.OpenWindow<PetDataWindow>(Constant.ResourcesPath.GetUiWindowPrefab("PetDataWindow"),e.Context);
        Dismiss();
    }
}
