using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCardDeck : MMNode
{
    public static MMCardDeck instance;

    public List<MMSkillNode> cards;

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

    

    public void AddCard(MMSkill card)
    {
        MMSkillNode node = MMSkillNode.Create();
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



    public void AddCard(MMSkillNode card)
    {
        this.cards.Add(card);
        card.SetParent(this);
        card.gameObject.SetActive(false);
    }

    public void RemoveCard(MMSkillNode card)
    {
        this.cards.Remove(card);
    }


    public void Shuffle()
    {
        List<MMSkillNode> ret = new List<MMSkillNode>();

        for(int i = 0; i < cards.Count; i++)
        {
            MMSkillNode card = cards[i];
            this.cards.Remove(card);
            int index = Random.Range(0, cards.Count - 1);
            cards.Insert(index, card);
        }
        
    }


}
