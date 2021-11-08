using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 宠物属性
/// </summary>
public class PetPropertyData : PropertyData
{
    /// <summary>
    /// 是否出战
    /// </summary>
    private bool isBattle;

    /// <summary>
    /// 寿命
    /// </summary>
    private int lifeTime;
    
    /// <summary>
    /// 资质评分
    /// </summary>
    private int aptitudeGrade;

    /// <summary>
    /// 宠物的实际资质
    /// </summary>
    private int aptitudeAttack;

    private int aptitudeWakan;

    private int aptitudeAttackDefense;

    private int aptitudeHp;

    private int aptitudeSpeed;

    private float aptitudeGrow;

    private float attackPercent;

    private float defensePercent;

    private float hpPercent;

    private float wakanPercent;

    private float speedPercent;
    
    public PetPropertyData(PetData data,int level)
    {
        this.Level = level;

        InitRandomPetAptitude(data);

        AttackPercent = (aptitudeAttack - data.ConfigData.AttackMin) * data.ConfigData.AttackPercent;
        
        DefensePercent = (aptitudeAttackDefense - data.ConfigData.DefenseMin) * data.ConfigData.DefensePercent;
        
        HpPercent = (aptitudeHp - data.ConfigData.HPMin) * data.ConfigData.HpPercent;
        
        WakanPercent = (aptitudeWakan - data.ConfigData.WakanMin) * data.ConfigData.WakanPercent;
        
        SpeedPercent = (aptitudeSpeed - data.ConfigData.SpeedMin) * data.ConfigData.SpeedPercent;
    }

    private void InitRandomPetAptitude(PetData data)
    {
        //初始化随机 属性
        AptitudeAttack = Random.Range(data.ConfigData.AttackMin, data.ConfigData.AttackMax);
        
        AptitudeWakan = Random.Range(data.ConfigData.WakanMin
            , data.ConfigData.WakanMax);
        
        AptitudeHp = Random.Range(data.ConfigData.HPMin
            , data.ConfigData.HPMax);
        
        AptitudeSpeed = Random.Range(data.ConfigData.SpeedMin
            , data.ConfigData.SpeedMax);
        
        AptitudeGrow = Random.Range(data.ConfigData.GrowMin
            , data.ConfigData.GrowMax);
        
        AptitudeAttackDefense = Random.Range(data.ConfigData.DefenseMin
            , data.ConfigData.DefenseMax);
    }

    public bool IsBattle {
        get => this.isBattle;
        set => this.Set<bool> (ref this.isBattle, value, "IsBattle");
    } 
    
    public int LifeTime
    {
        get => this.lifeTime;
        set => this.Set<int>(ref this.lifeTime, value, "LifeTime");
    }
    
    public int AptitudeGrade
    {
        get => this.aptitudeGrade;
        set => this.Set<int>(ref this.aptitudeGrade, value, "AptitudeGrade");
    }
    
    public int AptitudeAttack
    {
        get => this.aptitudeAttack;
        set => this.Set<int>(ref this.aptitudeAttack, value, "AptitudeAttack");
    }
    
    public int AptitudeWakan
    {
        get => this.aptitudeWakan;
        set => this.Set<int>(ref this.aptitudeWakan, value, "AptitudeWakan");
    }
    
    public int AptitudeAttackDefense
    {
        get => this.aptitudeAttackDefense;
        set => this.Set<int>(ref this.aptitudeAttackDefense, value, "AptitudeAttackDefense");
    }
    
    public int AptitudeHp
    {
        get => this.aptitudeHp;
        set => this.Set<int>(ref this.aptitudeHp, value, "AptitudeHp");
    }
    
    public int AptitudeSpeed
    {
        get => this.aptitudeSpeed;
        set => this.Set<int>(ref this.aptitudeSpeed, value, "AptitudeSpeed");
    }
    
    public float AptitudeGrow
    {
        get => this.aptitudeGrow;
        set => this.Set<float>(ref this.aptitudeGrow, value, "AptitudeGrow");
    }
    
    public float AttackPercent
    {
        get => this.attackPercent;
        set => this.Set<float>(ref this.attackPercent, value, "AttackPercent");
    }
    
    public float DefensePercent
    {
        get => this.defensePercent;
        set => this.Set<float>(ref this.defensePercent, value, "DefensePercent");
    }
    
    public float HpPercent
    {
        get => this.hpPercent;
        set => this.Set<float>(ref this.hpPercent, value, "HpPercent");
    }
    
    public float WakanPercent
    {
        get => this.wakanPercent;
        set => this.Set<float>(ref this.wakanPercent, value, "WakanPercent");
    }
    
    public float SpeedPercent
    {
        get => this.speedPercent;
        set => this.Set<float>(ref this.speedPercent, value, "SpeedPercent");
    }
}
