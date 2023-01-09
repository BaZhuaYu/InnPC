using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMReward_ItemNode : MonoBehaviour, IPointerClickHandler
{
    public MMUnitNode unit;
    public MMItemNode item;

    public void OnPointerClick(PointerEventData eventData)
    {
        unit = GetComponent<MMUnitNode>();
        
        OnGainItem();
        
        MMExplorePanel.Instance.UpdateUI();
    }


    void OnGainItem()
    {
        switch(item.effect)
        {
            case 1:
                unit.unit.atk += item.value;
                break;
            case 2:
                unit.unit.maxHP += item.value;
                unit.unit.hp += item.value;
                break;
            case 3:
                unit.unit.ap += item.value;
                break;
            case 4:
                foreach(var unit in MMExplorePanel.Instance.units)
                {
                    unit.atk += item.value;
                }
                break;
            case 5:
                foreach (var unit in MMExplorePanel.Instance.units)
                {
                    unit.maxHP += item.value;
                    unit.hp += item.value;
                }
                break;
            case 6:
                unit.unit.maxAP += item.value;
                unit.unit.ap += item.value;
                break;
        }
    }

    
}
