﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMUnitNode : MMNode
{

    List<MMCell> tempMoveCells;
    List<MMCell> tempAttackCells;

    public List<MMCell> FindMovableCells()
    {
        List<MMCell> ret = new List<MMCell>();
        if (this.state == MMUnitState.Stunned)
        {
            return ret;
        }

        if (this.group == 1)
        {
            int min = this.tempCell.row;
            int max = MMMap.Instance.row + 1;

            MMUnitNode unit = MMBattleManager.Instance.FindFrontUnitOfGroup(2);
            if (unit != null)
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
            int max = this.tempCell.row;
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
        if (isMoved)
        {
            return;
        }

        List<MMCell> cells = FindMovableCells();
        tempMoveCells = cells;
        foreach (var cell in cells)
        {
            cell.HandleHighlight(MMNodeHighlight.Green);
        }
    }


    public void HideMoveCells()
    {
        if (tempMoveCells == null)
        {
            return;
        }

        foreach (var cell in tempMoveCells)
        {
            cell.HandleHighlight(MMNodeHighlight.Normal);
        }
        
    }


    public List<MMCell> FindAttackCells()
    {
        List<MMCell> ret = new List<MMCell>();
        if (this.state == MMUnitState.Stunned)
        {
            return ret;
        }

        if (this.group == 1)
        {
            ret = MMMap.Instance.FindCellsInColGreaterThanCell(this.cell, this.attackRange);
        }
        else
        {
            ret = MMMap.Instance.FindCellsInColLessThanCell(this.cell, this.attackRange);
        }
        
        return ret;
    }


    public void ShowAttackCells()
    {
        tempAttackCells = FindAttackCells();
        foreach (var cell in tempAttackCells)
        {
            cell.HandleHighlight(MMNodeHighlight.Blue);
            if (cell.unitNode != null)
            {
                if (cell.unitNode.group != this.group)
                {
                    cell.HandleHighlight(MMNodeHighlight.Red);
                }
            }
        }
        
    }


    public void HideAttackCells()
    {
        if(tempAttackCells == null)
        {
            return;
        }
        List<MMCell> cells = tempAttackCells;
        foreach (var cell in cells)
        {
            cell.HandleHighlight(MMNodeHighlight.Normal);
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


    public void ShowSelected()
    {
        //if(this.cell !=null)
        //{
        //    this.cell.HandleHighlight(MMNodeHighlight.Green);
        //}
        this.HandleHighlight(MMNodeHighlight.Green);
    }

    public void HideSelected()
    {
        //if (this.cell != null)
        //{
        //    this.cell.HandleHighlight(MMNodeHighlight.Normal);
        //}
        this.HandleHighlight(MMNodeHighlight.Normal);
    }



    public void PlayAnimationHurt(int value)
    {
        m_DamageAnimator.gameObject.SetActive(true);
        m_DamageAnimator.SetTrigger("Show");
        m_DamageText.text = "-" + value;
        Invoke("aaa", 1f);
    }

    public void aaa()
    {
        m_DamageAnimator.gameObject.SetActive(false);
    }






}
