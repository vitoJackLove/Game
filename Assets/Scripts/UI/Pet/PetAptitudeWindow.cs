using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 宠物资质面板
/// </summary>
public class PetAptitudeWindow : MonoBehaviour
{
    public Text aptitudeLevelText;

    public Text gradeText;

    public Text lifeTimeText;

    public Text aptitudeAttackText;
    
    public Text aptitudeWakanText;
    
    public Text aptitudeAttackDefenseText;
    
    public Text aptitudeHpText;
    
    public Text aptitudeSpeedText;
    
    public Text aptitudeGrowText;

    public Image aptitudeAttackSlider;
    
    public Image aptitudeWakanSlider;
    
    public Image aptitudeAttackDefenseSlider;
    
    public Image aptitudeHpSlider;
    
    public Image aptitudeSpeedSlider;
    
    public void Init(SelectPetDataViewModel viewModel)
    {
        BindingSet<PetAptitudeWindow, SelectPetDataViewModel> bindingSet = this.CreateBindingSet(viewModel);
        
        bindingSet.Bind(this.aptitudeLevelText).For(v => v.text).To("天资");
        
        bindingSet.Bind(this.gradeText).For(v => v.text).ToExpression(vm=>$"绝品({vm.Data.PropertyData.AptitudeGrade})");
        
        bindingSet.Bind(this.lifeTimeText).For(v => v.text).To(vm=>vm.Data.PropertyData.LifeTime);
        
        bindingSet.Bind(this.aptitudeAttackText).For(v => v.text).To(vm=>vm.Data.PropertyData.AptitudeAttack);
        
        bindingSet.Bind(this.aptitudeWakanText).For(v => v.text).To(vm=>vm.Data.PropertyData.AptitudeWakan);
        
        bindingSet.Bind(this.aptitudeAttackDefenseText).For(v => v.text).To(vm=>vm.Data.PropertyData.AptitudeAttackDefense);
        
        bindingSet.Bind(this.aptitudeHpText).For(v => v.text).To(vm=>vm.Data.PropertyData.AptitudeHp);
        
        bindingSet.Bind(this.aptitudeSpeedText).For(v => v.text).To(vm=>vm.Data.PropertyData.AptitudeSpeed);
        
        bindingSet.Bind(this.aptitudeGrowText).For(v => v.text).To(vm=>vm.Data.PropertyData.AptitudeGrow);
        
        bindingSet.Bind(this.aptitudeAttackSlider).For(v => v.fillAmount).ToExpression(vm=>vm.Data.PropertyData.AttackPercent);
        
        bindingSet.Bind(this.aptitudeWakanSlider).For(v => v.fillAmount).ToExpression(vm=>vm.Data.PropertyData.WakanPercent);
        
        bindingSet.Bind(this.aptitudeAttackDefenseSlider).For(v => v.fillAmount).ToExpression(vm=>vm.Data.PropertyData.DefensePercent);

        bindingSet.Bind(this.aptitudeHpSlider).For(v => v.fillAmount).ToExpression(vm=>vm.Data.PropertyData.HpPercent);
        
        bindingSet.Bind(this.aptitudeSpeedSlider).For(v => v.fillAmount).ToExpression(vm=>vm.Data.PropertyData.SpeedPercent);

        
        bindingSet.Build();
    }
}
