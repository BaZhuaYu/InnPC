using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMUnitNode_Battle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    int index = 0;
    MMUnitNode unit;
    MMCardNode tempCard;
    int tempSiblingIndex;

    // Start is called before the first frame update
    void Start()
    {
        unit = gameObject.GetComponent<MMUnitNode>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.phase == MMBattlePhase.BattleEnd)
        {
            return;
        }

        //if (MMBattleManager.Instance.state == MMBattleState.Normal)
        //{
        //    if (unit.group != 1)
        //    {
        //        return;
        //    }
        //}

        //if (MMBattleManager.Instance.state == MMBattleState.Normal)
        //{
        //    if (unit.group != 2)
        //    {
        //        return;
        //    }
        //}


        if (MMBattleManager.Instance.state == MMBattleState.Normal)
        {
            MMBattleManager.Instance.TryEnterStateSelectedSourceUnit(unit);
        }
        else if (MMBattleManager.Instance.state == MMBattleState.SelectingCard)
        {
            MMBattleManager.Instance.TryEnterStateSelectedTargetUnit(unit);
        }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.state == MMBattleState.None)
        {
            tempCard = MMCardNode.Create();
            tempCard.Accept(unit.cards[0].card);
            tempSiblingIndex = tempCard.transform.GetSiblingIndex();
            tempCard.transform.SetSiblingIndex(1000);
            tempCard.SetParent(MMMap.Instance);
            if (unit.group == 1)
            {
                tempCard.MoveLeft(MMMap.Instance.FindWidth() / 2 + tempCard.FindWidth() );
            }
            else
            {
                tempCard.MoveRight(MMMap.Instance.FindWidth() / 2 + tempCard.FindWidth() );
            }
            
            unit.ShowAttackCells();
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (MMBattleManager.Instance.state == MMBattleState.None)
        {
            tempCard.DestroySelf();
            tempCard.transform.SetSiblingIndex(tempSiblingIndex);
            unit.HideAttackCells();
        }
    }
}
