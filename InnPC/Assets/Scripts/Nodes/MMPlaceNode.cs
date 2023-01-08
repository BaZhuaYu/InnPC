using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMPlaceNode : MMNode
{

    public MMNode avatar;
    public Text textName;
    public Text textNote;
    public GameObject bgPrice;
    public Text textPrice;

    public MMPlace place;


    [HideInInspector]
    public string key;
    [HideInInspector]
    public int id;
    [HideInInspector]
    public string displayName;
    [HideInInspector]
    public string displayNote;
    [HideInInspector]
    public int num;
    [HideInInspector]
    public int price;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void Accpet(MMPlace place)
    {
        this.place = place;
        Reload();
    }

    public void Reload()
    {
        this.id = place.id;
        this.key = place.key;
        this.displayName = place.displayName;
        this.displayNote = place.displayNote;

        this.price = place.price;
        this.num = 10;
    }

    public void Clear()
    {

    }



    void UpdateUI()
    {
        this.textName.text = this.displayName;
        this.textNote.text = this.displayNote;
        this.textPrice.text = "" + this.price;
        this.avatar.LoadImage("Places/" + this.key);
       
    }




    public static MMPlaceNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMPlaceNode") as GameObject);
        obj.name = "MMPlaceNode";
        MMPlaceNode ret = obj.GetComponent<MMPlaceNode>();
        return ret;
    }

    public static MMPlaceNode Create(MMPlace place)
    {
        MMPlaceNode ret = Create();
        ret.Accpet(place);
        ret.UpdateUI();
        return ret;
    }
    
    public static MMPlaceNode Create(int id)
    {
        MMPlaceNode ret = Create();
        MMPlace one = MMPlace.FindOne(id);
        ret.Accpet(one);

        ret.UpdateUI();
        return ret;
    }


}
