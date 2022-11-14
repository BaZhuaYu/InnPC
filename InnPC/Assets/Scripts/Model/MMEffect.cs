using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MMEffectType
{
    None,       //0
    Attack,     //1
    InATK,      //2
    DeATK,
    InHP,       //4
    DeHP,       //
    InAP,       //6
    DeAP,

    Damage,     //8
    Summon,     //9
    TempATKDEF,     //10

    AddUnit,
    AddHand,
    AddBuff,

    AttackNum,   //14
    HengSao,        
    GuanChuan,
}


public class MMEffect
{
    public MMEffectType type;
    public int value;

    public MMCell sourCell;
    public MMCell destCell;

    public MMUnitNode source;
    public MMUnitNode target;

    public List<MMUnitNode> sideTargets;

    public MMArea area;

    public Dictionary<string, int> userinfo;


    public MMEffect()
    {
        userinfo = new Dictionary<string, int>();
        sideTargets = new List<MMUnitNode>();
        userinfo = new Dictionary<string, int>();
    }
    
}
