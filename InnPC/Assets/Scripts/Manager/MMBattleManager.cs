using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMBattleManager : MonoBehaviour
{

    public static MMBattleManager instance;
    
    public Button main;
    public Text buttonMain;
    public Text title;


    public MMNode background;

    public MMBattleState state;

    public List<MMNodeUnit> units1;
    public List<MMNodeUnit> units2;


    public MMCell cellSource;
    public MMCell cellTarget;


    public MMNodeCard selectedCard;
    public MMNodeUnit selectedUnit;



    public LineRenderer liner;
    public int currentPoint;



    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        LoadLevel();
        EnterState(MMBattleState.Begin);
        main.onClick.AddListener(OnClickMainButton);

        liner = GetComponent<LineRenderer>();
        liner.material = new Material(Shader.Find("Standard"));
        liner.positionCount = 2;

        liner.startWidth = liner.endWidth = 5f;
        liner.material.SetColor("_Color", Color.black);

        currentPoint = 0;
    }


    public void LoadLevel()
    {
        //MMMap.instance.FindCellOfIndex(0).AcceptUnit("Unit_10100QS");
        MMUnit unit1 = MMUnit.Create(1);
        MMNodeUnit node1 = MMNodeUnit.Create();
        node1.group = 1;
        node1.Accept(unit1);

        //MMMap.instance.FindCellOfIndex(32).AcceptUnit("Unit_10200QS");
        MMUnit unit2 = MMUnit.Create(2);
        MMNodeUnit node2 = MMNodeUnit.Create();
        node2.group = 2;
        node2.Accept(unit2);


        units1.Add(node1);
        units2.Add(node2);

        MMMap.instance.FindCellOfIndex(0).Accept(node1);
        MMMap.instance.FindCellOfIndex(32).Accept(node2);
    }



    public void SetSelectedUnit(MMNodeUnit unit)
    {
        this.selectedUnit = unit;
    }

    

    public void PlayCard()
    {
        if (this.selectedCard == null)
        {
            MMTipManager.instance.CreateTip("没有卡牌");
            return;
        }

        if (this.cellSource.nodeUnit == null)
        {
            MMTipManager.instance.CreateTip("没有己方英雄");
            return;
        }

        selectedCard.ExecuteEffect(cellSource, cellTarget);        
        MMCardManager.instance.PlayCard(selectedCard);
        
    }



    public void OnClickMainButton()
    {
        if (state == MMBattleState.Begin)
        {
            EnterState(MMBattleState.PlayerRound);
        }
        else if (state == MMBattleState.PlayerRound)
        {
            EnterState(MMBattleState.EnemyRound);
        }
        else if (state == MMBattleState.EnemyRound)
        {
            EnterState(MMBattleState.End);
        }
        else if (state == MMBattleState.End)
        {
            EnterState(MMBattleState.Begin);
        }
    }

    

    public void EnterState(MMBattleState s)
    {
        this.state = s;
        switch(s)
        {
            case MMBattleState.Begin:
                ShowButton("Start");
                ShowTitle("Begin");
                break;
            case MMBattleState.PlayerRound:
                ShowButton("End Turn");
                ShowTitle("PlayerRound");
                OnEnterPlayerState();
                break;
            case MMBattleState.EnemyRound:
                ShowButton("");
                ShowTitle("EnemyRound");
                main.enabled = false;
                OnEnterEnemyState();
                break;
            case MMBattleState.End:
                ShowButton("");
                ShowTitle("End");
                main.enabled = false;
                break;
        }
    }

    
    public void OnEnterPlayerState()
    {
        MMCardManager.instance.Draw(4);
        SetSourceCell(units1[0].cell);

    }


    public void OnEnterEnemyState()
    {
        Invoke("OnClickMainButton", 3);
        Invoke("OnClickMainButton", 5);
    }


    public void SetSourceCell(MMCell cell)
    {
        if(cellSource != null)
        {
            cellSource.EnterState(MMNodeState.Normal);
        }

        cellSource = cell;
        cell.highlight = MMNodeHighlight.Green;
    }


    public void SetTargetCell(MMCell cell)
    {
        if (cellTarget != null)
        {
            cellTarget.EnterState(MMNodeState.Normal);
        }

        cellTarget = cell;
        cell.highlight = MMNodeHighlight.Red;
    }


    public void ShowButton(string s)
    {
        buttonMain.text = s;
    }

    public void ShowTitle(string s)
    {
        title.text = s;
    }
    


}
