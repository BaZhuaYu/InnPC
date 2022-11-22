﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDebugManager : MonoBehaviour
{
    public static void Log(string s)
    {
        return;
        Debug.Log(s);
    }

    public static void Warning(string s)
    {
        Debug.LogWarning(s);
    }

    public static void FatalError(string s)
    {
        Debug.LogError(s);
    }


    public void GMWin()
    {
        PrintSkillHistory();
        //MMBattleManager.Instance.CheckGameWin();
        MMBattleManager.Instance.EnterPhase(MMBattlePhase.End);
    }


    public void PrintUnits()
    {
        foreach(var unit in MMBattleManager.Instance.units2)
        {
            Debug.Log("Unit: " + unit.displayName);
            Debug.Log("Cell: " + unit.cell.index);
        }
    }

    public void PrintSkillHistory()
    {
        foreach(var (round, skills) in MMBattleManager.Instance.historySkills)
        {
            foreach (var skill in skills)
            {
                Debug.Log(round + ": " + skill.displayName);
            }
        }
    }

}
