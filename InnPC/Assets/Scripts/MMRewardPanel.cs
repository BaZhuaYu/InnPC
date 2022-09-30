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
        List<MMUnit> units = MMUnit.FindRandomCount(3);
        List<MMUnitNode> nodes = new List<MMUnitNode>();

        foreach (var unit in units)
        {
            MMUnitNode node = MMUnitNode.Create();
            node.Accept(unit);
            nodes.Add(node);
        }

        nodes[0].SetParent(this);
        nodes[1].SetParent(this);
        nodes[2].SetParent(this);

        nodes[0].gameObject.AddComponent<MMRewardUnit>();
        nodes[1].gameObject.AddComponent<MMRewardUnit>();
        nodes[2].gameObject.AddComponent<MMRewardUnit>();

        nodes[0].MoveLeft(150);
        nodes[2].MoveRight(150);
    }


    public void LoadSkillPanel()
    {
        MMSkill skill = MMSkill.FindRandomOne();
        MMSkillNode node = MMSkillNode.Create();
        node.Accept(skill);
        node.SetParent(this);
        node.MoveUp(50);


        List<MMUnitNode> nodes = MMPlayerManager.instance.CreateAllUnitNodes();
        foreach (var unit in nodes)
        {
            unit.SetParent(this);
            unit.MoveDown(100);
            MMRewardSkill rewardSkill = unit.gameObject.AddComponent<MMRewardSkill>();
            rewardSkill.skill = skill;
        }

        nodes[0].MoveLeft(200);
        nodes[2].MoveRight(200);
    }


    public void LoadCardPanel()
    {

    }







}
