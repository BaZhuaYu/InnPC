using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMItemNode : MMNode
{
    //
    public MMNode icon;
    public Text textName;
    public Text textNote;
    public Text textPrice;

    //
    public MMItem item;

    [HideInInspector]
    public int id;
    [HideInInspector]
    public string key;
    [HideInInspector]
    public string displayName;
    [HideInInspector]
    public string displayNote;
    [HideInInspector]
    public int price;

    [HideInInspector]
    public int effect;
    [HideInInspector]
    public int value;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Accept(MMItem item)
    {
        this.item = item;

        this.id = item.id;
        this.key = item.key;
        this.displayName = item.displayName;
        this.displayNote = item.displayNote;
        this.price = item.price;

        this.effect = item.effect;
        this.value = item.value;

        Reload();
    }

    public void Reload()
    {
        this.name = key;
        textName.text = displayName;
        textNote.text = displayNote;
        textPrice.text = price + "";
    }

    public void Clear()
    {
        this.item = null;
        this.textName.text = "";
        this.textNote.text = "";
        this.name = "Item_0";
        this.gameObject.transform.SetParent(null);
    }



    public static MMItemNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMItemNode") as GameObject);
        obj.name = "MMItemNode";
        return obj.GetComponent<MMItemNode>();
    }

    public static MMItemNode Create(MMItem item)
    {
        MMItemNode ret = MMItemNode.Create();
        ret.Accept(item);
        return ret;
    }

    public static MMItemNode Create(int id)
    {
        MMItem item = MMItem.Create(id);
        return MMItemNode.Create(item);
    }


    public MMEffect CreateEffect()
    {
        MMEffect ret = new MMEffect();
        ret.type = MMUtility.DeserializeEffectType(item.effect + "");
        ret.value = this.value;             

        return ret;
    }

    
}
