using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMCell : MMNode
{
    public MMUnitNode unitNode;

    public int id;

    public int row;
    public int col;

    public Text labelID;


    private void Awake()
    {
        id = 0;
    }

    private void Start()
    {
        HandleState(MMNodeState.Normal);
        HandleHighlight(MMNodeHighlight.Normal);
    }

    
    public void AcceptUnit(MMUnit unit)
    {
        MMUnitNode node = MMUnitNode.Create();
        node.Accept(unit);
        this.Accept(node);
    }


    public void Accept(MMUnitNode node)
    {
        if(node.cell != null)
        {
            node.cell.Clear();
        }

        this.unitNode = node;
        node.cell = this;
        node.SetParent(this);
    }
    

    public void Reload()
    {

    }

    public void Clear()
    {
        if(this.unitNode != null)
        {
            this.unitNode.cell = null;
            this.unitNode = null;
        }   
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
