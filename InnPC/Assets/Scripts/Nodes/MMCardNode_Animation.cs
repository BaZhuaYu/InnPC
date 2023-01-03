using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMCardNode : MMNode
{
    
    public void ShowMiddleY()
    {
        this.MoveToCenterY();
    }

    public void ShowUp()
    {
        this.MoveToCenterY();
        this.MoveUp(this.FindHeight() * 0.15f);
    }

    public void ShowDown()
    {
        this.MoveToCenterY();
        this.MoveDown(this.FindHeight() * 0.35f);
    }

    
}
