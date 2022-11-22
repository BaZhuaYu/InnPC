using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MMBattlePhase
{
    Begin,
    PlayerRound,
    EnemyRound,
    Routing,
    UnitBegin,
    UnitActing,
    UnitEnd,
    End,
}


public partial class MMBattleManager
{

    List<MMUnitNode> tempEnemyUnits;

    public void EnterPhase(MMBattlePhase p)
    {

        if (phase == MMBattlePhase.End)
        {
            if (p != MMBattlePhase.Begin)
            {
                Debug.Log(phase.ToString() + "XXXXXXXXXXXXXXXXXXX" + p.ToString());
                return;
            }
        }

        if(phase == MMBattlePhase.PlayerRound)
        {
            if (p != MMBattlePhase.Routing)
            {
                Debug.Log(phase.ToString() + "XXXXXXXXXXXXXXXXXXX" + p.ToString());
                return;
            }
        }


        if(p == MMBattlePhase.UnitBegin || p == MMBattlePhase.UnitActing || p == MMBattlePhase.UnitEnd)
        {
            if(sourceUnit == null)
            {
                Debug.Log(phase.ToString() + "XXXXXXXXXXXXXXXXXXX" + p.ToString());
                return;
            }
        }




        OnExitPhase(phase);

        this.phase = p;
        

        switch (p)
        {
            case MMBattlePhase.Begin:
                Debug.Log("--------Begin--------");
                round = 0;
                historySkills = new Dictionary<int, List<MMSkillNode>>();

                MMCardPanel.Instance.LoadDeck(MMPlayerManager.Instance.cards);
                MMCardPanel.Instance.ShuffleDeck();

                DebugConfig();

                DrawCards(4, true);

                EnterPhase(MMBattlePhase.PlayerRound);
                break;

            case MMBattlePhase.PlayerRound:
                Debug.Log("--------PlayerRound--------");
                round += 1;
                historySkills.Add(round, new List<MMSkillNode>());
                DrawCards(2);
                isPlayerRound = true;
                foreach(var unit in units1)
                {
                    unit.isActived = false;
                }
                MMMap.Instance.SetColor(MMUtility.FindColorWhite());
                BroadCast(MMTriggerTime.OnRoundBegin);
                EnterState(MMBattleState.Normal);
                EnterPhase(MMBattlePhase.Routing);
                break;

            case MMBattlePhase.EnemyRound:
                Debug.Log("--------EnemyRound--------");
                MMMap.Instance.SetColor(MMUtility.FindColorLightRed());
                isPlayerRound = false;
                EnterState(MMBattleState.Normal);
                BroadCast(MMTriggerTime.OnRoundBegin);

                foreach (var unit in units2)
                {
                    unit.isActived = false;
                }

                tempEnemyUnits = new List<MMUnitNode>();
                foreach(var unit in units2)
                {
                    if(unit.ap == unit.maxAP && unit.isActived == false)
                    {
                        tempEnemyUnits.Add(unit);
                    }
                }

                EnterPhase(MMBattlePhase.Routing);
                break;


            case MMBattlePhase.UnitBegin:
                Debug.Log("UnitBegin: " + " " + sourceUnit.displayName);
                sourceUnit.OnRoundBegin();
                MMSkillPanel.Instance.Accept(sourceUnit.skills);
                EnterPhase(MMBattlePhase.UnitActing);
                break;


            case MMBattlePhase.UnitActing:
                Debug.Log("UnitActing: " + " " + sourceUnit.displayName);
                if (isPlayerRound)
                {

                }
                else
                {
                    AutoUnitActing();
                }
                break;


            case MMBattlePhase.UnitEnd:
                Debug.Log("UnitEnd: " + " " + sourceUnit.displayName);
                
                sourceUnit.OnRoundEnd();
                UnselectSourceCell();
                ClearDeadUnits();
                EnterPhase(MMBattlePhase.Routing);

                break;


            case MMBattlePhase.End:
                Debug.Log("--------End--------");
                MMBattleManager.Instance.Clear();
                break;


            case MMBattlePhase.Routing:
                //AutoRouting();
                //StartRoutingNextPhase();
                AutoRouting();
                break;

        }


        OnEnterPhase(this.phase);

    }


    void OnExitPhase(MMBattlePhase phase)
    {
        switch (phase)
        {
            case MMBattlePhase.Begin:
                BroadCast(MMTriggerTime.OnBattleBegin);
                break;


            case MMBattlePhase.PlayerRound:
                BroadCast(MMTriggerTime.OnRoundEnd);
                IncreaseAPUnits(units1);
                break;


            case MMBattlePhase.EnemyRound:
                BroadCast(MMTriggerTime.OnRoundEnd);
                IncreaseAPUnits(units2);
                break;


            case MMBattlePhase.UnitEnd:
                EnterState(MMBattleState.Normal);
                break;


            default:
                break;
        }
    }



    void OnEnterPhase(MMBattlePhase phase)
    {

        ClosePanels();


        switch (phase)
        {
            case MMBattlePhase.End:
                ShowTitle("Begin");
                ShowButton("End");
                MMUnitPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.Begin:
                ShowTitle("Round 1");
                ShowButton("Begin");
                MMCardPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.PlayerRound:
                ShowButton("PlayerRound");
                MMCardPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.EnemyRound:
                ShowButton("EnemyRound");
                MMCardPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.Routing:
                MMCardPanel.Instance.OpenUI();
                ShowButton("Routing");
                break;

            case MMBattlePhase.UnitBegin:
                ShowButton("UnitBegin");
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.UnitActing:
                ShowButton("UnitActing");
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                break;

            case MMBattlePhase.UnitEnd:
                ShowButton("UnitEnd");
                MMCardPanel.Instance.OpenUI();
                MMSkillPanel.Instance.OpenUI();
                break;

        }

        textPhase.text = phase.ToString();
    }



    public void AutoRouting()
    {

        if (CheckGameOver())
        {
            ShowButton("Game Over");
            return;
        }


        if(this.phase != MMBattlePhase.Routing)
        {
            Debug.LogError(this.phase + "");
        }


        if (isPlayerRound)
        {
            foreach(var unit in units1)
            {
                if(unit.isActived)
                {
                    EnterPhase(MMBattlePhase.EnemyRound);
                    return;
                }
            }
        }
        else
        {
            foreach(var unit in tempEnemyUnits)
            {
                if(unit.ap == unit.maxAP && unit.isActived == false)
                {
                    TryEnterPhase_UnitBegin(unit);
                    return;
                }
            }
            EnterPhase(MMBattlePhase.PlayerRound);
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
