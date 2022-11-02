using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{

    public List<MMCell> FindMoveCells()
    {
        List<MMCell> ret = new List<MMCell>();
        if (this.unitState == MMUnitState.Stunned)
        {
            return ret;
        }



        if (this.group == 1)
        {
            int min = this.cell.row;
            int max = MMMap.Instance.row + 1;
            
            MMUnitNode unit = MMBattleManager.Instance.FindFrontUnitOfGroup(2);
            if(unit != null)
            {
                max = unit.cell.row;
            }

            List<int> rows = new List<int>();
            for (int i = min; i < max; i++)
            {
                rows.Add(i);
            }
            return MMMap.Instance.FindCellsInRows(rows);
        }
        else
        {
            int max = this.cell.row;
            int min = -1;
            
            MMUnitNode unit = MMBattleManager.Instance.FindFrontUnitOfGroup(1);
            if (unit != null)
            {
                min = unit.cell.row;
            }
            List<int> rows = new List<int>();
            for (int i = min + 1; i <= max; i++)
            {
                rows.Add(i);
            }
            return MMMap.Instance.FindCellsInRows(rows);
        }
        
    }



    public void ShowMoveCells()
    {
        List<MMCell> cells = FindMoveCells();
        foreach (var cell in cells)
        {
            cell.HandleState(MMNodeState.Blue);
        }
    }


    public void HideMoveCells()
    {
        List<MMCell> cells = FindMoveCells();
        foreach (var cell in cells)
        {
            cell.HandleState(MMNodeState.Normal);
        }
    }


    public List<MMCell> FindAttackCells()
    {
        if (this.unitState == MMUnitState.Stunned)
        {
            return new List<MMCell>();
        }

        if (this.group == 1)
        {
            return MMMap.Instance.FindCellsInColGreaterThanCell(this.cell, this.attackRange);
        }
        else
        {
            return MMMap.Instance.FindCellsInColLessThanCell(this.cell, this.attackRange);
        }
    }

    public void ShowAttackCells()
    {
        List<MMCell> cells = FindAttackCells();
        foreach (var cell in cells)
        {
            cell.HandleState(MMNodeState.Blue);
            if (cell.unitNode != null)
            {
                if (cell.unitNode.group != this.group)
                {
                    cell.HandleHighlight(MMNodeHighlight.Red);
                }
                //else
                //{
                //    cell.EnterHighlight(MMNodeHighlight.Green);
                //}
            }
        }
    }


    public void HideAttackCells()
    {
        List<MMCell> cells = FindAttackCells();
        foreach (var cell in cells)
        {
            cell.HandleState(MMNodeState.Normal);
            if (cell.unitNode != null)
            {
                cell.HandleHighlight(MMNodeHighlight.Normal);
            }
        }
    }


    public void MoveToCell(MMCell cell)
    {
        int dis = this.cell.FindDistanceFromCell(cell);
        this.spd = 0;
        cell.Accept(this);
    }



}
