using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.ViewModels;
using UnityEngine;

/// <summary>
/// 宠物详细信息ViewModel
/// </summary>
public class PetDataViewModel : ViewModelBase
{
    /// <summary>
    /// 玩家所有的宠物信息
    /// </summary>
    private PetListData petListData;

    public PetListData Data => petListData;
    
    public PetDataViewModel(){}
    
    public PetDataViewModel(PetListData petListData)
    {
        this.petListData = petListData;
    }
}
