using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MMHintNode : MMNode, IPointerEnterHandler, IPointerExitHandler
{

    public MMNode node;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        node.OpenUI();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        node.CloseUI();
    }




    public static MMHintNode Create(MMNode node)
    {
        MMNode n = MMNode.Create("");
        n.name = "MMHintNode";
        n.GetComponent<Image>().raycastTarget = true;
        Destroy(n.GetComponent<MMNode>());
        MMHintNode ret = n.gameObject.AddComponent<MMHintNode>();
        ret.node = node;
        node.CloseUI();
        return ret;
    }


}
