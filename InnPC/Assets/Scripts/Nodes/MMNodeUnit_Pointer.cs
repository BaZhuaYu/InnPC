﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMNodeUnit : MMNode, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{

    MMCell cellsHighlight;
    MMCell cellsState;
    //bool


    public void OnPointerClick(PointerEventData eventData)
    {

        //if(this.group == 1)
        //{
        //    MMBattleManager.instance.SetSourceCell(this.cell);
        //    this.cell.EnterHighlight(MMNodeHighlight.Green);
        //    MMCardManager.instance.Draw(4);
        //}
        //else if (this.group == 2)
        //{
        //    MMBattleManager.instance.SetTargetCell(this.cell);
        //    this.cell.EnterHighlight(MMNodeHighlight.Red);
        //}




        //if(MMBattleManager.instance.cellSource == null)
        //{
        //    MMBattleManager.instance.SetSourceCell(this.cell);
        //    this.cell.EnterHighlight(MMNodeHighlight.Green);
        //    MMCardManager.instance.Draw(4);
        //}
        //else if (MMBattleManager.instance.cellTarget == null)
        //{
        //    MMBattleManager.instance.SetTargetCell(this.cell);
        //    this.cell.EnterHighlight(MMNodeHighlight.Red);
        //}



    }




    public void OnPointerDown(PointerEventData eventData)
    {
        //this.ShowMoveCells();
        
        //MMBattleManager.instance.SetSourceCell(this.cell);
        

    }

    
    public void OnPointerUp(PointerEventData eventData)
    {
        //this.HideMoveCells();

        //this.transform.localPosition = Vector3.zero;

        //if(MMBattleManager.instance.cellTarget == null)
        //{
        //    MMCardManager.instance.DiscardHand();
        //}
        

        //if (cellsHighlight != null)
        //{
        //    cellsHighlight.EnterHighlight(MMNodeHighlight.Normal);
        //    cellsHighlight = null;
        //}
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

        //this.transform.position = eventData.position;
        
        //MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);

        //if (cell == null)
        //{
        //    return;
        //}
        

        //cellsHighlight = cell;

        //if (CheckAbleMove(cell))
        //{
        //    cell.EnterHighlight(MMNodeHighlight.Green);
        //}
        //else
        //{
        //    cell.EnterHighlight(MMNodeHighlight.Red);
        //}

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

        if (cell.nodeUnit == null)
        {
            return;
        }

        MMBattleManager.instance.SetTargetCell(cell);



        //if (CheckAbleMove(cell))
        //{
        //    this.MoveTo(cell);
        //}

        ////MMBattleManager.instance.SetTarget(cell.nodeUnit);
        //this.cell.transform.SetSiblingIndex(0);
        //this.transform.SetSiblingIndex(10);

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
