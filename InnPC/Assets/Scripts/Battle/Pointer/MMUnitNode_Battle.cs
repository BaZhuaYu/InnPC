using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMUnitNode_Battle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    MMUnitNode unit;

    
    void Start()
    {
        unit = gameObject.GetComponent<MMUnitNode>();
    }


    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.phase == MMBattlePhase.BattleEnd)
        {
            return;
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
        //if (MMBattleManager.Instance.sourceUnit != null)
        //{
        //    return;
        //}
        unit.ShowCard();

        if (MMBattleManager.Instance.phase != MMBattlePhase.PickUnit)
        {
            return;
        }
        
        
        unit.ShowAttackCells();
        //MMBattleManager.Instance.
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        unit.HideCard();

        if (MMBattleManager.Instance.phase != MMBattlePhase.PickUnit)
        {
            return;
        }
        
        unit.HideAttackCells();
        MMBattleManager.Instance.UpdateUI();
    }


}
