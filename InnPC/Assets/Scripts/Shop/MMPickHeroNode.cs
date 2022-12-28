using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPickHeroNode : MMNode
{

    public List<MMUnitNode> units;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Accept(List<MMUnit> units)
    {
        this.units = new List<MMUnitNode>();
        
        foreach(var unit in units)
        {
            MMUnitNode node = MMUnitNode.Create(unit);
            this.units.Add(node);
            this.AddChild(node);
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        float offset = this.FindWidth() * 0.4f;
        foreach (var unit in units)
        {
            unit.MoveLeft(offset);
            offset += unit.FindWidth() * 1.1f;
        }
    }



    public static MMPickHeroNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMPickHeroNode") as GameObject);
        obj.name = "MMPickHeroNode";
        return obj.GetComponent<MMPickHeroNode>();
    }


}
