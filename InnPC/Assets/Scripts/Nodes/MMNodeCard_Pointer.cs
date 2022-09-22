using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMNodeCard : MMNode, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    MMCell tempHighlight;
    MMCell tempState;


    public void OnPointerClick(PointerEventData eventData)
    {
        //MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);


        //MMBattleManager.instance.SetTargetCell(cell);

        if(MMBattleManager.instance.cellSource != null && MMBattleManager.instance.cellTarget != null)
        {
            MMBattleManager.instance.selectedCard = this;
            MMBattleManager.instance.PlayCard();
            MMBattleManager.instance.selectedCard = null;
        }        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //if(MMBattleManager.instance.cellSource.nodeUnit == null)
        //{
        //    return;
        //}

        //MMNodeUnit unit = MMBattleManager.instance.cellSource.nodeUnit;
        //unit.ShowAttackCells();
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        //if (MMBattleManager.instance.cellSource.nodeUnit == null)
        //{
        //    return;
        //}

        //MMNodeUnit unit = MMBattleManager.instance.cellSource.nodeUnit;
        //unit.HideAttackCells();
        //if (tempHighlight != null)
        //{
        //    tempHighlight.EnterHighlight(MMNodeHighlight.Normal);
        //    tempHighlight = null;
        //}
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        MMBattleManager.instance.selectedCard = this;
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (tempHighlight != null)
        {
            tempHighlight.EnterHighlight(MMNodeHighlight.Normal);
        }

        MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);

        if (cell == null)
        {
            return;
        }
        
        tempHighlight = cell;

        cell.EnterHighlight(MMNodeHighlight.Red);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);

        if (cell == null)
        {
            return;
        }
        
        if (cell.nodeUnit == null)
        {
            return;
        }

        MMBattleManager.instance.SetTargetCell(cell);
        MMBattleManager.instance.PlayCard();
        MMBattleManager.instance.selectedCard = null;
    }

}
