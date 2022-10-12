using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MMEffectType
{
    None,       //0
    Attack,     //1
    InHP,       //2
    DeHP,       //3
    InAP,       //4
    DeAP,
    InATK,      //6
    DeATK,
    Damage,     //8
    Summon,     //9


    AddUnit,
    AddHand,
    AddBuff,

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
