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
        if (MMExplorePanel.Instance.numBuy < 1)
        {
            MMTipManager.instance.CreateTip("ʹ�ô�������");
            return;
        }

        if (MMExplorePanel.Instance.gold < price)
        {
            MMTipManager.instance.CreateTip("��Ҳ���");
            return;
        }

        SetEnable(false);

        MMExplorePanel.Instance.gold -= price;
        MMExplorePanel.Instance.numBuy -= 1;
        MMExplorePanel.Instance.UpdateUI();

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
