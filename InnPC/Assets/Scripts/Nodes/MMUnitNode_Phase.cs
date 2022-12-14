using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{

    public void OnBattleBegin()
    {

    }
    
    public void OnRoundBegin()
    {
        tempCell = this.cell;
        if (this.HasSkillEnabled(10300))
        {
            this.IncreaseAP(1);
        }
    }

    public void OnRoundEnd()
    {
        
    }


    public void OnActive()
    {
        this.isActived = true;
    }


    public void OnLeave()
    {
        foreach(var skill in skills)
        {
            if(skill.time == MMTriggerTime.OnLeave)
            {
                MMEffect effect = skill.CreateEffect();
                MMBattleManager.Instance.ExecuteEffect(effect);
            }
        }


        //if(HasSkillEnabled(16100))
        //{
        //    public MMEffect CreateEffect()
        //    {
        //        MMEffect effect = new MMEffect();
        //        effect.type = this.effectType;
        //        effect.area = this.area;
        //        effect.value = this.value;
        //        effect.source = this.unit;

        //        switch (this.target)
        //        {
        //            case MMEffectTarget.Source:
        //                effect.target = this.unit;
        //                break;
        //            case MMEffectTarget.Target:
        //                effect.target = this.unit.FindTarget();
        //                break;
        //            default:
        //                break;
        //        }

        //        switch (this.type)
        //        {
        //            case MMSkillType.Attack:
        //            case MMSkillType.Power:
        //                effect.userinfo.Add("TempATK", tempATK);
        //                effect.userinfo.Add("TempDEF", tempDEF);
        //                break;
        //            default:
        //                break;
        //        }

        //        if (effect.target != null)
        //        {
        //            effect.sideTargets = FindSideTargets(effect.target.cell);
        //        }

        //        return effect;
        //    }
        //}
    }


    public void OnBeforeAttack(MMUnitNode defender)
    {

    }

    public void OnAfterAttack(MMUnitNode defender)
    {

    }

    public void OnAfterBeAttack(MMUnitNode attacker)
    {
        if(this.HasSkillEnabled(10100))
        {
            this.IncreaseHP(1);
        }
    }

    public void OnBeforeBeAttack(MMUnitNode attacker)
    {

    }

    public void OnKill(MMUnitNode target)
    {
        if (this.HasSkillEnabled(10200))
        {
            this.IncreaseATK(1);
        }
    }

    public void OnDead(MMUnitNode attacker)
    {

    }

    public void OnSummon(MMUnitNode unit)
    {

    }


}
