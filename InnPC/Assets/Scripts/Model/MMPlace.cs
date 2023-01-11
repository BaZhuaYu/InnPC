using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMPlace
{

    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;


    public static List<MMPlace> all;
    public static List<MMPlace> places;
    

    /// <summary>
    /// 
    /// </summary>
    public int id;
    public string key;
    public string displayName;
    public string displayNote;

    public int type;
    public int clss;

    public int prob;
    public int costTime;
    public int costGold;
    public int num;
    
    

    public static MMPlace Create(int id)
    {
        if (allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMPlace Create: " + id);
        }
        return CreateFromString(allValues[id]);
    }
    

    public static MMPlace CreateFromString(string s)
    {
        string[] values = s.Split(',');

        MMPlace place = new MMPlace();
        place.id = int.Parse(values[allKeys["ID"]]);
        place.key = values[allKeys["Key"]];
        place.displayName = values[allKeys["Name"]];
        place.displayNote = values[allKeys["Note"]];

        place.type = int.Parse(values[allKeys["Type"]]);
        place.clss = int.Parse(values[allKeys["Clss"]]);

        int.TryParse(values[allKeys["Price"]], out place.costGold);
        int.TryParse(values[allKeys["Time"]], out place.costTime);
        int.TryParse(values[allKeys["Num"]], out place.num);
        int.TryParse(values[allKeys["Prob"]], out place.prob);

        return place;
    }


    public static void Init()
    {
        all = new List<MMPlace>();
        places = new List<MMPlace>();
        foreach (var temp in allValues.Values)
        {
            MMPlace card = MMPlace.CreateFromString(temp);
            all.Add(card);
            if (card.prob > 0)
            {
                places.Add(card);
            }
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMCard> FindAll()");
        }
    }


    public static MMPlace FindRandomOne()
    {
        return all[Random.Range(0, all.Count)];
    }


    public static MMPlace FindOne(int id)
    {
        foreach (var one in all)
        {
            if (one.id == id)
            {
                return one;
            }
        }
        return null;
    }

    public static MMPlace FindOne(string key)
    {
        foreach (var one in all)
        {
            if (one.key == key)
            {
                return one;
            }
        }
        return null;
    }

    
}
