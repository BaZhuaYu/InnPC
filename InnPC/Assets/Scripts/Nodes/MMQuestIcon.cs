using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMQuestIcon : MMNode, IPointerClickHandler
{
    [HideInInspector]
    public MMQuestNode node;


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
        Debug.Log("asdasdasdasdasdasd");
        node.gameObject.SetActive(true);
    }

}
