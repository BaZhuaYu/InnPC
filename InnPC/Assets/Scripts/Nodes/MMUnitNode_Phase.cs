using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnitNode : MMNode
{
    
    public void OnRoundBegin()
    {
        tempCell = this.cell;
    }

    public void OnRoundEnd()
    {
        this.isActived = true;
    }

}
