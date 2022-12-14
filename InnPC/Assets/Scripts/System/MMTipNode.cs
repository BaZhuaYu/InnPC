using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMTipNode : MMNode
{
    public Text note;
    bool isMoving;

    private void Update()
    {
        if(isMoving)
        {
            Move();
        }
    }


    private void Move()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, this.transform.position.z);
    }

    public void Show(string s)
    {   
        note.text = s;

        //this.transform.SetParent(MMBattleManager.Instance.background.transform.parent);

        this.transform.localPosition = new Vector3(0, 400f, 0);
        this.transform.SetSiblingIndex(100);
        Invoke("GoMove", 1f);
        Destroy(gameObject, 2f);
    }


    public void Hide()
    {
        Destroy(gameObject);
    }

    void GoMove()
    {
        isMoving = true;
    }

}
