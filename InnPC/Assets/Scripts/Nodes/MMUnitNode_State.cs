using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{
    
    public void EnterState(MMUnitState s)
    {
        if(this.state == MMUnitState.Dead)
        {
            return;
        }
        
        if(state == s)
        {
            return;
        }

        this.state = s;

        this.iconRage.SetActive(false);
        this.iconWeak.SetActive(false);

        switch (state)
        {
            case MMUnitState.Rage:
                break;
            case MMUnitState.Normal:
                break;
            case MMUnitState.Weak:
                break;
            case MMUnitState.Stunned:
                break;
            case MMUnitState.Dead:
                if(MMBattleManager.Instance.sourceUnit == this)
                {
                    //MMBattleManager.Instance.EnterPhase(MMBattlePhase.UnitEnd);
                }
                return;
        }

        UpdateUI();
    }


}
