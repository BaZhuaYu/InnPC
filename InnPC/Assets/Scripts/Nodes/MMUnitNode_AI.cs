using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{

    public MMUnitNode FindTarget()
    {
        List<MMCell> cells = MMMap.Instance.FindCellsInCol(this.cell.col);

        if (group == 1)
        {
            cells.Sort((c1, c2) => c1.row < c2.row ? -1 : 1);
            cells.RemoveAll(c => (c.row <= this.cell.row || c.row > cell.row + attackRange));
        }
        else
        {
            cells.Sort((c1, c2) => c1.row > c2.row ? -1 : 1);
            cells.RemoveAll(c => (c.row >= this.cell.row || c.row < cell.row - attackRange));
        }

        foreach (var cell in cells)
        {
            if(cell.unitNode == null)
            {
                continue;
            }

            if (cell.unitNode.HasBuff(MMBuff.YinNi))
            {
                continue;
            }

            if (cell.unitNode.group == this.group)
            {
                continue;
            }

            return cell.unitNode;
        }

        return null;
    }


    public MMUnitNode FindMinHPEnemy()
    {
        List<MMUnitNode> units = this.group == 1 ? MMBattleManager.Instance.units2 : MMBattleManager.Instance.units1;

        MMUnitNode ret = units[0];
        foreach (var unit in units)
        {
            if(unit.HasBuff(MMBuff.YinNi))
            {
                continue;
            }

            if (unit.hp <= ret.hp)
            {
                ret = unit;
            }
        }

        return ret;
    }


    public MMUnitNode FindMaxHPEnemy()
    {
        List<MMUnitNode> units = this.group == 1 ? MMBattleManager.Instance.units2 : MMBattleManager.Instance.units1;

        MMUnitNode ret = units[0];
        foreach (var unit in units)
        {
            if (unit.HasBuff(MMBuff.YinNi))
            {
                continue;
            }

            if (unit.hp > ret.hp)
            {
                ret = unit;
            }
        }

        return ret;
    }


    public MMUnitNode FindMinHP()
    {
        List<MMUnitNode> units = this.group == 1 ? MMBattleManager.Instance.units1 : MMBattleManager.Instance.units2;

        MMUnitNode ret = units[0];
        foreach (var unit in units)
        {
            if (unit.HasBuff(MMBuff.YinNi))
            {
                continue;
            }

            if (unit.hp <= ret.hp)
            {
                ret = unit;
            }
        }

        return ret;
    }

    public MMUnitNode FindMaxHP()
    {
        List<MMUnitNode> units = this.group == 1 ? MMBattleManager.Instance.units1 : MMBattleManager.Instance.units2;

        MMUnitNode ret = units[0];
        foreach (var unit in units)
        {
            if (unit.HasBuff(MMBuff.YinNi))
            {
                continue;
            }

            if (unit.hp > ret.hp)
            {
                ret = unit;
            }
        }

        return ret;
    }

}
