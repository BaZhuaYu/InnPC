using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMUnitNode : MMNode, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (MMBattleManager.Instance.state == MMBattleState.Normal)
    //    {
    //        MMBattleManager.Instance.TryEnterStateSelectedSourceUnit(this);
    //    }
    //    else if (MMBattleManager.Instance.state == MMBattleState.SelectingCard)
    //    {
    //        MMBattleManager.Instance.TryEnterStateSelectedTargetUnit(this);
    //    } 
    //}



    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }


    public void OnEndDrag(PointerEventData eventData)
    {

    }


    public bool CheckAbleMove(MMCell target)
    {
        if (cell.FindDistanceFromCell(target) > this.spd)
        {
            return false;
        }

        if (target.unitNode != null)
        {
            return false;
        }

        return true;
    }


}
