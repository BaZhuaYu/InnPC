using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMReward_UnitNode : MonoBehaviour, IPointerClickHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MMRewardUnit");
        
        MMUnitNode node = GetComponent<MMUnitNode>();
        MMUnit unit = node.unit;


        if(MMExplorePanel.Instance.HasUnit(unit))
        {
            MMTipManager.instance.CreateTip("已拥有该角色");
            return;
        }

        MMExplorePanel.Instance.minions.Add(unit);
        //MMPlayerManager.Instance.units.Add(unit);
        node.SetActive(false);

        MMRewardPanel.instance.CloseUI();
        MMExplorePanel.Instance.UpdateUI();
    }

}
