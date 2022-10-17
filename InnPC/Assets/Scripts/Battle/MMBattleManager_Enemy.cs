﻿using System.Collections;
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
        foreach (var unit in units2)
        {
            SetSource(unit);
            yield return new WaitForSeconds(1f);
            
            unit.ConfigSkill();

            if(unit.skill == 1)
            {
                SetSelectingSkill(unit.skills[0]);
                yield return new WaitForSeconds(1f);

                SetTarget(FindRandomUnit1());
                yield return new WaitForSeconds(1f);

                PlaySkill();
            }
            else if (unit.skill == 2)
            {
                SetSelectingSkill(unit.skills[1]);
                yield return new WaitForSeconds(1f);

                SetTarget(FindRandomUnit1());
                yield return new WaitForSeconds(1f);

                PlaySkill();
            }
            else
            {
                EnterState(MMBattleState.SourDone);
            }
        }

        yield return new WaitForSeconds(1.0f);
        OnClickMainButton();
        //yield return new WaitForSeconds(1.0f);
        //OnClickMainButton();
    }

    
    public MMUnitNode FindFrontUnitOfGroup(int group)
    {
        MMUnitNode ret;

        if (group == 1)
        {
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
