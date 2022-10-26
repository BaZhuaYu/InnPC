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
        foreach(var skill in MMBattleManager.Instance.historySkill)
        {
            Debug.Log(skill.displayName);
        }
    }

}