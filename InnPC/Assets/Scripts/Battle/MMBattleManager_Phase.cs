﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager
{
    public void EnterPhase(MMBattlePhase p)
    {
        this.phase = p;
        switch (p)
        {
            case MMBattlePhase.Begin:
                round = 0;
                historySkills = new Dictionary<int, List<MMSkillNode>>();
                ShowButton("Start");
                ShowTitle("Begin");
                buttonMain.enabled = true;
                ClosePanels();
                MMUnitPanel.Instance.OpenUI();
                break;
            case MMBattlePhase.PlayerRound:
                round += 1;
                historySkills.Add(round, new List<MMSkillNode>());
                ShowButton("End Turn");
                ShowTitle("PlayerRound");
                buttonMain.enabled = true;
                OnPhaseBegin();
                OnPhasePlayerRound();
                MMCardPanel.Instance.OpenUI();
                BroadCast(MMTriggerTime.OnRoundBegin);
                break;
            case MMBattlePhase.EnemyRound:
                ShowButton("Wait");
                ShowTitle("EnemyRound");
                buttonMain.enabled = false;
                MMCardPanel.Instance.CloseUI();
                BroadCast(MMTriggerTime.OnRoundBegin);
                OnPhaseEnemyRound();
                break;
            case MMBattlePhase.End:
                ShowButton("End");
                ShowTitle("End");
                buttonMain.enabled = false;
                MMCardPanel.Instance.CloseUI();
                MMSkillPanel.Instance.CloseUI();
                MMUnitPanel.Instance.CloseUI();
                MMBattleManager.Instance.Clear();
                break;
        }
    }



    public void OnPhaseBegin()
    {
        foreach (var unit in units1)
        {
            unit.ConfigState();
            unit.ConfigSkill();
            unit.EnterPhase(MMUnitPhase.Normal);
        }
        foreach (var unit in units2)
        {
            unit.ConfigState();
            unit.ConfigSkill();
            unit.EnterPhase(MMUnitPhase.Normal);
        }
    }

    
    public void OnPhasePlayerRound()
    {
        foreach (var unit in units1)
        {
            unit.tempCell = unit.cell;
        }
        foreach (var unit in units2)
        {
            unit.tempCell = unit.cell;
        }
        AutoSelectSour();
    }


    public void OnPhaseEnemyRound()
    {
        StartCoroutine(ConfigEnemyAI());
    }


    public void OnPhaseEnd()
    {
        foreach (var unit in units1)
        {
            //if (unit.unitState == MMUnitState.Stunned)
            //{
            //    unit.IncreaspAPToMax();
            //}
            //else
            //{
            //    //unit.IncreaseAP();
            //}

            //if(unit.unitState != MMUnitState.Rage)
            //{
            //    unit.EnterState(MMUnitState.Normal);
            //}

        }

        foreach (var unit in units2)
        {
            //if (unit.unitState == MMUnitState.Stunned)
            //{
            //    unit.IncreaspAPToMax();
            //}
            //else
            //{
            //    unit.IncreaseAP();
            //}

            //if (unit.unitState != MMUnitState.Rage)
            //{
            //    unit.EnterState(MMUnitState.Normal);
            //}
        }
        
    }


}
