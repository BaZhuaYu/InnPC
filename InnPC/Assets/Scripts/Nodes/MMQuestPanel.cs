using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MMQuestType
{
    RewardUnit,
    RewardCard,
    RewardItem,
    RewardPlace,
}


public class MMQuestPanel : MMNode
{
    [HideInInspector]
    public MMQuestNode quest;


    public MMButton option1;
    public MMButton option2;
    public MMButton option3;
    public MMPickHeroNode pickHero;
    

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

        Debug.Log("lllllllllllllll" + quest.displayName);
        Debug.Log("lllllllllllllll" + quest.displayNote);

        this.quest.name = "sadfsadfsdaf";

        switch (quest.quest.type)
        {
            case MMQuestType.RewardUnit:
                option2.gameObject.AddComponent<MMReward_UnitNode>();
                option2.AddClickAction(DestroyThis);
                option3.AddClickAction(DestroyThis);

                option2.GetComponentInChildren<Text>().text = quest.quest.options[0];
                option3.GetComponentInChildren<Text>().text = quest.quest.options[1];

                option2.gameObject.GetComponent<MMReward_UnitNode>().unit = this.quest.quest.units[0];

                break;

            case MMQuestType.RewardCard:
                option1.gameObject.AddComponent<MMReward_CardNode>();
                option2.gameObject.AddComponent<MMReward_CardNode>();
                option3.gameObject.AddComponent<MMReward_CardNode>();

                option1.GetComponentInChildren<Text>().text = quest.quest.options[0];
                option2.GetComponentInChildren<Text>().text = quest.quest.options[1];
                option3.GetComponentInChildren<Text>().text = quest.quest.options[2];

                option1.gameObject.GetComponent<MMReward_CardNode>().card = quest.quest.cards[0];
                option2.gameObject.GetComponent<MMReward_CardNode>().card = quest.quest.cards[1];
                option3.gameObject.GetComponent<MMReward_CardNode>().card = quest.quest.cards[2];

                option1.AddClickAction(DestroyThis);
                option2.AddClickAction(DestroyThis);
                option3.AddClickAction(DestroyThis);


                MMHintNode hintNode1 = MMHintNode.Create(MMCardNode.Create(quest.quest.cards[0]));
                MMHintNode hintNode2 = MMHintNode.Create(MMCardNode.Create(quest.quest.cards[1]));
                MMHintNode hintNode3 = MMHintNode.Create(MMCardNode.Create(quest.quest.cards[2]));

                option1.AddChild(hintNode1);
                option2.AddChild(hintNode2);
                option3.AddChild(hintNode3);

                hintNode1.MoveRight(option1.FindWidth() * 0.5f);
                hintNode2.MoveRight(option2.FindWidth() * 0.5f);
                hintNode3.MoveRight(option3.FindWidth() * 0.5f);

                hintNode1.node.SetParent(option1);
                hintNode2.node.SetParent(option2);
                hintNode3.node.SetParent(option3);

                hintNode1.node.transform.position = hintNode1.transform.position + new Vector3(0, hintNode1.node.FindHeight(), 0);
                hintNode2.node.transform.position = hintNode2.transform.position + new Vector3(0, hintNode2.node.FindHeight(), 0);
                hintNode3.node.transform.position = hintNode3.transform.position + new Vector3(0, hintNode3.node.FindHeight(), 0);


                hintNode1.LoadImage("UI/Icon/IconRewardCard");
                hintNode2.LoadImage("UI/Icon/IconRewardCard");
                hintNode3.LoadImage("UI/Icon/IconRewardCard");

                break;

            case MMQuestType.RewardItem:
                pickHero.Accept(MMPlayerManager.Instance.heroes);
                foreach (var unit in pickHero.units)
                {
                    unit.gameObject.AddComponent<MMReward_ItemNode>();
                    unit.gameObject.GetComponent<MMReward_ItemNode>().item = MMItemNode.Create(quest.quest.items[0]);

                }
                break;


            case MMQuestType.RewardPlace:
                option3.GetComponentInChildren<Text>().text = quest.quest.options[0];
                option3.AddClickAction(GainPlace);
                option3.AddClickAction(DestroyThis);
                break;
        }

        UpdateUI();
    }


    public void UpdateUI()
    {
        MMExplorePanel.Instance.UpdateUI();
        switch (quest.quest.type)
        {
            case MMQuestType.RewardUnit:
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(true);
                option3.gameObject.SetActive(true);
                pickHero.gameObject.SetActive(false);
                break;

            case MMQuestType.RewardCard:
                option1.gameObject.SetActive(true);
                option2.gameObject.SetActive(true);
                option3.gameObject.SetActive(true);
                pickHero.gameObject.SetActive(false);
                break;

            case MMQuestType.RewardItem:
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
                option3.gameObject.SetActive(false);
                pickHero.gameObject.SetActive(true);
                break;

            case MMQuestType.RewardPlace:
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
                option3.gameObject.SetActive(true);
                pickHero.gameObject.SetActive(false);
                break;

        }
    }


    public override void CloseUI()
    {
        Destroy(gameObject);
    }


    public void DestroyThis()
    {
        MMExplorePanel.Instance.quests.Remove(this.quest.quest);
        Destroy(gameObject);
    }


    public void GainUnit()
    {

    }


    public void GainCard()
    {

    }

    public void GainPlace()
    {
        MMExplorePanel.Instance.places.Add(MMPlaceNode.Create(this.quest.quest.place));
    }




    public static MMQuestPanel Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMQuestPanel") as GameObject);
        obj.name = "MMQuestPanel";
        return obj.GetComponent<MMQuestPanel>();
    }


    public static MMQuestPanel Create(MMQuest quest)
    {
        MMQuestPanel node = Create();
        node.Accept(MMQuestNode.Create(quest));
        return node;
    }



}
