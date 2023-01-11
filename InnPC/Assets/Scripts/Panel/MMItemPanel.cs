using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMItemPanel : MMNode
{

    public MMButton buttonClose;
    public MMItemNode item;
    public MMPickHeroNode pickHero;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Accept(MMItemNode item)
    {
        this.item = item;
    }

    public void Reload()
    {

    }

    public void Clear()
    {

    }

    void UpdateUI()
    {

    }

    public void Use()
    {
        Destroy(gameObject);
    }


    public static MMItemPanel Create(MMItem item)
    {
        MMItemNode node = MMItemNode.Create(item);
        return Create(node);
    }


    public static MMItemPanel Create(MMItemNode item)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMItemPanel") as GameObject);
        obj.name = "MMItemPanel";
        MMItemPanel ret = obj.GetComponent<MMItemPanel>();
        ret.Accept(item);
        ret.pickHero.Accept(MMExplorePanel.Instance.units);

        foreach (var unit in ret.pickHero.units)
        {
            unit.gameObject.AddComponent<MMReward_ItemNode>();
            unit.gameObject.GetComponent<MMReward_ItemNode>().item = item;
            unit.gameObject.GetComponent<MMReward_ItemNode>().panel = ret;
        }

        return ret;
    }

}
