using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMSkillData : MonoBehaviour
{

    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;

    
    public static void Deserialize(string[] ss)
    {
        allKeys = new Dictionary<string, int>();
        allValues = new Dictionary<int, string>();

        MMUnit.all = new List<MMUnit>();

        int index = 0;
        foreach (var s in ss)
        {
            string[] values = s.Split(',');
            if (index == 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == "End")
                    {
                        break;
                    }
                    allKeys.Add(values[i], i);
                }
            }
            else
            {
                int id = int.Parse(values[allKeys["ID"]]);
                allValues.Add(id, s);


                //MMUnit unit = new MMUnit();
                //unit.id = int.Parse(values[keys["ID"]]);
                //unit.key = values[keys["Key"]];
                //unit.displayName = values[keys["Name"]];
                //unit.displayNote = values[keys["Note"]];
                //unit.maxHP = int.Parse(values[keys["MaxHP"]]);
                //unit.hp = int.Parse(values[keys["HP"]]);
                //unit.maxAP = int.Parse(values[keys["MaxAP"]]);
                //unit.ap = int.Parse(values[keys["AP"]]);
                //unit.atk = int.Parse(values[keys["ATK"]]);
                //unit.def = int.Parse(values[keys["DEF"]]);
                //unit.mag = int.Parse(values[keys["MAG"]]);
                //unit.spd = int.Parse(values[keys["SPD"]]);
                //unit.attackRange = int.Parse(values[keys["AttackRange"]]);
                //foreach(var temp in values[keys["Skills"]].Split(';'))
                //{
                //    unit.skills.Add(int.Parse(temp));
                //}


                //MMUnit.all.Add(CreateFromString(s));
            }
            index++;
        }
    }









}
