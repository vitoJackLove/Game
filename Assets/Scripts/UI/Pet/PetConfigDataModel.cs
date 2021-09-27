using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

/// <summary>
/// 宠物配置属性
/// </summary>
public class PetConfigData : ModelBase
{
    /// <summary>
    /// 名字
    /// </summary>
    private string name;

    private int attackMin;

    private int attackMax;

    private int defenseMin;

    private int defenseMax;

    private int hpMax;

    private int hpMin;

    private int wakanMax;

    private int wakanMin;

    private int speedMax;

    private int speedMin;

    private float growMax;

    private float growMin;

    private string assetPath;

    private float attackPercent;

    private float defensePercent;

    private float hpPercent;

    private float wakanPercent;

    private float speedPercent;

    /// <summary>
    /// 最大技能数量
    /// </summary>
    private int maxSkillValue;

    public PetConfigData(DRPetData data)
    {
        Name = data.Name;
        AttackMax = data.AttackMax;
        AttackMin = data.AttackMin;
        DefenseMax = data.DefenseMax;
        defenseMin = data.DefenseMin;
        HPMax = data.HPMax;
        HPMin = data.HPMin;
        WakanMax = data.WakanMax;
        WakanMin = data.WakanMin;
        SpeedMax = data.SpeedMax;
        SpeedMin = data.SpeedMin;
        GrowMax = data.GrowMax;
        GrowMin = data.GrowMin;
        AssetPath = data.AssetsPath;
        maxSkillValue = data.MaxSkillValue;

        attackPercent = (float)1.0f / ((float)AttackMax - (float)attackMin);

        defensePercent = 1 / (DefenseMax - DefenseMin);

        hpPercent = 1 / (HPMax - HPMin);

        wakanPercent = 1 / (WakanMax - WakanMin);

        speedPercent = 1 / (SpeedMax - SpeedMin);
    }

    public string Name
    {
        get => this.name;
        set => this.Set<string>(ref this.name, value, "Name");
    }

    public int AttackMin
    {
        get => this.attackMin;
        set => this.Set<int>(ref this.attackMin, value, "AttackMin");
    }

    public int DefenseMin
    {
        get => this.defenseMin;
        set => this.Set<int>(ref this.defenseMin, value, "DefenseMin");
    }

    public int DefenseMax
    {
        get => this.defenseMax;
        set => this.Set<int>(ref this.defenseMax, value, "DefenseMax");
    }

    public int AttackMax
    {
        get => this.attackMax;
        set => this.Set<int>(ref this.attackMax, value, "AttackMax");
    }

    public int HPMax
    {
        get => this.hpMax;
        set => this.Set<int>(ref this.hpMax, value, "HPMax");
    }

    public int HPMin
    {
        get => this.hpMin;
        set => this.Set<int>(ref this.hpMin, value, "HPMin");
    }

    public int WakanMax
    {
        get => this.wakanMax;
        set => this.Set<int>(ref this.wakanMax, value, "WakanMax");
    }

    public int WakanMin
    {
        get => this.wakanMin;
        set => this.Set<int>(ref this.wakanMin, value, "WakanMin");
    }

    public int SpeedMax
    {
        get => this.speedMax;
        set => this.Set<int>(ref this.speedMax, value, "SpeedMax");
    }

    public int SpeedMin
    {
        get => this.speedMin;
        set => this.Set<int>(ref this.speedMin, value, "SpeedMin");
    }

    public float GrowMax
    {
        get => this.growMax;
        set => this.Set<float>(ref this.growMax, value, "GrowMax");
    }

    public float GrowMin
    {
        get => this.growMin;
        set => this.Set<float>(ref this.growMin, value, "GrowMin");
    }

    public string AssetPath
    {
        get => this.assetPath;
        set => this.Set<string>(ref this.assetPath, value, "AssetPath");
    }

    public int MaxSkillValue => maxSkillValue;

    public float AttackPercent => attackPercent;

    public float DefensePercent => defensePercent;

    public float WakanPercent => wakanPercent;

    public float HpPercent => hpPercent;

    public float SpeedPercent => speedPercent;
}