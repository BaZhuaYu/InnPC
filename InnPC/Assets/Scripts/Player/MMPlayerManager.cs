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

    public List<MMUnit> units;

    public List<MMSkill> skills;

    public List<MMCard> cards;



    void Start()
    {
        gold = 0;

        level = 1;

        units = new List<MMUnit>();
        skills = new List<MMSkill>();
        cards = new List<MMCard>();

        LoadData();
    }


    public void LoadData()
    {
        for (int i = 0; i < 3; i++)
        {
            MMUnit unit1 = MMUnit.Create((i + 1) * 100 + 10000);
            units.Add(unit1);
            //MMUnitNode node1 = MMUnitNode.Create();
            //node1.group = 1;
            //node1.Accept(unit1);
            //units1.Add(node1);
            //MMMap.instance.FindCellOfIndex(i).Accept(node1);
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


}
