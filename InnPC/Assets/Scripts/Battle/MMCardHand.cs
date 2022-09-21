using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCardHand : MMNode
{
    
    public List<MMNodeCard> cards;
    public static MMCardHand instance;

    private void Awake()
    {
        instance = this;
    }



    public void UpdateUI()
    {
        float offset = 10f;
        for(int i = 0; i < cards.Count; i++)
        {
            MMNodeCard card = cards[i];
            card.MoveToLeft((offset + card.FindWidth())* (float)i);
        }
    }


    

    public void AddCard(MMNodeCard card)
    {
        this.cards.Add(card);
        card.SetParent(this);
        card.gameObject.SetActive(true);
        UpdateUI();
    }

    public void RemoveCard(MMNodeCard card)
    {
        this.cards.Remove(card);
    }

    
}
