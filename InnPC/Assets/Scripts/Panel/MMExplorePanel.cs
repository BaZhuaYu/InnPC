using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMExplorePanel : MMNode
{
    public static MMExplorePanel Instance;

    private void Awake()
    {
        Instance = this;
    }



    public Button mainButton;
    public Button discoverButton;
    public Text mainText;

    public Text textLevel;
    public Text textGold;
    public Text textExp;
    public Text textTime;
    
    public Text textDiscover;


    /// <summary>
    /// Public 
    /// </summary>
    [HideInInspector]
    public bool isWin;
    [HideInInspector]
    public bool isLost;

    [HideInInspector]
    public int hp;
    [HideInInspector]
    public int tansuoGold;
    [HideInInspector]
    public int tansuoExp;
    [HideInInspector]
    public int tansuoTime;

    [HideInInspector]
    public int levelBattle;
    [HideInInspector]
    public int tansuoLevel;

    [HideInInspector]
    List<int> exps;


    [HideInInspector]
    public List<MMPlaceNode> places;
    [HideInInspector]
    public List<MMUnit> units;
    [HideInInspector]
    public List<MMCard> cards;
    [HideInInspector]
    public List<MMUnit> minions;
    [HideInInspector]
    public List<MMItem> items;


    /// <summary>
    /// Private
    /// </summary>


    void Start()
    {
        mainButton = GameObject.Find("PanelExplore/MainButton").GetComponent<Button>();
        mainText = mainButton.GetComponentInChildren<Text>();
        mainButton.onClick.AddListener(OnClickMainButton);
        
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
        levelBattle = 1;

        tansuoLevel = 1;
        tansuoGold = 100;
        tansuoExp = 0;
        tansuoTime = 12;
    }


    public void AcceptHeroes(List<MMUnit> units)
    {
        this.units = units;
        //foreach(var unit in units)
        //{
        //    unit.maxHP += 30;
        //    unit.hp += 30;
        //    unit.atk += 10;
        //    unit.ap += 5;
        //}
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

        MMPlaceNode place1 = MMPlaceNode.Create(1);
        places.Add(place1);
        
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
        MMExplorePanel.Instance.tansuoGold += coin;
        
        tansuoExp += 1;

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
        MMExplorePanel.Instance.tansuoGold += coin;
        
        tansuoExp += 1;

        OpenUI();
        MMRewardPanel.instance.CloseUI();

        UpdateUI();
    }


    public void UpdateUI()
    {
        mainText.text = "开始战斗: " + levelBattle;
        textLevel.text = tansuoLevel + "";
        textGold.text = tansuoGold + "";
        textExp.text = tansuoExp + "";
        textTime.text = tansuoTime + "";
        textDiscover.text = "新地点（" + tansuoExp + "/" + exps[tansuoLevel] + "）";


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
        this.CloseUI();
        MMBattleManager.Instance.OpenUI();
        MMBattleManager.Instance.EnterPhase(MMBattlePhase.None);
        MMBattleManager.Instance.panelGameover.SetActive(false);
    }


    public void OnClickButtonDiscover()
    {
        if (this.places.Count >= MMPlace.places.Count)
        {
            MMTipManager.instance.CreateTip("没有更多地点");
            return;
        }

        if (tansuoGold < (exps[tansuoLevel] - tansuoExp))
        {
            MMTipManager.instance.CreateTip("银两不足");
            return;
        }

        tansuoGold -= (exps[tansuoLevel] - tansuoExp);
        tansuoLevel += 1;
        tansuoExp = 0;

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



    public void GainExp(int value)
    {
        this.tansuoExp += value;
        MMTipManager.instance.CreateTip("获得" + value + "点经验" );
    }

    public void GainGold(int value)
    {
        this.tansuoGold += value;
        MMTipManager.instance.CreateTip("获得" + value + "两银子");
    }

}
