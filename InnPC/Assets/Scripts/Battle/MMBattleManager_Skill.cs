using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{

    public void HandlePlaySkill()
    {
        
        sourceUnit.DecreaseAP(selectingSkill.cost);
        
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

        

        //Final Card
        if (selectingSkill.keywords.Contains(MMSkillKeyWord.Final))
        {
            EnterPhase(MMBattlePhase.UnitEnd);
        }
        else
        {
            EnterPhase(MMBattlePhase.UnitActing);
        }

    }









    public void DrawCards(int count, bool instance = false)
    {
        MMCardPanel.Instance.DrawCard(count, instance);
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



    
    

}
