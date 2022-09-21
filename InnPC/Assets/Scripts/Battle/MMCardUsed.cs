using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCardUsed : MMNode
{
    public List<MMNodeCard> cards;

    public Text textCount;

    public static MMCardUsed instance;

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


    public void AddCard(MMNodeCard card)
    {
        this.cards.Add(card);
        card.SetParent(this);
        card.gameObject.SetActive(false);
    }

    public void RemoveCard(MMNodeCard card)
    {
        this.cards.Remove(card);
    }


    public bool isEmpty()
    {
        return cards.Count == 0;
    }


    public void Clear()
    {
        this.cards.Clear();
        this.cards = new List<MMNodeCard>();
    }
}
