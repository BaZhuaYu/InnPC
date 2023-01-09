using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMEnrollPanel : MMNode
{
    public static MMEnrollPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    
    public Text textGold; 

   
    List<MMHeroNode> heroes;



    void Start()
    {
        heroes = new List<MMHeroNode>();
        CloseUI();
    }


    void Update()
    {

    }

    public override void OpenUI()
    {
        base.OpenUI();
        UpdateUI();
    }


    public void OnClickButtonEnroll()
    {
        Clear();

        if (MMPlayerManager.Instance.diamond < 10)
        {
            MMTipManager.instance.CreateTip("金币不足");
            return;
        }

        if (MMPlayerManager.Instance.heroes.Count >= MMUnit.units.Count)
        {
            MMTipManager.instance.CreateTip("已获得所有侠客");
            return;
        }

        MMPlayerManager.Instance.diamond -= 10;
        UpdateUI();
        EnrollHero();
    }


    public void OnClickButtonIndex()
    {
        MMCardIndexPanel panel = MMCardIndexPanel.Create();
        panel.Accept(MMUnit.units);
        AddChild(panel);
        //panel.OpenUI();
    }


    public void OnClickButtonBack()
    {
        CloseUI();
        MMMainPanel.Instance.OpenUI();
    }

    
    void EnrollHero()
    {
        MMUnit unit = MMUnit.FindRandomOne();

        while (MMPlayerManager.Instance.HasHero(unit))
        {
            unit = MMUnit.FindRandomOne();
        }

        MMHeroNode node = MMHeroNode.Create(unit);
        MMPlayerManager.Instance.AddHero(unit);
        node.SetParent(this);
        this.heroes.Add(node);
        node.MoveUp(200);
    }


    void Clear()
    {
        foreach (var unit in heroes)
        {
            unit.Clear();
        }
        heroes = new List<MMHeroNode>();
    }


    public void UpdateUI()
    {
        textGold.text = MMPlayerManager.Instance.diamond + "";
    }
}
