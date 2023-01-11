using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMOptionNode : MMNode
{
    [HideInInspector]
    public MMQuestPanel panel;

    public Text title;
    public int cardIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void LoadTitle(string s)
    {
        this.title.text = s;
    }

    public void LoadHintNode(MMHintNode hint)
    {
        AddChild(hint);
        hint.MoveRight(this.FindWidth() * 0.5f);
        hint.node.SetParent(this);
        hint.node.transform.position = hint.transform.position + new Vector3(0, hint.node.FindHeight(), 0);
    }

    public void LoadAction(UnityEngine.Events.UnityAction call)
    {
        this.GetComponent<Button>().onClick.AddListener(call);
    }


    



    public static MMOptionNode Create(MMQuestPanel panel)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMHintNode") as GameObject);
        obj.name = "MMHintNode";
        MMOptionNode ret = obj.GetComponent<MMOptionNode>();
        ret.panel = panel;
        ret.LoadAction(panel.DestroyThis);
        return ret;
    }

}
