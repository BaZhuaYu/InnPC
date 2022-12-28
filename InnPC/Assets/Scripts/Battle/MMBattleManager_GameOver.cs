using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MMNode
{
    
    public bool CheckGameOver()
    {
        if (CheckGameLost())
        {
            EnterPhase(MMBattlePhase.BattleEnd);
            //CloseUI();
            //MMExplorePanel.Instance.SetLost();
            Debug.Log("aaaaaaaaaa");

            textGameOver.text = "战斗失败";
            panelGameover.SetActive(true);


            return true;
        }
        else if (CheckGameWin())
        {
            EnterPhase(MMBattlePhase.BattleEnd);
            //CloseUI();
            //MMExplorePanel.Instance.SetWin();
            Debug.Log("bbbbbbbbbbbbbbbb");

            textGameOver.text = "战斗胜利";
            panelGameover.SetActive(true);

            return true;
        }
        else
        {
            return false;
        }
    }
    

    public void ClearDeadUnits()
    {
        foreach (var unit in FindAllUnits())
        {
            if (unit.state == MMUnitState.Dead)
            {
                unit.OnLeave();
                unit.Clear();
            }
        }
        
        units1.RemoveAll(unit => unit.state == MMUnitState.Dead);
        units2.RemoveAll(unit => unit.state == MMUnitState.Dead);
    }
    

    public bool CheckGameWin()
    {
        if (units2.Count == 0)
        {
            return true;
        }

        foreach (var unit in units2)
        {
            if (unit.state != MMUnitState.Dead)
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
            if (unit.state != MMUnitState.Dead)
            {
                return false;
            }
        }

        return true;
    }
    

}
