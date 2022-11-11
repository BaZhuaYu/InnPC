using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{

    public void PlaySkill()
    {
        if (this.selectingSkill == null)
        {
            MMTipManager.instance.CreateTip("没有选择技能");
            return;
        }

        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("没有己方英雄");
            return;
        }
        
        sourceUnit.DecreaseAP(selectingSkill.cost);
        sourceUnit.numSkillUsed += 1;
        
        if(selectingSkill.type == MMSkillType.Power)
        {
            sourceUnit.IncreaseATK(selectingSkill.tempATK);
            sourceUnit.IncreaseHP(selectingSkill.tempDEF);
        }
        else
        {

        }

        MMEffect effect = selectingSkill.CreateEffect();
        effect.target = this.targetUnit;

        Debug.Log("aaaaaaaaaaa: " + effect.type);

        if(selectingSkill.id == 1034)
        {
            effect.value = 0;
            List<MMSkillNode> skills = sourceUnit.FindAllHistorySkills();
            foreach(var skill in skills)
            {
                if(skill.type == MMSkillType.Attack)
                {
                    effect.value += 1;
                }
            }
        }

        ExecuteEffect(effect);

        
        this.historySkills[round].Add(selectingSkill);

        MMSkillPanel.Instance.PlaySkill(selectingSkill);


        //Power Card
        if(selectingSkill.type == MMSkillType.Power)
        {
            selectingSkill.EnterState(MMSkillState.Used);

            foreach(var skill in selectingSkill.unit.skills)
            {
                if(skill.type == MMSkillType.Power)
                {
                    selectingSkill.isEnabled = false;
                }
            }
            selectingSkill.isEnabled = true;
        }

        //After
        if(selectingSkill.id == 1074)
        {
            if(effect.target.unitState == MMUnitState.Dead)
            {
                effect.source.IncreaseAP(1);
                Debug.Log("!!!!!!!!!!!!!!!selectingSkill: " + selectingSkill);
            }
        }

        

        //Final Card
        if (selectingSkill.keywords.Contains(MMSkillKeyWord.Final))
        {
            EnterState(MMBattleState.SourDone);
        }
        else
        {
            EnterState(MMBattleState.PlayedSkill);
        }

    }







    public void DrawSkill()
    {
        //MMCardPanel.Instance.Draw(count);

        MMSkillPanel.Instance.Accept(sourceUnit.skills);
    }


    public void ClearSelectSkill()
    {
        this.selectingSkill = null;
        MMSkillPanel.Instance.selectingSkill = null;
    }



    public void SelectSkill(MMSkillNode skill)
    {

        if (sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("需要选中己方英雄");
            return;
        }

        if (skill.cost > sourceUnit.ap)
        {
            MMTipManager.instance.CreateTip("行动力不足");
            return;
        }

        MMSkillPanel.Instance.selectingSkill = skill;
        selectingSkill = skill;

        if (skill.target == MMEffectTarget.None)
        {
            MMBattleManager.Instance.EnterState(MMBattleState.SelectSkill);
        }
        else
        {
            switch (skill.target)
            {
                case MMEffectTarget.Source:
                    this.targetUnit = this.sourceUnit;
                    break;
                case MMEffectTarget.Target:
                    this.targetUnit = this.sourceUnit.FindTarget();
                    break;
                default:
                    MMDebugManager.Warning("SelectSkill: " + skill.target);
                    break;
            }
            MMBattleManager.Instance.PlaySkill();
        }

    }
    

}
