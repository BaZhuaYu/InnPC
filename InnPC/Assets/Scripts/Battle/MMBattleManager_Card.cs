using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MMNode
{
    int cardindex = 0;

    public void HandlePlayCard()
    {
        sourceUnit.DecreaseAP(selectingCard.cost);
        MMCardPanel.Instance.PlayCard(selectingCard);
        
        if (selectingCard.type == MMCardType.Power)
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
        effect.sideTargets = FindSideTargets(effect.source, effect.target, effect.area);

        ExecuteEffect(effect);
        
    }


    public void ClearSelectCard()
    {
        this.selectingCard = null;
        MMCardPanel.Instance.SetSelectingCard(null);
    }

}
