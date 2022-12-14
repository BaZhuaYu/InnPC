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
            SetEnable(false);
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

        MMExplorePanel.Instance.gold -= price;

        MMRewardPanel.instance.OpenUI();

        switch (key)
        {
            case "YouJianKeZhan":
                MMRewardPanel.instance.LoadUnitPanel();
                break;

            case "LuoYangCheng":
                MMRewardPanel.instance.LoadCardPanel();
                break;

            case "JiShi":
                MMRewardPanel.instance.LoadItemPanel();
                break;

            default:
                break;
        }
    }
}
