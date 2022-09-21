using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMNodeUnit : MMNode, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        MMDebugManager.Log(this.unit.key);
        MMBattleManager.instance.SetSource(this);
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
        
        MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);

        if (cell == null)
        {
            return;
        }

        if(cell.nodeUnit == null)
        {
            return;
        }
        
        //MMBattleManager.instance.SetTarget(cell.nodeUnit);
        
    }







}
