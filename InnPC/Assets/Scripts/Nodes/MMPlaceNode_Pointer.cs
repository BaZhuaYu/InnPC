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

        if (MMExplorePanel.Instance.tansuoGold < price)
        {
            MMTipManager.instance.CreateTip("½ð±Ò²»×ã");
            return;
        }
        

        MMExplorePanel.Instance.tansuoTime -= price;
        MMExplorePanel.Instance.UpdateUI();

        //MMRewardPanel.instance.OpenUI();


        switch (key)
        {
            case "Place_YouJianKeZhan":
                //MMRewardPanel.instance.LoadUnitPanel();
                MMQuestNode node1 = MMQuestNode.Create(MMQuest.Create(1));
                AddBrother(node1);
                break;

            case "Place_LuoYangJiaoWai":
                //MMRewardPanel.instance.LoadCardPanel();
                MMQuestNode node2 = MMQuestNode.Create(MMQuest.Create(2));
                AddBrother(node2);
                break;

            case "Place_JiShi":
                //MMRewardPanel.instance.LoadItemPanel();
                MMQuestNode node3 = MMQuestNode.Create(MMQuest.Create(3));
                AddBrother(node3);
                break;

            default:
                break;
        }
    }
}
