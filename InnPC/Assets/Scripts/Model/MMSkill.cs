using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class MMSkill
{

    private MMSkill()
    {

    }


    public string key;
    public int id;
    public string displayName;
    public string displayNote;
    public int cost;
    public string icon;
    public int prob;

    public MMEffectType effect;
    public int value;
    public MMTriggerTime time;
    public MMArea area;
    public List<MMSkillKeyWord> keywords;

    public int tempATK;
    public int tempDEF;




    public void ExecuteEffect(MMUnit unit)
    {
        switch (effect)
        {
            case MMEffectType.InATK:
                unit.atk += 1;
                break;
            case MMEffectType.InHP:
                unit.maxHP += 1;
                unit.hp += 1;
                break;
            case MMEffectType.InAP:
                unit.ap += 1;
                break;
            default:
                break;
        }
    }




    public static MMSkill Create(int id)
    {
        if (allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMSkill Create: " + id);
        }

        return CreateFromString(allValues[id]);
    }


    public static MMSkill CreateFromString(string s)
    {
        string[] values = s.Split(',');

        MMSkill skill = new MMSkill();
        skill.id = int.Parse(values[allKeys["ID"]]);
        skill.key = values[allKeys["Key"]];
        skill.displayName = values[allKeys["Name"]];
        skill.displayNote = values[allKeys["Note"]];

        int.TryParse(values[allKeys["Prob"]], out skill.prob);

        int.TryParse(values[allKeys["Cost"]], out skill.cost);
        int.TryParse(values[allKeys["Value"]], out skill.value);
        int.TryParse(values[allKeys["TempATK"]], out skill.tempATK);
        int.TryParse(values[allKeys["TempDEF"]], out skill.tempDEF);

        skill.effect = DeserializeEffectType(values[allKeys["Effect"]]);
        skill.area = DeserializeArea(values[allKeys["Area"]]);
        skill.time = DeserializeTriggerTime(values[allKeys["Time"]]);

        return skill;
    }


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
                return MMEffectType.InHP;
            case "3":
                return MMEffectType.DeHP;
            case "4":
                return MMEffectType.InAP;
            case "5":
                return MMEffectType.DeAP;
            case "6":
                return MMEffectType.InATK;
            case "7":
                return MMEffectType.DeATK;
            case "8":
                return MMEffectType.Damage;
            case "9":
                return MMEffectType.Summon;

        }

        return MMEffectType.None;
    }

}
