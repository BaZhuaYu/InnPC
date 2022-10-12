using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnit
{
    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;

    public static List<MMUnit> all;
    public static List<MMUnit> units;


    public static void Init()
    {
        all = new List<MMUnit>();
        units = new List<MMUnit>();
        foreach (var temp in allValues.Values)
        {
            MMUnit unit = MMUnit.CreateFromString(temp);
            all.Add(unit);
            if(unit.prob > 0)
            {
                units.Add(unit);
            }
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMUnit> FindAll()");
        }
    }


    public static List<MMUnit> FindAll()
    {
        List<MMUnit> ret = new List<MMUnit>();

        foreach (var one in all)
        {
            if (one.prob > 0)
            {
                ret.Add(one);
            }
        }
        return ret;
    }


    public static MMUnit FindRandomOne()
    {
        return units[Random.Range(0, units.Count)];
    }
    

    public static List<MMUnit> FindRandomCount(int count)
    {
        if (count > units.Count)
        {
            MMDebugManager.FatalError("FindRandom: " + count);
        }

        List<MMUnit> ret = new List<MMUnit>();

        while(ret.Count < count)
        {
            MMUnit unit = FindRandomOne();
            if(MMUtility.CheckListNotHasOne<MMUnit>(ret, unit))
            {
                ret.Add(unit);
            }
        }

        return ret;
    }
    

    //public static bool CheckListHasOne(List<MMUnit> units, MMUnit one)
    //{
    //    foreach(var unit in units)
    //    {
    //        if(unit.id == one.id)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}
    
}
