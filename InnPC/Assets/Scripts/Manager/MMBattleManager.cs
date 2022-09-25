using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMBattleManager : MonoBehaviour
{

    public static MMBattleManager instance;
    
    public Button main;
    public Text buttonMain;
    public Text title;


    public MMNode background;

    public MMBattleState state;
    public MMBattleUXState uxState;

    public List<MMNodeUnit> units1;
    public List<MMNodeUnit> units2;

    


    public MMNodeCard selectedCard;
    public MMNodeUnit selectedUnit;
    public MMNodeUnit sourceUnit;
    public MMNodeUnit targetUnit;
    

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        LoadLevel();
        EnterState(MMBattleState.Begin);
        main.onClick.AddListener(OnClickMainButton);
    }


    public void LoadLevel()
    {
        for (int i =0;i<4;i++)
        {
            MMUnit unit1 = MMUnit.Create(i + 1);
            MMNodeUnit node1 = MMNodeUnit.Create();
            node1.group = 1;
            node1.Accept(unit1);
            units1.Add(node1);
            MMMap.instance.FindCellOfIndex(i).Accept(node1);
        }

        for (int i = 0; i < 4; i++)
        {
            MMUnit unit2 = MMUnit.Create(4-i);
            MMNodeUnit node2 = MMNodeUnit.Create();
            node2.group = 2;
            node2.Accept(unit2);
            units2.Add(node2);
            MMMap.instance.FindCellOfIndex(32 + i).Accept(node2);
        }
        
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

        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("没有己方英雄");
            return;
        }

        selectedCard.ExecuteEffect(sourceUnit.cell, targetUnit.cell);        
        MMCardManager.instance.PlayCard(selectedCard);
        
    }


    public void EnterUXState(MMBattleUXState state)
    {
        this.uxState = state;
        switch(state)
        {
            case MMBattleUXState.Normal:
                break;
            case MMBattleUXState.SelectSour:
                sourceUnit.ShowMoveCells();
                break;
            case MMBattleUXState.SourMoved:
                Debug.Log("MMBattleUXState.SourMoved");
                sourceUnit.HideMoveCells();
                break;
            case MMBattleUXState.SelectCard:
                sourceUnit.HideMoveCells();
                sourceUnit.ShowAttackCells();
                break;
        }
    }


    

    public void OnClickMainButton()
    {
        if (state == MMBattleState.Begin)
        {
            EnterState(MMBattleState.PlayerRound);
        }
        else if (state == MMBattleState.PlayerRound)
        {
            if(sourceUnit == null)
            {
                EnterState(MMBattleState.EnemyRound);
            }
            else
            {
                UnselectSourceCell();
            }
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
        //MMCardManager.instance.Draw(4);
        //SetSourceCell(units1[0].cell);

    }


    public void OnEnterEnemyState()
    {
        StartCoroutine(ConfigEnemyAI());
    }




    public void DrawCard(int count)
    {
        MMCardManager.instance.Draw(count);
    }


    public void ClearCard()
    {
        //MMCardManager.instance.clear
    }














    public void SetSource(MMNodeUnit unit)
    {
        sourceUnit = unit;
        sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
        MMCardManager.instance.ShowHandCards(sourceUnit.cards);
        sourceUnit.ShowMoveCells();
        sourceUnit.ShowAttackCells();
    }


    public void SetTarget(MMNodeUnit unit)
    {
        targetUnit = unit;
        targetUnit.cell.EnterHighlight(MMNodeHighlight.Red);

        sourceUnit.HideMoveCells();
        sourceUnit.HideAttackCells();
        sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
    }


    public void ClearSource()
    {
        if (sourceUnit == null)
        {
            return;
        }
        sourceUnit.cell.EnterState(MMNodeState.Normal);
        sourceUnit.cell.EnterHighlight(MMNodeHighlight.Normal);
        sourceUnit.HideMoveCells();
        sourceUnit.HideAttackCells();
        MMCardManager.instance.HideHandCards();
        sourceUnit = null;
    }


    public void ClearTarget()
    {
        if(targetUnit == null)
        {
            return;
        }
        targetUnit.cell.EnterState(MMNodeState.Normal);
        targetUnit.cell.EnterHighlight(MMNodeHighlight.Normal);
        targetUnit = null;
    }

    
    public void UnselectSourceCell()
    {
        this.ClearSource();
        this.ClearTarget();
    }


    public void OnClickButtonBack()
    {
        this.ClearSource();
        this.ClearTarget();
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
