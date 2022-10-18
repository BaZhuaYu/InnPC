using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class MMBattleManager : MonoBehaviour
{
    
    public List<MMUnitNode> FindAllUnits()
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();

        foreach (var unit in units1)
        {
            ret.Add(unit);
        }

        foreach (var unit in units2)
        {
            ret.Add(unit);
        }

        return ret;
    }


    public void BroadCast(MMTriggerTime time)
    {
        MMDebugManager.Log("" + time);

        foreach (var unit in FindAllUnits())
        {
            foreach (var skill in unit.skills)
            {
                if (skill.time == time)
                {
                    MMDebugManager.Warning("Unit:" + unit.displayName + " skill: " + skill.displayName);
                    //skill.ExecuteEffect(sourceUnit.cell, targetUnit.cell);
                    MMEffect effect = skill.CreateEffect();

                    //switch (time)
                    //{
                    //    case MMTriggerTime.OnSummon:
                    //        if (unit == eff.source)
                    //        {
                    //            effect = skill.Create(unit, eff.target);
                    //        }
                    //        break;
                    //    case MMTriggerTime.OnDead:
                    //        Debug.Log("" + unit.displayName + "  " + skill.displayName + "  " + time);
                    //        break;
                    //    default:
                    //        //effect = skill.Create(unit, null);
                    //        break;
                    //}

                    ExecuteEffect(effect);
                }
            }
        }
    }


    public void BroadCastOnDead(MMUnitNode unit)
    {
        List<MMEffect> effects = unit.CreateEffect(MMTriggerTime.OnDead);
        
        foreach (var effect in effects)
        {
            ExecuteEffect(effect);
        }
    }
    

    public void BroadCastUnitSkill(MMTriggerTime time, MMUnitNode unit)
    {
        foreach(var skill in unit.skills)
        {
            if(skill.time == time)
            {
                MMEffect effect = skill.CreateEffect();
                
                switch(effect.type)
                {
                    case MMEffectType.Summon:
                        effect.destCell = unit.cell;
                        break;
                    default:
                        break;
                }

                ExecuteEffect(effect);
            }
        }
        
    }



    public void ExecuteEffectOnGain(MMEffect effect)
    {
        switch (effect.type)
        {
            case MMEffectType.InATK:
                effect.target.unit.atk += 1;
                break;
            case MMEffectType.InHP:
                effect.target.unit.maxHP += 1;
                effect.target.unit.hp += 1;
                break;
            case MMEffectType.InAP:
                effect.target.unit.ap += 1;
                break;    
            default:
                break;
        }
    }


    public void ExecuteEffectOnSummon(MMEffect effect)
    {

    }

    public void ExecuteEffectOnDead(MMEffect effect)
    {

    }



    //public IEnumerator Execute()
    //{
    //    while(true)
    //    {
    //        if (effects.Count > 0)
    //        {
    //            ExecuteEffect(effects[0]);
    //            effects.RemoveAt(0);
    //        }
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}


    //public void Trigger




    public void ExecuteEffect(MMEffect effect)
    {
        Debug.Log(effect.type + " " + effect.value);

        switch (effect.type)
        {
            case MMEffectType.Attack:
                Attack(effect);
                break;
            case MMEffectType.InHP:
                this.InHP(effect);
                break;
            case MMEffectType.DeHP:
                this.DeHP(effect);
                break;

            case MMEffectType.InAP:
                this.InAP(effect);
                break;
            case MMEffectType.DeAP:
                this.DeAP(effect);
                break;

            case MMEffectType.InATK:
                this.InATK(effect);
                break;
            case MMEffectType.DeATK:
                this.DeATK(effect);
                break;
                
            case MMEffectType.Damage:
                Damage(effect);
                break;

            case MMEffectType.Summon:
                Summon(effect, null, null);
                break;

            case MMEffectType.TempATKDEF:


            default:
                MMDebugManager.Log("Not Find" + effect.type);
                break;
        }

    }



    private void Attack(MMEffect effect)
    {
        int tempATK = effect.userinfo["TempATK"];
        int tempDEF = effect.userinfo["TempDEF"];
        MMUnitNode source = effect.source;
        MMUnitNode target = effect.target;

        target.DecreaseHP(source.atk + tempATK);
        if(source.hengsao > 0)
        {
            List<MMCell> cells = MMMap.Instance.FindCellsBeside(target.cell);
            foreach(var cell in cells)
            {
                if(cell.unitNode != null)
                {
                    cell.unitNode.DecreaseHP(source.hengsao);
                }
            }
            //List<MMUnitNode> units = source.find
        }
        if (source.guanchuan > 0)
        {
            List<MMCell> cells = MMMap.Instance.FindCellsBehind(target.cell);
            foreach (var cell in cells)
            {
                if (cell.unitNode != null)
                {
                    cell.unitNode.DecreaseHP(source.guanchuan);
                }
            }
            //List<MMUnitNode> units = source.find
        }



        //if (target.unitState != MMUnitState.Stunned)
        //{
        //    //眩晕状态不还击    
        //}
        //else
        //{
        //    //还击
        //    int value2 = Mathf.Max(target.atk - tempDEF, 0);
        //    source.DecreaseHP(value2);
        //}
        //还击
        int value2 = Mathf.Max(target.atk - tempDEF, 0);
        source.DecreaseHP(value2);


        //Target从Weak状态进入Stunned的状态时，Source可以连击
        //bool flag1 = (target.unitState == MMUnitState.Weak);
        //if (target.group == 2)
        //{
        //    target.DecreaseAP();
        //}
        //bool flag2 = (target.unitState == MMUnitState.Stunned);
        //if (flag1 && flag2)
        //{
        //    source.EnterPhase(MMUnitPhase.Combo);
        //}
    }


    private void InHP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.IncreaseHP(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.IncreaseHP(effect.value);
        }
    }

    private void DeHP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseHP(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseHP(effect.value);
        }
    }

    private void InAP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.IncreaseAP();
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.IncreaseAP();
        }
    }

    private void DeAP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseAP();
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseAP();
        }
    }

    private void InATK(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.IncreaseATK(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.IncreaseATK(effect.value);
        }
    }

    private void DeATK(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseATK(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseATK(effect.value);
        }
    }

    private void Damage(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseHP(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseHP(effect.value);
        }
    }

    private void Summon(MMEffect effect, MMUnit unit, MMCell cell)
    {
        MMUnitNode node1 = MMUnitNode.CreateFromID(effect.value);
        node1.group = effect.source.group;

        MMCell c;
        int row;

        
        

        //if(effect.destCell.Accept(node1) == false)
        //{
        //    MMTipManager.instance.CreateTip("存在单位");
        //    return;
        //}


        if (node1.group == 1)
        {
            row = 3 - node1.clss;
            c = MMMap.Instance.FindRandomEmptyCellInRow(row);
            units1.Add(node1);
        }
        else
        {
            row = node1.clss + 3;
            c = MMMap.Instance.FindRandomEmptyCellInRow(row);
            units2.Add(node1);
        }

        c.Accept(node1);
        effect.target = node1;
        //BroadCast(MMTriggerTime.OnSummon, effect);
        ExecuteEffectOnSummon(effect);

    }


    private void TempATKDEF(MMEffect effect)
    {
        effect.target.tempATK += effect.value;
        effect.target.tempDEF += effect.value;
    }

}
