﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMUtility : MonoBehaviour
{
    public static MMArea DeserializeArea(string s)
    {
        switch (s)
        {
            case "None":
                return MMArea.None;
            case "Single":
                return MMArea.Single;
            case "Row":
                return MMArea.Row;
            case "Col":
                return MMArea.Col;
            case "Beside":
                return MMArea.Beside;
            case "Behind":
                return MMArea.Behind;
        }
        return MMArea.Single;
    }


    public static MMTriggerTime DeserializeTriggerTime(string s)
    {
        switch (s)
        {
            case "Gain":
                return MMTriggerTime.Gain;
            case "OnRoundBegin":
                return MMTriggerTime.OnRoundBegin;
            case "OnRoundEnd":
                return MMTriggerTime.OnRoundEnd;
            case "OnNormalAttack":
                return MMTriggerTime.OnNormalAttack;
            case "BeforeNormalAttack":
                return MMTriggerTime.BeforeNormalAttack;
            case "AfterNormalAttack":
                return MMTriggerTime.AfterNormalAttack;
            case "OnTargetDead":
                return MMTriggerTime.OnKillTarget;
            case "OnDead":
                return MMTriggerTime.OnDead;
            case "OnSummon":
                return MMTriggerTime.OnSummon;
        }

        return MMTriggerTime.None;
    }



    public static MMEffectType DeserializeEffectType(string s)
    {

        //Attack,
        //InHP,
        //DeHP,
        //InAP,
        //DeAP,
        //InATK,
        //DeATK,
        //Summon,


        //AddUnit,
        //AddHand,
        //AddBuff,

        switch (s)
        {
            case "1":
                return MMEffectType.Attack;
            case "2":
                return MMEffectType.InATK;
            case "3":
                return MMEffectType.DeATK;
            case "4":
                return MMEffectType.InHP;
            case "5":
                return MMEffectType.DeHP;
            case "6":
                return MMEffectType.InAP;
            case "7":
                return MMEffectType.DeAP;
            case "8":
                return MMEffectType.Damage;
            case "9":
                return MMEffectType.Summon;
            case "15":
                return MMEffectType.HengSao;
            case "16":
                return MMEffectType.GuanChuan;


        }

        return MMEffectType.None;
    }



    public static Text CreateText(string name)
    {
        GameObject obj = new GameObject(name);
        Text ret = obj.AddComponent<Text>();
        ret.fontSize = 34;
        ret.SetNativeSize();
        return ret;
    }


    public static bool CheckListHasOne<T>(List<T> all, T one)
    {
        foreach (var temp in all)
        {
            if (temp.Equals(one))
            {
                return true;
            }
        }

        return false;
    }

    public static bool CheckListNotHasOne<T>(List<T> all, T one)
    {
        return !CheckListHasOne<T>(all, one);
    }


}



public enum MMSkillState
{
    Ready,
    NotReady,
    Used
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
    Combo,
    Actived,
    Stunned
}


public enum MMRewardType : int
{
    Gold,
    Unit,
    Skill,
    Card,
    Item
}