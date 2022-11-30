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

    public static MMEffectTarget DeserializeEffectTarget(string s)
    {
        switch (s)
        {
            case "Source":
                return MMEffectTarget.Source;
            case "Target":
                return MMEffectTarget.Target;
            default:
                return MMEffectTarget.None;
        }
    }


    //public static 



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
            case "OnBattleBegin":
                return MMTriggerTime.OnBattleBegin;
            case "OnActiveUnit":
                return MMTriggerTime.OnActiveUnit;

            case "OnAttack":
                return MMTriggerTime.OnAttack;
            case "BeforeAttack":
                return MMTriggerTime.BeforeAttack;
            case "AfterAttack":
                return MMTriggerTime.AfterAttack;
            case "BeforeBeAttack":
                return MMTriggerTime.BeforeBeAttack;
            case "AfterBeAttack":
                return MMTriggerTime.AfterBeAttack;
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
            case "10":
            case "101":
                return MMEffectType.TempATKDEF;
            case "15":
                return MMEffectType.HengSao;
            case "16":
                return MMEffectType.GuanChuan;
            default:
                if (s == "")
                {
                    s = "0";
                }
                int a = int.Parse(s);

                return (MMEffectType)(a);
        }

        return MMEffectType.None;
    }


    public static MMSkillType DeserializeSkillType(int t)
    {
        switch (t)
        {
            case 1:
                return MMSkillType.Attack;
            case 2:
                return MMSkillType.Spell;
            case 3:
                return MMSkillType.Power;
            case 4:
                return MMSkillType.Passive;
        }
        return MMSkillType.None;
    }


    public static Color FindColorRed()
    {
        return new Color(235f / 255f, 75f / 255f, 23f / 255f);
    }

    public static Color FindColorYellow()
    {
        return new Color(254f / 255f, 186f / 255f, 7f / 255f);
    }

    public static Color FindColorBlue()
    {
        return new Color(11f / 255f, 50f / 255f, 140f / 255f);
    }

    public static Color FindColorGreen()
    {
        return new Color(63f / 255f, 110f / 255f, 83f / 255f);
    }

    public static Color FindColorBlack()
    {
        return new Color(16f / 255f, 42f / 255f, 58f / 255f);
    }

    public static Color FindColorWhite()
    {
        return new Color(238f / 255f, 222f / 255f, 176f / 255f);
    }

    public static Color FindColorLightGreen()
    {
        return new Color(46f / 255f, 223f / 255f, 163f / 255f);
    }

    public static Color FindColorLightRed()
    {
        return new Color(240f / 255f, 207f / 255f, 227f / 255f);
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
