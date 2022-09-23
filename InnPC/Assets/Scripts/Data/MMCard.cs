using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMCard
{
    public string key;
    public int id;
    public string displayName;
    public string displayNote;
    public string icon;


    public int value;
    public MMArea area;

    

    public static MMCard Create(int id)
    {
        MMCard ret = new MMCard();
        ret.LoadData(id);
        return ret;
    }

    
    public void ExecuteEffect()
    {

    }


}
