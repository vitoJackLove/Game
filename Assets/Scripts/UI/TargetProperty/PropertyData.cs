using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性数据
/// </summary>
public class PropertyData : ModelBase
{
    /// <summary>
    /// 名字
    /// </summary>
    private string name;
    
    /// <summary>
    /// 法攻
    /// </summary>
    private int wakan;

    /// <summary>
    /// 物攻
    /// </summary>
    private int attack;

    /// <summary>
    /// 法防
    /// </summary>
    private int wakanDefense;
    
    /// <summary>
    /// 物防
    /// </summary>
    private int attackDefense;

    /// <summary>
    /// 速度
    /// </summary>
    private int speed;

    /// <summary>
    /// 体
    /// </summary>
    private int power;

    /// <summary>
    /// 灵
    /// </summary>
    private int wisdom;

    /// <summary>
    /// 力
    /// </summary>
    private int strength;

    /// <summary>
    /// 耐
    /// </summary>
    private int endurance;

    /// <summary>
    /// 敏
    /// </summary>
    private int quick;

    /// <summary>
    /// 血量
    /// </summary>
    private int hpMax;

    /// <summary>
    /// 魔法
    /// </summary>
    private int magicMax;

    /// <summary>
    /// 经验
    /// </summary>
    private int experienceMax;

    /// <summary>
    /// 当前血量
    /// </summary>
    private int currentHp;

    /// <summary>
    /// 当前法力值
    /// </summary>
    private int currentMagic;

    /// <summary>
    /// 当钱经验值
    /// </summary>
    private int currentExperience;

    /// <summary>
    /// 等级
    /// </summary>
    private int level;

    /// <summary>
    /// 评分
    /// </summary>
    private int grade;
    
    public int Wakan
    {
        get => this.wakan;
        set => this.Set<int>(ref this.wakan, value, "Wakan");
    }
    
    public int Attack
    {
        get => this.attack;
        set => this.Set<int>(ref this.attack, value, "Attack");
    }
    
    public int WakanDefense
    {
        get => this.wakanDefense;
        set => this.Set<int>(ref this.wakanDefense, value, "WakanDefense");
    }
    
    public int AttackDefense
    {
        get => this.attackDefense;
        set => this.Set<int>(ref this.attackDefense, value, "AttackDefense");
    }
    
    public int Speed
    {
        get => this.speed;
        set => this.Set<int>(ref this.speed, value, "Speed");
    }
    
    public int Power
    {
        get => this.power;
        set => this.Set<int>(ref this.power, value, "Power");
    }
    
    public int Wisdom
    {
        get => this.wisdom;
        set => this.Set<int>(ref this.wisdom, value, "Wisdom");
    }
    
    public int Strength
    {
        get => this.strength;
        set => this.Set<int>(ref this.strength, value, "Strength");
    }
    
    public int Endurance
    {
        get => this.endurance;
        set => this.Set<int>(ref this.endurance, value, "Endurance");
    }
    
    public int Quick
    {
        get => this.quick;
        set => this.Set<int>(ref this.quick, value, "Quick");
    }
    
    public int Level
    {
        get => this.level;
        set => this.Set<int>(ref this.level, value, "Level");
    }
    
    public int Grade
    {
        get => this.grade;
        set => this.Set<int>(ref this.grade, value, "Grade");
    }
    
    public string Name
    {
        get => this.name;
        set => this.Set<string>(ref this.name, value, "Name");
    }
}
