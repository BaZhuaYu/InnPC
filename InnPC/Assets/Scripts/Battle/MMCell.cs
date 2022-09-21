using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public partial class MMCell : MMNode
{
    public MMNodeUnit nodeUnit;
    public int id;

    public Text labelID;

    public MMNodeHighlight highlight;


    private void Awake()
    {
        id = 0;
    }

    private void Start()
    {
        EnterHighlight(MMNodeHighlight.Normal);
    }



    public void AcceptUnit(MMUnit unit)
    {
        MMNodeUnit node = MMNodeUnit.Create();
        node.Accept(unit);
        this.AcceptUnitNode(node);
    }


    public void AcceptUnitNode(MMNodeUnit node)
    {
        this.nodeUnit = node;
        node.cell = this;
        node.SetParent(this);
    }
    


    public static MMCell Create()
    {
        GameObject prefabCell = Resources.Load("Prefabs/MMCell") as GameObject;
        GameObject node = Instantiate(prefabCell);
        MMCell cell = node.GetComponent<MMCell>();
        return cell;
    }

    public void EnterHighlight(MMNodeHighlight s)
    {
        this.highlight = s;
        switch (s)
        {
            case MMNodeHighlight.Normal:
                GetComponent<Outline>().effectColor = new Color(0f, 0f, 0f, 0f);
                this.transform.SetSiblingIndex(0);
                break;
            case MMNodeHighlight.Red:
                GetComponent<Outline>().effectColor = new Color(1f, 0f, 0f, 1f);
                this.transform.SetSiblingIndex(100);
                break;
            case MMNodeHighlight.Yellow:
                GetComponent<Outline>().effectColor = new Color(1f, 0.5f, 0f, 1f);
                this.transform.SetSiblingIndex(100);
                break;
            case MMNodeHighlight.Blue:
                GetComponent<Outline>().effectColor = new Color(0f, 0f, 1f, 1f);
                this.transform.SetSiblingIndex(100);
                break;
            case MMNodeHighlight.Green:
                GetComponent<Outline>().effectColor = new Color(0f, 1f, 0f, 1f);
                this.transform.SetSiblingIndex(100);
                break;
        }

    }
}
