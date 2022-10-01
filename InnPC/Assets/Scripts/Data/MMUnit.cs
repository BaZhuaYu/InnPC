using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class MMUnit
{

    public string key;
    public int id;

    public string displayName;
    public string displayNote;

    public int maxHP;
    public int hp;
    public int maxAP;
    public int ap;

    public int atk;
    public int def;
    public int mag;
    public int spd;

    public int tempATK;
    public int tempDEF;


    public int attackRange;

    public List<int> skills;
    

    public static MMUnit Create(int id)
    {
        MMUnit ret = new MMUnit();
        ret.id = id;
        ret.LoadData();
        return ret;
    }


    

}
