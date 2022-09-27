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

    public MMBattlePhase phase;
    public MMBattleState state;


    public List<MMUnitNode> units1;
    public List<MMUnitNode> units2;


    public MMCardNode selectedCard;
    public MMUnitNode sourceUnit;
    public MMUnitNode targetUnit;


    public MMCell tempCell;


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        LoadLevel();
        EnterPhase(MMBattlePhase.Begin);
        main.onClick.AddListener(OnClickMainButton);
    }


    public void LoadLevel()
    {
        LoadLevel1();
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

        if (selectedCard.area == MMArea.None)
        {
            selectedCard.ExecuteEffect(sourceUnit.cell, null);
        }
        else
        {
            selectedCard.ExecuteEffect(sourceUnit.cell, targetUnit.cell);
        }

        MMCardManager.instance.PlayCard(selectedCard);
        EnterState(MMBattleState.Normal);

    }


    public void EnterState(MMBattleState state)
    {
        this.state = state;
        switch (state)
        {
            case MMBattleState.Normal:
                ClearSource();
                ClearTarget();
                ClearTarget();
                break;
            case MMBattleState.SelectSour:
                sourceUnit.tempCell.Accept(sourceUnit);
                sourceUnit.ShowMoveCells();
                break;
            case MMBattleState.SourMoved:
                sourceUnit.HideMoveCells();
                break;
            case MMBattleState.SelectCard:
                sourceUnit.HideMoveCells();
                sourceUnit.ShowAttackCells();
                break;
        }

        UpdateUI();
    }



    public void OnClickMainButton()
    {
        if (phase == MMBattlePhase.Begin)
        {
            EnterPhase(MMBattlePhase.PlayerRound);
        }
        else if (phase == MMBattlePhase.PlayerRound)
        {
            if (sourceUnit == null)
            {
                EnterPhase(MMBattlePhase.EnemyRound);
            }
            else
            {
                UnselectSourceCell();
            }
        }
        else if (phase == MMBattlePhase.EnemyRound)
        {
            EnterPhase(MMBattlePhase.End);
        }
        else if (phase == MMBattlePhase.End)
        {
            EnterPhase(MMBattlePhase.Begin);
        }
    }



    public void EnterPhase(MMBattlePhase p)
    {
        this.phase = p;
        switch (p)
        {
            case MMBattlePhase.Begin:
                ShowButton("Start");
                ShowTitle("Begin");
                break;
            case MMBattlePhase.PlayerRound:
                ShowButton("End Turn");
                ShowTitle("PlayerRound");
                OnPhasePlayerRound();
                break;
            case MMBattlePhase.EnemyRound:
                ShowButton("");
                ShowTitle("EnemyRound");
                main.enabled = false;
                OnPhaseEnemyRound();
                break;
            case MMBattlePhase.End:
                ShowButton("");
                ShowTitle("End");
                main.enabled = false;
                break;
        }
    }


    public void DrawCard(int count)
    {
        MMCardManager.instance.Draw(count);
    }


    public void ClearSelectCard()
    {
        //this.sourceUnit.HideAttackCells();
        //selectedCard.MoveDown(20);
        this.selectedCard = null;
        //EnterUXState(MMBattleUXState.SourMoved);
    }














    public void SetSource(MMUnitNode unit)
    {
        sourceUnit = unit;
        sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
        MMCardManager.instance.ShowHandCards(sourceUnit.cards);
        sourceUnit.ShowMoveCells();
        sourceUnit.ShowAttackCells();
    }


    public void SetTarget(MMUnitNode unit)
    {
        targetUnit = unit;
        targetUnit.cell.EnterHighlight(MMNodeHighlight.Red);

        sourceUnit.HideMoveCells();
        sourceUnit.HideAttackCells();
        sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
    }


    public void SetSelectCard(MMCardNode card)
    {
        this.selectedCard = card;
        card.MoveUp(20);
        sourceUnit.ShowAttackCells();
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
        if (targetUnit == null)
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
        if (this.state == MMBattleState.SelectCard)
        {
            this.ClearSelectCard();
            this.EnterState(MMBattleState.SourMoved);
        }
        else if (this.state == MMBattleState.SourMoved)
        {
            this.EnterState(MMBattleState.SelectSour);
        }
        else if (this.state == MMBattleState.SelectSour)
        {
            this.ClearSource();
            this.EnterState(MMBattleState.Normal);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnClickButtonBack();
        }
    }


    private void UpdateUI()
    {
        foreach (var cell in MMMap.instance.cells)
        {
            cell.EnterState(MMNodeState.Normal);
            cell.EnterHighlight(MMNodeHighlight.Normal);
        }


        foreach (var card in MMCardManager.instance.hand.cards)
        {
            card.MoveToCenterY();
        }

        switch (this.state)
        {
            case MMBattleState.Normal:
                break;
            case MMBattleState.SelectSour:
                sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
                sourceUnit.ShowMoveCells();
                break;
            case MMBattleState.SourMoved:
                sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
                break;
            case MMBattleState.SelectCard:
                sourceUnit.cell.EnterHighlight(MMNodeHighlight.Green);
                sourceUnit.ShowAttackCells();
                selectedCard.MoveUp(20);
                break;
        }



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
