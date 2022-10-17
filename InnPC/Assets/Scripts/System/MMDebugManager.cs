using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDebugManager : MonoBehaviour
{
    public static void Log(string s)
    {
        Debug.Log(s);
    }


    public static void FatalError(string s)
    {
        Debug.LogError(s);
    }


    public void GMWin()
    {
        MMBattleManager.Instance.CheckGameWin();
    }


    public void PrintUnits()
    {
        foreach(var unit in MMBattleManager.Instance.units2)
        {
            Debug.Log("Unit: " + unit.displayName);
            Debug.Log("Cell: " + unit.cell.index);
        }
    }

}
