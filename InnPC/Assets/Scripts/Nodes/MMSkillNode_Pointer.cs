using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMSkillNode : MMNode, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (MMBattleManager.Instance.phase == MMBattlePhase.BattleEnd)
        {
            return;
        }


        MMBattleManager.Instance.TryEnterStateSelectingSkill(this);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.textNote.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.textNote.gameObject.SetActive(false);
    }

}
