using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{
    int cardindex = 0;
    public void HandlePlayCard()
    {
        sourceUnit.DecreaseAP(selectingCard.cost);
        

        if (selectingCard.type == MMSkillType.Power)
        {
            sourceUnit.IncreaseATK(selectingCard.tempATK);
            sourceUnit.IncreaseHP(selectingCard.tempDEF);
        }
        else
        {

        }

        MMEffect effect = selectingCard.CreateEffect();
        effect.source = this.sourceUnit;
        effect.target = this.targetUnit;
        ExecuteEffect(effect);

       
        //如果Source是Dead状态，SelectingCard没有了
        //Power Card
        if (selectingCard.type == MMSkillType.Power)
        {
            selectingCard.EnterState(MMSkillState.Used);

            foreach (var skill in selectingCard.unit.skills)
            {
                if (skill.type == MMSkillType.Power)
                {
                    selectingCard.isEnabled = false;
                }
            }
            selectingCard.isEnabled = true;
        }


        if (sourceUnit.group == 1)
        {
            MMCardPanel.Instance.PlayCard(selectingCard);
        }

        

        if (effect.target.state == MMUnitState.Dead)
        {
            effect.source.isMoved = false;
        }


        if (effect.source.state == MMUnitState.Dead)
        {
            EnterPhase(MMBattlePhase.UnitEnd);
        }
        else
        {
            //Final Card
            if (selectingCard.keywords.Contains(MMSkillKeyWord.Final))
            {
                EnterPhase(MMBattlePhase.UnitEnd);
            }
            else
            {
                if (sourceUnit.group == 1)
                {
                    EnterState(MMBattleState.SelectedSourceUnit);
                    EnterPhase(MMBattlePhase.UnitActing);
                }
                else
                {

                }
            }
        }
        

        ClearDeadUnits();
    }



}
