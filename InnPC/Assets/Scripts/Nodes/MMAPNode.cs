using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMAPNode : MMNode
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowIncrease()
    {
        Debug.Log("ShowIncrease xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        LoadImage("UI/ddd/AP_Up");
        Destroy(gameObject, 1.0f);
    }

    public void ShowDecrease()
    {
        LoadImage("UI/ddd/AP_Down");
        Destroy(gameObject, 1.0f);
    }

    public static MMAPNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMAPNode") as GameObject);
        obj.name = "MMAPNode";
        return obj.GetComponent<MMAPNode>();
    }

}
