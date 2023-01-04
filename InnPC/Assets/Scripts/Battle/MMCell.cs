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
    public MMNode border;


    private void Awake()
    {
        index = 0;
    }

    private void Start()
    {
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
        HandleHighlight(MMNodeHighlight.Normal);
    }


    public void HandleHighlight(MMNodeHighlight s)
    {
        this.nodeHighlight = s;
        switch (s)
        {
            case MMNodeHighlight.Normal:
                //GetComponent<Image>().color = MMUtility.FindColorWhite();
                LoadImage("");
                //LoadImage("UI/aaa/CellFill_Blue");
                break;
            case MMNodeHighlight.Red:
//                GetComponent<Image>().color = MMUtility.FindColorRed();
                LoadImage("UI/aaa/CellFill_Red");
                break;
            case MMNodeHighlight.Yellow:
                //GetComponent<Image>().color = MMUtility.FindColorYellow();
                LoadImage("UI/aaa/CellFill_Yellow");
                break;
            case MMNodeHighlight.Blue:
                //GetComponent<Image>().color = MMUtility.FindColorBlue();
                LoadImage("UI/aaa/CellFill_Blue");
                break;
            case MMNodeHighlight.Green:
                //GetComponent<Image>().color = MMUtility.FindColorLightGreen();
                LoadImage("UI/aaa/CellFill_Green");
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
