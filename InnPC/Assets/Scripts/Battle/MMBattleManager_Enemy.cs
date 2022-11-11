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


    public IEnumerator ConfigEnemyAI()
    {
        //foreach (var unit in units2)
        //{
        //    SetSource(unit);
        //    yield return new WaitForSeconds(0.5f);

        //    unit.ConfigSkill();

        //    if(unit.ap == unit.maxAP)
        //    {
        //        SelectSkill(unit.skills[0]);
        //        yield return new WaitForSeconds(0.5f);

        //        SetTarget(FindRandomUnit1());
        //        yield return new WaitForSeconds(0.5f);
        //    }
        //    else
        //    {
        //        SelectSkill(unit.skills[2]);
        //        yield return new WaitForSeconds(0.5f);
        //    }

        //    PlaySkill();

        //}
        foreach (var unit in units2)
        {
            SetSource(unit);
            if (unit.ap == unit.maxAP)
            {
                MMUnitNode dest = unit.FindTarget();
                if (dest != null)
                {

                    SelectSkill(unit.skills[0]);
                    SetTarget(dest);

                    Debug.Log("---------------------------");
                    Debug.Log(sourceUnit.displayName);
                    Debug.Log(selectingSkill.displayName);
                    Debug.Log(targetUnit.displayName);
                    Debug.Log("---------------------------");

                    PlaySkill();
                }
                else
                {
                    Debug.Log("xxxxxxxxxxxxxxxxxxxxx");
                }
            }
            else
            {
                OnClickAwaitButton();
                //unit.IncreaseAP(1);
            }
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(1.0f);
        OnClickMainButton();
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
