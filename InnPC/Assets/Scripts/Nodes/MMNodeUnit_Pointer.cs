using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMNodeUnit : MMNode, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    MMCell cellsHighlight;
    MMCell cellsState;



    public void OnPointerDown(PointerEventData eventData)
    {
        this.ShowMoveCells();
    }

    
    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localPosition = Vector3.zero;

        this.HideMoveCells();
        if (cellsHighlight != null)
        {
            cellsHighlight.EnterHighlight(MMNodeHighlight.Normal);
            cellsHighlight = null;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        this.ShowMoveCells();

        MMBattleManager.instance.SetSourceCell(this.cell);
        MMBattleManager.instance.SetSelectedUnit(this);

        this.cell.transform.SetSiblingIndex(101);
        this.transform.SetSiblingIndex(200);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cellsHighlight != null)
        {
            cellsHighlight.EnterHighlight(MMNodeHighlight.Normal);
        }

        this.transform.position = eventData.position;
        
        MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);

        if (cell == null)
        {
            return;
        }
        

        cellsHighlight = cell;

        if (CheckAbleMove(cell))
        {
            cell.EnterHighlight(MMNodeHighlight.Green);
        }
        else
        {
            cell.EnterHighlight(MMNodeHighlight.Red);
        }

        //MMBattleManager.instance.SetTarget(cell.nodeUnit);
        //cell.EnterState(MMNodeState.Red);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);

        if (cell == null)
        {
            return;
        }

        if (CheckAbleMove(cell))
        {
            this.MoveTo(cell);
        }

        //MMBattleManager.instance.SetTarget(cell.nodeUnit);
        this.cell.transform.SetSiblingIndex(0);
        this.transform.SetSiblingIndex(10);

    }



    public bool CheckAbleMove(MMCell target)
    {
        if (cell.FindDistanceFromCell(target) > this.spd)
        {
            return false;
        }

        if(target.nodeUnit != null)
        {
            return false;
        }

        return true;
    }






}
