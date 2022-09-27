using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMCardNode : MMNode
{


    public List<MMCell> FindSideTargetCells(MMCell target)
    {
        List<MMCell> ret = new List<MMCell>();
        switch (this.area)
        {
            case MMArea.Single:
                break;
            case MMArea.Row:
                ret = MMMap.instance.FindCellsInRow(target);
                break;
            case MMArea.Col:
                ret = MMMap.instance.FindCellsInCol(target);
                break;
            case MMArea.Beside:
                ret = MMMap.instance.FindCellsBeside(target);
                break;
            case MMArea.Behind:
                ret = MMMap.instance.FindCellsBehind(target);
                break;
        }

        return ret;
    }


    public List<MMUnitNode> FindTargetUnits(MMCell tagetCell)
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();
        ret.Add(tagetCell.nodeUnit);

        List<MMCell> cells = FindSideTargetCells(tagetCell);
        foreach (var cell in cells)
        {
            if (cell.nodeUnit != null)
            {
                ret.Add(cell.nodeUnit);
            }
        }

        return ret;
    }





    public void ExecuteEffect(MMCell source, MMCell target)
    {
        List<MMUnitNode> targets = null;

        if (this.area == MMArea.None)
        {

        }
        else
        {
            targets = FindTargetUnits(target);
        }

        
        if (id == 1)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(3);
            }
        }
        else if (id == 2)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(2);
            }
        }
        else if (id == 3)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(1);
            }
        }
        else if (id == 100)
        {
            source.nodeUnit.EnterState(MMUnitState.Stunned);
        }
        else if (id == 1000)
        {
            source.nodeUnit.IncreaseAP();
        }
        else if (id == 10000)
        {
            target.nodeUnit.DecreaseHP(source.nodeUnit.atk);
            source.nodeUnit.DecreaseHP(target.nodeUnit.atk);
        }
        else if (id == 10101)
        {
            target.nodeUnit.DecreaseHP(2);
            source.nodeUnit.IncreaseHP(2);
        }
        else if (id == 10201)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(source.nodeUnit.atk);
            }
        }
        else if (id == 10301)
        {
            foreach (var dest in targets)
            {
                dest.DecreaseHP(2);
            }
        }
        else if (id == 10401)
        {
            source.nodeUnit.IncreaseHP(4);
        }
    }


}
