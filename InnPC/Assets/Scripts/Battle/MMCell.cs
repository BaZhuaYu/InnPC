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
        HandleFill(MMNodeState.Normal);
        HandleBorder(MMNodeHighlight.Normal);
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

        if (node.cell != null)
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
        if (this.unitNode != null)
        {
            this.unitNode.cell = null;
            this.unitNode = null;
        }
        HandleFill(MMNodeState.Normal);
        HandleBorder(MMNodeHighlight.Normal);
    }


    public void HandleFill(MMNodeState s)
    {
        this.nodeState = s;
        switch (s)
        {
            case MMNodeState.Normal:
                GetComponent<Image>().color = MMUtility.FindColorWhite();
                break;
            case MMNodeState.Red:
                GetComponent<Image>().color = MMUtility.FindColorRed();
                break;
            case MMNodeState.Yellow:
                GetComponent<Image>().color = MMUtility.FindColorYellow();
                break;
            case MMNodeState.Blue:
                GetComponent<Image>().color = MMUtility.FindColorBlue();
                break;
            case MMNodeState.Green:
                GetComponent<Image>().color = MMUtility.FindColorGreen();
                break;
        }
    }


    public void HandleBorder(MMNodeHighlight s)
    {
        this.nodeHighlight = s;
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
