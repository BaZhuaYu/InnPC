using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMExplorePanel : MMNode
{
    public static MMExplorePanel Instance;

    private void Awake()
    {
        Instance = this;
    }


    public bool isWin;
    public bool isLost;

    public Button mainButton;
    public Button discoverButton;
    public Text mainText;
    public Text goldText;
    public Text numText;
    public Text textDiscover;


    /// <summary>
    /// Public 
    /// </summary>

    public int hp;

    public int gold;

    public int levelBattle;

    public int levelShop;
    
    public int expTanSuo;


    List<int> exps;
    


    public List<MMPlaceNode> places;

    public List<MMUnit> units;

    public List<MMCard> cards;

    public List<MMUnit> minions;

    public List<MMItem> items;


    /// <summary>
    /// Private
    /// </summary>


    void Start()
    {
        mainButton = GameObject.Find("PanelExplore/MainButton").GetComponent<Button>();
        mainText = mainButton.GetComponentInChildren<Text>();
        mainButton.onClick.AddListener(OnClickMainButton);

        numText = GameObject.Find("PanelExplore/BGNum/Text").GetComponent<Text>();

        discoverButton = GameObject.Find("PanelExplore/DiscoverButton").GetComponent<Button>();
        discoverButton.onClick.AddListener(OnClickButtonDiscover);

        textDiscover = discoverButton.GetComponentInChildren<Text>();

        exps = new List<int>() {0, 1, 2, 4, 7, 11, 16, 22 };

        CloseUI();
    }


    public override void OpenUI()
    {
        base.OpenUI();
        UpdateUI();
    }



    public void AcceptProps()
    {
        hp = 100;
        gold = 100;
        levelBattle = 1;
        levelShop = 1;
        expTanSuo = 0;
    }


    public void AcceptHeroes(List<MMUnit> units)
    {
        this.units = units;
    }

    public void AcceptCards(List<MMCard> cards)
    {
        this.cards = new List<MMCard>();
        
        this.cards.Add(MMCard.Create(1003));
        this.cards.Add(MMCard.Create(1003));
        this.cards.Add(MMCard.Create(1005));
        this.cards.Add(MMCard.Create(1005));
        this.cards.Add(MMCard.Create(1005));
        this.cards.Add(MMCard.Create(1007));
        this.cards.Add(MMCard.Create(1007));
        this.cards.Add(MMCard.Create(1007));


        foreach (var unit in units)
        {
            foreach (var id in unit.cards)
            {
                this.cards.Add(MMCard.Create(id));
            }
        }
    }

    public void AcceptMinions(List<MMUnit> units)
    {
        this.minions = new List<MMUnit>();
    }

    public void AcceptItems(List<MMItem> items)
    {
        this.items = new List<MMItem>();
    }

    public void AcceptPlaces(MMPlaceNode p)
    {
        this.places = new List<MMPlaceNode>();

        MMPlaceNode place1 = MMPlaceNode.Create("Place_JiShi");
        places.Add(place1);

        //MMPlaceNode place2 = MMPlaceNode.Create("Place_YouJianKeZhan");
        //places.Add(place2);

        //MMPlaceNode place3 = MMPlaceNode.Create("Place_LuoYangJiaoWai");
        //places.Add(place3);
    }


    public List<MMUnitNode> CreateAllUnitNodes()
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();

        foreach (var unit in units)
        {
            MMUnitNode node = MMUnitNode.Create();
            node.Accept(unit);
            ret.Add(node);
        }

        return ret;
    }


    public bool HasUnit(MMUnit unit)
    {
        foreach (var temp in units)
        {
            if (unit.id == temp.id)
            {
                return true;
            }
        }

        return false;
    }


    public MMUnit FindUnit(int id)
    {
        foreach (var unit in units)
        {
            if (unit.id == id)
            {
                return unit;
            }
        }
        return null;
    }



    public void SetWin()
    {
        isWin = true;
        isLost = false;

        MMExplorePanel.Instance.levelBattle += 1;

        int coin = HandleRewardGold();
        MMExplorePanel.Instance.gold += coin;
        
        expTanSuo += 1;

        OpenUI();
        MMRewardPanel.instance.CloseUI();

        UpdateUI();
    }


    public void SetLost()
    {
        isWin = false;
        isLost = true;

        MMExplorePanel.Instance.levelBattle += 1;

        int coin = HandleRewardGold();
        MMExplorePanel.Instance.gold += coin;
        
        expTanSuo += 1;

        OpenUI();
        MMRewardPanel.instance.CloseUI();

        UpdateUI();
    }


    public void UpdateUI()
    {
        mainText.text = "开始战斗: " + levelBattle;
        goldText.text = gold + "";
        numText.text = "等级：" + levelShop;
        textDiscover.text = "新地点（" + expTanSuo + "/" + exps[levelShop] + "）";


        //Reward
        float offsetX = 500f;
        float offsetY = 200f;

        for(int i =0;i<places.Count;i++)
        {
            MMPlaceNode place = places[i];
            place.SetParent(this);
            place.MoveUp(offsetY);
            place.MoveLeft(offsetX);

            offsetX -= 500f;
            if(i %3 == 2)
            {
                offsetX = 500;
                offsetY -= 400;
            }
        }

    }


    public void Clear()
    {

    }


    public void OnClickMainButton()
    {
        foreach (var place in places)
        {
            place.SetEnable(true);
        }

        this.CloseUI();
        MMBattleManager.Instance.LoadLevel();
        MMBattleManager.Instance.OpenUI();
        MMBattleManager.Instance.panelGameover.SetActive(false);
    }


    public void OnClickButtonDiscover()
    {
        if (this.places.Count >= MMPlace.places.Count)
        {
            MMTipManager.instance.CreateTip("没有更多地点");
            return;
        }

        if (gold < (exps[levelShop] - expTanSuo))
        {
            MMTipManager.instance.CreateTip("银两不足");
            return;
        }

        gold -= (exps[levelShop] - expTanSuo);
        levelShop += 1;
        expTanSuo = 0;

        MMPlaceNode n = MMPlaceNode.Create(FindRandomPlace());
        this.places.Add(n);
        UpdateUI();
    }


    int HandleRewardGold()
    {
        int ret = MMExplorePanel.Instance.levelBattle + 2;
        if (ret > 10)
        {
            ret = 10;
        }
        return ret;
    }


    public void SkipReward()
    {
        MMRewardPanel.instance.CloseUI();
    }



    MMPlace FindRandomPlace()
    {
        MMPlace place = MMPlace.FindRandomOne();
        if(HasPlace(place))
        {
            return FindRandomPlace();
        }
        else
        {
            return place;
        }
    }


    bool HasPlace(MMPlace p)
    {
        foreach (var place in places)
        {
            if (place.id == p.id)
            {
                return true;
            }
        }
        return false;
    }

    
}
