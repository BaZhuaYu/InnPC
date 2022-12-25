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


    public GameObject content;
    public GameObject selectedContent;

    public List<MMHeroNode> allHeroes;
    public List<MMHeroNode> selectedHeroes;


    // Start is called before the first frame update
    void Start()
    {
        allHeroes = new List<MMHeroNode>();
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
            MMHeroNode node = MMHeroNode.Create(unit);
            node.gameObject.AddComponent<MMHeroNode_PickHero>();
            node.transform.SetParent(content.transform);
            allHeroes.Add(node);
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
        float offsetX = -500;
        float offsetY = 150;

        for (int i = 0; i < allHeroes.Count; i++)
        {
            MMHeroNode hero = allHeroes[i];
            hero.transform.SetParent(content.transform);
            hero.transform.localPosition = new Vector2(offsetX, offsetY);
            Debug.Log(hero.transform.localPosition);
            offsetX += 250;

            if (i % 5 == 4)
            {
                offsetX = -500;
                offsetY -= 400;
            }
        }


        for(int i = 0; i < selectedContent.transform.childCount; i++)
        {
            Destroy(selectedContent.transform.GetChild(i).gameObject);
        }

        
        offsetX = 500;
        foreach (var hero in selectedHeroes)
        {
            //hero.transform.SetParent(selectedContent.transform);
            //hero.transform.position = Vector3.zero;
            //hero.MoveToCenter();
            //hero.MoveLeft(offsetX);
            //offsetX -= 250;


            MMUnitNode node = MMUnitNode.Create(hero.unit);
            node.transform.SetParent(selectedContent.transform);
            node.MoveToCenter();
            node.MoveLeft(offsetX);
            offsetX -= 250;
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

        MMExplorePanel.Instance.AcceptProps();
        MMExplorePanel.Instance.AcceptHeroes(units);
        MMExplorePanel.Instance.AcceptCards(null);
        MMExplorePanel.Instance.AcceptMinions(null);
        MMExplorePanel.Instance.AcceptItems(null);
        MMExplorePanel.Instance.AcceptPlaces(null);

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

        selectedHeroes.Add(hero);
        UpdateUI();

        return true;
    }


    public bool UnselectHero(MMHeroNode hero)
    {
        if (this.selectedHeroes.Contains(hero))
        {
            this.selectedHeroes.Remove(hero);
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
