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

        switch (this.phase)
        {
            case MMBattlePhase.BattleBegin:
                break;

            case MMBattlePhase.RoundBegin:
                break;

            case MMBattlePhase.PickUnit:
                break;

            case MMBattlePhase.UnitBegin:

                break;

            case MMBattlePhase.UnitActing:
                
                break;

            case MMBattlePhase.UnitEnd:
                break;

            case MMBattlePhase.RoundEnd:
                isSourceUnitDead = false;
                if (isPlayerRound == 1)
                {
                    IncreaseAPUnits(units1);
                }
                else if (isPlayerRound == 2)
                {
                    IncreaseAPUnits(units2);
                }
                isPlayerRound = (isPlayerRound + 1) % 2 + 1;
                break;

            case MMBattlePhase.BattleEnd:
                break;

        }
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
                
                DebugConfig();

                DrawCards(4, true);
                isPlayerRound += 1;

                BroadCast(MMTriggerTime.OnBattleBegin);
                break;

            case MMBattlePhase.RoundBegin:
                round += 1;
                historySkills.Add(round, new List<MMSkillNode>());

                if (isPlayerRound == 1)
                {
                    DrawCards(2);
                    foreach (var unit in units1)
                    {
                        unit.tempCell = unit.cell;
                        unit.isActived = false;
                    }
                }
                else
                {
                    foreach (var unit in units2)
                    {
                        unit.isActived = false;
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
                BroadCast(MMTriggerTime.OnRoundBegin);
                break;

            case MMBattlePhase.PickUnit:
                break;

            case MMBattlePhase.UnitBegin:
                Debug.Log("UnitBegin: " + " " + sourceUnit.displayName + " " + sourceUnit.cell.index);
                sourceUnit.OnActive();
                MMSkillPanel.Instance.Accept(sourceUnit.skills);
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
                UnselectSourceCell();
                ClearDeadUnits();
                break;

            case MMBattlePhase.RoundEnd:
                BroadCast(MMTriggerTime.OnRoundEnd);
                break;


            case MMBattlePhase.BattleEnd:
                isPlayerRound = 0;
                MMBattleManager.Instance.Clear();
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
                MMSkillPanel.Instance.OpenUI();
                avatar.LoadImage("Units/Unit_10000QS");

                MMCardPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.RoundBegin:
                ShowButton("PlayerRound");
                MMSkillPanel.Instance.OpenUI();
                avatar.LoadImage("Units/Unit_10000QS");
                MMCardPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.RoundEnd:
                ShowButton("PlayerRound");
                MMSkillPanel.Instance.OpenUI();
                avatar.LoadImage("Units/Unit_10000QS");
                MMCardPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.UnitBegin:
                ShowButton("UnitBegin");
                sourceUnit.HandleHighlight(MMNodeHighlight.Green);
                avatar.LoadImage("Units/" + sourceUnit.key + "A");
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.UnitActing:
                ShowButton("UnitActing");
                sourceUnit.HandleHighlight(MMNodeHighlight.Green);
                avatar.LoadImage("Units/" + sourceUnit.key + "A");
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.UnitEnd:
                ShowButton("UnitEnd");
                MMCardPanel.Instance.OpenUI();
                //MMSkillPanel.Instance.OpenUI();
                break;

        }

        textPhase.text = phase.ToString();
        if (isPlayerRound == 1)
        {
            backgroundNote.SetColor(MMUtility.FindColorLightGreen());
        }
        else if (isPlayerRound == 1)
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

        if (CheckGameOver())
        {
            ShowButton("Game Over");
            return;
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
                    foreach (var unit in tempEnemyUnits)
                    {
                        if (unit.ap == unit.maxAP && unit.isActived == false)
                        {
                            TryEnterPhase_UnitBegin(unit);
                            return;
                        }
                    }
                    EnterPhase(MMBattlePhase.RoundEnd);
                }
                break;

            case MMBattlePhase.UnitBegin:
                EnterPhase(MMBattlePhase.UnitActing);
                break;

            case MMBattlePhase.UnitActing:
                if (isPlayerRound == 1)
                {

                }
                else
                {
                    EnterPhase(MMBattlePhase.UnitEnd);
                }
                break;

            case MMBattlePhase.UnitEnd:
                EnterPhase(MMBattlePhase.PickUnit);
                break;

            case MMBattlePhase.RoundEnd:
                EnterPhase(MMBattlePhase.RoundBegin);
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



    /// <summary>
    /// Private
    /// </summary>
    /// <param name="units"></param>


    private void IncreaseAPUnits(List<MMUnitNode> units)
    {
        foreach (var unit in units)
        {
            unit.IncreaseAP(1);
        }
    }


    public void ClearUnitEnd()
    {
        this.sourceUnit = null;
        this.targetUnit = null;
    }


}
