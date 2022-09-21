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

        row = 4;
        col = 9;
        cells = new List<MMCell>();
        LoadData();
        LoadCells();
        LoadUI();
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
            cell.SetParent(this);
            cells.Add(cell);
            
            cell.labelID.text = "" + i;
            
            cell.MoveToLeft((float)(i / row) * cell.FindWidth());
            cell.MoveToBottom((float)(i % row) * cell.FindHeight());
            
        }
    }


    public void LoadData()
    {
        
    }

    public void LoadUI()
    {
        
        foreach(var cell in cells)
        {
            
            
        }
    }





    public void Accept(MMNodeUnit nodeUnit)
    {
        nodeUnit.SetParent(this);
    }

    public void Reload()
    {

    }

    public void Clear()
    {

    }


}
