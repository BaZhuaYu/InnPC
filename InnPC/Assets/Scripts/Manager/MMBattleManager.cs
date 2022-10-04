using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMBattleManager : MonoBehaviour
{

    public static MMBattleManager Instance;

    public MMNode background;
    public Button main;
    public Text buttonMain;
    public Text title;

    
    public MMBattlePhase phase;
    public MMBattleState state;


    public List<MMUnitNode> units1;
    public List<MMUnitNode> units2;


    public MMSkillNode selectingSkill;
    public MMUnitNode sourceUnit;
    public MMUnitNode targetUnit;
    

    public int level;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        level = 0;

        main.onClick.AddListener(OnClickMainButton);

        EnterPhase(MMBattlePhase.Begin);
    }

    
    public void LoadLevel()
    {
        
        if (this.level == 0)
        {
            LoadLevel0();
        }
        else if(this.level==1)
        {
            LoadLevel1();
        }
        else if (this.level == 2)
        {
            LoadLevel2();
        }
        else if (this.level == 3)
        {
            LoadLevel3();
        }
    }


    public void LoadCard()
    {

    }






    public void PlaySkill()
    {
        if (this.selectingSkill == null)
        {
            MMTipManager.instance.CreateTip("没有选择技能");
            return;
        }

        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("没有己方英雄");
            return;
        }

        if (selectingSkill.area == MMArea.None)
        {
            selectingSkill.ExecuteEffect(sourceUnit.cell, null);
        }
        else
        {
            selectingSkill.ExecuteEffect(sourceUnit.cell, targetUnit.cell);
        }

        //MMCardPanel.Instance.PlayCard(selectedSkill);
        MMSkillPanel.Instance.PlaySkill(selectingSkill);
        OnSourActionDone();
    }


    

    public void EnterState(MMBattleState state)
    {

        this.state = state;

        MMDebugManager.Log(this.state + "");
        switch (state)
        {
            case MMBattleState.Normal:
                ClearSource();
                ClearSelectSkill();
                ClearTarget();
                break;
            case MMBattleState.SelectSour:
                sourceUnit.tempCell.Accept(sourceUnit);
                sourceUnit.ShowMoveCells();
                break;
            case MMBattleState.SourMoved:
                sourceUnit.HideMoveCells();
                break;
            case MMBattleState.SelectSkill:
                sourceUnit.HideMoveCells();
                sourceUnit.ShowAttackCells();
                break;
            case MMBattleState.SourDone:
                sourceUnit.tempCell = sourceUnit.cell;
                EnterState(MMBattleState.Normal);
                return;
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
            OnPhaseEnd();
            EnterPhase(MMBattlePhase.PlayerRound);
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
                main.enabled = true;
                break;
            case MMBattlePhase.PlayerRound:
                ShowButton("End Turn");
                ShowTitle("PlayerRound");
                main.enabled = true;
                OnPhasePlayerRound();
                break;
            case MMBattlePhase.EnemyRound:
                ShowButton("Wait");
                ShowTitle("EnemyRound");
                main.enabled = false;
                OnPhaseEnemyRound();
                break;
            case MMBattlePhase.End:
                ShowButton("End");
                ShowTitle("End");
                main.enabled = false;
                break;
        }
    }


    public void DrawSkill(int count)
    {
        //MMCardPanel.Instance.Draw(count);

        MMSkillPanel.Instance.Accept(sourceUnit.cards);
    }


    public void ClearSelectSkill()
    {
        //this.sourceUnit.HideAttackCells();
        //selectedCard.MoveDown(20);
        this.selectingSkill = null;
        //EnterUXState(MMBattleUXState.SourMoved);
    }


    public void Clear()
    {

        this.EnterPhase(MMBattlePhase.End);
        this.EnterState(MMBattleState.Normal);

        foreach(var unit in units1)
        {
            unit.Clear();
        }

        foreach (var unit in units2)
        {
            unit.Clear();
        }
        
        units1.Clear();
        units2.Clear();
        MMMap.Instance.Clear();
        
    }



    


    public void AutoSelectSour()
    {

        List<MMUnitNode> units = FindSortedUnits1();
        foreach (var unit in units)
        {
            if (unit.unitPhase == MMUnitPhase.Combo)
            {
                SetSource(unit);
                EnterState(MMBattleState.SelectSour);
                return;
            }
        }

        foreach (var unit in units)
        {
            if (unit.unitPhase == MMUnitPhase.Normal)
            {
                SetSource(unit);
                EnterState(MMBattleState.SelectSour);
                return;
            }
        }


        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("己方回合行动结束");
        }
    }



    public void OnClickButtonBack()
    {
        if (this.state == MMBattleState.SelectSkill)
        {
            this.ClearSelectSkill();
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


        if(this.state == MMBattleState.SelectSour || this.state == MMBattleState.SourMoved)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetSelectingSkill(sourceUnit.cards[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetSelectingSkill(sourceUnit.cards[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetSelectingSkill(sourceUnit.cards[2]);
            }
            //else if (Input.GetKeyDown(KeyCode.Alpha4))
            //{
            //    SetSelectSkill(MMCardManager.instance.hand.cards[3]);
            //}
        }
        
    }


    private void UpdateUI()
    {
        foreach (var cell in MMMap.Instance.cells)
        {
            cell.HandleState(MMNodeState.Normal);
            cell.HandleHighlight(MMNodeHighlight.Normal);
        }

        foreach (var card in MMCardPanel.Instance.hand)
        {
            card.MoveToCenterY();
        }

        switch (this.state)
        {
            case MMBattleState.Normal:
                break;
            case MMBattleState.SelectSour:
                sourceUnit.cell.HandleHighlight(MMNodeHighlight.Green);
                sourceUnit.ShowMoveCells();
                break;
            case MMBattleState.SourMoved:
                sourceUnit.cell.HandleHighlight(MMNodeHighlight.Green);
                break;
            case MMBattleState.SelectSkill:
                sourceUnit.cell.HandleHighlight(MMNodeHighlight.Green);
                sourceUnit.ShowAttackCells();
                selectingSkill.MoveUp(20);
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
