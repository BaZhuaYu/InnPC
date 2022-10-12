using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMSkill
{
    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;


    public static List<MMSkill> all;
    public static List<MMSkill> skills;


    public static void Init()
    {
        all = new List<MMSkill>();
        skills = new List<MMSkill>();
        foreach (var temp in allValues.Values)
        {
            MMSkill skill = MMSkill.CreateFromString(temp);
            all.Add(skill);
            if(skill.prob > 0)
            {
                skills.Add(skill);
            }
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMSkill> FindAll()");
        }
    }
    

    public static List<MMSkill> FindAll()
    {
        List<MMSkill> ret = new List<MMSkill>();;
        foreach (var one in all)
        {
            if(one.prob > 0)
            {
                ret.Add(one);
            }
        }
        return ret;
    }


    public static MMSkill FindRandomOne()
    {
        List<MMSkill> all = FindAll();
        return all[Random.Range(0, all.Count)];
    }


    public static List<MMSkill> FindRandomCount(int count)
    {
        List<MMSkill> all = FindAll();
        if (count > all.Count)
        {
            MMDebugManager.FatalError("FindRandom: " + count);
        }

        List<MMSkill> ret = new List<MMSkill>();

        while (ret.Count < count)
        {
            MMSkill skill = FindRandomOne();
            if (MMUtility.CheckListNotHasOne<MMSkill>(ret, skill))
            {
                ret.Add(skill);
            }
        }

        return ret;
    }


    //public static bool CheckHasOne(List<MMSkill> units, MMSkill one)
    //{
    //    foreach (var unit in units)
    //    {
    //        if (unit.id == one.id)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    
}
