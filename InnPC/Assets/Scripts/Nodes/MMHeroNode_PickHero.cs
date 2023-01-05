using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMHeroNode_PickHero : MonoBehaviour, IPointerClickHandler
{

    MMHeroNode hero;

    public bool isSelected;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hero = this.gameObject.GetComponent<MMHeroNode>();
        
        if(MMPickHeroPanel.Instance.selectedHeroes.Contains(hero))
        {
            isSelected = false;
            hero.avatar.LoadColor(Color.white);
            MMPickHeroPanel.Instance.UnselectHero(hero);
        }
        else
        {
            isSelected = true;
            hero.avatar.LoadColor(Color.gray);
            MMPickHeroPanel.Instance.SelectHero(hero);
        }
        
    }

}
