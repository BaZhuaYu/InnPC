using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMCard
{
    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;

    
    public static List<MMCard> all;
    public static List<MMCard> cards;


    public static void Init()
    {
        all = new List<MMCard>();
        cards = new List<MMCard>();
        foreach (var temp in allValues.Values)
        {
            MMCard card = MMCard.CreateFromString(temp);
            all.Add(card);
            if (card.prob > 0)
            {
                cards.Add(card);
            }
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMCard> FindAll()");
        }
    }


    public static List<MMCard> FindAll()
    {
        List<MMCard> ret = new List<MMCard>(); ;
        foreach (var one in all)
        {
            if (one.prob > 0)
            {
                ret.Add(one);
            }
        }
        return ret;
    }


    public static MMCard FindRandomOne()
    {
        List<MMCard> all = FindAll();
        return all[Random.Range(0, all.Count)];
    }


    public static List<MMCard> FindRandomCount(int count)
    {
        List<MMCard> all = FindAll();
        if (count > all.Count)
        {
            MMDebugManager.FatalError("FindRandom: " + count);
        }

        List<MMCard> ret = new List<MMCard>();

        while (ret.Count < count)
        {
            MMCard skill = FindRandomOne();
            if (MMUtility.CheckListNotHasOne<MMCard>(ret, skill))
            {
                ret.Add(skill);
            }
        }

        return ret;
    }


}
