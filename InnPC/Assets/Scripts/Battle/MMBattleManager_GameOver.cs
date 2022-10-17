using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{



    public bool CheckGameOver()
    {
        if (CheckGameLost())
        {
            MMGameOverManager.Instance.SetLost();
            return true;
        }
        else if (CheckGameWin())
        {
            MMGameOverManager.Instance.SetWin();
            return true;
        }
        else
        {
            return false;
        }
    }


    public void ClearUnitsInMap()
    {
        for (int i = 0; i < units1.Count; i++)
        {
            if (units1[i].unitState == MMUnitState.Dead)
            {
                units1[i].Clear();
            }
        }

        for (int i = 0; i < units2.Count; i++)
        {
            if (units2[i].unitState == MMUnitState.Dead)
            {
                units2[i].Clear();
            }
        }
    }


    public void ClearUnitsInList()
    {
        units1.RemoveAll(unit => unit.unitState == MMUnitState.Dead);
        units2.RemoveAll(unit => unit.unitState == MMUnitState.Dead);
    }




    public bool CheckGameWin()
    {
        if (units2.Count == 0)
        {
            return true;
        }

        foreach (var unit in units2)
        {
            if (unit.unitState != MMUnitState.Dead)
            {
                return false;
            }
        }

        return true;
    }


    public bool CheckGameLost()
    {
        if (units1.Count == 0)
        {
            return true;
        }

        foreach (var unit in units1)
        {
            if (unit.unitState != MMUnitState.Dead)
            {
                return false;
            }
        }

        return true;
    }







}
