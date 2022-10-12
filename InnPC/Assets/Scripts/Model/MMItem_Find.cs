using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMItem
{

    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;

    public static List<MMItem> all;
    public static List<MMItem> items;


    public static void Init()
    {
        all = new List<MMItem>();
        items = new List<MMItem>();
        foreach (var temp in allValues.Values)
        {
            MMItem item = MMItem.CreateFromString(temp);
            all.Add(item);
            if (item.prob > 0)
            {
                items.Add(item);
            }
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMSkill> FindAll()");
        }
    }


    public static List<MMItem> FindAll()
    {
        return items;
    }



    public static MMItem FindRandomOne()
    {
        return items[Random.Range(0, items.Count)];
    }


    public static List<MMItem> FindRandomCount(int count)
    {
        List<MMItem> ret = new List<MMItem>();

        if (count > items.Count)
        {
            MMDebugManager.FatalError("FindRandom: " + count);
        }
        
        while (ret.Count < count)
        {
            MMItem item = FindRandomOne();
            if (MMUtility.CheckListNotHasOne<MMItem>(ret, item))
            {
                ret.Add(item);
            }
        }

        return ret;

    }

}
