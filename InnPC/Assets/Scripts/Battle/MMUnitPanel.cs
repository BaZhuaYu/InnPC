using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMUnitPanel : MMNode
{

    public static MMUnitPanel Instance;

    private void Awake()
    {
        Instance = this;
    }


    public List<MMHeroNode> units;

    public MMUnitNode selectingUnit;



    // Start is called before the first frame update
    void Start()
    {
        units = new List<MMHeroNode>();
    }



    public void Accept(List<MMUnit> units)
    {
        if (this.units.Count > 0)
        {
            Clear();
        }

        this.units = new List<MMHeroNode>();
        foreach (var unit in units)
        {
            MMHeroNode node = MMHeroNode.Create(unit);
            node.gameObject.AddComponent<MMUnitNode_Panel>();
            this.units.Add(node);
        }
        
        Reload();
    }


    public void Reload()
    {
        float offset = -(float)(units.Count/2) * 200;
        
        foreach (var unit in units)
        {
            unit.SetParent(this);
            //unit.MoveToParentLeftOffset(offset);
            unit.MoveRight(offset);
            offset += unit.FindWidth() * 1.1f;
        }
    }


    public void Clear()
    {
        foreach (var unit in units)
        {
            unit.RemoveFromParent();
        }
        units = new List<MMHeroNode>();
        selectingUnit = null;
        Reload();
    }


}
