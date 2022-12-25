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

    public int numBuy;

    public int probTanSuo;
    

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
        gold = 0;
        levelBattle = 1;
        levelShop = 1;
        numBuy = 1;
        probTanSuo = 100;
    }


    public void AcceptHeroes(List<MMUnit> units)
    {
        this.units = units;
    }

    public void AcceptCards(List<MMCard> cards)
    {
        this.cards = new List<MMCard>();
        for (int i = 0; i < 3; i++)
        {
            MMCard cardx = MMCard.Create(10000);
            MMCard cardy = MMCard.Create(10000);
            this.cards.Add(cardx);
            this.cards.Add(cardy);
        }

        foreach(var unit in units)
        {
            foreach(var id in unit.cards)
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

        MMPlaceNode place1 = MMPlaceNode.Create("LuoYangCheng");
        MMPlaceNode place2 = MMPlaceNode.Create("JiShi");
        MMPlaceNode place3 = MMPlaceNode.Create("YouJianKeZhan");
        places.Add(place1);
        //places.Add(place2);
        //places.Add(place3);
        //this.places = places;
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
        
        numBuy = 2;

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

        numBuy = 2;
        
        OpenUI();
        MMRewardPanel.instance.CloseUI();

        UpdateUI();
    }


    public void UpdateUI()
    {
        mainText.text = "开始战斗: " + levelBattle;
        goldText.text = gold + "";
        numText.text = numBuy + "";
        textDiscover.text = "探索（" + probTanSuo + "）";


        //Reward
        float offset = 500f;
        foreach (var place in places)
        {
            place.SetParent(this);
            place.MoveUp(150);
            place.MoveLeft(offset);
            offset -= 500f;
        }

    }
    

    public void Clear()
    {
        
    }

    
    public void OnClickMainButton()
    {
        foreach(var place in places)
        {
            place.SetEnable(true);
        }
        
        this.CloseUI();
        MMBattleManager.Instance.LoadLevel();
        MMBattleManager.Instance.OpenUI();
    }


    public void OnClickButtonDiscover()
    {
        if(numBuy < 1)
        {
            MMTipManager.instance.CreateTip("没有更多使用次数");
            return;
        }

        if(this.places.Count >= MMPlace.places.Count)
        {
            MMTipManager.instance.CreateTip("没有更多地点");
            return;
        }

        numBuy -= 1;
        
        if (Random.Range(0,100) > probTanSuo)
        {
            probTanSuo += 10;
            UpdateUI();
            MMTipManager.instance.CreateTip("什么都没有发现");
            return;
        }

        probTanSuo = 35;
        MMPlaceNode n = MMPlaceNode.Create(MMPlace.FindOne());
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
    
}
