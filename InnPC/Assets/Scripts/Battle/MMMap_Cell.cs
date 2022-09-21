using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMMap : MMNode
{
    
    public MMCell FindCellOfIndex(int index)
    {
        foreach(var cell in cells)
        {
            if(cell.id == index)
            {
                return cell;
            }
        }


        MMDebugManager.Log("FindCell");
        return null;
    }



    public MMCell FindCellAtPosition(Vector2 pos)
    {
        foreach (var cell in cells)
        {
            if(cell.ContainsPoints(pos))
            {
                return cell;
            }
        }
        
        return null;
    }


}
