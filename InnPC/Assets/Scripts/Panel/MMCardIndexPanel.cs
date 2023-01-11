using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCardIndexPanel : MMNode
{

    List<MMNode> nodes;
    public Button buttonClose;
    

    
    void Start()
    {
        buttonClose = gameObject.transform.Find("Close").GetComponent<Button>();
        buttonClose.onClick.AddListener(CloseUI);
    }
    

    public void Accept(List<MMUnit> units)
    {
        nodes = new List<MMNode>();
        
        for (int i = 0; i < units.Count; i++)
        {
            MMHeroNode node = MMHeroNode.Create(units[i]);
            this.nodes.Add(node);
        }

        UpdateUI();
    }


    public void Accept(List<MMCard> cards)
    {
        nodes = new List<MMNode>();
        
        for (int i = 0; i < cards.Count; i++)
        {
            MMCardNode node = MMCardNode.Create(cards[i]);
            this.nodes.Add(node);
        }

        UpdateUI();
    }


    public void UpdateUI()
    {
        float xoffset = this.FindWidth() * 0.1f;
        float yoffset = this.FindHeight() * 0.15f;

        for (int i = 0; i < nodes.Count; i++)
        {
            MMNode node = nodes[i];
            node.SetParent(this);
            node.SetActive(true);
            node.MoveToParentLeftOffset(xoffset);
            node.MoveToParentTopOffset(yoffset);

            xoffset += node.FindWidth() * 1.1f;

            if (i % 5 == 4)
            {
                xoffset = this.FindWidth() * 0.1f;
                yoffset += node.FindHeight() * 1.1f;
            }

        }
    }


    public void ShowHeroes()
    {
        this.Accept(MMExplorePanel.Instance.units);
        OpenUI();
    }

    public void ShowDeck()
    {
        this.Accept(MMExplorePanel.Instance.cards);
        OpenUI();
    }


    public void Clear()
    {
        
    }


    public override void CloseUI()
    {
        //Clear();
        //base.CloseUI();
        Destroy(gameObject);
    }


    public void Reload()
    {

    }




    public static MMCardIndexPanel Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMCardIndexPanel") as GameObject);
        obj.name = "MMCardIndexPanel";
        return obj.GetComponent<MMCardIndexPanel>();
    }

}
