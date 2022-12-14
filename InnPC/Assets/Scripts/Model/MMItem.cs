using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMItem
{

    public int id;
    public string key;
    public string displayName;
    public string displayNote;
    public int price;

    public int prob;
    public int effect;
    public int value;




    public static MMItem Create(int id)
    {
        if (MMItem.allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMSkill Create: " + id);
        }

        return CreateFromString(MMItem.allValues[id]);
    }


    public static MMItem CreateFromString(string s)
    {
        string[] values = s.Split(',');

        MMItem item = new MMItem();
        item.id = int.Parse(values[allKeys["ID"]]);
        item.key = values[allKeys["Key"]];
        item.displayName = values[allKeys["Name"]];
        item.displayNote = values[allKeys["Note"]];
        int.TryParse(values[allKeys["Price"]], out item.price);

        int.TryParse(values[allKeys["Prob"]], out item.prob);
        int.TryParse(values[allKeys["Effect"]], out item.effect);
        int.TryParse(values[allKeys["Value"]], out item.value);

        return item;
    }
    

}
