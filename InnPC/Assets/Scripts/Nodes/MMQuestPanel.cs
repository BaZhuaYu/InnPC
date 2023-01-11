using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MMQuestType
{
    None,
    RewardUnit,
    RewardCard,
    RewardItem,
    RewardPlace,
}


public class MMQuestPanel : MMNode
{
    [HideInInspector]
    public MMQuestNode quest;


    public MMOptionNode option1;
    public MMOptionNode option2;
    public MMOptionNode option3;


    void Start()
    {
        
    }


    void Update()
    {

    }



    public void Accept(MMQuestNode q)
    {
        this.quest = q;
        AddChild(q);
        q.MoveUp(this.FindHeight() * 0.2f);
        q.transform.SetSiblingIndex(0);


        switch (quest.quest.type)
        {
            case MMQuestType.RewardUnit:
                option2.LoadAction(GainUnit);

                option2.LoadTitle(quest.quest.options[0]);
                option3.LoadTitle(quest.quest.options[1]);

                MMHintNode hintNode = MMHintNode.Create(MMHeroNode.Create(quest.quest.units[0]));
                hintNode.LoadImage("UI/Icon/IconRewardUnit");
                option2.LoadHintNode(hintNode);
                break;


            case MMQuestType.RewardCard:
                option1.gameObject.AddComponent<MMReward_CardNode>();
                option2.gameObject.AddComponent<MMReward_CardNode>();
                option3.gameObject.AddComponent<MMReward_CardNode>();
                
                option1.gameObject.GetComponent<MMReward_CardNode>().card = quest.quest.cards[0];
                option2.gameObject.GetComponent<MMReward_CardNode>().card = quest.quest.cards[1];
                option3.gameObject.GetComponent<MMReward_CardNode>().card = quest.quest.cards[2];


                option1.LoadTitle(quest.quest.options[0]);
                option2.LoadTitle(quest.quest.options[1]);
                option3.LoadTitle(quest.quest.options[2]);
                
                MMHintNode hintNode1 = MMHintNode.Create(MMCardNode.Create(quest.quest.cards[0]));
                MMHintNode hintNode2 = MMHintNode.Create(MMCardNode.Create(quest.quest.cards[1]));
                MMHintNode hintNode3 = MMHintNode.Create(MMCardNode.Create(quest.quest.cards[2]));
                
                option1.LoadHintNode(hintNode1);
                option2.LoadHintNode(hintNode2);
                option3.LoadHintNode(hintNode3);

                hintNode1.LoadImage("UI/Icon/IconRewardCard");
                hintNode2.LoadImage("UI/Icon/IconRewardCard");
                hintNode3.LoadImage("UI/Icon/IconRewardCard");
                
                break;

            case MMQuestType.RewardItem:
                option3.LoadTitle(quest.quest.options[0]);
                MMItemNode n = MMItemNode.Create(this.quest.quest.items[0]);
                MMHintNode hintnode = MMHintNode.Create(n);
                option3.LoadHintNode(hintnode);
                option3.LoadAction(() =>
                {
                    MMItemPanel panel = MMItemPanel.Create(this.quest.quest.items[0]);
                    MMExplorePanel.Instance.AddChild(panel);
                });

                break;


            case MMQuestType.RewardPlace:
                option3.LoadTitle(quest.quest.options[0]);
                option3.LoadAction(GainPlace);
                break;

            case MMQuestType.None:
                break;
        }


        if (quest.quest.id == 10)
        {
            option2.LoadAction(DuBo);
        }
        else if (quest.quest.id == 107)
        {
            MMCard card = this.quest.quest.cards[0];
            MMHintNode nodeHint = MMHintNode.Create(MMCardNode.Create(card));
            option2.LoadHintNode(nodeHint);
            option2.LoadAction(() =>
            {
                MMExplorePanel.Instance.GainCard(card);
            });
        }
        else if (quest.quest.id == 110)
        {
            option2.LoadAction(() =>
            {
                if (Random.Range(0, 100) < 50)
                {
                    MMExplorePanel.Instance.GainGold(10);
                    MMTipManager.instance.CreateTip("¶ÄÓ®ÁË£¡");
                }
                else
                {
                    MMTipManager.instance.CreateTip("Ê®¶Ä¾ÅÊä");
                }
                CloseUI();
            });
        }


        UpdateUI();
    }


    public void UpdateUI()
    {
        MMExplorePanel.Instance.UpdateUI();
        
        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);
        option3.gameObject.SetActive(false);

        //if(quest.quest.type == MMQuestType.RewardItem)
        //{
        //    return;
        //}

        if (quest.quest.options.Count == 1)
        {
            option3.gameObject.SetActive(true);
            option3.LoadTitle(quest.quest.options[0]);
        }
        else if (quest.quest.options.Count == 2)
        {
            option2.gameObject.SetActive(true);
            option3.gameObject.SetActive(true);
            option2.LoadTitle(quest.quest.options[0]);
            option3.LoadTitle(quest.quest.options[1]);
        }
        else if (quest.quest.options.Count == 3)
        {
            option1.gameObject.SetActive(true);
            option2.gameObject.SetActive(true);
            option3.gameObject.SetActive(true);
            option1.LoadTitle(quest.quest.options[0]);
            option2.LoadTitle(quest.quest.options[1]);
            option3.LoadTitle(quest.quest.options[2]);
        }

    }


    public override void CloseUI()
    {
        Destroy(gameObject);
    }


    public void DestroyThis()
    {
        MMExplorePanel.Instance.quests.Remove(this.quest.quest);
        MMExplorePanel.Instance.UpdateUI();
        Destroy(gameObject);
    }


    public void LoadHintNode(MMHintNode hint, MMButton option)
    {
        option.AddChild(hint);
        hint.MoveRight(option.FindWidth() * 0.5f);
        hint.node.SetParent(option);
        hint.node.transform.position = hint.transform.position + new Vector3(0, hint.node.FindHeight(), 0);
    }


    public void GainUnit()
    {
        MMExplorePanel.Instance.minions.Add(this.quest.quest.units[0]);
    }


    public void GainCard()
    {

    }

    public void GainPlace()
    {
        MMExplorePanel.Instance.GainPlace(this.quest.quest.place);
    }



    public void DuBo()
    {
        if (Random.Range(0, 100) < 50)
        {
            MMExplorePanel.Instance.GainGold(10);
        }
        else
        {
            MMTipManager.instance.CreateTip("Ê®¶Ä¾ÅÊä");
        }
    }


    public MMOptionNode CreateOption()
    {
        return MMOptionNode.Create(this);
    }



    public static MMQuestPanel Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMQuestPanel") as GameObject);
        obj.name = "MMQuestPanel";
        MMQuestPanel ret = obj.GetComponent<MMQuestPanel>();
        //ret.option1 = MMOptionNode.Create(ret);
        //ret.option2 = MMOptionNode.Create(ret);
        //ret.option3 = MMOptionNode.Create(ret);
        return ret;
    }


    public static MMQuestPanel Create(MMQuest quest)
    {
        MMQuestPanel node = Create();
        node.Accept(MMQuestNode.Create(quest));
        return node;
    }

    
}
