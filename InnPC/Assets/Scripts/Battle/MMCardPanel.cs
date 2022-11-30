using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCardPanel : MMNode
{

    public static MMCardPanel Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Text textDeckCount;
    public Text textUsedCount;


    private List<MMCardNode> deck;
    public List<MMCardNode> hand;
    private List<MMCardNode> used;


    MMCardNode selectingCard;




    private void Start()
    {

    }


    public void SetSelectingCard(MMCardNode card)
    {
        this.selectingCard = card;
        UpdateUI();
    }


    public void LoadDeck(List<MMCard> cards)
    {
        Debug.Log("LoadDeck");

        deck = new List<MMCardNode>();
        hand = new List<MMCardNode>();
        used = new List<MMCardNode>();

        foreach (var card in cards)
        {
            MMCardNode node = MMCardNode.Create(card);
            node.gameObject.AddComponent<MMCardNode_Battle>();
            this.deck.Add(node);
        }
    }


    public void DrawCard(int count, bool instance = false)
    {
        for (int i = 0; i < count; i++)
        {
            Draw();
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


    void Draw()
    {
        if (deck.Count == 0)
        {
            MMTipManager.instance.CreateTip("没有更多卡牌");
        }
        else
        {
            Draw(deck[0]);
        }

        return;

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
        //List<MMCardNode> ret = new List<MMCardNode>();

        for (int i = 0; i < deck.Count; i++)
        {
            MMCardNode card = deck[0];
            this.deck.Remove(card);
            int index = Random.Range(0, deck.Count - 1);
            deck.Insert(index, card);
        }

        UpdateUI();

    }










    public void Report(string s)
    {
        Debug.Log("---------" + s + "---------");
        Debug.Log("Deck: " + deck.Count);
        Debug.Log("Hand: " + hand.Count);
        Debug.Log("Used: " + used.Count);
    }


    private void Update()
    {

    }


    public void UpdateUI()
    {
        Debug.Log("MMCardPanel UpdateUI");

        foreach(var card in hand)
        {
            card.SetActive(true);
            card.SetParent(this);
        }

        SortHand();

        for (int i = 0; i < hand.Count; i++)
        {   
            float x = this.FindWidth() / 2f - hand[i].FindWidth() / 2f - (float)i / (float)(hand.Count + 1) * this.FindWidth();

            hand[i].transform.localPosition = new Vector2(x, hand[i].transform.localPosition.y);

            if (selectingCard != null && this.selectingCard == hand[i])
            {
                hand[i].MoveToCenterY();
                hand[i].MoveUp(20);
            }
        }

        
        int index = hand.Count;
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].gameObject.transform.SetSiblingIndex(index--);
        }
        

        foreach (var card in used)
        {
            card.gameObject.transform.SetParent(textUsedCount.gameObject.transform);
            card.SetActive(false);
        }
        foreach (var card in deck)
        {
            card.gameObject.transform.SetParent(textDeckCount.gameObject.transform);
            card.SetActive(false);
        }

        textDeckCount.text = deck.Count + "";
        textUsedCount.text = used.Count + "";

    }


    void SortHand()
    {
        if(MMBattleManager.Instance.sourceUnit == null)
        {
            foreach(var card in hand)
            {
                card.sortingOrder = card.clss;
                card.MoveToCenterY();
            }
        }
        else
        {
            foreach (var card in hand)
            {
                if(card.clss == MMBattleManager.Instance.sourceUnit.clss)
                {
                    card.sortingOrder = card.clss;
                    card.MoveToCenterY();
                }
                else if (card.clss == 0)
                {
                    card.sortingOrder = card.clss + 100;
                    card.MoveToCenterY();
                }
                else
                {
                    card.sortingOrder = card.clss + 1000;
                    card.MoveToCenterY();
                    card.MoveDown(80);
                }
            }
        }

        hand.Sort((x, y) => x.sortingOrder < y.sortingOrder ? -1 : 1);
    }
    


    public void Clear()
    {
        //used.Clear();
        //deck.Clear();
        //hand.Clear();
        foreach (var card in deck)
        {
            card.DestroySelf();
        }
        foreach (var card in hand)
        {
            card.DestroySelf();
        }
        foreach (var card in used)
        {
            card.DestroySelf();
        }
        hand = new List<MMCardNode>();
        deck = new List<MMCardNode>();
        used = new List<MMCardNode>();
        UpdateUI();
    }

    

    /// <summary>
    /// Private
    /// </summary>



    private void Draw(MMCardNode card)
    {
        deck.Remove(card);
        hand.Add(card);
        UpdateUI();
    }


    private void Play(MMCardNode card)
    {
        hand.Remove(card);
        used.Add(card);
        UpdateUI();
    }


    private void Shuffle(MMCardNode card)
    {
        used.Remove(card);
        deck.Add(card);
        UpdateUI();
    }



    public void ShowDeck()
    {
        MMCardIndexPanel.Instance.Accept(deck);
        MMCardIndexPanel.Instance.OpenUI();
    }

    public void ShowUsed()
    {
        MMCardIndexPanel.Instance.Accept(used);
        MMCardIndexPanel.Instance.OpenUI();
    }


}
