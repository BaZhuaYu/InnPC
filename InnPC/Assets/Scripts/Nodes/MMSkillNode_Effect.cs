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
                ret = MMMap.instance.FindCellsInRow(target);
                break;
            case MMArea.Col:
                ret = MMMap.instance.FindCellsInCol(target);
                break;
            case MMArea.Beside:
                ret = MMMap.instance.FindCellsBeside(target);
                break;
            case MMArea.Behind:
                ret = MMMap.instance.FindCellsBehind(target);
                break;
        }

        return ret;
    }


    public List<MMUnitNode> FindTargetUnits(MMCell tagetCell)
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();
        ret.Add(tagetCell.nodeUnit);

        List<MMCell> cells = FindSideTargetCells(tagetCell);
        foreach (var cell in cells)
        {
            if (cell.nodeUnit != null)
            {
                ret.Add(cell.nodeUnit);
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
            source.nodeUnit.EnterState(MMUnitState.Stunned);
        }
        else if (id == 1000)
        {
            source.nodeUnit.IncreaseAP();
        }
        else if (id == 10000)
        {
            HandleAttackCard(source, target);
        }
        else if (id == 10101)
        {
            target.nodeUnit.DecreaseHP(2);
            source.nodeUnit.IncreaseHP(2);
        }
        else if (id == 10201)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(source.nodeUnit.atk);
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
            source.nodeUnit.IncreaseHP(4);
        }
        else if (id == 16101)
        {
            HandleAttackCard(source, target);
        }
    }














    public void HandleAttackCard(MMCell source, MMCell target)
    {
        target.nodeUnit.DecreaseHP(source.nodeUnit.atk + this.tempATK);

        bool flag1 = (target.nodeUnit.unitState != MMUnitState.Stunned);
        //敌方单位受到物理伤害减少1点行动力
        if(target.nodeUnit.group == 2)
        {
            target.nodeUnit.DecreaseAP();
        }

        bool flag2 = (target.nodeUnit.unitState == MMUnitState.Stunned);

        if(flag1 && flag2)
        {
            source.nodeUnit.EnterPhase(MMUnitPhase.Combo);
        }


        //眩晕状态不还击
        if (target.nodeUnit.unitState != MMUnitState.Stunned)
        {
            int value2 = Mathf.Max(target.nodeUnit.atk - this.tempDEF, 0);
            source.nodeUnit.DecreaseHP(value2);
        }
    }








}
