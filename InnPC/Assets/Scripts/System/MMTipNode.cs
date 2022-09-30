using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMTipNode : MMNode
{
    public Text note;



    public void ShowBattleLog(string s)
    {
        Debug.Log(s);
    }



    public void Show(string s)
    {   
        note.text = s;
        //this.SetParent(MMBattleManager.instance.background);

        this.transform.SetParent(MMBattleManager.instance.background.transform.parent);

        this.transform.localPosition = new Vector3(0, 495f, 0);
        this.transform.SetSiblingIndex(100);
        Invoke("Hide", 2);
    }


    public void Hide()
    {
        Destroy(gameObject);
    }

}
