using System.Collections.Generic;
using Game;
using Loxodon.Framework.Observables;

/// <summary>
/// 宠物数据
/// </summary>
public class PetData : ModelBase
{
    /// <summary>
    /// 宠物表格数据
    /// </summary>
    private DRPetData data;

    /// <summary>
    /// 宠物配置数据
    /// </summary>
    private PetConfigData configData;

    public PetConfigData ConfigData => configData;

    /// <summary>
    /// 属性
    /// </summary>
    private PetPropertyData propertyData;

    public PetPropertyData PropertyData => propertyData;

    //元素 TODO

    /// <summary>
    /// 宠物技能
    /// </summary>
    private List<PetSkillData> skillDataList;
    
    /// <summary>
    /// 初始化宠物信息
    /// </summary>
    /// <param name="petId"></param>
    /// <param name="level"></param>
    public void Init(int petId, int level)
    {
        data = GameEnter.DataTable.GetDataRow<DRPetData>(petId);
        
        configData = new PetConfigData(data);

        propertyData = new PetPropertyData(this, level);
        
        skillDataList = new List<PetSkillData>();
    }
    
    //public 
}