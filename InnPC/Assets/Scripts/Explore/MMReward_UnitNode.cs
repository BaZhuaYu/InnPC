using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMReward_UnitNode : MonoBehaviour, IPointerClickHandler
{
    public MMUnit unit;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(MMExplorePanel.Instance.HasUnit(unit))
        {
            MMTipManager.instance.CreateTip("已拥有该角色");
            return;
        }

        MMExplorePanel.Instance.minions.Add(unit);

        MMRewardPanel.instance.CloseUI();
        MMExplorePanel.Instance.UpdateUI();
    }

}
