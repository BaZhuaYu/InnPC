using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{

    public void LoadLevel0()
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    MMUnit unit1 = MMUnit.Create((i + 1) * 100 + 10000);
        //    MMUnitNode node1 = MMUnitNode.Create();
        //    node1.group = 1;
        //    node1.Accept(unit1);
        //    units1.Add(node1);
        //    MMMap.instance.FindCellOfIndex(i).Accept(node1);
        //}

        
        LoadPlayerUnits();

        AddEnemy(15100, 25);
    }


    public void LoadLevel1()
    {
        LoadPlayerUnits();

        AddEnemy(16101, 25);
        AddEnemy(16102, 22);
        AddEnemy(16103, 21);
    }



    public void LoadLevel2()
    {
        LoadPlayerUnits();

        AddEnemy(17101, 25);
        AddEnemy(16101, 26);
    }


    public void LoadLevel3()
    {
        LoadPlayerUnits();

        AddEnemy(18101, 21);
        AddEnemy(18101, 22);
        AddEnemy(18101, 25);
        AddEnemy(18101, 26);
    }


    public void LoadLevel4()
    {
        LoadPlayerUnits();

        AddEnemy(17101, 25);
        AddEnemy(17102, 26);
    }



    public void LoadLevel5()
    {
        LoadPlayerUnits();

        AddEnemy(18101, 25);
    }



    public void LoadLevel6()
    {
        LoadPlayerUnits();

        AddEnemy(18101, 25);
    }


    public void LoadLevel7()
    {
        LoadPlayerUnits();

        AddEnemy(17101, 24);
        AddEnemy(17102, 25);
        AddEnemy(17103, 26);
    }


    public void LoadLevel8()
    {
        LoadPlayerUnits();

        AddEnemy(18101, 25);
        AddEnemy(18102, 29);
    }


    public void LoadLevel9()
    {
        LoadPlayerUnits();

        AddEnemy(18101, 25);
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




    public void LoadPlayerUnits()
    {
        int i = 1;
        foreach (var unit in MMPlayerManager.instance.units)
        {
            MMUnitNode node = MMUnitNode.Create();
            node.group = 1;
            node.Accept(unit);
            units1.Add(node);
            MMMap.instance.FindCellOfIndex(i++).Accept(node);
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
