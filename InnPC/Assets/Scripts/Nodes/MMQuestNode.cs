using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MMQuestType
{
    RewardUnit,
    RewardCard,
    RewardItem,
}


public class MMQuestNode : MMNode
{

    public Transform card;
    public MMButton option1;
    public MMButton option2;
    public MMButton option3;
    public MMPickHeroNode pickHero;


    [HideInInspector]
    public MMQuest quest;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Accept(MMQuest q)
    {
        this.quest = q;
        AddChild(q.reward);
        q.reward.transform.position = card.transform.position;
        

        switch(quest.type)
        {
            case MMQuestType.RewardUnit:
                option2.gameObject.AddComponent<MMReward_UnitNode>();
                option3.AddClickAction(CloseUI);

                option2.gameObject.GetComponent<MMReward_UnitNode>().unit = this.quest.units[0];

                break;

            case MMQuestType.RewardCard:
                option1.gameObject.AddComponent<MMReward_CardNode>();
                option2.gameObject.AddComponent<MMReward_CardNode>();
                option3.gameObject.AddComponent<MMReward_CardNode>();


                option1.gameObject.GetComponent<MMReward_CardNode>().card = quest.cards[0];
                option2.gameObject.GetComponent<MMReward_CardNode>().card = quest.cards[0];
                option3.gameObject.GetComponent<MMReward_CardNode>().card = quest.cards[0];
                break;

            case MMQuestType.RewardItem:
                pickHero.Accept(MMPlayerManager.Instance.heroes);
                foreach (var unit in pickHero.units)
                {
                    unit.gameObject.AddComponent<MMReward_ItemNode>();
                }
                break;
        }

        UpdateUI();
    }
   

    public void UpdateUI()
    {
        
        switch(quest.type)
        {
            case MMQuestType.RewardUnit:
                pickHero.gameObject.SetActive(false);
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(true);
                option3.gameObject.SetActive(true);
                pickHero.gameObject.SetActive(false);
                break;

            case MMQuestType.RewardCard:
                pickHero.gameObject.SetActive(false);
                option1.gameObject.SetActive(true);
                option2.gameObject.SetActive(true);
                option3.gameObject.SetActive(true);
                pickHero.gameObject.SetActive(false);
                break;

            case MMQuestType.RewardItem:
                pickHero.gameObject.SetActive(true);
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
                option3.gameObject.SetActive(false);
                pickHero.gameObject.SetActive(true);
                break;
        }
    }


    public override void CloseUI()
    {
        //Destroy(gameObject);
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMQuestIcon") as GameObject);
        obj.name = "MMQuestIcon";
        //obj.GetComponent<MMQuestIcon>();
        MMQuestIcon icon = obj.GetComponent<MMQuestIcon>();
        icon.node = this;
        MMExplorePanel.Instance.AddChild(icon);

        this.gameObject.SetActive(false);
    }


    public void Minimize()
    {
        this.gameObject.SetActive(false);
    }


    public void GainUnit()
    {

    }


    public void GainCard()
    {

    }




    public static MMQuestNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMQuestNode") as GameObject);
        obj.name = "MMQuestNode";
        return obj.GetComponent<MMQuestNode>();
    }


    public static MMQuestNode Create(MMQuest quest)
    {
        MMQuestNode node = Create();
        node.Accept(quest);
        return node;
    }

    

}
