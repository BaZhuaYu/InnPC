﻿using System.Collections;
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
                if(this.state != MMUnitState.Rage)
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
        if (this.state == MMUnitState.Rage)
        {
            skill = 1;
        }
        else if (this.state == MMUnitState.Normal)
        {
            skill = 2;
        }
        else if (this.state == MMUnitState.Weak)
        {
            skill = 2;
        }
        else if (this.state == MMUnitState.Stunned)
        {
            skill = 0;
        }
        else if (this.state == MMUnitState.Dead)
        {
            skill = 0;
        }
    }

    

    public MMUnitNode FindTarget()
    {
        List<MMCell> cells = MMMap.Instance.FindCellsInCol(this.cell.col) ;

        if(group == 1)
        {
            cells.Sort((c1,c2) => c1.row < c2.row ? -1:1);
            cells.RemoveAll(c => (c.row <= this.cell.row || c.row > cell.row + attackRange));
        }
        else
        {
            cells.Sort((c1, c2) => c1.row > c2.row ? -1 : 1);
            cells.RemoveAll(c => (c.row >= this.cell.row || c.row < cell.row - attackRange));
        }


        foreach(var cell in cells)
        {
            if(cell.unitNode != null && cell.unitNode.group != this.group)
            {
                return cell.unitNode;
            }
        }

        
        return null;
    }


}
