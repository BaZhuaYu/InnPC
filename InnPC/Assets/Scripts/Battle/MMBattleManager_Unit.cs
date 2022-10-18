using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{

    public void SetSource(MMUnitNode unit)
    {
        sourceUnit = unit;
        sourceUnit.cell.HandleHighlight(MMNodeHighlight.Green);
        sourceUnit.ShowMoveCells();
        sourceUnit.ShowAttackCells();
    }


    public void SetTarget(MMUnitNode unit)
    {
        targetUnit = unit;
        targetUnit.cell.HandleHighlight(MMNodeHighlight.Red);

        sourceUnit.HideMoveCells();
        sourceUnit.HideAttackCells();
        sourceUnit.cell.HandleHighlight(MMNodeHighlight.Green);
    }


    



    public void ClearSource()
    {
        if (sourceUnit == null)
        {
            return;
        }

        if (sourceUnit.cell == null)
        {
            sourceUnit = null;
            return;
        }

        sourceUnit.cell.HandleState(MMNodeState.Normal);
        sourceUnit.cell.HandleHighlight(MMNodeHighlight.Normal);
        sourceUnit.HideMoveCells();
        sourceUnit.HideAttackCells();
        sourceUnit = null;
    }


    public void ClearTarget()
    {
        if (targetUnit == null)
        {
            return;
        }

        if (targetUnit.cell == null)
        {
            targetUnit = null;
            return;
        }

        targetUnit.cell.HandleState(MMNodeState.Normal);
        targetUnit.cell.HandleHighlight(MMNodeHighlight.Normal);
        targetUnit = null;
    }


    public void UnselectSourceCell()
    {
        this.ClearSource();
        this.ClearTarget();
    }




    public void AutoSelectSour()
    {
        List<MMUnitNode> units = FindSortedUnits1();
        foreach (var unit in units)
        {
            if (unit.unitPhase == MMUnitPhase.Combo)
            {
                SetSource(unit);
                EnterState(MMBattleState.SelectSour);
                return;
            }
        }

        foreach (var unit in units)
        {
            if (unit.unitPhase == MMUnitPhase.Normal)
            {
                SetSource(unit);
                EnterState(MMBattleState.SelectSour);
                return;
            }
        }


        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("己方回合行动结束");
        }
    }



    public void HandleSourceActionDone()
    {
        sourceUnit.tempCell = sourceUnit.cell;

        if (sourceUnit.unitPhase == MMUnitPhase.Combo)
        {
            sourceUnit.EnterPhase(MMUnitPhase.Normal);
        }
        else
        {
            sourceUnit.EnterPhase(MMUnitPhase.Actived);
        }
        
        //ClearUnitsInMap();

        foreach (var unit in FindAllUnits())
        {
            if (unit.unitState == MMUnitState.Dead)
            {
                BroadCastOnDead(unit);
                unit.Clear();
            }
        }

        
    }



    /// <summary>
    /// Find
    /// </summary>
    /// <returns></returns>



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
