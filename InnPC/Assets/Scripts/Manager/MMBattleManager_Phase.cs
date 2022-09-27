using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager
{

    public void OnPhaseBegin()
    {
        foreach (var unit in units1)
        {
            unit.ConfigSkill();
        }
        foreach (var unit in units2)
        {
            unit.ConfigSkill();
        }
    }

    
    public void OnPhasePlayerRound()
    {
        //MMCardManager.instance.Draw(4);
        //SetSourceCell(units1[0].cell);
        foreach (var unit in units1)
        {
            unit.tempCell = unit.cell;
        }
        foreach (var unit in units2)
        {
            unit.tempCell = unit.cell;
        }
    }


    public void OnPhaseEnemyRound()
    {
        StartCoroutine(ConfigEnemyAI());
    }


    public void OnPhaseEnd()
    {
        foreach (var unit in units1)
        {
            if (unit.unitState == MMUnitState.Stunned)
            {
                unit.IncreaspAPToMax();
                unit.EnterState(MMUnitState.Normal);
            }
        }
        foreach (var unit in units2)
        {
            if (unit.unitState == MMUnitState.Stunned)
            {
                unit.IncreaspAPToMax();
                unit.EnterState(MMUnitState.Normal);
            }
        }
    }


}
