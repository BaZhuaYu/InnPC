using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMNodeCard : MMNode, IBeginDragHandler, IDragHandler, IEndDragHandler
{



    public void OnBeginDrag(PointerEventData eventData)
    {
        MMBattleManager.instance.selectedCard = this;
    }


    public void OnDrag(PointerEventData eventData)
    {
        MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);

        if (cell == null)
        {
            return;
        }

        //cell.EnterHighlight(MMNodeHighlight.Red);
        if (cell.nodeUnit == null)
        {
            return;
        }

        MMBattleManager.instance.SetTarget(cell.nodeUnit);
        cell.EnterState(MMNodeState.Red);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //MMBattleManager.instance.selectedCard = null;
        if(MMBattleManager.instance.target == null)
        {
            Debug.Log("OnEndDragxxxxxxxxxxxxx");
        }
        else
        {
            MMBattleManager.instance.PlayCard();
        }
    }

}
