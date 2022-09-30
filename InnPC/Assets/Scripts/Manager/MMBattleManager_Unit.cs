using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{
    
    public void SetSource(MMUnitNode unit)
    {
        sourceUnit = unit;
        sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
        MMCardManager.instance.ShowHandCards(sourceUnit.cards);
        sourceUnit.ShowMoveCells();
        sourceUnit.ShowAttackCells();
    }


    public void SetTarget(MMUnitNode unit)
    {
        targetUnit = unit;
        targetUnit.cell.EnterHighlight(MMNodeHighlight.Red);

        sourceUnit.HideMoveCells();
        sourceUnit.HideAttackCells();
        sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
    }


    public void SetSelectSkill(MMSkillNode card)
    {
        this.selectedSkill = card;
        card.MoveUp(20);
        sourceUnit.ShowAttackCells();

        if (card.area == MMArea.None)
        {
            MMBattleManager.instance.PlaySkill();
        }
        else
        {
            MMBattleManager.instance.EnterState(MMBattleState.SelectSkill);
        }
    }



    public void ClearSource()
    {
        if (sourceUnit == null)
        {
            return;
        }
        sourceUnit.cell.EnterState(MMNodeState.Normal);
        sourceUnit.cell.EnterHighlight(MMNodeHighlight.Normal);
        sourceUnit.HideMoveCells();
        sourceUnit.HideAttackCells();
        MMCardManager.instance.HideHandCards();
        sourceUnit = null;
    }


    public void ClearTarget()
    {
        if (targetUnit == null)
        {
            return;
        }
        targetUnit.cell.EnterState(MMNodeState.Normal);
        targetUnit.cell.EnterHighlight(MMNodeHighlight.Normal);
        targetUnit = null;
    }


    public void UnselectSourceCell()
    {
        this.ClearSource();
        this.ClearTarget();
    }




    public void OnSourActionDone()
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
        
        EnterState(MMBattleState.Normal);

        
        for (int i = 0; i < units1.Count; i++)
        {
            if (units1[i].unitState == MMUnitState.Dead)
            {
                units1[i].Clear();
                this.units1.Remove(units1[i]);
            }
        }

        for (int i = 0; i < units2.Count; i++)
        {
            if (units2[i].unitState == MMUnitState.Dead)
            {
                units2[i].Clear();
                this.units2.Remove(units2[i]);
            }
        }


        if (CheckGameLost())
        {
            MMGameOverManager.instance.SetLost();
        }
        else if (CheckGameWin())
        {
            MMGameOverManager.instance.SetWin();
        }
        else
        {
            AutoSelectSour();
        }
        
    }


    public bool CheckGameWin()
    {
        if (units2.Count == 0)
        {
            return true;
        }

        foreach (var unit in units2)
        {
            if(unit.unitState != MMUnitState.Dead)
            {
                return false;
            }
        }

        return true;
    }


    public bool CheckGameLost()
    {
        if(units1.Count == 0)
        {
            return true;
        }

        foreach (var unit in units1)
        {
            if (unit.unitState != MMUnitState.Dead)
            {
                return false;
            }
        }

        return true;
    }




    /// <summary>
    /// Find
    /// </summary>
    /// <returns></returns>



    public List<MMUnitNode> FindSortedUnits1()
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();

        List<int> indexes = new List<int>();
        for (int i = 8; i >= 0; i--)
        {
            for (int j = 0; j < 4; j++)
            {
                indexes.Add(i * 4 + j);
            }
        }

        foreach (var index in indexes)
        {
            MMCell cell = MMMap.instance.FindCellOfIndex(index);
            if (cell.nodeUnit != null && cell.nodeUnit.group == 1)
            {
                ret.Add(cell.nodeUnit);
            }
        }

        return ret;
    }


    public List<MMUnitNode> FindSortedUnits2()
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();

        List<int> indexes = new List<int>();
        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                indexes.Add(i * 4 + j);
            }
        }


        foreach (var index in indexes)
        {
            MMCell cell = MMMap.instance.FindCellOfIndex(index);
            if (cell.nodeUnit != null && cell.nodeUnit.group == 2)
            {
                ret.Add(cell.nodeUnit);
            }
        }

        return ret;
    }



}
