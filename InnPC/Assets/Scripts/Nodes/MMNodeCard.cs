﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMNodeCard : MMNode
{
    
    public MMCard card;

    public MMNode icon;
    public Text textName;
    public Text textNote;


    public int id;
    public string key;

    


    public void Accept(MMCard card)
    {
        this.card = card;
        this.id = card.id;
        this.key = card.key;

        this.name = "Card_" + id;

        Reload();
    } 


    public void Reload()
    {
        if(this.card == null)
        {
            Clear();
            return;
        }


        this.textName.text = card.displayName;
        this.textNote.text = card.displayNote;
        icon.LoadImage("Cards/" + key);
    }

    public void Clear()
    {
        this.card = null;
        this.textName.text = "";
        this.textNote.text = "";
        this.name = "Card_0";
    }

    
    public void ExecuteEffect(MMCell source, MMCell target)
    {
        if (id == 1)
        {
            target.nodeUnit.DecreaseHP(1);
        }
        else if (id == 2)
        {
            source.nodeUnit.IncreaseDEF(1);
        }
        else if (id == 3)
        {
            source.nodeUnit.IncreaseSPD(1);
        }
    }



    public static MMNodeCard Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMNodeCard") as GameObject);
        obj.name = "MMNodeCard";
        return obj.GetComponent<MMNodeCard>();
    }



}
