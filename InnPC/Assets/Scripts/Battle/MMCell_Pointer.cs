using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MMCell : MMNode, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (MMBattleManager.Instance.state == MMBattleState.SelectedSourceUnit)
        {
            if (MMBattleManager.Instance.sourceUnit.FindMovableCells().Contains(this))
            {
                this.Accept(MMBattleManager.Instance.sourceUnit);
                MMBattleManager.Instance.sourceUnit.isMoved = true;
                MMBattleManager.Instance.sourceUnit.HideMoveCells();
                MMBattleManager.Instance.UpdateUI();
            }
        }
        else if (MMBattleManager.Instance.state == MMBattleState.SelectingCard)
        {
            if (this.unitNode == null)
            {
                MMTipManager.instance.CreateTip("没有目标");
            }
        }
        else if (MMBattleManager.Instance.state == MMBattleState.SelectingSkill)
        {
            if (this.unitNode == null)
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
        if (MMBattleManager.Instance.phase != MMBattlePhase.UnitActing)
        {
            return;
        }

        if (MMBattleManager.Instance.sourceUnit.isMoved)
        {
            return;
        }

        if(this.unitNode != null)
        {
            return;
        }

        if (MMBattleManager.Instance.sourceUnit.FindMovableCells().Contains(this))
        {
            MMBattleManager.Instance.sourceUnit.ShowWillMove(this);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.phase == MMBattlePhase.UnitActing)
        {
            MMBattleManager.Instance.sourceUnit.HideWillMove(this);
        }
    }

}
