using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{

    /// <summary>
    /// Find
    /// </summary>
    /// <returns></returns>


    public void AutoSelectSour()
    {
        List<MMUnitNode> units = FindSortedUnits1();

        foreach (var unit in units)
        {
            if (unit.isActived == false)
            {
                TryEnterPhase_UnitBegin(unit);
                return;
            }
        }

        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("己方回合行动结束");
        }
    }

    
    public List<MMUnitNode> FindSortedUnits1()
    {
        int row = MMMap.Instance.row;
        int col = MMMap.Instance.col;

        List<MMUnitNode> ret = new List<MMUnitNode>();

        List<int> indexes = new List<int>();
        for (int i = row - 1; i >= 0; i--)
        {
            for (int j = 0; j < col; j++)
            {
                indexes.Add(i * col + j);
            }
        }

        foreach (var index in indexes)
        {
            MMCell cell = MMMap.Instance.FindCellOfIndex(index);
            if (cell.unitNode != null && cell.unitNode.group == 1)
            {
                ret.Add(cell.unitNode);
            }
        }

        return ret;
    }


    public List<MMUnitNode> FindSortedUnits2()
    {
        int row = MMMap.Instance.row;
        int col = MMMap.Instance.col;

        List<MMUnitNode> ret = new List<MMUnitNode>();

        List<int> indexes = new List<int>();
        for (int i = 0; i <= row - 1; i++)
        {
            for (int j = 0; j < col; j++)
            {
                indexes.Add(i * col + j);
            }
        }


        foreach (var index in indexes)
        {
            MMCell cell = MMMap.Instance.FindCellOfIndex(index);
            if (cell.unitNode != null && cell.unitNode.group == 2)
            {
                ret.Add(cell.unitNode);
            }
        }

        return ret;
    }
    
    
}
