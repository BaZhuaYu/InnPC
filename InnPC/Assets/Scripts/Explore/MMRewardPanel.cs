using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMRewardPanel : MMNode
{
    public static MMRewardPanel instance;

    private void Awake()
    {
        instance = this;
    }


    List<MMUnitNode> unitNodes;
    List<MMCardNode> cardNodes;
    MMSkillNode skillNode;
    MMItemNode itemNode;

    
    private void Start()
    {
        CloseUI();
    }


    public override void OpenUI()
    {
        base.OpenUI();
        transform.SetSiblingIndex(100);
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }


    public void LoadUnitPanel()
    {
        Clear();

        unitNodes = new List<MMUnitNode>();

        List<MMUnit> units = MMUnit.FindRandomCount(3);
        
        foreach (var unit in units)
        {
            MMUnitNode node = MMUnitNode.Create();
            node.Accept(unit);
            unitNodes.Add(node);
        }

        unitNodes[0].SetParent(this);
        unitNodes[1].SetParent(this);
        unitNodes[2].SetParent(this);

        unitNodes[0].gameObject.AddComponent<MMReward_UnitNode>();
        unitNodes[1].gameObject.AddComponent<MMReward_UnitNode>();
        unitNodes[2].gameObject.AddComponent<MMReward_UnitNode>();

        unitNodes[0].MoveLeft(150);
        unitNodes[2].MoveRight(150);
    }


    public void LoadSkillPanel()
    {
        Clear();

        skillNode = MMSkillNode.Create();
        MMSkill skill = MMSkill.FindRandomOne();
        skillNode.Accept(skill);
        skillNode.SetParent(this);
        skillNode.MoveUp(this.FindHeight() * 0.25f);

        unitNodes = MMExplorePanel.Instance.CreateAllUnitNodes();
        //List<MMUnitNode> nodes = MMPlayerManager.Instance.CreateAllUnitNodes();
        foreach (var unit in unitNodes)
        {
            unit.SetParent(this);
            unit.MoveDown(this.FindHeight() * 0.15f);
            MMReward_SkillNode rewardSkill = unit.gameObject.AddComponent<MMReward_SkillNode>();
            rewardSkill.skill = skillNode;
        }

        unitNodes[0].MoveLeft(this.FindWidth() * 0.25f);
        unitNodes[2].MoveRight(this.FindWidth() * 0.25f);
    }


    public void LoadCardPanel()
    {
        Clear();

        cardNodes = new List<MMCardNode>();
        
        List<MMCard> cards = MMCard.FindRandomCount(3);

        foreach (var card in cards)
        {
            MMCardNode node = MMCardNode.Create();
            node.Accept(card);
            cardNodes.Add(node);
        }

        cardNodes[0].SetParent(this);
        cardNodes[1].SetParent(this);
        cardNodes[2].SetParent(this);

        cardNodes[0].gameObject.AddComponent<MMReward_CardNode>();
        cardNodes[1].gameObject.AddComponent<MMReward_CardNode>();
        cardNodes[2].gameObject.AddComponent<MMReward_CardNode>();

        cardNodes[0].MoveLeft(this.FindWidth() * 0.25f);
        cardNodes[2].MoveRight(this.FindWidth() * 0.25f);
    }


    public void LoadItemPanel()
    {
        Clear();

        itemNode = MMItemNode.Create();
        MMItem item = MMItem.FindRandomOne();
        itemNode.Accept(item);
        itemNode.SetParent(this);
        itemNode.MoveUp(this.FindHeight() * 0.25f);

        unitNodes = MMExplorePanel.Instance.CreateAllUnitNodes();
        foreach (var unit in unitNodes)
        {
            unit.SetParent(this);
            unit.MoveDown(100);
            MMReward_ItemNode rewardItem = unit.gameObject.AddComponent<MMReward_ItemNode>();
            rewardItem.item = itemNode;
        }

        unitNodes[0].MoveLeft(this.FindWidth() * 0.25f);
        unitNodes[2].MoveRight(this.FindWidth() * 0.25f);
    }





    public void Clear()
    {
        if(unitNodes != null)
        {
            foreach (var unit in unitNodes)
            {
                unit.RemoveFromParent();
            }
            unitNodes.Clear();
        }

        if(cardNodes != null)
        {
            foreach (var card in cardNodes)
            {
                card.RemoveFromParent();
            }
            cardNodes.Clear();
        }
        
        if(skillNode != null)
        {
            skillNode.Clear();
            skillNode = null;
        }

        if (itemNode != null)
        {
            itemNode.Clear();
            itemNode = null;
        }

    }
    

}
