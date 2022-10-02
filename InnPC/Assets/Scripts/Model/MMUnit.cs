using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class MMUnit
{
    
    private MMUnit()
    {

    }


    public string key;
    public int id;

    public string displayName;
    public string displayNote;

    public int maxHP;
    public int hp;
    public int maxAP;
    public int ap;

    public int atk;
    public int def;
    public int mag;
    public int spd;

    public int tempATK;
    public int tempDEF;


    public int attackRange;

    public List<int> skills;


    public static MMUnit Create(int id)
    {
        if (MMUnitData.allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMSkill Create: " + id);
        }

        return CreateFromString(MMUnitData.allValues[id]);
    }


    private static MMUnit CreateFromString(string s)
    {
        Dictionary<string, int> allKeys = MMUnitData.allKeys;

        string[] values = s.Split(',');

        MMUnit unit = new MMUnit();
        unit.id = int.Parse(values[allKeys["ID"]]);
        unit.key = values[allKeys["Key"]];
        unit.displayName = values[allKeys["Name"]];
        unit.displayNote = values[allKeys["Note"]];
        unit.maxHP = int.Parse(values[allKeys["MaxHP"]]);
        unit.hp = int.Parse(values[allKeys["HP"]]);
        unit.maxAP = int.Parse(values[allKeys["MaxAP"]]);
        unit.ap = int.Parse(values[allKeys["AP"]]);

        int.TryParse(values[allKeys["ATK"]], out unit.atk);
        int.TryParse(values[allKeys["DEF"]], out unit.def);
        int.TryParse(values[allKeys["MAG"]], out unit.mag);
        int.TryParse(values[allKeys["SPD"]], out unit.spd);
        
        unit.attackRange = int.Parse(values[allKeys["AttackRange"]]);

        unit.skills = new List<int>();
        string[] ssss = values[allKeys["Skills"]].Split(';');

        foreach (var temp in ssss)
        {
            int a = int.Parse(temp);
            unit.skills.Add(a);
        }
        unit.skills.Add(10000);
        unit.skills.Add(1000);

        return unit;
    }

    
}
