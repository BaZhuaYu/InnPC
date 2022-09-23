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
            SetSourceCell(unit.cell);
            yield return new WaitForSeconds(1f);

            SetTargetCell(FindRandomUnit1().cell);
            yield return new WaitForSeconds(1f);

            cellTarget.nodeUnit.DecreaseHP(3);
            yield return new WaitForSeconds(1f);

            ClearSourceCell();
            ClearTargetCell();
        }

        yield return new WaitForSeconds(1.0f);
        OnClickMainButton();
        yield return new WaitForSeconds(1.0f);
        OnClickMainButton();
    }







}
