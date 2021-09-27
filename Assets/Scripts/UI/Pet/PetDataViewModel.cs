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

    private bool isShowPetPropertyPanel;

    /// <summary>
    /// 选中的宠物
    /// </summary>
    private PetData selectPet;

    /// <summary>
    /// 选择的宠物ViewModel
    /// </summary>
    private SelectPetDataViewModel selectPetDataViewModel;
    
    public PetDataViewModel(){}
    
    public PetDataViewModel(PetListData petListData)
    {
        this.petListData = petListData;
        
        selectPetDataViewModel = new SelectPetDataViewModel();
        
        ShowPropertyPanel();
    }

    /// <summary>
    /// 左侧面板选择的宠物
    /// </summary>
    public void SetSelectPet(PetData selectPetData)
    {
        this.selectPet = selectPetData;
        
        selectPetDataViewModel.RefreshSelectPet(selectPet);
    }

    public void ShowPropertyPanel()
    {
        IsShowPetPropertyPanel = true;
    }

    public void ShowAptitudePanel()
    {
        IsShowPetPropertyPanel = false;
    }
    
    public bool IsShowPetPropertyPanel {
        get => this.isShowPetPropertyPanel;
        set => this.Set<bool> (ref this.isShowPetPropertyPanel, value, "IsShowPetPropertyPanel");
    } 
}
