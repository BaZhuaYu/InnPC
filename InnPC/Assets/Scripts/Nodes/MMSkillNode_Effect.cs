using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMSkillNode : MMNode
{

    public MMEffect Create(MMUnitNode source, MMUnitNode target)
    {
        MMEffect effect = new MMEffect();
        effect.type = this.effectType;
        effect.value = this.value;
        effect.source = source;
        effect.target = target;

        switch (effect.type)
        {
            case MMEffectType.Attack:
                effect.userinfo.Add("TempATK", tempATK);
                effect.userinfo.Add("TempDEF", tempDEF);
                break;
            //case MMEffectType.InAP

            //case MMEffectType.InAP:
            //case MMEffectType.InHP:
            //case MMEffectType.InATK:
            //    effect.target = effect.source;
                //break;
            case MMEffectType.Summon:
                effect.destCell = effect.source.cell;
                break;
            default:
                break;
        }

        if (effect.target != null)
        {
            effect.sideTargets = FindSideTargets(effect.target.cell);
        }


        return effect;
    }






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
            case MMArea.Target:
                ret.Add(this.unit.FindTarget().cell);
                //ret = MMMap.Instance.FindCellsInCol(this.unit.cell.col);
                break;
            case MMArea.RaceUnits:
                ret = MMMap.Instance.FindCellsWithUnitRace(this.unit.race);
                break;

        }

        return ret;
    }





    public List<MMUnitNode> FindSideTargets(MMCell tagetCell)
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();
        //ret.Add(tagetCell.unitNode);

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
            targets = FindSideTargets(target);
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

        //眩晕状态不还击
        if (target.unitNode.unitState != MMUnitState.Stunned)
        {
            int value2 = Mathf.Max(target.unitNode.atk - this.tempDEF, 0);
            source.unitNode.DecreaseHP(value2);
        }


        //Target从Weak状态进入Stunned的状态时，Source可以连击
        bool flag1 = (target.unitNode.unitState == MMUnitState.Weak);
        if (target.unitNode.group == 2)
        {
            target.unitNode.DecreaseAP();
        }
        bool flag2 = (target.unitNode.unitState == MMUnitState.Stunned);
        if (flag1 && flag2)
        {
            source.unitNode.EnterPhase(MMUnitPhase.Combo);
        }


        //bool flag1 = (target.unitNode.unitState != MMUnitState.Stunned);
        ////敌方单位受到物理伤害减少1点行动力
        //if(target.unitNode.group == 2)
        //{
        //    target.unitNode.DecreaseAP();
        //}

        //bool flag2 = (target.unitNode.unitState == MMUnitState.Stunned);

        //if(flag1 && flag2)
        //{
        //    source.unitNode.EnterPhase(MMUnitPhase.Combo);
        //}


        ////眩晕状态不还击
        //if (target.unitNode.unitState != MMUnitState.Stunned)
        //{
        //    int value2 = Mathf.Max(target.unitNode.atk - this.tempDEF, 0);
        //    source.unitNode.DecreaseHP(value2);
        //}
    }



}
