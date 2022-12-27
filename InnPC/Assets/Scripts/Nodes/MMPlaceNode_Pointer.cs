using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMPlaceNode : MMNode, IPointerClickHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(isEnabled)
        {
            
            OnClick();
        }
    }


    void OnClick()
    {

        if (MMExplorePanel.Instance.gold < price)
        {
            MMTipManager.instance.CreateTip("½ð±Ò²»×ã");
            return;
        }

        SetEnable(false);

        MMExplorePanel.Instance.gold -= price;
        MMExplorePanel.Instance.UpdateUI();

        MMRewardPanel.instance.OpenUI();

        switch (key)
        {
            case "Place_YouJianKeZhan":
                MMRewardPanel.instance.LoadUnitPanel();
                break;

            case "Place_LuoYangJiaoWai":
                MMRewardPanel.instance.LoadCardPanel();
                break;

            case "Place_JiShi":
                MMRewardPanel.instance.LoadItemPanel();
                break;

            default:
                break;
        }
    }
}
