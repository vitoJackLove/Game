using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

/// <summary>
/// 宠物技能
/// </summary>
public class PetSkillData : ModelBase
{
    /// <summary>
    /// 技能数据
    /// </summary>
    private DRPetSkillData data;

    /// <summary>
    /// 技能名字
    /// </summary>
    private string name;

    /// <summary>
    /// 技能级别 （0高级，1低级）
    /// </summary>
    private int level;

    /// <summary>
    /// 描述
    /// </summary>
    private string describe;

    /// <summary>
    /// 是否是主动技能
    /// </summary>
    private bool isActive;

    /// <summary>
    /// 消耗魔法
    /// </summary>
    private int expendMagic;

    /// <summary>
    /// 图片路径
    /// </summary>
    private string iconPath;
    
    public void Init(int skillId)
    {
        data = GameEnter.DataTable.GetDataRow<DRPetSkillData>(skillId);
        Name = data.Name;
        Level = data.Level;
        Describe = data.Describe;
        IsActive = data.IsActive;
        ExpendMagic = data.Magic;
        IconPath = data.IconPath;
    }
    
    public string Name
    {
        get => this.name;
        set => this.Set<string>(ref this.name, value, "Name");
    }
    
    public string Describe
    {
        get => this.describe;
        set => this.Set<string>(ref this.describe, value, "Describe");
    }
    
    public string IconPath
    {
        get => this.iconPath;
        set => this.Set<string>(ref this.iconPath, value, "IconPath");
    }
    
    public int Level
    {
        get => this.level;
        set => this.Set<int>(ref this.level, value, "Level");
    }
    
    public int ExpendMagic
    {
        get => this.expendMagic;
        set => this.Set<int>(ref this.expendMagic, value, "ExpendMagic");
    }
    
    public bool IsActive
    {
        get => this.isActive;
        set => this.Set<bool>(ref this.isActive, value, "IsActive");
    }
}
