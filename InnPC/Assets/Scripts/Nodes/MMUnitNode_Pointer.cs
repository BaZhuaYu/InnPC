using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMUnitNode : MMNode, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this);

        if(MMBattleManager.Instance.phase == MMBattlePhase.Begin)
        {
            return;
        }


        if (MMBattleManager.Instance.state == MMBattleState.Normal)
        {
            if (this.group == 1)
            {
                //if (MMBattleManager.instance.cellSource != null)
                //{
                //    MMBattleManager.instance.ClearSourceCell();
                //}
                MMBattleManager.Instance.SetSource(this);
                MMBattleManager.Instance.EnterState(MMBattleState.SelectSour);
            }
        }
        else if (MMBattleManager.Instance.state == MMBattleState.SelectSour)
        {
            if (this.group == 1)
            {
                if (MMBattleManager.Instance.sourceUnit != null)
                {
                    MMBattleManager.Instance.ClearSource();
                }
                MMBattleManager.Instance.SetSource(this);
                MMBattleManager.Instance.EnterState(MMBattleState.SelectSour);
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
        else if (MMBattleManager.Instance.state == MMBattleState.SourMoved)
        {

        }
        else if (MMBattleManager.Instance.state == MMBattleState.SelectSkill)
        {
            if (this.group == 2)
            {
                //if (MMBattleManager.instance.cellTarget != null)
                //{
                //    MMBattleManager.instance.ClearTargetCell();
                //}
                MMBattleManager.Instance.SetTarget(this);
                MMBattleManager.Instance.PlaySkill();
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
        if(MMBattleManager.Instance.phase == MMBattlePhase.Begin)
        {
            MMCell cell = MMMap.Instance.FindCellAtPosition(eventData.position);
            cell.Accept(this);
        }
    }

    
    public bool CheckAbleMove(MMCell target)
    {
        if (cell.FindDistanceFromCell(target) > this.spd)
        {
            return false;
        }

        if(target.unitNode != null)
        {
            return false;
        }

        return true;
    }

    
}
