﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMMap : MMNode
{

    public MMCell FindCellOfIndex(int index)
    {
        foreach (var cell in cells)
        {
            if (cell.id == index)
            {
                return cell;
            }
        }

        MMDebugManager.Log("FindCellOfIndex: " + index);
        return null;
    }


    public MMCell FindCellInXY(int x, int y)
    {
        foreach (var cell in cells)
        {
            if (cell.row == x && cell.col == y)
            {
                return cell;
            }
        }

        MMDebugManager.Log("FindCellOfIndex: X " + x + " Y " + y);
        return null;
    }
    

    public MMCell FindCellAtPosition(Vector2 pos)
    {
        foreach (var cell in cells)
        {
            if (cell.ContainsPoints(pos))
            {
                return cell;
            }
        }

        return null;
    }


    public List<MMCell> FindCellsWithinDistance(MMCell cell, int dis)
    {
        List<MMCell> ret = new List<MMCell>();

        foreach (var c in cells)
        {
            if (cell.FindDistanceFromCell(c) <= dis)
            {
                ret.Add(c);
            }
        }

        return ret;
    }

    public List<MMCell> FindCellsInFrontDistance(MMCell cell, int dis)
    {
        List<MMCell> ret = new List<MMCell>();

        for(int i = 1; i <= dis; i++)
        {
            MMCell c = this.FindCellInXY(cell.row + i, cell.col);
            if (c != null) {
                ret.Add(c);
            }
        }

        return ret;
    }


    public List<MMCell> FindCellsInColLessThanCell(MMCell cell, int dis)
    {
        List<MMCell> ret = new List<MMCell>();

        for (int i = 1; i <= dis; i++)
        {
            MMCell c = this.FindCellInXY(cell.row - i, cell.col);
            if (c != null)
            {
                ret.Add(c);
            }
        }

        return ret;
    }

    public List<MMCell> FindCellsInColGreaterThanCell(MMCell cell, int dis)
    {
        List<MMCell> ret = new List<MMCell>();

        for (int i = 1; i <= dis; i++)
        {
            MMCell c = this.FindCellInXY(cell.row + i, cell.col);
            if (c != null)
            {
                ret.Add(c);
            }
        }

        return ret;
    }



    public List<MMCell> FindCellsInRows(List<int> rows)
    {
        List<MMCell> ret = new List<MMCell>();

        foreach (var c in cells)
        {
            if (rows.Contains(c.row))
            {
                ret.Add(c);
            }
        }

        return ret;
    }



    //Single,
    //Row,
    //Col,
    //Beside,
    //Behind


    public List<MMCell> FindCellsInRow(int row)
    {
        List<MMCell> ret = new List<MMCell>();
        foreach (var cell in cells)
        {
            if (cell.row == row)
            {
                ret.Add(cell);
            }
        }

        return ret;
    }

    public List<MMCell> FindCellsInCol(int col)
    {
        List<MMCell> ret = new List<MMCell>();
        foreach (var cell in cells)
        {
            if (cell.col == col)
            {
                ret.Add(cell);
            }
        }

        return ret;
    }


    public List<MMCell> FindCellsInRow(MMCell cell)
    {
        return FindCellsInRow(cell.row);
    }


    public List<MMCell> FindCellsInCol(MMCell cell)
    {
        return FindCellsInCol(cell.col);
    }


    public List<MMCell> FindCellsBeside(MMCell cell)
    {
        List<MMCell> ret = new List<MMCell>();

        MMCell cell1 = FindCellOfIndex(cell.id - 1);
        MMCell cell2 = FindCellOfIndex(cell.id + 1);

        Debug.Log(cell.id);
        Debug.Log(cell1.id);
        Debug.Log(cell2.id);

        if (cell1.row == cell.row)
        {
            ret.Add(cell1);
        }

        if (cell2.row == cell.row)
        {
            ret.Add(cell2);
        }

        return ret;
    }


    public List<MMCell> FindCellsBehind(MMCell cell)
    {
        List<MMCell> ret = new List<MMCell>();

        MMCell cell1 = FindCellInXY(cell.row + 1, cell.col);

        if (cell1 != null)
        {
            ret.Add(cell1);
        }

        return ret;
    }


}
