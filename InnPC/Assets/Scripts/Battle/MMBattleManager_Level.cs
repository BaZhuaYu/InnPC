using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MMNode
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
        Debug.Log("LoadPlayerUnits: " + MMExplorePanel.Instance.units.Count);
        int i = 0;
        foreach (var unit in MMExplorePanel.Instance.units)
        {
            Debug.Log("LoadPlayerUnits: " + unit.displayName);
            MMUnitNode node = MMUnitNode.Create();
            node.gameObject.AddComponent<MMUnitNode_Battle>();
            node.group = 1;
            node.Accept(unit);
            units1.Add(node);
            //MMMap.Instance.FindCellOfIndex(i++).Accept(node);

            if(node.clss == 1)
            {
                MMMap.Instance.FindRandomEmptyCellInRow(2).Accept(node);
            }
            else if (node.clss == 2)
            {
                MMMap.Instance.FindRandomEmptyCellInRow(1).Accept(node);
            }
            else if (node.clss == 3)
            {
                MMMap.Instance.FindRandomEmptyCellInRow(0).Accept(node);
            }
            else
            {
                MMMap.Instance.FindRandomEmptyCellInRow(0).Accept(node);
            }
        }
    }
    

    public void AddEnemy(int id, int pos)
    {
        MMUnit unit2 = MMUnit.Create(id);
        MMUnitNode node2 = MMUnitNode.Create();
        node2.gameObject.AddComponent<MMUnitNode_Battle>();
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
