using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMExplorePanel : MMNode
{

    public void EnterExplore()
    {
        OnBeginDay();
        OpenUI();
    }


    public void ExitExplore()
    {
        OnEndDay();
        CloseUI();
    }


    public void EnterBattle()
    {
        MMBattleManager.Instance.OpenUI();
        MMBattleManager.Instance.EnterPhase(MMBattlePhase.None);
        MMBattleManager.Instance.panelGameover.SetActive(false);
    }

    public void ExitBattle()
    {
        MMBattleManager.Instance.CloseUI();
    }

    
    public void OnBeginDay()
    {
        this.tansuoTime = 0;
        foreach(var place in places)
        {
            place.num = place.place.num;
        }
    }

    public void OnEndDay()
    {
        this.tansuoEvil += 1;
        MMEvilPanel.Instance.UpdateUI();
    }


}
