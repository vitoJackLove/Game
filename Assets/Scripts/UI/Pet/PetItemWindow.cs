using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 宠物信息面板Item窗口
/// </summary>
public class PetItemWindow : MonoBehaviour
{
    public Button selectBtn;

    public GameObject isBattleIcon;

    public TextMeshProUGUI level;

    public SpiritLoader spiritLoader;
    
    public void Init(PetItemViewModel itemViewModel)
    {
        BindingSet<PetItemWindow, PetItemViewModel> bindingSet = this.CreateBindingSet(itemViewModel);

        bindingSet.Bind(this.isBattleIcon).For(v => v.activeSelf).To(vm => vm.Data.PropertyData.IsBattle);
        
        bindingSet.Bind(this.level).For(v => v.text).To(vm => vm.Data.PropertyData.Level);
        
        bindingSet.Bind(this.selectBtn).For(v => v.onClick).To(vm => vm.ShowPetBtn);
        
        bindingSet.Bind(this.spiritLoader).For(v => v.SpiritPath).To(vm => vm.Data.ConfigData.AssetPath);
        
        bindingSet.Build();
    }
    
    public void SetItemData(PetData petData, int itemIndex, int row, int column)
    {
        
    }
}
