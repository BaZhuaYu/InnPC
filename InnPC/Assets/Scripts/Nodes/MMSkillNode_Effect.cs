using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMSkillNode : MMNode
{


    public List<MMCell> FindSideTargetCells(MMCell target)
    {
        List<MMCell> ret = new List<MMCell>();
        switch (this.area)
        {
            case MMArea.Single:
                break;
            case MMArea.Row:
                ret = MMMap.Instance.FindCellsInRow(target);
                break;
            case MMArea.Col:
                ret = MMMap.Instance.FindCellsInCol(target);
                break;
            case MMArea.Beside:
                ret = MMMap.Instance.FindCellsBeside(target);
                break;
            case MMArea.Behind:
                ret = MMMap.Instance.FindCellsBehind(target);
                break;
        }

        return ret;
    }


    public List<MMUnitNode> FindTargetUnits(MMCell tagetCell)
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();
        ret.Add(tagetCell.unitNode);

        List<MMCell> cells = FindSideTargetCells(tagetCell);
        foreach (var cell in cells)
        {
            if (cell.unitNode != null)
            {
                ret.Add(cell.unitNode);
            }
        }

        return ret;
    }





    public void ExecuteEffect(MMCell source, MMCell target)
    {
        List<MMUnitNode> targets = null;

        if (this.area == MMArea.None)
        {

        }
        else
        {
            targets = FindTargetUnits(target);
        }

        
        if (id == 1)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(3);
            }
        }
        else if (id == 2)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(2);
            }
        }
        else if (id == 3)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(1);
            }
        }
        else if (id == 100)
        {
            source.unitNode.EnterState(MMUnitState.Stunned);
        }
        else if (id == 1000)
        {
            source.unitNode.IncreaseAP();
        }
        else if (id == 10000)
        {
            HandleAttackCard(source, target);
        }
        else if (id == 10101)
        {
            target.unitNode.DecreaseHP(2);
            source.unitNode.IncreaseHP(2);
        }
        else if (id == 10201)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(source.unitNode.atk);
            }
        }
        else if (id == 10301)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(2);
            }
        }
        else if (id == 10401)
        {
            source.unitNode.IncreaseHP(4);
        }
        else if (id == 16101)
        {
            HandleAttackCard(source, target);
        }
    }














    public void HandleAttackCard(MMCell source, MMCell target)
    {
        target.unitNode.DecreaseHP(source.unitNode.atk + this.tempATK);

        bool flag1 = (target.unitNode.unitState != MMUnitState.Stunned);
        //敌方单位受到物理伤害减少1点行动力
        if(target.unitNode.group == 2)
        {
            target.unitNode.DecreaseAP();
        }

        bool flag2 = (target.unitNode.unitState == MMUnitState.Stunned);

        if(flag1 && flag2)
        {
            source.unitNode.EnterPhase(MMUnitPhase.Combo);
        }


        //眩晕状态不还击
        if (target.unitNode.unitState != MMUnitState.Stunned)
        {
            int value2 = Mathf.Max(target.unitNode.atk - this.tempDEF, 0);
            source.unitNode.DecreaseHP(value2);
        }
    }








}
