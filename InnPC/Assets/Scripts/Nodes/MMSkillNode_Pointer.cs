using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMSkillNode : MMNode, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log(this.type);

        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        MMBattleState state = MMBattleManager.Instance.state;

        //if ((state == MMBattleState.SelectSour || state == MMBattleState.SourMoved) == false)
        //{
        //    MMTipManager.instance.CreateTip("不能使用这张卡牌：状态不对");
        //    return;
        //}
        
        if (this.type == MMSkillType.Passive)
        {
            MMTipManager.instance.CreateTip("被动技能，无法使用");
            return;
        }

        if (this.state == MMSkillState.Used)
        {
            MMTipManager.instance.CreateTip("已使用，不能再次使用");
            return;
        }

        if (this.state == MMSkillState.NotReady)
        {
            MMTipManager.instance.CreateTip("这个技能还没有准备好");
            return;
        }

        if(this.cost > this.unit.ap)
        {
            MMTipManager.instance.CreateTip("行动力不足");
            return;
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
