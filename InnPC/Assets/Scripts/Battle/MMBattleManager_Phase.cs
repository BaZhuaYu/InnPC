using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MMBattlePhase
{
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
        if (p == MMBattlePhase.BattleBegin)
        {
            if (phase != MMBattlePhase.BattleEnd)
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

        UpdateUI();

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
            case MMBattlePhase.BattleBegin:
                round = 0;
                historySkills = new Dictionary<int, List<MMSkillNode>>();

                MMCardPanel.Instance.LoadDeck(MMExplorePanel.Instance.cards);
                MMCardPanel.Instance.ShuffleDeck();
                isPlayerRound = 1;

                DebugConfig();

                DrawCards(4, true);
                
                foreach(var unit in units1)
                {
                    unit.OnBattleBegin();
                }
                foreach (var unit in units2)
                {
                    unit.OnBattleBegin();
                }


                break;

            case MMBattlePhase.RoundBegin:
                round += 1;
                historySkills.Add(round, new List<MMSkillNode>());

                if (isPlayerRound == 1)
                {
                    MMRoundNode node = MMRoundNode.Create();
                    node.SetPlayerRound();

                    DrawCards(2);
                    foreach (var unit in units1)
                    {
                        unit.OnRoundBegin();
                    }
                }
                else
                {
                    MMRoundNode node = MMRoundNode.Create();
                    node.SetEnemyRound();

                    foreach (var unit in units2)
                    {
                        unit.OnRoundBegin();
                    }

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
                Debug.Log("UnitBegin: " + " " + sourceUnit.displayName + " " + sourceUnit.cell.index);
                sourceUnit.OnActive();
                //EnterState(MMBattleState.SelectedSourceUnit);
                MMSkillPanel.Instance.Accept(sourceUnit);
                break;


            case MMBattlePhase.UnitActing:
                Debug.Log("UnitActing: " + " " + sourceUnit.displayName + " " + sourceUnit.cell.index);
                if (isPlayerRound == 1)
                {
                    
                }
                else
                {
                    AutoUnitActing();
                }
                break;


            case MMBattlePhase.UnitEnd:
                Debug.Log("UnitEnd: " + " " + sourceUnit.displayName + " " + sourceUnit.cell.index);
                if (sourceUnit.state == MMUnitState.Dead)
                {
                    isSourceUnitDead = true;
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
                isPlayerRound = (isPlayerRound + 1) % 2;
                
                break;


            case MMBattlePhase.BattleEnd:
                isPlayerRound = 0;
                MMBattleManager.Instance.Clear();
                panelGameover.gameObject.SetActive(true);
                break;

        }
    }


    void UpdateUI()
    {
        ClosePanels();
        
        switch (phase)
        {
            case MMBattlePhase.BattleEnd:
                ShowTitle("Begin");
                ShowButton("End");
                MMUnitPanel.Instance.OpenUI();
                
                break;

            case MMBattlePhase.BattleBegin:
                ShowTitle("Round 1");
                ShowButton("Begin");
                MMUnitPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.PickUnit:
                ShowButton("PickUnit");
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                MMSkillPanel.Instance.Clear();
                break;

            case MMBattlePhase.RoundBegin:
                ShowButton("PlayerRound");
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                MMSkillPanel.Instance.Clear();
                break;

            case MMBattlePhase.RoundEnd:
                ShowButton("PlayerRound");
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                MMSkillPanel.Instance.Clear();
                break;

            case MMBattlePhase.UnitBegin:
                ShowButton("UnitBegin");
                sourceUnit.HandleHighlight(MMNodeHighlight.Green);
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                MMSkillPanel.Instance.Accept(sourceUnit);
                break;

            case MMBattlePhase.UnitActing:
                ShowButton("UnitActing");
                sourceUnit.HandleHighlight(MMNodeHighlight.Green);
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                MMSkillPanel.Instance.Accept(sourceUnit);
                break;

            case MMBattlePhase.UnitEnd:
                ShowButton("UnitEnd");
                MMCardPanel.Instance.OpenUI();
                break;

        }

        textPhase.text = phase.ToString();
        if (isPlayerRound == 1)
        {
            backgroundNote.SetColor(MMUtility.FindColorLightGreen());
        }
        else if (isPlayerRound == 2)
        {
            backgroundNote.SetColor(MMUtility.FindColorLightRed());
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
                EnterPhase(MMBattlePhase.RoundBegin);
                break;

            case MMBattlePhase.RoundBegin:
                EnterPhase(MMBattlePhase.PickUnit);
                break;

            case MMBattlePhase.PickUnit:
                if (isPlayerRound == 1)
                {
                    if (isSourceUnitDead)
                    {
                        EnterPhase(MMBattlePhase.RoundEnd);
                        return;
                    }

                    foreach (var unit in units1)
                    {
                        if (unit.isActived)
                        {
                            EnterPhase(MMBattlePhase.RoundEnd);
                            return;
                        }
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
                        Invoke("AutoBegin", 0.5f);
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
                    Invoke("AutoActing", 2.0f);
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

    void AutoActing()
    {
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
                return unit;
            }
        }

        return null;
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
