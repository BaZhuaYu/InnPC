using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{
    
    public void EnterState(MMUnitState s)
    {
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
                HandleSkill1();
                this.iconRage.SetActive(true);
                break;
            case MMUnitState.Normal:
                HandleSkill2();
                break;
            case MMUnitState.Weak:
                HandleSkill2();
                this.iconWeak.SetActive(true);
                break;
            case MMUnitState.Stunned:
                HandleSkill0();
                break;
            case MMUnitState.Dead:
                HandleSkill0();
                break;
        }

    }


}
