using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{
    
    public MMNodeUnit FindRandomUnit1()
    {
        int index = Random.Range(0, units1.Count);
        return units1[index];
    }


    public IEnumerator ConfigEnemyAI()
    {
        foreach(var unit in units2)
        {
            SetSource(unit);
            yield return new WaitForSeconds(1f);

            SetTarget(FindRandomUnit1());
            yield return new WaitForSeconds(1f);

            targetUnit.DecreaseHP(3);
            yield return new WaitForSeconds(1f);

            ClearSource();
            ClearTarget();
        }

        yield return new WaitForSeconds(1.0f);
        OnClickMainButton();
        yield return new WaitForSeconds(1.0f);
        OnClickMainButton();
    }
    

    public MMNodeUnit FindFrontUnit2()
    {
        MMNodeUnit ret= units2[0];
        foreach (var unit in units2)
        {
            if (ret.cell.row < unit.cell.row)
            {
                ret= unit;
            }
            else if (ret.cell.row == unit.cell.row)
            {
                if (ret.cell.col < unit.cell.col)
                {
                    ret = unit;
                }
            }
        }
        return ret;
       
    }


}
