using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCardHand : MMNode
{

    public static MMCardHand instance;

    public List<MMCardNode> cards;
    

    private void Awake()
    {
        instance = this;
    }

    
    public void UpdateUI()
    {
        float offset = 10f;
        for(int i = 0; i < cards.Count; i++)
        {
            MMCardNode card = cards[i];
            card.MoveToLeft((offset + card.FindWidth())* (float)i);
        }
    }

    
    public void AddCard(MMCardNode card)
    {
        this.cards.Add(card);
        card.SetParent(this);
        card.gameObject.SetActive(true);
        UpdateUI();
    }

    public void RemoveCard(MMCardNode card)
    {
        this.cards.Remove(card);
    }

    
    public void Reload()
    {

    }


    public void Clear()
    {
        this.cards = new List<MMCardNode>();
    }
    
}
