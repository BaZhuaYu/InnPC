using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMUnit
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


    void LoadData()
    {
        if (id == 1)
        {
            //Knight
            this.key = "Unit_10100QS";
            this.maxHP = 50;
            this.hp = maxHP;
            this.maxAP = 2;
            this.ap = maxAP;

            this.spd = 3;
            attackRange = 1;
        }
        else if (id == 2)
        {
            //Hunter
            this.key = "Unit_10200QS";
            this.maxHP = 40;
            this.hp = maxHP;
            this.maxAP = 3;
            this.ap = maxAP;

            this.spd = 3;
            attackRange = 2;
        }
        else if (id == 3)
        {
            //Mage
            this.key = "Unit_10300QS";
            this.maxHP = 30;
            this.hp = maxHP;
            this.maxAP = 4;
            this.ap = maxAP;

            this.spd = 3;
            attackRange = 3;
        }
    }


    public static MMUnit Create(int id)
    {
        MMUnit ret = new MMUnit();
        ret.id = id;
        ret.LoadData();
        return ret;
    }

}
