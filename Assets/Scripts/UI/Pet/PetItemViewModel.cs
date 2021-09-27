using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.ViewModels;
using UnityEngine;

/// <summary>
/// 宠物ItemViewModel
/// </summary>
public class PetItemViewModel : ViewModelBase
{
    private PetData data;

    public PetData Data => data;

    /// <summary>
    /// 宠物信息总面板
    /// </summary>
    private PetDataViewModel petDataViewModel;

    public PetItemViewModel(PetData petData, PetDataViewModel petDataViewModel)
    {
        data = petData;

        this.petDataViewModel = petDataViewModel;
    }

    public void ShowPetBtn()
    {
        petDataViewModel.SetSelectPet(Data);
    }
}
