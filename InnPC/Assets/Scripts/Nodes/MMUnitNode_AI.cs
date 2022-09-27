using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{

    public int skill = 0;


    public void ConfigSkill()
    {
        if (this.unitState == MMUnitState.Rage)
        {
            skill = 2;
        }
        else if (this.unitState == MMUnitState.Normal)
        {
            skill = 1;
        }
        else if (this.unitState == MMUnitState.Weak)
        {
            skill = 1;
        }
        else if (this.unitState == MMUnitState.Stunned)
        {
            skill = 0;
        }
        else if (this.unitState == MMUnitState.Dead)
        {
            skill = 0;
        }
    }


    public void HandleAction()
    {
        if (skill == 1)
        {
            HandleSkill1();
            if(this.unitState == MMUnitState.Rage)
            {
                EnterState(MMUnitState.Normal);
            }
        }
        else if (skill == 2)
        {
            HandleSkill2();
        }
        else if (skill == 2)
        {
            HandleSkill3();
        }
        else if (skill == 2)
        {
            HandleSkill4();
        }
        else
        {
            HandleSkill0();
        }
    }




    public void HandleSkill0()
    {

    }

    public void HandleSkill1()
    {

    }

    public void HandleSkill2()
    {

    }

    public void HandleSkill3()
    {
        this.IncreaseAP();
    }

    public void HandleSkill4()
    {
        for (int i = 0; i < maxAP; i++)
        {
            IncreaseAP();
        }
    }
}
