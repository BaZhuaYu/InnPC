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
                MMQuestPanel node102 = MMQuestPanel.Create(MMQuest.Create(102));
                AddBrother(node102);
                MMExplorePanel.Instance.quests.Add(node102.quest.quest);
                break;


            //����
            case "Place_JiShi":
                MMQuestPanel node103 = MMQuestPanel.Create(MMQuest.Create(103));
                AddBrother(node103);
                MMExplorePanel.Instance.quests.Add(node103.quest.quest);
                break;


            //��������
            case "Place_LuoYangJiaoWai":
                //MMRewardPanel.instance.LoadCardPanel();
                MMQuestPanel node2 = MMQuestPanel.Create(MMQuest.Create(2));
                AddBrother(node2);
                break;


            //ҽ��
            case "Place_YiGuan":
                MMQuestPanel node105 = MMQuestPanel.Create(MMQuest.Create(105));
                AddBrother(node105);
                MMExplorePanel.Instance.quests.Add(node105.quest.quest);
                break;

            //��վ
            case "Place_YiZhan":
                MMQuestPanel node106 = MMQuestPanel.Create(MMQuest.Create(106));
                AddBrother(node106);
                MMExplorePanel.Instance.quests.Add(node106.quest.quest);
                break;


            //�ƽ�
            case "Place_JiuJiao":
                GainQuest(107);
                break;

            //�ķ�
            case "Place_XiaoBaoDuFang":
                GainQuest(110);
                break;

            default:
                break;
        }

        MMExplorePanel.Instance.UpdateUI();
    }



    void Place_LuoYangCheng()
    {
        //MMQuestPanel node = MMQuestPanel.Create(MMQuest.Create(103));
        //AddBrother(node);
        

        int r = Random.Range(0,100);
        if(r < 25)
        {
            MMExplorePanel.Instance.GainExp(1);
        }
        else if (r < 50)
        {
            MMExplorePanel.Instance.GainGold(1);
        }
        else 
        {
            //����
            MMQuestPanel node = MMQuestPanel.Create(MMQuest.Create(101));
            AddBrother(node);
            MMExplorePanel.Instance.quests.Add(node.quest.quest);
            MMExplorePanel.Instance.UpdateUI();
        }


        

        //else if (r < 60)
        //{
        //    //����
        //    MMQuestPanel node1 = MMQuestPanel.Create(MMQuest.Create(1));
        //    AddBrother(node1);
        //}
        //else if (r < 75)
        //{
        //    //����
        //    MMQuestPanel node2 = MMQuestPanel.Create(MMQuest.Create(2));
        //    AddBrother(node2);
        //}
        //else if (r < 85)
        //{
        //    //��Ʒ
        //    MMQuestPanel node3 = MMQuestPanel.Create(MMQuest.Create(3));
        //    AddBrother(node3);
        //}

    }


    void Place_YouJianKeZhan()
    {
        MMQuestPanel node1 = MMQuestPanel.Create(MMQuest.Create(1));
        AddBrother(node1);
    }









    void GainQuest(int id)
    {
        MMQuestPanel node = MMQuestPanel.Create(MMQuest.Create(id));
        AddBrother(node);
        MMExplorePanel.Instance.quests.Add(node.quest.quest);
    }

}
