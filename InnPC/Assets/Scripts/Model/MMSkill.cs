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

    public int type;
    public int cost;
    public string icon;
    public int prob;

    public MMEffectTarget target;
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

        
        int.TryParse(values[allKeys["Type"]], out skill.type);
        int.TryParse(values[allKeys["Prob"]], out skill.prob);

        int.TryParse(values[allKeys["Cost"]], out skill.cost);
        int.TryParse(values[allKeys["Value"]], out skill.value);
        int.TryParse(values[allKeys["TempATK"]], out skill.tempATK);
        int.TryParse(values[allKeys["TempDEF"]], out skill.tempDEF);

        skill.target = MMUtility.DeserializeEffectTarget(values[allKeys["Target"]]);
        if(skill.id == 1078)
        {
            Debug.Log(values[allKeys["Effect"]]);
            Debug.Log("-----------------" + skill.id);
        }
        skill.effect = MMUtility.DeserializeEffectType(values[allKeys["Effect"]]);
        //int.TryParse(values[allKeys["Effect"]], out skill.effect);
        skill.area = MMUtility.DeserializeArea(values[allKeys["Area"]]);
        skill.time = MMUtility.DeserializeTriggerTime(values[allKeys["Time"]]);

        
        skill.keywords = new List<MMSkillKeyWord>();
        if(skill.id == 10000 || skill.id == 1000 || skill.id == 1035)
        {
            skill.keywords.Add(MMSkillKeyWord.Final);
        }


        return skill;
    }

    
}
