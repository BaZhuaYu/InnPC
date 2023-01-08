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
            MMTipManager.instance.CreateTip("�����ٴ�̽��");
            return;
        }

        if (MMExplorePanel.Instance.tansuoTime < price)
        {
            MMTipManager.instance.CreateTip("��ɫ����");
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
            //������
            case "Place_LuoYangCheng":
                Place_LuoYangCheng();
                break;


            //�м��ջ
            case "Place_YouJianKeZhan":
                Place_YouJianKeZhan();
                break;


            //��������
            case "Place_LuoYangJiaoWai":
                //MMRewardPanel.instance.LoadCardPanel();
                MMQuestNode node2 = MMQuestNode.Create(MMQuest.Create(2));
                AddBrother(node2);
                break;


            //����
            case "Place_JiShi":
                //MMRewardPanel.instance.LoadItemPanel();
                MMQuestNode node3 = MMQuestNode.Create(MMQuest.Create(3));
                AddBrother(node3);
                break;

            default:
                break;
        }
    }



    void Place_LuoYangCheng()
    {
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
            //����
            MMQuestNode node1 = MMQuestNode.Create(MMQuest.Create(1));
            AddBrother(node1);
        }
        else if (r < 75)
        {
            //����
            MMQuestNode node2 = MMQuestNode.Create(MMQuest.Create(2));
            AddBrother(node2);
        }
        else if (r < 85)
        {
            //��Ʒ
            MMQuestNode node3 = MMQuestNode.Create(MMQuest.Create(3));
            AddBrother(node3);
        }
        else if (r < 100)
        {
            //����
            MMQuestNode node4 = MMQuestNode.Create(MMQuest.Create(4));
            AddBrother(node4);
        }
    }


    void Place_YouJianKeZhan()
    {
        MMQuestNode node1 = MMQuestNode.Create(MMQuest.Create(1));
        AddBrother(node1);
    }






}
