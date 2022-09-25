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
        //if(MMBattleManager.instance.cellSource == null)
        //{
        //    MMTipManager.instance.CreateTip("没有己方英雄");
        //    return;
        //}

        //if (MMBattleManager.instance.cellTarget == null)
        //{
        //    MMTipManager.instance.CreateTip("没有目标");
        //    return;
        //}
        
        //MMBattleManager.instance.selectedCard = this;
        //MMBattleManager.instance.PlayCard();
        //MMBattleManager.instance.selectedCard = null;


        MMBattleUXState state = MMBattleManager.instance.uxState;
        if (state == MMBattleUXState.SelectSour || state == MMBattleUXState.SourMoved)
        {
            MMBattleManager.instance.selectedCard = this;
            MMBattleManager.instance.EnterUXState(MMBattleUXState.SelectCard);
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

        MMBattleManager.instance.SetTarget(cell.nodeUnit);
        MMBattleManager.instance.PlayCard();
        MMBattleManager.instance.selectedCard = null;
    }

}
