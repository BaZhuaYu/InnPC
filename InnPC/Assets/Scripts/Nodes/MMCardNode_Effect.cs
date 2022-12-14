using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMCardNode : MMNode
{

    public MMEffect CreateEffect()
    {
        MMEffect effect = new MMEffect();
        effect.type = this.effectType;
        effect.area = card.area;
        effect.value = this.value;
        
        switch (this.type)
        {
            case MMSkillType.Attack:
            case MMSkillType.Power:
                effect.userinfo.Add("TempATK", tempATK);
                effect.userinfo.Add("TempDEF", tempDEF);
                break;
            default:
                break;
        }
        
        return effect;
    }

    
    //public List<MMCell> FindSideTargetCells(MMCell target)
    //{
    //    List<MMCell> ret = new List<MMCell>();
    //    switch (this.area)
    //    {
    //        case MMArea.Single:
    //            break;
    //        case MMArea.Row:
    //            ret = MMMap.Instance.FindCellsInRow(target);
    //            break;
    //        case MMArea.Col:
    //            ret = MMMap.Instance.FindCellsInCol(target);
    //            break;
    //        case MMArea.Beside:
    //            ret = MMMap.Instance.FindCellsBeside(target);
    //            break;
    //        case MMArea.Behind:
    //            ret = MMMap.Instance.FindCellsBehind(target);
    //            break;
    //        case MMArea.Target:
    //            ret.Add(this.unit.FindTarget().cell);
    //            //ret = MMMap.Instance.FindCellsInCol(this.unit.cell.col);
    //            break;
    //        case MMArea.RaceUnits:
    //            ret = MMMap.Instance.FindCellsWithUnitRace(this.unit.race);
    //            break;


    //    }

    //    return ret;
    //}



    //public List<MMUnitNode> FindSideTargets(MMCell targetCell)
    //{
    //    List<MMUnitNode> ret = new List<MMUnitNode>();
    //    //ret.Add(tagetCell.unitNode);

    //    List<MMCell> cells = FindSideTargetCells(targetCell);
    //    foreach (var cell in cells)
    //    {
    //        if (cell != targetCell)
    //        {
    //            if (cell.unitNode != null)
    //            {
    //                ret.Add(cell.unitNode);
    //            }
    //        }
    //    }

    //    return ret;
    //}

}
