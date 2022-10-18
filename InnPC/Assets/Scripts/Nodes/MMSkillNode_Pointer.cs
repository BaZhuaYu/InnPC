using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMSkillNode : MMNode, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        MMBattleState state = MMBattleManager.Instance.state;

        if ((state == MMBattleState.SelectSour || state == MMBattleState.SourMoved) == false)
        {
            MMTipManager.instance.CreateTip("不能使用这张卡牌：状态不对");
            return;
        }


        if (this.keywords.Contains(MMSkillKeyWord.Ultimate))
        {
            if (MMBattleManager.Instance.sourceUnit.unitState != MMUnitState.Rage)
            {
                MMTipManager.instance.CreateTip("不能使用这张卡牌：需要Rage状态");
                return;
            }
        }

        
        MMBattleManager.Instance.SelectSkill(this);
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
