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


    public void SetSelectingSkill(MMSkillNode skill)
    {
        this.selectingSkill = skill;
        skill.MoveUp(20);
        sourceUnit.ShowAttackCells();

        if (skill.area == MMArea.None)
        {
            MMBattleManager.Instance.PlaySkill();
        }
        else
        {
            MMBattleManager.Instance.EnterState(MMBattleState.SelectSkill);
        }
    }



    public void ClearSource()
    {
        if (sourceUnit == null)
        {
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
        targetUnit.cell.HandleState(MMNodeState.Normal);
        targetUnit.cell.HandleHighlight(MMNodeHighlight.Normal);
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
            MMGameOverManager.Instance.SetLost();
        }
        else if (CheckGameWin())
        {
            MMGameOverManager.Instance.SetWin();
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
