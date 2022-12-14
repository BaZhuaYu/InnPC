using System.Collections;
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

            case "RaceUnits":
                return MMArea.RaceUnits;

            case "TeamUnits":
                return MMArea.TeamUnits;

            case "Nine":
                return MMArea.Nine;

            case "All":
                return MMArea.All;

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
            case "MinHPTeam":
                return MMEffectTarget.MinHPTeam;
            case "MaxHPTeam":
                return MMEffectTarget.MaxHPTeam;
            case "MinHPEnemy":
                return MMEffectTarget.MinHPEnemy;
            case "MaxHPEnemy":
                return MMEffectTarget.MaxHPEnemy;
            case "Unit":
                return MMEffectTarget.Unit;
            case "Cell":
                return MMEffectTarget.Cell;
            default:
                return MMEffectTarget.None;
        }
    }


    //public static 



    public static MMTriggerTime DeserializeTriggerTime(string s)
    {
        switch (s)
        {
            case "OnBattleBegin":
                return MMTriggerTime.OnBattleBegin;
            case "OnRoundBegin":
                return MMTriggerTime.OnRoundBegin;
            case "OnRoundEnd":
                return MMTriggerTime.OnRoundEnd;
            

            case "OnActive":
                return MMTriggerTime.OnActive;
            case "OnLeave":
                return MMTriggerTime.OnLeave;
            case "OnKill":
                return MMTriggerTime.OnKill;
            case "OnDead":
                return MMTriggerTime.OnDead;
            case "OnBeSummon":
                return MMTriggerTime.OnBeSummon;
            case "OnSummon":
                return MMTriggerTime.OnSummon;
                
            case "BeforeAttack":
                return MMTriggerTime.BeforeAttack;
            case "AfterAttack":
                return MMTriggerTime.AfterAttack;
            case "BeforeBeAttack":
                return MMTriggerTime.BeforeBeAttack;
            case "AfterBeAttack":
                return MMTriggerTime.AfterBeAttack;
                
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


    public static MMCardType DeserializeCardType(int t)
    {
        switch (t)
        {
            case 1:
                return MMCardType.Attack;
            case 2:
                return MMCardType.Spell;
            case 3:
                return MMCardType.Power;
            case 4:
                return MMCardType.Passive;
        }
        return MMCardType.None;
    }


    public static Color FindColorRed()
    {
        return new Color(154f / 255f, 91f / 255f, 95f / 255f);
    }

    public static Color FindColorYellow()
    {
        return new Color(199f / 255f, 170f / 255f, 62f / 255f);
    }

    public static Color FindColorBlue()
    {
        return new Color(76f / 255f, 137f / 255f, 167f / 255f);
    }

    public static Color FindColorGreen()
    {
        return new Color(114f / 255f, 159f / 255f, 95f / 255f);
    }

    public static Color FindColorBlack()
    {
        return new Color(15f / 255f, 15f / 255f, 15f / 255f);
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
