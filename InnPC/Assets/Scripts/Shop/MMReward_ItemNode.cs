using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMReward_ItemNode : MonoBehaviour, IPointerClickHandler
{

    public MMItemNode item;


    public void OnPointerClick(PointerEventData eventData)
    {
        MMUnitNode node = GetComponent<MMUnitNode>();
        MMUnit unit = node.unit;

        MMEffect effect = item.CreateEffect();
        effect.source = node;
        effect.target = node;
        
        MMBattleManager.Instance.ExecuteEffectOnGain(effect);


        MMRewardPanel.instance.CloseUI();
        MMGameOverManager.Instance.UpdateUI();
    }
}
