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

        unitNodes[0].gameObject.AddComponent<MMRewardUnit>();
        unitNodes[1].gameObject.AddComponent<MMRewardUnit>();
        unitNodes[2].gameObject.AddComponent<MMRewardUnit>();

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
        skillNode.MoveUp(50);

        unitNodes = MMPlayerManager.Instance.CreateAllUnitNodes();
        //List<MMUnitNode> nodes = MMPlayerManager.Instance.CreateAllUnitNodes();
        foreach (var unit in unitNodes)
        {
            unit.SetParent(this);
            unit.MoveDown(100);
            MMRewardSkill rewardSkill = unit.gameObject.AddComponent<MMRewardSkill>();
            rewardSkill.skill = skillNode;
        }

        unitNodes[0].MoveLeft(200);
        unitNodes[2].MoveRight(200);
    }


    public void LoadCardPanel()
    {
        Clear();
    }


    public void LoadItemPanel()
    {
        Clear();

        itemNode = MMItemNode.Create();
        MMItem item = MMItem.FindRandomOne();
        itemNode.Accept(item);
        itemNode.SetParent(this);
        itemNode.MoveUp(50);

        unitNodes = MMPlayerManager.Instance.CreateAllUnitNodes();
        foreach (var unit in unitNodes)
        {
            unit.SetParent(this);
            unit.MoveDown(100);
            MMRewardItem rewardItem = unit.gameObject.AddComponent<MMRewardItem>();
            rewardItem.item = itemNode;
        }

        unitNodes[0].MoveLeft(200);
        unitNodes[2].MoveRight(200);
    }






    public void Clear()
    {
        if(unitNodes != null)
        {
            foreach (var unit in unitNodes)
            {
                Debug.Log("Clear: " + unit.displayName);
                //unit.SetActive(false);
                unit.RemoveFromParent();
            }
            unitNodes.Clear();
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
