using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMUnitNode : MMNode, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {

        if(MMBattleManager.instance.phase == MMBattlePhase.Begin)
        {
            return;
        }


        if (MMBattleManager.instance.state == MMBattleState.Normal)
        {
            if (this.group == 1)
            {
                //if (MMBattleManager.instance.cellSource != null)
                //{
                //    MMBattleManager.instance.ClearSourceCell();
                //}
                MMBattleManager.instance.SetSource(this);
                MMBattleManager.instance.EnterState(MMBattleState.SelectSour);
            }
        }
        else if (MMBattleManager.instance.state == MMBattleState.SelectSour)
        {
            if (this.group == 1)
            {
                if (MMBattleManager.instance.sourceUnit != null)
                {
                    MMBattleManager.instance.ClearSource();
                }
                MMBattleManager.instance.SetSource(this);
                MMBattleManager.instance.EnterState(MMBattleState.SelectSour);
            }
            //else if (this.group == 2)
            //{
            //    if (MMBattleManager.instance.cellTarget != null)
            //    {
            //        MMBattleManager.instance.ClearTargetCell();
            //    }
            //    MMBattleManager.instance.SetTargetCell(this.cell);
            //}
        }
        else if (MMBattleManager.instance.state == MMBattleState.SourMoved)
        {

        }
        else if (MMBattleManager.instance.state == MMBattleState.SelectCard)
        {
            if (this.group == 2)
            {
                //if (MMBattleManager.instance.cellTarget != null)
                //{
                //    MMBattleManager.instance.ClearTargetCell();
                //}
                MMBattleManager.instance.SetTarget(this);
                MMBattleManager.instance.PlayCard();
            }
        }

        
    }

    

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
        if(MMBattleManager.instance.phase == MMBattlePhase.Begin)
        {
            MMCell cell = MMMap.instance.FindCellAtPosition(eventData.position);
            cell.Accept(this);
        }
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
