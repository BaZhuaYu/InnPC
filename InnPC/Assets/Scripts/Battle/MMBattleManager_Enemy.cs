using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{

    public MMUnitNode FindRandomUnit1()
    {
        int index = Random.Range(0, units1.Count);
        return units1[index];
    }

    

    MMUnitNode FindMaxAPUnit2()
    {
        foreach(var unit in units2)
        {
            if (unit.ap == unit.maxAP && unit.isActived == false)
            {
                return unit;
            }
        }

        return null;
    }


    public void AutoUnitActing()
    {
        MMUnitNode dest = sourceUnit.FindTarget();
        sourceUnit.isActived = true;
        if (dest == null)
        {

            sourceUnit.DecreaseAP(sourceUnit.maxAP);
            MMPlayerManager.Instance.hp -= 10;
        }
        else
        {
            TryEnterStateSelectingCard(sourceUnit.cards[0]);
            TryEnterStateSelectedTargetUnit(dest);
        }
        
    }
    


    public MMUnitNode FindFrontUnitOfGroup(int group)
    {
        MMUnitNode ret;

        if (group == 1)
        {
            if (units1.Count == 0)
            {
                return null;
            }

            ret = units1[0];
            foreach (var unit in units1)
            {
                if (unit.cell.index > ret.cell.index)
                {
                    ret = unit;
                }
            }
        }
        else
        {
            if (units2.Count == 0)
            {
                return null;
            }

            ret = units2[0];
            foreach (var unit in units2)
            {
                if (unit.cell.index < ret.cell.index)
                {
                    ret = unit;
                }
            }
        }

        return ret;
    }


}
