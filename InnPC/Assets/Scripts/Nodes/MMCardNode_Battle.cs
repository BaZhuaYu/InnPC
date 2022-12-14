using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMCardNode_Battle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    MMCardNode card;
    int siblingIndex = 0;
    MMUnitNode tempTarget;
    List<MMUnitNode> sideTargets;

    void Start()
    {
        card = gameObject.GetComponent<MMCardNode>();
    }


    void Update()
    {

    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.phase == MMBattlePhase.BattleEnd)
        {
            return;
        }

        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        
        MMBattleManager.Instance.TryEnterStateSelectingCard(card);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.selectingCard != null)
        {
            return;
        }

        ShowCard();
        ShowTarget();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideCard();
        HideTarget();
    }



    ///Private
    ///

    void ShowCard()
    {
        siblingIndex = card.transform.GetSiblingIndex();
        card.transform.SetSiblingIndex(1000);
        card.MoveToCenterY();
        card.MoveUp(20);
    }

    void HideCard()
    {
        card.MoveToCenterY();
        card.transform.SetSiblingIndex(siblingIndex);
    }

    void ShowTarget()
    {
        if(MMBattleManager.Instance.sourceUnit == null)
        {
            return;
        }

        tempTarget = MMBattleManager.Instance.FindMainTarget(MMBattleManager.Instance.sourceUnit, this.card.target);
        if(tempTarget == null)
        {
            return;
        }

        sideTargets = MMBattleManager.Instance.FindSideTargets(MMBattleManager.Instance.sourceUnit, tempTarget, this.card.area);

        tempTarget.HandleHighlight(MMNodeHighlight.Red);
        foreach(var unit in sideTargets)
        {
            unit.HandleHighlight(MMNodeHighlight.Red);
        }
    }

    void HideTarget()
    {
        if (tempTarget == null)
        {
            return;
        }

        tempTarget.HandleHighlight(MMNodeHighlight.Normal);
        tempTarget = null;

        if(sideTargets == null)
        {
            return;
        }
        foreach (var unit in sideTargets)
        {
            unit.HandleHighlight(MMNodeHighlight.Normal);
        }
        
    }

}
