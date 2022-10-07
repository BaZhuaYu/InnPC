using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMRewardItem : MonoBehaviour, IPointerClickHandler
{

    public MMItemNode item;


    public void OnPointerClick(PointerEventData eventData)
    {
        MMUnitNode node = GetComponent<MMUnitNode>();
        MMUnit unit = node.unit;


        switch(item.effect)
        {
            case 1:
                unit.atk += 1;
                break;
            case 2:
                unit.maxHP += 1;
                unit.hp += 1;
                break;
            case 3:
                unit.ap += 1;
                break;
            default:
                break;
        }
        
        MMRewardPanel.instance.CloseUI();
        MMGameOverManager.Instance.UpdateUI();
    }
}
