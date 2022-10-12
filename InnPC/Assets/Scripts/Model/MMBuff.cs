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
    Ultimate
}


public enum MMBuff
{
    JinZhongZhao,
}


