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


    public void BroadCast(MMTriggerTime time, MMEffect eff)
    {
        foreach (var unit in FindAllUnits())
        {
            foreach (var skill in unit.skills)
            {
                if (skill.time == time)
                {
                    //skill.ExecuteEffect(sourceUnit.cell, targetUnit.cell);
                    MMEffect effect = skill.Create(unit, unit);

                    switch (time)
                    {
                        case MMTriggerTime.OnSummon:
                            if (unit == eff.source)
                            {
                                effect = skill.Create(unit, eff.target);
                            }
                            break;
                        case MMTriggerTime.OnDead:
                            Debug.Log("" + unit.displayName + "  " + skill.displayName + "  " + time);
                            break;
                        default:
                            //effect = skill.Create(unit, null);
                            break;
                    }

                    ExecuteEffect(effect);
                }
            }
        }
    }


    public void BroadCastUnitSkill(MMTriggerTime time, MMUnitNode unit)
    {
        foreach(var skill in unit.skills)
        {
            if(skill.time == time)
            {
                MMEffect effect = skill.Create(unit, unit);
                
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
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseHP(source.atk + tempATK);
        }


        if (target.unitState != MMUnitState.Stunned)
        {
            //眩晕状态不还击    
        }
        else
        {
            //还击
            int value2 = Mathf.Max(target.atk - tempDEF, 0);
            source.DecreaseHP(value2);
        }

        //Target从Weak状态进入Stunned的状态时，Source可以连击
        bool flag1 = (target.unitState == MMUnitState.Weak);
        if (target.group == 2)
        {
            target.DecreaseAP();
        }
        bool flag2 = (target.unitState == MMUnitState.Stunned);
        if (flag1 && flag2)
        {
            source.EnterPhase(MMUnitPhase.Combo);
        }
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
        
        if(effect.destCell.Accept(node1) == false)
        {
            MMTipManager.instance.CreateTip("存在单位");
            return;
        }


        if (node1.group == 1)
        {
            units1.Add(node1);
        }
        else
        {
            units2.Add(node1);
        }
        effect.target = node1;
        BroadCast(MMTriggerTime.OnSummon, effect);

    }


    private void TempATKDEF(MMEffect effect)
    {
        effect.target.tempATK += effect.value;
        effect.target.tempDEF += effect.value;
    }

}
