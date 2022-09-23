using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnit
{


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

            this.spd = 1;
            attackRange = 1;

            this.cards = new List<int>() { 1, 1, 1, 1 };
        }
        else if (id == 2)
        {
            //Hunter
            this.key = "Unit_10200QS";
            this.maxHP = 40;
            this.hp = maxHP;
            this.maxAP = 3;
            this.ap = maxAP;

            this.spd = 2;
            attackRange = 2;

            this.cards = new List<int>() {2,2,2,2 };
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

            this.cards = new List<int>() { 3, 3, 3, 3 };
        }
        else if (id == 4)
        {
            //Mage
            this.key = "Unit_10400QS";
            this.maxHP = 30;
            this.hp = maxHP;
            this.maxAP = 4;
            this.ap = maxAP;

            this.spd = 4;
            attackRange = 2;
            this.cards = new List<int>() { 3, 3, 3, 3 };
        }
    }


}
