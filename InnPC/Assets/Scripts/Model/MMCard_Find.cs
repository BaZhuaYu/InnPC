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


    public static MMCard FindOne(int id)
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
        return cards[Random.Range(0, cards.Count)];
    }


    public static List<MMCard> FindRandomCount(int count)
    {
        if (count > cards.Count)
        {
            MMDebugManager.FatalError("FindRandom: " + count);
        }

        List<MMCard> ret = new List<MMCard>();

        while (ret.Count < count)
        {
            MMCard card = FindRandomOne();
            if (MMUtility.CheckListNotHasOne<MMCard>(ret, card))
            {
                ret.Add(card);
            }
        }

        return ret;
    }


}
