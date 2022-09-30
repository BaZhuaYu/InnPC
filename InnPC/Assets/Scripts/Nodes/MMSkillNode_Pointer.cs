﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMSkillNode : MMNode, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    MMCell tempHighlight;
    MMCell tempState;


    
    public void OnPointerClick(PointerEventData eventData)
    {
        MMBattleState state = MMBattleManager.instance.state;

        if ((state == MMBattleState.SelectSour || state == MMBattleState.SourMoved) == false)
        {
            MMTipManager.instance.CreateTip("不能使用这张卡牌：状态不对");
            return;
        }


        if (this.keywords.Contains(MMSkillKeyWord.Ultimate))
        {
            if (MMBattleManager.instance.sourceUnit.unitState != MMUnitState.Rage)
            {
                MMTipManager.instance.CreateTip("不能使用这张卡牌：需要Rage状态");
                return;
            }
        }


        MMBattleManager.instance.SetSelectSkill(this);
        
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