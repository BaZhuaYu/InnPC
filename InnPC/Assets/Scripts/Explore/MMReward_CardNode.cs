using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MMReward_CardNode : MonoBehaviour, IPointerClickHandler
{
    public MMCard card;

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

        MMExplorePanel.Instance.cards.Add(card);
        Debug.Log("asdasdasdad: " + card.displayName);
        gameObject.SetActive(false);
        
        MMExplorePanel.Instance.UpdateUI();
    }

}
