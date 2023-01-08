using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMExplorePanel : MMNode
{
    
    public void OnBeginDay()
    {
        this.tansuoTime = 12;
        foreach(var place in places)
        {
            place.num = place.place.num;
        }
    }

    public void OnEndDay()
    {

    }


}
