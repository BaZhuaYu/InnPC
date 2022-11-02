using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{


    public void EnterState(MMBattleState state)
    {

        this.state = state;

        MMDebugManager.Warning(this.state + "");

        switch (state)
        {
            case MMBattleState.Normal:
                ClearSource();
                ClearSelectSkill();
                ClearTarget();
                MMSkillPanel.Instance.Clear();
                MMSkillPanel.Instance.CloseUI();
                MMCardPanel.Instance.OpenUI();
                AutoSelectSour();
                break;

            case MMBattleState.SelectSour:
                sourceUnit.tempCell.Accept(sourceUnit);
                DrawSkill();
                sourceUnit.ShowMoveCells();
                MMSkillPanel.Instance.OpenUI();
                MMCardPanel.Instance.CloseUI();
                break;

            case MMBattleState.SourMoved:
                sourceUnit.HideMoveCells();
                break;

            case MMBattleState.SelectSkill:
                sourceUnit.HideMoveCells();
                sourceUnit.ShowAttackCells();
                break;

            case MMBattleState.PlayedSkill:
                ClearUnitsInList();
                EnterState(MMBattleState.SourMoved);
                break;

            case MMBattleState.SourDone:
                sourceUnit.tempCell = sourceUnit.cell;
                
                HandleSourceActionDone();
                ClearUnitsInList();

                if (CheckGameOver())
                {

                }
                else
                {
                    EnterState(MMBattleState.Normal);
                }

                return;
        }

        UpdateUI();
    }



    public void AutoSelectSour()
    {
        List<MMUnitNode> units = FindSortedUnits1();
        foreach (var unit in units)
        {
            if (unit.unitPhase == MMUnitPhase.Combo)
            {
                SetSource(unit);
                EnterState(MMBattleState.SelectSour);
                return;
            }
        }

        foreach (var unit in units)
        {
            if (unit.unitPhase == MMUnitPhase.Normal)
            {
                SetSource(unit);
                EnterState(MMBattleState.SelectSour);
                return;
            }
        }


        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("己方回合行动结束");
        }
    }



    public void HandleSourceActionDone()
    {
        sourceUnit.tempCell = sourceUnit.cell;

        if (sourceUnit.unitPhase == MMUnitPhase.Combo)
        {
            sourceUnit.EnterPhase(MMUnitPhase.Normal);
        }
        else
        {
            sourceUnit.EnterPhase(MMUnitPhase.Actived);
        }

    }



}
