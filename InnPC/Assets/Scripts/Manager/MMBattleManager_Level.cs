using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{

    public void LoadLevel(int id)
    {
        
        MMLevel level = MMLevel.Create(id);
        foreach (var unit in level.enemies)
        {
            AddEnemy(unit.Value.id, unit.Key);
        }
    }
    
    

    public void LoadPlayerUnits()
    {
        int i = 1;
        foreach (var unit in MMPlayerManager.Instance.units)
        {
            MMUnitNode node = MMUnitNode.Create();
            node.group = 1;
            node.Accept(unit);
            units1.Add(node);
            MMMap.Instance.FindCellOfIndex(i++).Accept(node);
        }
    }


    

    public void AddEnemy(int id, int pos)
    {
        MMUnit unit2 = MMUnit.Create(id);
        MMUnitNode node2 = MMUnitNode.Create();
        node2.group = 2;
        node2.Accept(unit2);
        units2.Add(node2);
        MMMap.Instance.FindCellOfIndex(pos).Accept(node2);
    }


    public void AddEnemy(int id, int row, int col)
    {
        MMUnit unit2 = MMUnit.Create(id);
        MMUnitNode node2 = MMUnitNode.Create();
        node2.group = 2;
        node2.Accept(unit2);
        units2.Add(node2);
        MMMap.Instance.FindCellInXY(row, col).Accept(node2);
    }
}
