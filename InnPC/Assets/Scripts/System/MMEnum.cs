using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MMSkillState
{
    Ready,
    NotReady,
    Used
}


public enum MMSkillType
{
    None,
    Attack,
    Spell,
    Power,
    Passive,
}


public enum MMBattlePhase
{
    Begin,
    PlayerRound,
    EnemyRound,
    End
}


public enum MMNodeHighlight
{
    Normal,
    Yellow,
    Red,
    Blue,
    Green
}


public enum MMNodeState
{
    Normal,
    Yellow,
    Red,
    Blue,
    Green
}



public enum MMBattleState
{
    Normal,
    SelectSour,
    SourMoved,
    SelectSkill,
    PlayedSkill,
    SourDone
}


public enum MMUnitState
{
    Rage,
    Normal,
    Weak,
    Stunned,
    Dead
}


public enum MMUnitPhase
{
    Normal,
    Acting,
    Actived,
}


public enum MMRewardType : int
{
    Gold,
    Unit,
    Skill,
    Card,
    Item
}
