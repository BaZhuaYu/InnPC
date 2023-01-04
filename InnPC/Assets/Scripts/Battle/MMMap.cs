using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMMap : MMNode
{
    public static MMMap Instance;

    public int row;

    public int col;

    public List<MMCell> cells;


    private void Awake()
    {
        Instance = this;

        row = 7;
        col = 4;
        cells = new List<MMCell>();


        SetSize(new Vector2(row * 150, col * 150));

        LoadData();
        LoadCells();

    }

    private void Start()
    {
        MoveToCenter();
        MoveUp(125f);
    }



    public void LoadCells()
    {
        //GameObject prefabCell = Resources.Load("Prefabs/MMCell") as GameObject;
        for (int i = 0; i < row * col; i++)
        {
            //GameObject node = Instantiate(prefabCell);
            //MMCell cell = node.GetComponent<MMCell>();
            MMCell cell = MMCell.Create();
            //cell.gameObject.transform.SetParent(this.transform);

            cell.name = "Cell" + i;
            cell.index = i;
            cell.row = i / col;
            cell.col = i % col;
            cell.SetParent(this);
            cells.Add(cell);

            cell.labelID.text = "" + i;

            cell.MoveToParentLeftOffset((float)(cell.row) * cell.FindWidth());
            cell.MoveToParentTopOffset((float)(cell.col) * cell.FindHeight());

        }
    }


    public void LoadData()
    {

    }


    public void Reload()
    {

    }

    public void Clear()
    {
        foreach (var cell in cells)
        {
            cell.Clear();
        }
    }


    public void ShowHighlightRow(int row)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/MMHighlightRow");
        MMNode node = Instantiate(prefab).GetComponent<MMNode>();
        AddChild(node);
        node.name = "HighlightRow";

        Vector2 pos = new Vector2(FindCellsInRow(row)[0].transform.localPosition.x, 0);
        node.transform.localPosition = pos;
    }

    public void HideHightlightRow()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            if (obj.name == "HighlightRow")
            {
                Destroy(obj);
            }
        }
    }

    public void ShowHighlightCol(int col)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/MMNode");
        MMNode node = Instantiate(prefab).GetComponent<MMNode>();
        node.LoadImage("");
        AddChild(node);

        Vector2 pos = new Vector2(FindCellsInCol(col)[0].transform.localPosition.x, 0);
        node.transform.localPosition = pos;
    }


}
