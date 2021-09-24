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

    public PetItemViewModel(PetData petData)
    {
        data = petData;
    }

    public void ShowPetBtn()
    {
        
    }
}
