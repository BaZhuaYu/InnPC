using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class MMLevel
{

    public int id;
    public string key;

    public string displayName;
    public string displayNote;

    public Dictionary<int, MMUnit> enemies;



    public static MMLevel Create(int id)
    {
        if (MMLevelData.allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMSkill Create: " + id);
        }

        return CreateFromString(MMLevelData.allValues[id]);
    }



    public static MMLevel CreateFromString(string s)
    {
        Dictionary<string, int> keys = MMLevelData.allKeys;

        string[] values = s.Split(',');

        MMLevel level = new MMLevel();
        level.id = int.Parse(values[keys["ID"]]);
        level.key = values[keys["Key"]];
        level.displayName = values[keys["Name"]];
        level.displayNote = values[keys["Note"]];


        int cellIndex = 0;

        string[] row4 = values[keys["Row4"]].Split(';');
        foreach(var unitIDString in row4)
        {
            int unitID = int.Parse(unitIDString);
            if(unitID != 0)
            {
                level.enemies.Add(cellIndex + 16, MMUnit.Create(unitID));
            }

            cellIndex++;
        }

        cellIndex = 0;
        string[] row5 = values[keys["Row5"]].Split(';');
        foreach (var unitIDString in row5)
        {
            int unitID = int.Parse(unitIDString);
            if (unitID != 0)
            {
                level.enemies.Add(cellIndex + 20, MMUnit.Create(unitID));
            }

            cellIndex++;
        }

        cellIndex = 0;
        string[] row6 = values[keys["Row6"]].Split(';');
        foreach (var unitIDString in row6)
        {
            int unitID = int.Parse(unitIDString);
            if (unitID != 0)
            {
                level.enemies.Add(cellIndex + 24, MMUnit.Create(unitID));
            }

            cellIndex++;
        }


        return level;
    }


}
