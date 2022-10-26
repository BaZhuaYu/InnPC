using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager
{

    public void OnPhaseBegin()
    {
        foreach (var unit in units1)
        {
            unit.numAction = 1;
            unit.ConfigState();
            unit.ConfigSkill();
        }
        foreach (var unit in units2)
        {
            unit.numAction = 1;
            unit.ConfigState();
            unit.ConfigSkill();
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
            if (unit.unitState == MMUnitState.Stunned)
            {
                unit.IncreaspAPToMax();
            }
            else
            {
                //unit.IncreaseAP();
            }

            if(unit.unitState != MMUnitState.Rage)
            {
                unit.EnterState(MMUnitState.Normal);
            }
            
        }

        foreach (var unit in units2)
        {
            if (unit.unitState == MMUnitState.Stunned)
            {
                unit.IncreaspAPToMax();
            }
            else
            {
                unit.IncreaseAP();
            }

            if (unit.unitState != MMUnitState.Rage)
            {
                unit.EnterState(MMUnitState.Normal);
            }
        }
        
    }


}
