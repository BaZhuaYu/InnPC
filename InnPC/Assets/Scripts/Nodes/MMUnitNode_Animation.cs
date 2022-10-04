using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{
    
    public List<MMCell> FindMoveCells()
    {

        if (this.unitState == MMUnitState.Stunned)
        {
            return new List<MMCell>();
        }


        List<int> rows = new List<int>();
        
        MMUnitNode unit;
        if(this.group == 1)
        {
            unit = MMBattleManager.Instance.FindFrontUnitOfGroup(2);
        }
        else
        {
            unit = MMBattleManager.Instance.FindFrontUnitOfGroup(1);
        }
        
        for (int i = this.cell.row; i < unit.cell.row; i++)
        {
            rows.Add(i);
        }
        return MMMap.Instance.FindCellsInRows(rows);

        //return MMMap.instance.FindCellsWithinDistance(this.cell, this.spd);
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
