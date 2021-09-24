using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.ViewModels;
using UnityEngine;

/// <summary>
/// 大厅的ViewModel
/// </summary>
public class StateRoomViewModel : ViewModelBase
{
    /// <summary>
    /// 玩家信息 玩家不持有所有的宠物 只持有出战的宠物
    /// </summary>
    private RoleData roleData;

    /// <summary>
    /// 玩家所有的宠物信息
    /// </summary>
    private PetListData petListData;

    /// <summary>
    /// 展示宠物面板申请
    /// </summary>
    private InteractionRequest<PetDataViewModel> showPetDataWindowRequest;
    
    public StateRoomViewModel()
    {
        roleData = new RoleData();
        
        petListData = new PetListData();

        showPetDataWindowRequest = new InteractionRequest<PetDataViewModel>();
    }
    
    
    /// <summary>
    /// 展示人物面板
    /// </summary>
    public void ShowRoleDataWindow()
    {
       
    }

    /// <summary>
    /// 展示宠物面板
    /// </summary>
    public void ShowPetDataWindow()
    {
        PetDataViewModel petDataViewModel = new PetDataViewModel(petListData);

        showPetDataWindowRequest.Raise(petDataViewModel);
    }

    public IInteractionRequest ShowPetDataWindowRequest => showPetDataWindowRequest;

}
