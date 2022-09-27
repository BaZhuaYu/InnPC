using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{



    public void LoadLevel1()
    {
        for (int i = 0; i < 3; i++)
        {
            MMUnit unit1 = MMUnit.Create(i + 1);
            MMUnitNode node1 = MMUnitNode.Create();
            node1.group = 1;
            node1.Accept(unit1);
            units1.Add(node1);
            MMMap.instance.FindCellOfIndex(i).Accept(node1);
        }

        AddEnemy(16100, 21);
    }







    public void LoadLevel10()
    {
        for (int i = 0; i < 4; i++)
        {
            MMUnit unit1 = MMUnit.Create(i + 1);
            MMUnitNode node1 = MMUnitNode.Create();
            node1.group = 1;
            node1.Accept(unit1);
            units1.Add(node1);
            MMMap.instance.FindCellOfIndex(i).Accept(node1);
        }

        for (int i = 0; i < 4; i++)
        {
            MMUnit unit2 = MMUnit.Create(4 - i);
            MMUnitNode node2 = MMUnitNode.Create();
            node2.group = 2;
            node2.Accept(unit2);
            units2.Add(node2);
            MMMap.instance.FindCellOfIndex(32 + i).Accept(node2);
        }
    }





    public void AddEnemy(int id, int pos)
    {
        MMUnit unit2 = MMUnit.Create(id);
        MMUnitNode node2 = MMUnitNode.Create();
        node2.group = 2;
        node2.Accept(unit2);
        units2.Add(node2);
        MMMap.instance.FindCellOfIndex(pos).Accept(node2);
    }




}
