using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMTipManager : MMNode
{

    public static MMTipManager instance;


    private void Awake()
    {
        instance = this;
    }


    public void CreateTip(string s)
    {
        GameObject obj = Resources.Load("Prefabs/MMTipNode") as GameObject;
        MMNodeTip tip = Instantiate(obj).GetComponent<MMNodeTip>();
        tip.Show(s);
    }

    
}
