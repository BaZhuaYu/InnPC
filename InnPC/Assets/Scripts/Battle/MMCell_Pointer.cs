using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMCell : MMNode, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (this.nodeUnit == null)
        //{
        //    return;
        //}

        //if (this.nodeUnit.group == 1)
        //{
        //    //this.EnterHighlight(MMNodeHighlight.Green);
        //}
        //else
        //{
        //    MMBattleManager.instance.SetTarget(this.nodeUnit);
        //    //this.EnterHighlight(MMNodeHighlight.Red);
        //}


        if (MMBattleManager.instance.state == MMBattleState.SelectSour)
        {
            if (MMBattleManager.instance.sourceUnit.FindMoveCells().Contains(this))
            {
                this.Accept(MMBattleManager.instance.sourceUnit);
                MMBattleManager.instance.EnterState(MMBattleState.SourMoved);
            }
        }
        else if (MMBattleManager.instance.state == MMBattleState.SelectCard)
        {
            if(this.nodeUnit == null)
            {
                MMTipManager.instance.CreateTip("没有目标");
            }
        }


    }


    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //MMBattleManager.instance.liner.SetPosition(0, eventData.position);
        //MMBattleManager.instance.liner.SetPosition(1, Vector2.zero);

        //if(MMBattleManager.instance.selectedCard == null)
        //{
        //    return;
        //}

        //if (this.nodeUnit == null)
        //{
        //    this.EnterState(MMNodeState.Yellow);
        //    return;
        //}

        //if (this.nodeUnit.group == 1)
        //{
        //    this.EnterState(MMNodeState.Green);
        //}
        //else
        //{
        //    this.EnterState(MMNodeState.Red);
        //}

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //this.EnterState(MMNodeState.Normal);
    }

}
