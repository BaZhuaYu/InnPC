using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnit
{

    public string key;
    public int id;

    public int maxHP;
    public int hp;
    public int maxAP;
    public int ap;

    public int atk;
    public int def;
    public int mag;
    public int spd;

    public int attackRange;

    public List<int> cards;
    

    public static MMUnit Create(int id)
    {
        MMUnit ret = new MMUnit();
        ret.id = id;
        ret.LoadData();
        return ret;
    }

}
