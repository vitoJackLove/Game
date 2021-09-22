/// <summary>
/// 队伍Id。
/// </summary>
public enum TeamIdEnum
{
Hero,
Monster,
Scene,
TestHero,
Enemy,
Summon,
Ally,
Neutral,
None,
}

/// <summary>
/// 队伍标题。
/// </summary>
public enum TeamTitleEnum
{
All,
Player,
NPC,
}

/// <summary>
/// 角色类型。
/// </summary>
public enum RoleTypeEnum
{
Hero,
HeroPet,
Monster,
MonsterPet,
Neutral,
}

/// <summary>
/// 子弹移动类型。
/// </summary>
public enum BulletMovementTypeEnum
{
None,
Straight,
Circle,
Curve,
Rebounce,
CosCurve,
IncreaseCircle,
Parabola,
}

/// <summary>
/// 追踪效果。
/// </summary>
public enum BulletTrackTypeEnum
{
None,
AttackOnce,
AttackAnother,
AttackAll,
}

/// <summary>
/// 子弹碰撞盒形状。
/// </summary>
public enum ColliderShapeEnum
{
None,
Sector,
Rect,
Circle,
Ring,
}

/// <summary>
/// Intention条件方向。
/// </summary>
public enum ConditionalDirectionEnum
{
None,
fromSelf,
targetSelf,
fromAlly,
targetAlly,
}

/// <summary>
/// 狂暴阶段。
/// </summary>
public enum OverDriveStateEnum
{
Normal,
Charge,
OverDrive,
Break,
}

/// <summary>
/// buff指定技能类型。
/// </summary>
public enum TimesToSkillTypeEnum
{
None,
Skill1,
Skill2,
NomalAttack,
XpSkill,
EnergyAttack,
Dodge,
DashAttack,
RollAttack,
ChangeWeapon,
}

/// <summary>
/// buff叠加时的时间算法。
/// </summary>
public enum BuffAddTimeTypeEnum
{
Reset,
DoNoting,
AddTime,
}

/// <summary>
/// Buff结束时销毁一层/全部。
/// </summary>
public enum BuffOverTypeEnum
{
OneLayer,
All,
}

/// <summary>
/// buff跟随武器方式。
/// </summary>
public enum BuffFollowWeaponWayEnum
{
NotFollow,
FollowType,
FollowCurrent,
}

/// <summary>
/// XP技能类型。
/// </summary>
public enum XPSkillTypeEnum
{
NotXP,
Continue,
Moment,
}

/// <summary>
/// 子弹攻击类型。
/// </summary>
public enum BulletAttackTypeEnum
{
Far,
Near,
}

/// <summary>
/// Shape类型。
/// </summary>
public enum ShapeEnum
{
Sphere,
Capsule,
Sector,
Cylinder,
None,
Box,
Circle,
}

/// <summary>
/// 碰撞层级。
/// </summary>
public enum CollsionLayerEnum
{
Layer1,
Layer2,
Layer3,
Layer4,
Layer5,
Everything,
Default,
}

/// <summary>
/// 子弹类型。
/// </summary>
public enum BulletTypeEnum
{
NoMove,
EvenlyMoveForward,
MoveByMaster,
AroundMaster,
None,
}

/// <summary>
/// 大大。
/// </summary>
public enum AttachSpecialPropertyEnum
{
none,
flame,
poison,
frozen,
paralysis,
curse,
}

/// <summary>
/// 特效类型。
/// </summary>
public enum EffectTypeEnum
{
Timer,
Loop,
}

/// <summary>
/// 怪物阶段类型。
/// </summary>
public enum MonsterPhaseEnum
{
None,
Normal,
OverDrive,
Break,
}

/// <summary>
/// 技能类型。
/// </summary>
public enum SkillTypeEnum
{
FreedomHand,
Hand,
None,
}

/// <summary>
/// 子弹实体类型。
/// </summary>
public enum BulletEntityEnum
{
AnnulusExpandNoMove,
AnnulusNoMove,
BoxMoveByMaster,
BoxNoMove,
CircleAroundMaster,
CircleMove,
CircleNoMove,
SectorNoMove,
AnnulusSectorExpandNoMove,
BoxMove,
BoxAroundMaster,
CircleNoMoveAttractBullet,
FollowMasteraCircleAttractBullet,
SectorNoMoveAttractBullet,
FollowMasteraSectorAttractBullet,
CircleMoveStopBullet,
SectorMoveByMaster,
CircleMoveStopHitStopBullet,
CircleMoveStopCollideStopBullet,
BoxNoMoveAttractBullet,
AnnulusNoMoveAttractBullet,
}

/// <summary>
/// 目标阵营。
/// </summary>
public enum TargetTeamEnum
{
Friend,
Enemy,
Self,
All,
}

/// <summary>
/// 台词轨道类型。
/// </summary>
public enum LineEnum
{
Good,
Bad,
NoChar,
}

/// <summary>
/// 武器类型。
/// </summary>
public enum WeaponEnum
{
Sword,
Hammer,
Gloves,
Arrow,
Gun,
}

/// <summary>
/// 小怪阶段类型。
/// </summary>
public enum LowLevelMonsterPhaseEnum
{
Birth,
Wait,
Battle,
Chase,
RunAway,
}

/// <summary>
/// 拼刀系统子弹类型。
/// </summary>
public enum FightKnifeBulletEnum
{
Normal,
BeFightKnife,
FightKnife,
Rebound,
BeRebound,
}