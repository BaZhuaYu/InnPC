using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMMap : MMNode
{

    public MMCell FindCellOfIndex(int index)
    {
        foreach (var cell in cells)
        {
            if (cell.index == index)
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

        MMCell cell1 = FindCellOfIndex(cell.index - 1);
        MMCell cell2 = FindCellOfIndex(cell.index + 1);

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


    public List<MMCell> FindCellsNine(MMCell cell)
    {
        List<MMCell> ret = new List<MMCell>();

        MMCell cell1 = MMMap.Instance.FindCellInXY(cell.row - 1, cell.col + 0);
        MMCell cell2 = MMMap.Instance.FindCellInXY(cell.row - 1, cell.col + 1);
        MMCell cell3 = MMMap.Instance.FindCellInXY(cell.row + 0, cell.col + 1);
        MMCell cell4 = MMMap.Instance.FindCellInXY(cell.row + 1, cell.col + 1);
        MMCell cell5 = MMMap.Instance.FindCellInXY(cell.row + 1, cell.col + 0);
        MMCell cell6 = MMMap.Instance.FindCellInXY(cell.row + 1, cell.col - 1);
        MMCell cell7 = MMMap.Instance.FindCellInXY(cell.row + 0, cell.col - 1);
        MMCell cell8 = MMMap.Instance.FindCellInXY(cell.row - 1, cell.col - 1);

        if (cell1 != null) { ret.Add(cell1); }
        if (cell2 != null) { ret.Add(cell2); }
        if (cell3 != null) { ret.Add(cell3); }
        if (cell4 != null) { ret.Add(cell4); }
        if (cell5 != null) { ret.Add(cell5); }
        if (cell6 != null) { ret.Add(cell6); }
        if (cell7 != null) { ret.Add(cell7); }
        if (cell8 != null) { ret.Add(cell8); }

        return ret;
    }

    public List<MMCell> FindCellsTeamCells(MMCell cell)
    {
        List<int> cells;
        if (cell.row < 4)
        {
            cells = new List<int> { 0, 1, 2 };
        }
        else
        {
            cells = new List<int> { 4, 5, 6 };
        }

        return MMMap.Instance.FindCellsInRows(cells);
        
    }

    public List<MMCell> FindCellsAll()
    {
        return MMMap.Instance.cells;
    }


    public List<MMCell> FindEmptyCells()
    {
        List<MMCell> ret = new List<MMCell>();
        foreach (var cell in cells)
        {
            if (cell.unitNode == null)
            {
                ret.Add(cell);
            }
        }

        return ret;
    }


    public List<MMCell> FindEmptyCellInRow(int row)
    {
        List<MMCell> ret = new List<MMCell>();
        List<MMCell> rowcells = FindCellsInRow(row);
        foreach (var cell in rowcells)
        {
            if (cell.unitNode == null)
            {
                ret.Add(cell);
            }
        }

        return ret;
    }


    public MMCell FindRandomEmptyCellInRow(int row)
    {
        List<MMCell> cells = FindEmptyCellInRow(row);
        if (cells.Count == 0)
        {
            MMDebugManager.FatalError("FindRandomEmptyCellInRow: " + row);
            return null;
        }

        return cells[Random.Range(0, cells.Count)];
    }


    public List<MMCell> FindCellsWithUnitRace(int race)
    {
        List<MMCell> ret = new List<MMCell>();
        foreach (var cell in cells)
        {
            if (cell.unitNode != null)
            {
                if (cell.unitNode.race == race)
                {
                    ret.Add(cell);
                }
            }
        }

        return ret;
    }



    public int FindFrontRowOfGroup(int group)
    {
        int ret = 0;
        if (group == 2)
        {
            ret = 7;
        }
        foreach (var cell in cells)
        {
            if (cell.unitNode != null && cell.unitNode.group == group)
            {
                if (group == 1)
                {
                    if (cell.row > ret)
                    {
                        ret = cell.row;
                    }
                }
                else
                {
                    if (cell.row < ret)
                    {
                        ret = cell.row;
                    }
                }
            }
        }

        return ret;
    }

}
