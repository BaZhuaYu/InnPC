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
    public Text mainText;
    public Text goldText;
    
    public List<MMPlaceNode> places;

    /// <summary>
    /// Public 
    /// </summary>

    public int gold;

    public int level;

    public int hp;

    public List<MMUnit> units;
    
    public List<MMCard> cards;

    public List<MMUnit> minions;

    public List<MMItem> items;


    /// <summary>
    /// Private
    /// </summary>


    void Start()
    {
        mainButton = GameObject.Find("PanelGameOver/MainButton").GetComponent<Button>();
        mainText = GameObject.Find("PanelGameOver/MainButton/Text").GetComponent<Text>();
        mainButton.onClick.AddListener(OnClickMainButton);
        
        LoadReward();

        CloseUI();
        
    }


    public void StartNewGame()
    {
        gold = 100;
        level = 1;
        hp = 100;

        units = new List<MMUnit>();
        cards = new List<MMCard>();
        minions = new List<MMUnit>();
        items = new List<MMItem>();

        //for (int i = 0; i < 3; i++)
        //{
        //    MMUnit unit = MMUnit.Create((i + 1) * 100 + 10000);
        //    units.Add(unit);

        //    MMCard card = MMCard.Create(unit.id + 1);
        //    cards.Add(card);
        //}

        //for (int i = 0; i < 4; i++)
        //{
        //    MMCard cardx = MMCard.Create(10000);
        //    MMCard cardy = MMCard.Create(10000);
        //    cards.Add(cardx);
        //    cards.Add(cardy);
        //}
    }


    public void Accept(List<MMUnit> units)
    {
        this.units = units;
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


    void LoadReward()
    {

        places = new List<MMPlaceNode>();

        MMPlaceNode place1 = MMPlaceNode.Create("LuoYangCheng");
        MMPlaceNode place2 = MMPlaceNode.Create("JiShi");
        MMPlaceNode place3 = MMPlaceNode.Create("YouJianKeZhan");
        places.Add(place1);
        places.Add(place2);
        places.Add(place3);

        place1.SetParent(this);
        place2.SetParent(this);
        place3.SetParent(this);

        place1.MoveUp(150);
        place2.MoveUp(150);
        place3.MoveUp(150);

        place1.MoveLeft(500);
        place3.MoveRight(500);
        
        UpdateUI();
    }

    public void SetWin()
    {
        isWin = true;
        isLost = false;

        MMExplorePanel.Instance.level += 1;

        int coin = HandleRewardGold();
        MMExplorePanel.Instance.gold += coin;
        
        this.SetActive(true);

        UpdateUI();
    }


    public void SetLost()
    {
        isWin = false;
        isLost = true;

        int coin = HandleRewardGold();
        MMExplorePanel.Instance.gold += coin;

        this.SetActive(true);

        MMRewardPanel.instance.CloseUI();

        UpdateUI();
    }


    public void UpdateUI()
    {
        if (isWin)
        {
            mainText.text = "下一关";
        }
        else
        {
            mainText.text = "重新战斗";
        }

        mainText.text = "开始战斗:" + (MMExplorePanel.Instance.level);
        
        goldText.text = MMExplorePanel.Instance.gold + "";
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

        this.SetActive(false);
        MMBattleManager.Instance.LoadLevel();
        MMBattleManager.Instance.gameObject.SetActive(true);
        //MMBattleManager.Instance.EnterPhase(MMBattlePhase.Begin);
    }


    int HandleRewardGold()
    {
        int ret = MMExplorePanel.Instance.level + 2;
        if (ret > 10)
        {
            ret = 10;
        }
        return ret;
    }
    
}
