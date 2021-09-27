using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.ViewModels;
using UnityEngine;

/// <summary>
/// 宠物属性面板ViewModel
/// </summary>
public class SelectPetDataViewModel : ViewModelBase
{
    private PetData data;

    /// <summary>
    /// 刷新选择的宠物
    /// </summary>
    /// <param name="data">选择的宠物</param>
    public void RefreshSelectPet(PetData data)
    {
        this.Data = data;
    }
    
    public PetData Data {
        get => this.data;
        set => this.Set<PetData> (ref this.data, value, "Data");
    } 
}
