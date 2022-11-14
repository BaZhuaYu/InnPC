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

    public int race;
    public int clss;

    public int tempATK;
    public int tempDEF;


    public int attackRange;

    public int card;
    public List<int> skills;


    public int prob;


    public static MMUnit Create(int id)
    {
        if (allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMSkill Create: " + id);
        }

        return CreateFromString(allValues[id]);
    }


    private static MMUnit CreateFromString(string s)
    {
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

        unit.race = int.Parse(values[allKeys["Race"]]);
        unit.clss = int.Parse(values[allKeys["Clss"]]);
        
        int.TryParse(values[allKeys["ATK"]], out unit.atk);
        int.TryParse(values[allKeys["DEF"]], out unit.def);
        int.TryParse(values[allKeys["MAG"]], out unit.mag);
        int.TryParse(values[allKeys["SPD"]], out unit.spd);
        
        int.TryParse(values[allKeys["Prob"]], out unit.prob);
        int.TryParse(values[allKeys["Card"]], out unit.card);

        unit.attackRange = int.Parse(values[allKeys["AttackRange"]]);

        unit.skills = new List<int>();
        unit.skills.Add(int.Parse(values[allKeys["Skill"]]));


        //if (unit.id < 10400)
        //{
        //    unit.skills.Add(unit.id);
        //}
        unit.skills.Add(unit.id/100 * 100);

        if(unit.id == 10100)
        {
            unit.skills.Add(1074);
            unit.ap = 1;
        }


        //if (unit.id == 10100)
        //{
        //    unit.skills.Add(1035);
        //    unit.skills.Add(1036);
        //    unit.skills.Add(1037);
        //}

        //if (unit.id == 10300)
        //{
        //    unit.skills.Add(1021);
        //}

        return unit;
        
    }

    
}
