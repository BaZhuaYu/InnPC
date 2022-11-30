using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMCardNode_Battle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    MMCardNode card;
    int siblingIndex = 0;


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

        siblingIndex = card.transform.GetSiblingIndex();
        card.transform.SetSiblingIndex(1000);
        card.MoveToCenterY();
        card.MoveUp(20);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        card.MoveToCenterY();
        card.transform.SetSiblingIndex(siblingIndex);
    }

}
