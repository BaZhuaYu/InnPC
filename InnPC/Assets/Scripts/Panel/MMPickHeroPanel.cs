using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPickHeroPanel : MMNode
{
    public static MMPickHeroPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<MMHeroNode> heroes;
    public List<MMHeroNode> selectedHeroes;


    // Start is called before the first frame update
    void Start()
    {
        heroes = new List<MMHeroNode>();
        selectedHeroes = new List<MMHeroNode>();
        CloseUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OpenUI()
    {
        base.OpenUI();
        Accept(MMPlayerManager.Instance.heroes);
    }



    public void Accept(List<MMUnit> units)
    {
        foreach (var unit in units)
        {
            MMHeroNode node = MMHeroNode.CreateFromUnit(unit);
            node.gameObject.AddComponent<MMHeroNode_PickHero>();
            AddChild(node);
            heroes.Add(node);
        }
        UpdateUI();
    }

    public void Reload()
    {

    }

    public void Clear()
    {

    }

    public void UpdateUI()
    {
        float offset = 500;
        foreach (var hero in heroes)
        {
            hero.MoveToCenter();
            hero.MoveLeft(offset);
            hero.MoveUp(250);
            offset -= 250;
        }

        offset = 500;
        foreach (var hero in selectedHeroes)
        {
            hero.MoveToCenter();
            hero.MoveLeft(offset);
            hero.MoveDown(250);
            offset -= 250;
        }
    }


    public void StartExplore()
    {
        if (this.selectedHeroes.Count < 3)
        {
            MMTipManager.instance.CreateTip("需要选择3名侠客");
            return;
        }

        List<MMUnit> units = new List<MMUnit>();
        foreach (var hero in selectedHeroes)
        {
            units.Add(hero.unit);
        }

        MMExplorePanel.Instance.gold = 100;
        MMExplorePanel.Instance.level = 1;
        MMExplorePanel.Instance.hp = 100;
        MMExplorePanel.Instance.Accept(units);

        CloseUI();
        MMExplorePanel.Instance.OpenUI();
    }


    public bool SelectHero(MMHeroNode hero)
    {
        if (this.selectedHeroes.Count == 3)
        {
            MMTipManager.instance.CreateTip("不能选择更多侠客");
            return false;
        }
        heroes.Remove(hero);
        selectedHeroes.Add(hero);
        UpdateUI();

        return true;
    }

    public bool UnselectHero(MMHeroNode hero)
    {
        if (this.selectedHeroes.Contains(hero))
        {
            this.selectedHeroes.Remove(hero);
            heroes.Add(hero);
            UpdateUI();
            return true;
        }

        return false;
    }


    public void OnClickButtonBack()
    {
        CloseUI();
        MMMainPanel.Instance.OpenUI();
    }

}
