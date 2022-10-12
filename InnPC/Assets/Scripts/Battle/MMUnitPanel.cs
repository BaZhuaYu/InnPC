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


    public List<MMUnitNode> units;

    public MMUnitNode selectingUnit;



    // Start is called before the first frame update
    void Start()
    {
        units = new List<MMUnitNode>();
    }



    public void Accept(List<MMUnitNode> units)
    {
        if(this.units.Count > 0)
        {
            Clear();
        }

        this.units = units;
        Reload();
    }


    public void Reload()
    {
        float offset = 0;
        foreach (var unit in units)
        {
            unit.SetParent(this);
            unit.MoveToParentLeftOffset(offset);
            offset += 10 + unit.FindWidth();
        }
    }


    public void Clear()
    {
        foreach (var unit in units)
        {
            unit.RemoveFromParent();
        }
        units = new List<MMUnitNode>();
        selectingUnit = null;
        Reload();
    }


}
