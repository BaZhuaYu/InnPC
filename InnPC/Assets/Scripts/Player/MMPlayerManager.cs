using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPlayerManager : MonoBehaviour
{

    public static MMPlayerManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public int gold;

    public int level;

    public int hp;

    public List<MMUnit> units;

    public List<MMSkill> skills;

    public List<MMCard> cards;

    

    void Start()
    {
        gold = 100;
        level = 1;
        hp = 100;

        units = new List<MMUnit>();
        skills = new List<MMSkill>();
        cards = new List<MMCard>();
        


        LoadData();
    }


    void LoadData()
    {
        for (int i = 0; i < 3; i++)
        {
            MMUnit unit = MMUnit.Create((i + 1) * 100 + 10000);
            units.Add(unit);

            MMCard card = MMCard.Create(unit.id + 1);
            cards.Add(card);
        }

        for (int i = 0; i < 4; i++)
        {
            MMCard card1 = MMCard.Create(10000);
            MMCard card2 = MMCard.Create(10000);
            cards.Add(card1);
            cards.Add(card2);
        }
        
    }

    
    public List<MMUnitNode> CreateAllUnitNodes()
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();

        foreach(var unit in units)
        {
            MMUnitNode node = MMUnitNode.Create();
            node.Accept(unit);
            ret.Add(node);
        }

        return ret;
    }


    public bool HasUnit(MMUnit unit)
    {
        foreach(var temp in units)
        {
            if(unit.id == temp.id)
            {
                return true;
            }
        }

        return false;
    }


    public MMUnit FindUnit(int id)
    {
        foreach(var unit in units)
        {
            if(unit.id == id)
            {
                return unit;
            }
        }
        return null;
    }

}
