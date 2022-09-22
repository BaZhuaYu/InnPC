using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCardManager : MonoBehaviour
{

    public static MMCardManager instance;

    public MMCardDeck deck;
    public MMCardHand hand;
    public MMCardUsed used;

    //public List<MMCard> hand;
    //public List<MMCard> used;
    //public List<MMCard> deck;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hand = MMCardHand.instance;
        deck = MMCardDeck.instance;
        used = MMCardUsed.instance;

        LoadDeck();

    }


    public void LoadDeck()
    {
        for (int i = 0; i < 20; i++)
        {
            MMCard card = MMCard.Create(i % 3 + 1);
            deck.AddCard(card);
        }
    }

    

    private void Update()
    {
        //UpdateUI();
    }


    public void UpdateUI()
    {
        deck.UpdateUI();
        hand.UpdateUI();
        used.UpdateUI();
    }


    public void Draw(int count)
    {
        StartCoroutine(WaitForSecond());
    }


    IEnumerator WaitForSecond()
    {
        for (int i = 0; i < 4; i++)
        {
            Draw();
            yield return new WaitForSeconds(0.2f);
        }
    }


    public void DiscardHand()
    {
        foreach(var card in hand.cards)
        {
            used.AddCard(card);
        }

        hand.Clear();
        UpdateUI();
    }





    /// <summary>
    /// Private
    /// </summary>


    public void Draw()
    {
        if (deck.IsEmpty())
        {
            if (used.isEmpty())
            {
                MMTipManager.instance.CreateTip("没有更多卡牌");
                return;
            }
            else
            {
                Shuffle();
            }
        }

        MMNodeCard card = deck.cards[0];
        DrawCard(card);
    }


    public void PlayCard(MMNodeCard card)
    {
        hand.RemoveCard(card);
        used.AddCard(card);

        UpdateUI();
    }


    public void Shuffle()
    {
        foreach (var card in used.cards)
        {
            deck.AddCard(card);
            used.RemoveCard(card);
        }

        deck.Shuffle();

        UpdateUI();
    }



    public void DrawCard(MMNodeCard card)
    {
        deck.RemoveCard(card);
        hand.AddCard(card);

        UpdateUI();
    }


}
