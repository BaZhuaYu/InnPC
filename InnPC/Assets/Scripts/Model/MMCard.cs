using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class MMCard
{

    private MMCard() { }

    public string key;
    public int id;
    public string displayName;
    public string displayNote;

    
    public int type;
    public int clss;
    public int cost;
    public int prob;

    public MMEffectTarget target;
    public MMEffectType effect;
    public int value;
    public MMTriggerTime time;
    public MMArea area;
    public List<MMSkillKeyWord> keywords;

    public int tempATK;
    public int tempDEF;

    
    public static MMCard Create(int id)
    {
        if (allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMCard Create: " + id);
        }
        return CreateFromString(allValues[id]);
    }


    public static MMCard CreateFromString(string s)
    {
        string[] values = s.Split(',');

        MMCard card = new MMCard();
        card.id = int.Parse(values[allKeys["ID"]]);
        card.key = values[allKeys["Key"]];
        card.displayName = values[allKeys["Name"]];
        card.displayNote = values[allKeys["Note"]];


        int.TryParse(values[allKeys["Clss"]], out card.clss);
        int.TryParse(values[allKeys["Type"]], out card.type);
        int.TryParse(values[allKeys["Prob"]], out card.prob);

        int.TryParse(values[allKeys["Cost"]], out card.cost);
        int.TryParse(values[allKeys["Value"]], out card.value);
        int.TryParse(values[allKeys["TempATK"]], out card.tempATK);
        int.TryParse(values[allKeys["TempDEF"]], out card.tempDEF);

        card.target = MMUtility.DeserializeEffectTarget(values[allKeys["Target"]]);
        card.effect = MMUtility.DeserializeEffectType(values[allKeys["Effect"]]);
        //int.TryParse(values[allKeys["Effect"]], out skill.effect);
        card.area = MMUtility.DeserializeArea(values[allKeys["Area"]]);
        card.time = MMUtility.DeserializeTriggerTime(values[allKeys["Time"]]);


        card.keywords = new List<MMSkillKeyWord>();

        return card;
    }


}
