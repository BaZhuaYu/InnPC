using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMNodeCard : MMNode
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


    public List<MMNodeUnit> FindTargetUnits(MMCell tagetCell)
    {
        List<MMNodeUnit> ret = new List<MMNodeUnit>();
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
        List<MMNodeUnit> targets = FindTargetUnits(target);

        if (id == 1)
        {
            foreach (var dest in targets)
            {
                target.nodeUnit.DecreaseHP(3);
            }
        }
        else if (id == 2)
        {
            foreach (var dest in targets)
            {
                target.nodeUnit.DecreaseHP(2);
            }
        }
        else if (id == 3)
        {
            foreach (var dest in targets)
            {
                target.nodeUnit.DecreaseHP(1);
            }
        }
    }


}
