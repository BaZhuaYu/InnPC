using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCardNode : MMNode
{

    public MMCard card;

    public MMNode icon;
    public Text textName;
    public Text textNote;


    public int id;
    public string key;

    public MMArea area;

    public List<MMSkillKeyWord> keywords;

    public int tempATK;
    public int tempDEF;



    public void Accept(MMCard card)
    {
        this.card = card;

        this.id = card.id;
        this.key = card.key;

        this.area = card.area;
        this.keywords = card.keywords;

        this.tempATK = card.tempATK;
        this.tempDEF = card.tempDEF;

        Reload();
    }


    public void Reload()
    {
        

    }


    public void Clear()
    {
        this.card = null;
    }


    public static MMCardNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMCardNode") as GameObject);
        obj.name = "MMCardNode";
        return obj.GetComponent<MMCardNode>();
    }

    public static MMCardNode Create(MMCard card)
    {
        MMCardNode node = MMCardNode.Create();
        node.Accept(card);
        return node;
    }


    public static MMCardNode Create(int id)
    {
        MMCard card = MMCard.Create(id);
        return MMCardNode.Create(card);
    }

}
