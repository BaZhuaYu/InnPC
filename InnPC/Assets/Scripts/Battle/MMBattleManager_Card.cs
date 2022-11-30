using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{
    int cardindex = 0;

    public void HandlePlayCard()
    {
        sourceUnit.DecreaseAP(selectingCard.cost);
        MMCardPanel.Instance.PlayCard(selectingCard);
        
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
        
    }


    public void ClearSelectCard()
    {
        this.selectingCard = null;
        MMCardPanel.Instance.SetSelectingCard(null);
    }

}
