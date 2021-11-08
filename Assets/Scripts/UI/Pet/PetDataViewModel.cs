using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Interactivity;
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
    public PetListData Data { get; }

    private bool isShowPetPropertyPanel;

    /// <summary>
    /// 选中的宠物
    /// </summary>
    private PetData selectPet;

    /// <summary>
    /// 选择的宠物ViewModel
    /// </summary>
    private SelectPetDataViewModel selectPetDataViewModel;

    public SelectPetDataViewModel SelectPetDataViewModel => selectPetDataViewModel;
    
    /// <summary>
    /// 展示宠物洗练面板
    /// </summary>
    private InteractionRequest showPetWashRequest;

    /// <summary>
    /// 展示宠物学习面板
    /// </summary>
    private InteractionRequest showPetStudyRequest;

    public PetDataViewModel(){}
    
    public PetDataViewModel(PetListData petListData)
    {
        this.Data = petListData;
        
        selectPetDataViewModel = new SelectPetDataViewModel();

        showPetWashRequest = new InteractionRequest();

        showPetStudyRequest = new InteractionRequest();

        SetSelectPet(petListData.PetDataList?[0]);
        
        ShowPropertyPanel();
    }

    /// <summary>
    /// 左侧面板选择的宠物
    /// </summary>
    public void SetSelectPet(PetData selectPetData)
    {
        if (selectPetData == selectPet)
        {
            return;
        }
        
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

    /// <summary>
    /// 展示宝宝洗髓面板
    /// </summary>
    public void ShowPetWashPanel()
    {
        showPetWashRequest.Raise();
    }

    /// <summary>
    /// 展示宝宝学习面板
    /// </summary>
    public void ShowPetStudyPanel()
    {
        showPetStudyRequest.Raise();
    }
    
    public bool IsShowPetPropertyPanel {
        get => this.isShowPetPropertyPanel;
        set => this.Set<bool> (ref this.isShowPetPropertyPanel, value, "IsShowPetPropertyPanel");
    }

    public IInteractionRequest ShowPetStudyRequest => showPetStudyRequest;

    public IInteractionRequest ShowPetWashRequest => showPetWashRequest;
}
