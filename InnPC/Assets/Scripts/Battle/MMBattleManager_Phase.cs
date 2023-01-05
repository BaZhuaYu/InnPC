using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MMBattlePhase
{
    None,
    BattleBegin,

    RoundBegin,

    PickUnit,
    UnitBegin,
    UnitActing,
    UnitEnd,

    RoundEnd,

    BattleEnd,
}


public partial class MMBattleManager
{
    bool isSourceUnitDead = false;
    List<MMUnitNode> tempEnemyUnits;

    public void EnterPhase(MMBattlePhase p)
    {

        if(isLocked)
        {
            return;
        }
        
        if (p == MMBattlePhase.BattleBegin)
        {
            if (phase != MMBattlePhase.None)
            {
                Debug.Log("From: " + phase.ToString() + " To: " + p.ToString());
                return;
            }
        }

        if (p == MMBattlePhase.RoundBegin)
        {
            if (phase != MMBattlePhase.BattleBegin && phase != MMBattlePhase.RoundEnd)
            {
                Debug.Log("From: " + phase.ToString() + " To: " + p.ToString());
                return;
            }
        }

        if (p == MMBattlePhase.PickUnit)
        {
            if (phase != MMBattlePhase.RoundBegin && phase != MMBattlePhase.UnitEnd)
            {
                Debug.Log("From: " + phase.ToString() + " To: " + p.ToString());
                return;
            }
        }

        if (p == MMBattlePhase.UnitBegin)
        {
            if (phase != MMBattlePhase.PickUnit)
            {
                Debug.Log("From: " + phase.ToString() + " To: " + p.ToString());
                return;
            }
        }

        if (p == MMBattlePhase.UnitActing)
        {
            if (phase != MMBattlePhase.UnitBegin && phase != MMBattlePhase.UnitActing)
            {
                Debug.Log("From: " + phase.ToString() + " To: " + p.ToString());
                return;
            }
        }

        if (p == MMBattlePhase.UnitEnd)
        {
            if (phase != MMBattlePhase.UnitActing)
            {
                Debug.Log("From: " + phase.ToString() + " To: " + p.ToString());
                return;
            }
        }


        if (p == MMBattlePhase.RoundEnd)
        {
            if (phase != MMBattlePhase.PickUnit)
            {
                Debug.Log("From: " + phase.ToString() + " To: " + p.ToString());
                return;
            }
        }

        if (p == MMBattlePhase.BattleEnd)
        {
            if (phase == MMBattlePhase.BattleEnd)
            {
                Debug.Log("From: " + phase.ToString() + " To: " + p.ToString());
                return;
            }
        }


        OnExitPhase(this.phase);
        this.phase = p;
        OnEnterPhase(p);

        UpdateUI_Phase();

        AutoRoute_Phase();
    }


    void OnExitPhase(MMBattlePhase p)
    {
        Debug.Log("--------" + p.ToString() + "--------" + isPlayerRound);
    }


    void OnEnterPhase(MMBattlePhase p)
    {
        Debug.Log("++++++++" + phase.ToString() + "++++++++" + isPlayerRound);

        switch (p)
        {
            case MMBattlePhase.None:
                isPlayerRound = 0;
                round = 0;
                LoadLevel();
                MMCardPanel.Instance.LoadDeck(MMExplorePanel.Instance.cards);
                MMCardPanel.Instance.ShuffleDeck();
                historySkills = new Dictionary<int, List<MMSkillNode>>();
                break;


            case MMBattlePhase.BattleBegin:
                
                foreach (var unit in units1)
                {
                    unit.OnBattleBegin();
                }
                foreach (var unit in units2)
                {
                    unit.OnBattleBegin();
                }

                DrawCards(4, true);
                DebugConfig();
                break;


            case MMBattlePhase.RoundBegin:
                round += 1;
                historySkills.Add(round, new List<MMSkillNode>());

                if (isPlayerRound == 1)
                {
                    MMRoundNode node = MMRoundNode.Create();
                    node.SetPlayerRound();
                    foreach (var unit in units1)
                    {
                        unit.OnRoundBegin();
                    }

                    DrawCards(2);
                }
                else
                {
                    foreach (var unit in units2)
                    {
                        unit.OnRoundBegin();
                    }

                    MMRoundNode node = MMRoundNode.Create();
                    node.SetEnemyRound();
                    
                    tempEnemyUnits = new List<MMUnitNode>();
                    foreach (var unit in units2)
                    {
                        if (unit.ap == unit.maxAP && unit.isActived == false)
                        {
                            tempEnemyUnits.Add(unit);
                        }
                    }
                }
                EnterState(MMBattleState.Normal);
                break;


            case MMBattlePhase.PickUnit:
                break;


            case MMBattlePhase.UnitBegin:
                sourceUnit.OnUnitBegin();
                MMSkillPanel.Instance.Accept(sourceUnit);
                break;


            case MMBattlePhase.UnitActing:
                if (isPlayerRound == 1)
                {
                    
                }
                else
                {
                    AutoUnitActing();
                }
                break;


            case MMBattlePhase.UnitEnd:
                if (sourceUnit.state == MMUnitState.Dead)
                {
                    isSourceUnitDead = true;
                }
                else
                {
                    sourceUnit.OnUnitEnd();
                }
                
                HandleUnitActionDone();
                EnterState(MMBattleState.None);
                ClearDeadUnits();
                break;


            case MMBattlePhase.RoundEnd:
                if (isPlayerRound == 1)
                {
                    foreach (var unit in units1)
                    {
                        unit.OnRoundEnd();
                    }
                }
                else 
                {
                    foreach (var unit in units2)
                    {
                        unit.OnRoundEnd();
                    }
                }

                isSourceUnitDead = false;
                
                break;


            case MMBattlePhase.BattleEnd:
                MMBattleManager.Instance.Clear();
                panelGameover.gameObject.SetActive(true);
                break;

        }
    }


    public void UpdateUI()
    {
        this.UpdateUI_Phase();
        this.UpdateUI_State();
    }

    void UpdateUI_Phase()
    {

        ClosePanels();
        this.HideAllUnitAttackCells();
        
        switch (phase)
        {

            case MMBattlePhase.None:
                ShowTitle(MMLevel.Create(MMExplorePanel.Instance.levelBattle).displayName);
                ShowButton("战斗开始");
                buttonMain.gameObject.SetActive(true);
                buttonAwait.gameObject.SetActive(false);
                panelAvatar.gameObject.SetActive(false);
                MMUnitPanel.Instance.OpenUI();
                foreach(var cell in MMMap.Instance.cells)
                {
                    cell.HideDark();
                }
                break;

            case MMBattlePhase.BattleEnd:
                ShowTitle("战斗胜利");
                //ShowButton("战斗结束", false);
                buttonMain.gameObject.SetActive(false);
                buttonAwait.gameObject.SetActive(false);
                panelAvatar.gameObject.SetActive(false);
                MMUnitPanel.Instance.CloseUI();
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.CloseUI();
                break;

            case MMBattlePhase.BattleBegin:
                ShowTitle("xxxx");
                ShowButton("xxxx");
                buttonMain.gameObject.SetActive(false);
                buttonAwait.gameObject.SetActive(false);
                panelAvatar.gameObject.SetActive(false);
                MMUnitPanel.Instance.OpenUI();
                MMSkillPanel.Instance.CloseUI();
                break;

            case MMBattlePhase.PickUnit:
                ShowTitle("激活侠客");
                ShowButton("回合结束");
                buttonMain.gameObject.SetActive(false);
                buttonAwait.gameObject.SetActive(false);
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.CloseUI();
                MMSkillPanel.Instance.Clear();
                break;

            case MMBattlePhase.RoundBegin:
                ShowButton("结束");
                buttonMain.gameObject.SetActive(false);
                buttonAwait.gameObject.SetActive(false);
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.CloseUI();
                //MMSkillPanel.Instance.Clear();
                break;

            case MMBattlePhase.RoundEnd:
                ShowButton("结束");
                buttonMain.gameObject.SetActive(false);
                buttonAwait.gameObject.SetActive(false);
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.CloseUI();
                //MMSkillPanel.Instance.Clear();
                break;

            case MMBattlePhase.UnitBegin:
                ShowButton("结束");
                buttonMain.gameObject.SetActive(false);
                buttonAwait.gameObject.SetActive(true);
                sourceUnit.HandleHighlight(MMNodeHighlight.Green);
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                MMSkillPanel.Instance.Accept(sourceUnit);
                break;

            case MMBattlePhase.UnitActing:
                ShowButton("行动结束");
                buttonMain.gameObject.SetActive(false);
                buttonAwait.gameObject.SetActive(true);
                sourceUnit.HandleHighlight(MMNodeHighlight.Green);
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                MMSkillPanel.Instance.Accept(sourceUnit);
                sourceUnit.ShowTarget();
                this.ShowAllUnitAttackCells();
                break;

            case MMBattlePhase.UnitEnd:
                ShowButton("行动结束");
                buttonMain.gameObject.SetActive(false);
                buttonAwait.gameObject.SetActive(true);
                panelAvatar.gameObject.SetActive(true);
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                break;

        }

        textPhase.text = phase.ToString();
        if (isPlayerRound == 1)
        {
            backgroundNote.SetColor(MMUtility.FindColorLightGreen());
        }
        else
        {
            backgroundNote.SetColor(MMUtility.FindColorWhite());
        }
    }



    public void AutoRoute_Phase()
    {

        ClearDeadUnits();

        if(this.phase == MMBattlePhase.BattleEnd)
        {

        }
        else
        {
            if (CheckGameOver())
            {
                ShowButton("Game Over");
                return;
            }
        }
        
        
        switch (phase)
        {
            case MMBattlePhase.BattleBegin:
                isPlayerRound = (isPlayerRound + 1) % 2;
                EnterPhase(MMBattlePhase.RoundBegin);
                break;

            case MMBattlePhase.RoundBegin:
                EnterPhase(MMBattlePhase.PickUnit);
                break;

            case MMBattlePhase.PickUnit:
                if (isPlayerRound == 1)
                {
                    if(CheckPlayerHasUnit())
                    {

                    }
                    else
                    {
                        EnterPhase(MMBattlePhase.RoundEnd);
                    }
                }
                else
                {
                    MMUnitNode unit = FindFirstEnemy();
                    if(unit == null)
                    {
                        EnterPhase(MMBattlePhase.RoundEnd);
                    }
                    else
                    {
                        sourceUnit = unit;
                        Invoke("AutoBegin", 1.0f);
                    }
                }
                break;

            case MMBattlePhase.UnitBegin:
                if (isPlayerRound == 1)
                {
                    EnterPhase(MMBattlePhase.UnitActing);
                }
                else
                {
                    Invoke("AutoSelectTarget", 1.0f);
                    StartCoroutine(AutoActing(2));
                }
                break;

            case MMBattlePhase.UnitActing:
                if (isPlayerRound == 1)
                {

                }
                else
                {
                    Invoke("AutoEnd", 2.0f);
                }
                break;

            case MMBattlePhase.UnitEnd:
                if (isPlayerRound == 1)
                {
                    EnterPhase(MMBattlePhase.PickUnit);
                }
                else
                {
                    Invoke("AutoPickUnit", 0.5f);
                }
                break;

            case MMBattlePhase.RoundEnd:
                isPlayerRound = (isPlayerRound + 1) % 2;
                Invoke("AutoRoundBegin", 2.0f);
                break;

            case MMBattlePhase.BattleEnd:
                break;
        }

    }


    public void TryEnterPhase_UnitBegin(MMUnitNode unit)
    {
        sourceUnit = unit;
        EnterPhase(MMBattlePhase.UnitBegin);
    }

    void AutoBegin()
    {
        EnterPhase(MMBattlePhase.UnitBegin);
    }

    IEnumerator AutoActing(float time)
    {
        yield return new WaitForSeconds(time);
        EnterPhase(MMBattlePhase.UnitActing);
    }
    
    void AutoEnd()
    {
        EnterPhase(MMBattlePhase.UnitEnd);
    }

    void AutoPickUnit()
    {
        EnterPhase(MMBattlePhase.PickUnit);
    }

    void AutoRoundBegin()
    {
        EnterPhase(MMBattlePhase.RoundBegin);
    }

    void AutoSelectTarget()
    {
        MMUnitNode target = sourceUnit.FindTarget();
        if(target == null)
        {

        }
        else
        {
            target.HandleHighlight(MMNodeHighlight.Red);
        }
    }



    MMUnitNode FindFirstEnemy()
    {
        foreach (var unit in tempEnemyUnits)
        {
            if (unit.ap == unit.maxAP && unit.isActived == false)
            {
                Debug.LogWarning("FindFirstEnemy: " + unit.displayName);

                return unit;
            }
        }

        return null;
    }


    bool CheckPlayerHasUnit()
    {
        if(isSourceUnitDead)
        {
            return false;
        }

        foreach (var unit in units1)
        {
            if (unit.isActived)
            {
                return false;
            }
        }

        return true;
    }




    /// <summary>
    /// Private
    /// </summary>
    /// <param name="units"></param>



    public void ClearUnitEnd()
    {
        this.sourceUnit = null;
        this.targetUnit = null;
    }


}
