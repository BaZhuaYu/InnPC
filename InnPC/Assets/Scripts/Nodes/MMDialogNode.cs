using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDialogNode : MMNode
{

    public MMQuest quest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Accept(MMQuest q)
    {
        this.quest = q;
    }

}
