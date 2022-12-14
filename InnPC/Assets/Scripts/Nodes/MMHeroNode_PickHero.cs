using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMHeroNode_PickHero : MonoBehaviour, IPointerClickHandler
{

    MMHeroNode hero;
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
        hero = this.gameObject.GetComponent<MMHeroNode>();

        if(MMPickHeroPanel.Instance.heroes.Contains(hero))
        {
            MMPickHeroPanel.Instance.SelectHero(hero);
        }
        else
        {
            MMPickHeroPanel.Instance.UnselectHero(hero);
        }
        
    }

}
