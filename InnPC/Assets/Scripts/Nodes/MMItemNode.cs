using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMItemNode : MMNode
{

    public Image icon;
    public Text textName;
    public Text textNote;

    public MMItem item;

    public int id;
    public string key;
    public string displayName;
    public string displayNote;

    public int effect;
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

        this.effect = item.effect;
        this.value = item.value;

        Reload();
    }

    public void Reload()
    {
        textName.text = displayName;
        //this.icon.sprite = //LoadImage(key);
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


}
