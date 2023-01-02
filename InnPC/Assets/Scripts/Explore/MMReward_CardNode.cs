using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MMReward_CardNode : MonoBehaviour, IPointerClickHandler
{
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
        MMCardNode node = GetComponent<MMCardNode>();

        MMExplorePanel.Instance.cards.Add(node.card);
        
        gameObject.SetActive(false);

        MMRewardPanel.instance.CloseUI();
        MMExplorePanel.Instance.UpdateUI();
    }

}
