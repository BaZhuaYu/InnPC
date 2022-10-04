using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCardPanel : MMNode
{

    public static MMCardPanel Instance;

    
    public Text textDeckCount;
    public Text textUsedCount;


    public List<MMCardNode> deck;
    public List<MMCardNode> hand;
    public List<MMCardNode> used;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        deck = new List<MMCardNode>();
        hand = new List<MMCardNode>();
        used = new List<MMCardNode>();
    }


    public void LoadDeck(List<MMCard> cards)
    {
        foreach(var card in cards)
        {
            MMCardNode node = MMCardNode.Create(card);
            this.deck.Add(node);
        }   
    }




    public void DrawCard(int count)
    {
        StartCoroutine(WaitForSecond(count));
    }


    IEnumerator WaitForSecond(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Draw();
            yield return new WaitForSeconds(0.2f);
        }
    }


    public void PlayCard(MMCardNode card)
    {
        this.Play(card);
    }


    public void ShuffleCards()
    {
        foreach (var card in used)
        {
            Shuffle(card);
        }

        UpdateUI();
    }


    public void DiscardHand()
    {
        foreach (var card in hand)
        {
            used.Add(card);
        }

        hand = new List<MMCardNode>();
        UpdateUI();
    }


    public void Draw()
    {
        if (deck.Count == 0)
        {
            if (used.Count == 0)
            {
                MMTipManager.instance.CreateTip("没有更多卡牌");
                return;
            }
            else
            {
                ShuffleDeck();
            }
        }

        MMCardNode card = deck[0];
        Draw(card);
    }



    public void ShuffleDeck()
    {
        List<MMCardNode> ret = new List<MMCardNode>();

        for (int i = 0; i < deck.Count; i++)
        {
            MMCardNode card = deck[i];
            this.deck.Remove(card);
            int index = Random.Range(0, deck.Count - 1);
            deck.Insert(index, card);
        }

        this.deck = ret;

    }













    private void Update()
    {
        
    }


    public void UpdateUI()
    {
        textDeckCount.text = deck.Count + "";
        textUsedCount.text = used.Count + "";

        float offset = 10f;
        foreach(var card in hand)
        {
            card.MoveToParentLeftOffset(offset);
            offset += 10 + card.FindWidth();
        }
    }




    /// <summary>
    /// Private
    /// </summary>



    private void Draw(MMCardNode card)
    {
        deck.Remove(card);
        hand.Add(card);
    }


    private void Play(MMCardNode card)
    {
        hand.Remove(card);
        used.Add(card);
    }


    private void Shuffle(MMCardNode card)
    {
        used.Remove(card);
        deck.Add(card);
    }


}
