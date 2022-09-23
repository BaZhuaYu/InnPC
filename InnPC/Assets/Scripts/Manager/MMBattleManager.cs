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

    public List<MMNodeUnit> units1;
    public List<MMNodeUnit> units2;


    public MMCell cellSource;
    public MMCell cellTarget;


    public MMNodeCard selectedCard;
    public MMNodeUnit selectedUnit;

    

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
            if(cellSource == null)
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














    public void SetSourceCell(MMCell cell)
    {
        cellSource = cell;
        cellSource.EnterHighlight(MMNodeHighlight.Green);
        MMCardManager.instance.ShowHandCards(cell.nodeUnit.cards);
        cell.nodeUnit.ShowMoveCells();
        cell.nodeUnit.ShowAttackCells();
    }


    public void SetTargetCell(MMCell cell)
    {
        cellTarget = cell;
        cellTarget.EnterHighlight(MMNodeHighlight.Red);

        cellSource.nodeUnit.HideMoveCells();
        cellSource.nodeUnit.HideAttackCells();
        cellSource.EnterHighlight(MMNodeHighlight.Green);
    }


    public void ClearSourceCell()
    {
        if (cellSource == null)
        {
            return;
        }
        cellSource.EnterState(MMNodeState.Normal);
        cellSource.EnterHighlight(MMNodeHighlight.Normal);
        cellSource.nodeUnit.HideMoveCells();
        cellSource.nodeUnit.HideAttackCells();
        MMCardManager.instance.HideHandCards();
        cellSource = null;
    }


    public void ClearTargetCell()
    {
        if(cellTarget == null)
        {
            return;
        }
        cellTarget.EnterState(MMNodeState.Normal);
        cellTarget.EnterHighlight(MMNodeHighlight.Normal);
        cellTarget = null;
    }

    
    public void UnselectSourceCell()
    {
        this.ClearSourceCell();
        this.ClearTargetCell();
    }


    public void OnClickButtonBack()
    {
        this.ClearSourceCell();
        this.ClearTargetCell();
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
