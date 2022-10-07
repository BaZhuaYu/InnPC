using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMItem
{

    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;

    public static List<MMItem> all;
    public static List<MMItem> all1;


    public static void Init()
    {
        all = new List<MMItem>();
        foreach (var temp in allValues.Values)
        {
            MMItem item = MMItem.CreateFromString(temp);
            all.Add(item);
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMSkill> FindAll()");
        }
    }


    public static List<MMItem> FindAll()
    {
        if(all == null)
        {
            all = new List<MMItem>();
            foreach (var temp in allValues.Values)
            {
                MMItem item = MMItem.CreateFromString(temp);
                all.Add(item);
                if(item.prob > 0)
                {
                    all1.Add(item);
                }
            }

            if (all.Count == 0)
            {
                MMDebugManager.FatalError("public static List<MMSkill> FindAll()");
            }
        }

        return all;
    }



    public static MMItem FindRandomOne()
    {
        return all[Random.Range(0, all.Count)];
    }


    public static List<MMItem> FindRandomCount(int count)
    {
        List<MMItem> ret = new List<MMItem>();

        if (count > all.Count)
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
