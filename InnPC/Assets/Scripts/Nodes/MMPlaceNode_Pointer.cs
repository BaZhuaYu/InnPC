using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMPlaceNode : MMNode, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.num <= 0)
        {
            MMTipManager.instance.CreateTip("不能再次探索");
            return;
        }

        if (MMExplorePanel.Instance.tansuoTime < price)
        {
            MMTipManager.instance.CreateTip("天色已晚");
            return;
        }


        MMExplorePanel.Instance.tansuoTime -= price;
        this.num -= 1;
        MMExplorePanel.Instance.UpdateUI();

        OnClick();
    }


    void OnClick()
    {
        
        switch (key)
        {
            //洛阳城
            case "Place_LuoYangCheng":
                Place_LuoYangCheng();
                break;


            //有间客栈
            case "Place_YouJianKeZhan":
                Place_YouJianKeZhan();
                break;


            //洛阳郊外
            case "Place_LuoYangJiaoWai":
                //MMRewardPanel.instance.LoadCardPanel();
                MMQuestPanel node2 = MMQuestPanel.Create(MMQuest.Create(2));
                AddBrother(node2);
                break;


            //集市
            case "Place_JiShi":
                //MMRewardPanel.instance.LoadItemPanel();
                MMQuestPanel node3 = MMQuestPanel.Create(MMQuest.Create(3));
                AddBrother(node3);
                break;

            default:
                break;
        }
    }



    void Place_LuoYangCheng()
    {
        MMQuestPanel node = MMQuestPanel.Create(MMQuest.Create(2));
        AddBrother(node);
        MMExplorePanel.Instance.quests.Add(node.quest.quest);
        MMExplorePanel.Instance.UpdateUI();
        return;


        int r = Random.Range(0,100);
        if(r < 25)
        {
            MMExplorePanel.Instance.GainExp(1);
        }
        else if (r < 50)
        {
            MMExplorePanel.Instance.GainGold(1);
        }
        else if (r < 60)
        {
            //侠客
            MMQuestPanel node1 = MMQuestPanel.Create(MMQuest.Create(1));
            AddBrother(node1);
        }
        else if (r < 75)
        {
            //卡牌
            MMQuestPanel node2 = MMQuestPanel.Create(MMQuest.Create(2));
            AddBrother(node2);
        }
        else if (r < 85)
        {
            //物品
            MMQuestPanel node3 = MMQuestPanel.Create(MMQuest.Create(3));
            AddBrother(node3);
        }
        else if (r < 100)
        {
            //场景
            MMQuestPanel node4 = MMQuestPanel.Create(MMQuest.Create(4));
            AddBrother(node4);
        }
    }


    void Place_YouJianKeZhan()
    {
        MMQuestPanel node1 = MMQuestPanel.Create(MMQuest.Create(1));
        AddBrother(node1);
    }






}
