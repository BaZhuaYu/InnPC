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


    public int value;
    public MMArea area;
    public List<MMSkillKeyWord> keywords;

    public int tempATK;
    public int tempDEF;

    

    public static MMSkill Create(int id)
    {
        if(MMSkillData.allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMSkill Create: " + id);
        }

        return CreateFromString(MMSkillData.allValues[id]);
    }

    
    public void ExecuteEffect()
    {

    }


    public static MMSkill CreateFromString(string s)
    {
        Dictionary<string, int> keys = MMSkillData.allKeys;

        string[] values = s.Split(',');

        MMSkill skill = new MMSkill();
        skill.id = int.Parse(values[keys["ID"]]);
        skill.key = values[keys["Key"]];
        skill.displayName = values[keys["Name"]];
        skill.displayNote = values[keys["Note"]];
        
        int.TryParse(values[keys["Cost"]], out skill.cost);
        int.TryParse(values[keys["Value"]], out skill.value);
        int.TryParse(values[keys["TempATK"]], out skill.tempATK);
        int.TryParse(values[keys["TempDEF"]], out skill.tempDEF);

        skill.area = DeserializeArea(values[keys["Area"]]);

        return skill;
    }

    

    public static MMArea DeserializeArea(string s)
    {
        switch(s)
        {
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
        return MMArea.None;
    }

    
}
