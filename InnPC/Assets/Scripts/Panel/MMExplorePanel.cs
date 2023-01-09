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

    public MMNode topBar;
    public MMNode botBar;

    public Button mainButton;
    public Text mainText;

    public Text textLevel;
    public Text textGold;
    public Text textExp;
    public Text textTime;


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
    public int tansuoEvil;
    [HideInInspector]
    public int tansuoDay;


    [HideInInspector]
    public int levelBattle;
    
    
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
    [HideInInspector]
    public List<MMQuest> quests;

    /// <summary>
    /// Private
    /// </summary>


    void Start()
    {
        mainButton = GameObject.Find("PanelExplore/BotBar/MainButton").GetComponent<Button>();
        mainText = mainButton.GetComponentInChildren<Text>();
        mainButton.onClick.AddListener(OnClickMainButton);

        quests = new List<MMQuest>();

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

        tansuoDay = 1;
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
        UpdateUI();
    }


    public void UpdateUI()
    {
        mainText.text = "开始战斗: " + levelBattle;
        textLevel.text = tansuoDay + "";
        textGold.text = tansuoGold + "";
        textExp.text = tansuoExp + "";
        textTime.text = tansuoTime + "";
        MMEvilPanel.Instance.UpdateUI();

        //Reward
        float offsetX = 500f;
        float offsetY = 200f;

        for (int i = 0; i < places.Count; i++)
        {
            MMPlaceNode place = places[i];
            place.SetParent(this);
            place.MoveUp(offsetY);
            place.MoveLeft(offsetX);

            offsetX -= 500f;
            if (i % 3 == 2)
            {
                offsetX = 500;
                offsetY -= 400;
            }
        }


        MMQuestIcon[] a = botBar.GetComponentsInChildren<MMQuestIcon>();
        foreach(var aa in a)
        {
            Destroy(aa.gameObject);
        }
        
        float offset = botBar.FindWidth() * 0.3f;
        foreach (var quest in quests)
        {
            MMQuestIcon icon = MMQuestIcon.Create(quest);
            botBar.AddChild(icon);
            icon.gameObject.name = "MMQuestIcon";
            icon.MoveRight(offset);
            offset -= icon.FindWidth();
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



    int HandleRewardGold()
    {
        int ret = MMExplorePanel.Instance.levelBattle + 2;
        if (ret > 10)
        {
            ret = 10;
        }
        return ret;
    }

   

    MMPlace FindRandomPlace()
    {
        MMPlace place = MMPlace.FindRandomOne();
        if (HasPlace(place))
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
        MMTipManager.instance.CreateTip("获得" + value + "点经验");
    }

    public void GainGold(int value)
    {
        this.tansuoGold += value;
        MMTipManager.instance.CreateTip("获得" + value + "两银子");
    }

}
