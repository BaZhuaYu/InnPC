using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{
    
    public void EnterState(MMUnitState s)
    {
        if(this.unitState == MMUnitState.Dead)
        {
            return;
        }
        
        if(unitState == s)
        {
            return;
        }

        this.unitState = s;

        this.iconRage.SetActive(false);
        this.iconWeak.SetActive(false);

        switch (unitState)
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

                return;
        }

        UpdateUI();
    }


}
