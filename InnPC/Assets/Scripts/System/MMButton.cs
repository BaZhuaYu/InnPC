using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMButton : MMNode
{
    public Text title;

    public static MMButton Create()
    {
        GameObject prefab = Resources.Load("Prefabs/MMButton") as GameObject;
        GameObject obj = Instantiate(prefab);
        return obj.GetComponent<MMButton>();
    }


    public void SetText(string s)
    {
        title.text = s;
    }


    public void AddClickAction(UnityEngine.Events.UnityAction call)
    {
        GetComponent<Button>().onClick.AddListener(call);
    }

}
