using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMQuestIcon : MMNode, IPointerClickHandler
{
    
    [HideInInspector]
    public MMQuest quest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("aaaaaa: " + quest.id);
        MMQuestPanel node = MMQuestPanel.Create(quest);
        MMExplorePanel.Instance.AddChild(node);
    }


    public static MMQuestIcon Create(MMQuest quest)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMQuestIcon") as GameObject);
        obj.name = "MMQuestIcon";
        MMQuestIcon ret = obj.GetComponent<MMQuestIcon>();
        ret.quest = quest;
        ret.LoadImage("Cards/" + quest.key);
        return ret;
    }


}
