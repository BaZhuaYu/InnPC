using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCard
{
    public string key;
    public int id;
    public string displayName;
    public string displayNote;
    public string icon;

    

    public static MMCard Create(int id)
    {
        MMCard ret = new MMCard();
        if(ret == null)
        {
            Debug.Log("xxxxxxxxxxxxxxxx");
        }
        ret.LoadData(id);
        return ret;
    }


    public void LoadData(int id)
    {
        if(id == 1)
        {
            this.id = 1;
            this.key = "Card_1";
            this.displayName = "普通攻击";
            this.displayNote = "造成1点伤害";
        }
        else if (id == 2)
        {
            this.id = 2;
            this.key = "Card_2";
            this.displayName = "普通防御";
            this.displayNote = "获得1点防御";
        }
        else if (id == 3)
        {
            this.id = 3;
            this.key = "Card_3";
            this.displayName = "普通移动";
            this.displayNote = "移动一格";
        }
        else
        {
            MMDebugManager.Log(key);
        }
    }

    
    public void ExecuteEffect()
    {

    }


}
