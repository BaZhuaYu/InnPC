using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{

    public int skill = 0;


    public void ConfigState()
    {
        if(this.maxAP == 0)
        {
            EnterState(MMUnitState.Weak);
        }
        else
        {
            if(this.ap == this.maxAP)
            {
                EnterState(MMUnitState.Rage);
            }
            else if(this.ap > 0)
            {
                if(this.unitState != MMUnitState.Rage)
                {
                    EnterState(MMUnitState.Normal);
                }
            }
            else
            {
                EnterState(MMUnitState.Weak);
            }
        }
    }


    public void ConfigSkill()
    {
        if (this.unitState == MMUnitState.Rage)
        {
            skill = 1;
        }
        else if (this.unitState == MMUnitState.Normal)
        {
            skill = 2;
        }
        else if (this.unitState == MMUnitState.Weak)
        {
            skill = 2;
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

    

    public MMUnitNode FindTarget()
    {
        List<MMCell> cells = MMMap.Instance.FindCellsInCol(this.cell.col) ;
        MMUnitNode ret = null;

        if(group == 1)
        {
            int index = MMMap.Instance.cells.Count;
            foreach (var cell in cells)
            {
                if (cell.unitNode != null)
                {
                    if (cell.unitNode.group == 2)
                    {
                        if (cell.index <= index)
                        {
                            ret = cell.unitNode;
                            index = cell.index;
                        }
                    }
                }
            }
        }
        else
        {
            int index = 0;
            foreach (var cell in cells)
            {
                if (cell.unitNode != null)
                {
                    if (cell.unitNode.group == 1)
                    {
                        if (cell.index >= index)
                        {
                            ret = cell.unitNode;
                            index = cell.index;
                        }
                    }
                }
            }
        }
        
        return ret;
    }


}
