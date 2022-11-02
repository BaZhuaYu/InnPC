using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MMTriggerTime
{
    None,
    Gain,               //1
    OnBattleBegin,      //2
    OnRoundBegin,       //3
    OnRoundEnd,
    OnNormalAttack,
    BeforeNormalAttack,
    AfterNormalAttack,
    OnDead,
    OnKillTarget,
    OnSummon,

    NormalAttackNum2,
}


public enum MMEffectTarget
{
    None,
    Source,
    Target,
}


public enum MMArea
{
    None,
    Single,
    Row,
    Col,
    Beside,
    Behind,
    Target,
    RaceUnits
}


public enum MMSkillKeyWord
{
    None,
    Ultimate,
    Final,
}


public enum MMBuff
{
    JinZhongZhao,
}


