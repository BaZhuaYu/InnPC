using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMCell : MMNode
{
    public MMUnitNode unitNode;

    public int index;

    public int row;
    public int col;

    public Text labelID;


    private void Awake()
    {
        index = 0;
    }

    private void Start()
    {
        HandleState(MMNodeState.Normal);
        HandleHighlight(MMNodeHighlight.Normal);
    }


    public bool Accept(MMUnitNode node)
    {
        if (this.unitNode != null)
        {
            return false;
        }
        
        if (node.cell == null)
        {
            node.tempCell = node.cell;
        }

        if(node.cell != null)
        {
            node.cell.Clear();
        }
        
        this.unitNode = node;
        node.cell = this;
        node.SetParent(this);

        return true;
    }
    

    public void Reload()
    {

    }


    public void Clear()
    {
        if(this.unitNode != null)
        {
            Debug.Log("Cell Clear: " + index + " Unit: " + this.unitNode.displayName);
            this.unitNode.cell = null;
            //this.unitNode.Clear();
            this.unitNode = null;
        }  
        HandleState(MMNodeState.Normal);
        HandleHighlight(MMNodeHighlight.Normal);
    }

    
    public static MMCell Create()
    {
        GameObject prefabCell = Resources.Load("Prefabs/MMCell") as GameObject;
        GameObject node = Instantiate(prefabCell);
        MMCell cell = node.GetComponent<MMCell>();
        return cell;
    }

    
    public int FindDistanceFromCell(MMCell cell)
    {
        int a = Mathf.Abs(cell.row - this.row);
        int b = Mathf.Abs(cell.col - this.col);
        return a + b;
    }

    
}
