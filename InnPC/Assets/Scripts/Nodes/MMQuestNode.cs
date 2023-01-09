using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMQuestNode : MMNode
{
    [HideInInspector]
    public MMQuest quest;

    
    public MMNode icon;
    public Text textName;
    public Text textNote;


    [HideInInspector]
    public int id;
    [HideInInspector]
    public string key;
    [HideInInspector]
    public string displayName;
    [HideInInspector]
    public string displayNote;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Accept(MMQuest quest)
    {
        this.quest = quest;
        Reload();
        UpdateUI();
    }

    public void Reload()
    {
        Debug.Log(quest.displayName);
        Debug.Log(quest.displayNote);
        this.id = quest.id;
        this.key = quest.key;
        this.displayName = quest.displayName;
        this.displayNote = quest.displayNote;
    }

    public void Clear()
    {
        Destroy(gameObject);
    }

    public void UpdateUI()
    {
        this.icon.LoadImage("Cards/" + quest.key);
        this.textName.text = this.displayName;
        this.textNote.text = this.displayNote;
    }



    public static MMQuestNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMQuestNode") as GameObject);
        obj.name = "MMQuestNode";
        return obj.GetComponent<MMQuestNode>();
    }

    public static MMQuestNode Create(MMQuest quest)
    {
        MMQuestNode ret = MMQuestNode.Create();
        ret.Accept(quest);
        return ret;
    }
    

}
