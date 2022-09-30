using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMMap : MMNode
{
    public static MMMap instance;
    
    public int row;

    public int col;

    public List<MMCell> cells;


    private void Awake()
    {
        instance = this;

        row = 9;
        col = 4;
        cells = new List<MMCell>();
        LoadData();
        LoadCells();
    }

    

    public void LoadCells()
    {
        GameObject prefabCell = Resources.Load("Prefabs/MMCell") as GameObject;
        for (int i = 0; i < row * col; i++)
        {
            GameObject node = Instantiate(prefabCell);
            MMCell cell = node.GetComponent<MMCell>();
            //cell.gameObject.transform.SetParent(this.transform);
            
            cell.name = "Cell" + i;
            cell.id = i;
            cell.row = i / 4;
            cell.col = i % 4;
            cell.SetParent(this);
            cells.Add(cell);
            
            cell.labelID.text = "" + i;
            
            cell.MoveToLeft((float)(cell.row) * cell.FindWidth());
            cell.MoveToTop((float)(cell.col) * cell.FindHeight());
            
        }
    }


    public void LoadData()
    {
        
    }







    public void Accept(MMUnitNode nodeUnit)
    {
        nodeUnit.SetParent(this);
    }

    public void Reload()
    {

    }

    public void Clear()
    {
        foreach(var cell in cells)
        {
            cell.Clear();
        }
    }


}
