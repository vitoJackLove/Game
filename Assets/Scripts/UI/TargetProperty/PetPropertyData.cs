using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public PetPropertyData(PetData data,int level)
    {
        this.Level = level;
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
}
