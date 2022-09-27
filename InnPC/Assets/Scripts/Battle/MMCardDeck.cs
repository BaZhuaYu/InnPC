using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCardDeck : MMNode
{
    public static MMCardDeck instance;

    public List<MMCardNode> cards;

    public Text textCount;
    
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //textCount = GameObject.Find("Text").GetComponent<Text>();
    }

    public void UpdateUI()
    {
        textCount.text = cards.Count + "";
    }

    public void OnClick()
    {
        MMDebugManager.Log(cards.Count + "");
    }

    

    public void AddCard(MMCard card)
    {
        MMCardNode node = MMCardNode.Create();
        node.Accept(card);
        AddCard(node);
    }



    public bool HasCards()
    {
        return cards.Count > 0;
    }

    public bool IsEmpty()
    {
        return cards.Count == 0;
    }



    public void AddCard(MMCardNode card)
    {
        this.cards.Add(card);
        card.SetParent(this);
        card.gameObject.SetActive(false);
    }

    public void RemoveCard(MMCardNode card)
    {
        this.cards.Remove(card);
    }


    public void Shuffle()
    {
        List<MMCardNode> ret = new List<MMCardNode>();

        for(int i = 0; i < cards.Count; i++)
        {
            MMCardNode card = cards[i];
            this.cards.Remove(card);
            int index = Random.Range(0, cards.Count - 1);
            cards.Insert(index, card);
        }
        
    }


}
