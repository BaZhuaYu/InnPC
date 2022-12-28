using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDebugManager : MonoBehaviour
{

    public int cardid;

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



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("AddHandCard: " + gameObject.name);
            AddHandCard(cardid);
        }
    }




    public void GMWin()
    {
        MMBattleManager.Instance.EnterPhase(MMBattlePhase.BattleEnd);
        MMBattleManager.Instance.textGameOver.text = "战斗胜利";
        MMBattleManager.Instance.panelGameover.SetActive(true);
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


    public void AddHandCard(int id)
    {
        MMCardNode card = MMCardNode.Create(id);
        //card.gameObject.AddComponent<MMCardNode_Battle>();
        MMCardPanel.Instance.hand.Add(card);
        MMCardPanel.Instance.UpdateUI();
    }

}
