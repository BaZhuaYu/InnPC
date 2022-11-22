using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMUnitNode_Battle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    int index = 0;
    MMUnitNode unit;

    // Start is called before the first frame update
    void Start()
    {
        unit = gameObject.GetComponent<MMUnitNode>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.phase == MMBattlePhase.End)
        {
            return;
        }
        
        if(MMBattleManager.Instance.phase == MMBattlePhase.PlayerRound)
        {
            if(unit.group != 1)
            {
                return;
            }
        }

        if (MMBattleManager.Instance.phase == MMBattlePhase.EnemyRound)
        {
            if (unit.group != 2)
            {
                return;
            }
        }


        if (MMBattleManager.Instance.state == MMBattleState.Normal)
        {
            MMBattleManager.Instance.TryEnterStateSelectedSourceUnit(unit);
        }
        else if (MMBattleManager.Instance.state == MMBattleState.SelectingCard)
        {
            MMBattleManager.Instance.TryEnterStateSelectedTargetUnit(unit);
        }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if(MMBattleManager.Instance.state == MMBattleState.Normal)
        {
            unit.ShowAttackCells();
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.state == MMBattleState.Normal)
        {
            unit.HideAttackCells();
        }
    }
}
