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

    public List<int> cards;
    public List<int> skills;


    public int prob;


    public static MMUnit Create(int id)
    {
        if (allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMUnit Create: " + id);
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
        
        unit.attackRange = int.Parse(values[allKeys["AttackRange"]]);

        unit.cards = new List<int>();
        int card = 0;
        int.TryParse(values[allKeys["Card"]], out card);
        unit.cards.Add(card);

        unit.skills = new List<int>();
        int skill = 0;
        int.TryParse(values[allKeys["Skill"]], out skill);
        unit.skills.Add(skill);
        

        return unit;
        
    }

    
}
