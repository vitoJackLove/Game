using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色数据
/// </summary>
public class RoleData : ModelBase
{
     /// <summary>
     /// 出战的宠物
     /// </summary>
     private PetData battlePet;

     public PetData BattlePet => battlePet;
}

/// <summary>
/// 所有宠物信息
/// </summary>
public class PetListData : ModelBase
{
     /// <summary>
     /// 宠物列表
     /// </summary>
     private List<PetData> petDataList;

     public List<PetData> PetDataList => petDataList;

     public PetListData()
     {
          petDataList = new List<PetData>();

          for (int i = 0; i < 3; i++)
          {
               PetData petData = new PetData();
               petData.Init(i + 1, 89);
               petDataList.Add(petData);
          }
     }
}
