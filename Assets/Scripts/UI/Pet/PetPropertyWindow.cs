using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 宠物属性窗口
/// </summary>
public class PetPropertyWindow : MonoBehaviour
{
    public Text configNameText;
    
    public Text levelTxt;

    public Text expText;

    public Text hpdText;

    public Text magicText;
    
    public Text wakanText; 
    
    public Text attackText;
    
    public Text wakanDefenseText;
    
    public Text attackDefenseText;
    
    public Text speedText;
    
    public Text powerText;
    
    public Text wisdomText;
    
    public Text strengthText;
    
    public Text enduranceText;
    
    public Text quickText;

    public void Init(SelectPetDataViewModel viewModel)
    {
        BindingSet<PetPropertyWindow, SelectPetDataViewModel> bindingSet = this.CreateBindingSet(viewModel);

        bindingSet.Bind(this.configNameText).For(v => v.text).To(vm => vm.Data.ConfigData.Name);
        
        bindingSet.Bind(this.levelTxt).For(v => v.text).To(vm => vm.Data.PropertyData.Level);
        
        bindingSet.Bind(this.expText).For(v => v.text).ToExpression(vm => $"{vm.Data.PropertyData.CurrentExperience}/{vm.Data.PropertyData.ExperienceMax}");
        
        bindingSet.Bind(this.hpdText).For(v => v.text).ToExpression(vm => $"{vm.Data.PropertyData.CurrentHp}/{vm.Data.PropertyData.HpMax}");
        
        bindingSet.Bind(this.magicText).For(v => v.text).ToExpression(vm => $"{vm.Data.PropertyData.CurrentMagic}/{vm.Data.PropertyData.MagicMax}");
        
        bindingSet.Bind(this.wakanText).For(v => v.text).To(vm => vm.Data.PropertyData.Wakan);
        
        bindingSet.Bind(this.attackText).For(v => v.text).To(vm => vm.Data.PropertyData.Attack);
        
        bindingSet.Bind(this.wakanDefenseText).For(v => v.text).To(vm => vm.Data.PropertyData.WakanDefense);
        
        bindingSet.Bind(this.attackDefenseText).For(v => v.text).To(vm => vm.Data.PropertyData.AttackDefense);
        
        bindingSet.Bind(this.speedText).For(v => v.text).To(vm => vm.Data.PropertyData.Speed);
        
        bindingSet.Bind(this.powerText).For(v => v.text).To(vm => vm.Data.PropertyData.Power);
        
        bindingSet.Bind(this.wisdomText).For(v => v.text).To(vm => vm.Data.PropertyData.Wisdom);
        
        bindingSet.Bind(this.strengthText).For(v => v.text).To(vm => vm.Data.PropertyData.Strength);
        
        bindingSet.Bind(this.enduranceText).For(v => v.text).To(vm => vm.Data.PropertyData.Endurance);
        
        bindingSet.Bind(this.quickText).For(v => v.text).To(vm => vm.Data.PropertyData.Quick);
        
        bindingSet.Build();

    }
}
