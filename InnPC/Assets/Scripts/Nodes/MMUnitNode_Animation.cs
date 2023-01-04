using System.Collections;
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

    public List<int> FindMoveCellRows()
    {
        List<int> ret = new List<int>();
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
            return rows;
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
            return rows;
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
        //foreach (var cell in cells)
        //{
        //    cell.HandleHighlight(MMNodeHighlight.Green);
        //}

        foreach(var cell in MMMap.Instance.cells)
        {
            if(tempMoveCells.Contains(cell))
            {
                cell.HideDark();
            }
            else
            {
                cell.ShowDark();
            }
        }


        //foreach (var row in FindMoveCellRows())
        //{
        //    MMMap.Instance.ShowHighlightRow(row);
        //}

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

        foreach (var cell in MMMap.Instance.cells)
        {
            cell.HideDark();
        }


        //MMMap.Instance.HideHightlightRow();
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
            cell.HandleHighlight(MMNodeHighlight.Red);
            //if (cell.unitNode != null)
            //{
            //    if (cell.unitNode.group != this.group)
            //    {
            //        cell.HandleHighlight(MMNodeHighlight.Red);
            //    }
            //}
        }
    }

    public void HideAttackCells()
    {
        if (tempAttackCells == null)
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


    public void ShowTarget()
    {
        MMUnitNode target = this.FindTarget();
        if (target != null)
        {
            target.HandleHighlight(MMNodeHighlight.Red);
        }
    }

    public void HideTarget()
    {
        MMUnitNode target = this.FindTarget();
        if (target != null)
        {
            target.HandleHighlight(MMNodeHighlight.Normal);
        }
    }


    public void ShowWillMove(MMCell cell)
    {
        MMUnitNode node = MMUnitNode.Create(this.unit);
        node.name = "WillMoveNode";
        Color c = node.GetComponent<Image>().color;
        node.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.5f);
        cell.AddChild(node);

        MMUnitNode target = this.FindTarget();
        if (target != null)
        {
            MMNode targetNode = MMNode.Create("CellBorder_Red");
            targetNode.gameObject.name = "WillMoveTargetNode";
            this.AddChild(targetNode);
            targetNode.transform.position = target.transform.position;
        }
    }

    public void HideWillMove(MMCell cell)
    {
        Transform a = cell.gameObject.transform.Find("WillMoveNode");
        if (a != null)
        {
            Destroy(a.gameObject);
        }

        Transform b = this.gameObject.transform.Find("WillMoveTargetNode");
        if (b != null)
        {
            Destroy(b.gameObject);
        }
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


    public void ShowCard()
    {
        MMCardNode tempCard = MMCardNode.Create();
        tempCard.Accept(MMCard.Create(this.unit.cards[0]));
        tempCard.transform.SetSiblingIndex(1000);
        tempCard.SetParent(MMMap.Instance);
        tempCard.name = "TempCard";
        if (this.group == 1)
        {
            tempCard.MoveLeft(MMMap.Instance.FindWidth() / 2 + tempCard.FindWidth());
        }
        else
        {
            tempCard.MoveRight(MMMap.Instance.FindWidth() / 2 + tempCard.FindWidth());
        }
    }


    public void HideCard()
    {
        MMCardNode[] nodes = MMMap.Instance.gameObject.GetComponentsInChildren<MMCardNode>();
        foreach (var node in nodes)
        {
            Destroy(node.gameObject);
        }
    }


}
