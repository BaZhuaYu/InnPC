using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMCardNode : MMNode, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    MMCell tempHighlight;
    MMCell tempState;


    public void OnPointerClick(PointerEventData eventData)
    {

        MMBattleState state = MMBattleManager.instance.state;
        if (state == MMBattleState.SelectSour || state == MMBattleState.SourMoved)
        {
            MMBattleManager.instance.SetSelectCard(this);
            if (this.area == MMArea.None)
            {
                MMBattleManager.instance.PlayCard();
            }
            else
            {
                MMBattleManager.instance.EnterState(MMBattleState.SelectCard);
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

    }

}
