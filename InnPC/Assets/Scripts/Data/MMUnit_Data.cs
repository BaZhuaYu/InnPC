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

            displayName = "赵太公";
            displayName = "";

            this.maxHP = 5;
            this.hp = maxHP;
            this.maxAP = 2;
            this.ap = maxAP;

            this.atk = 1;
            attackRange = 1;

            this.cards = new List<int>() { 10101, 10000, 1000, 100 };
        }
        else if (id == 2)
        {
            //Hunter
            this.key = "Unit_10200QS";

            displayName = "南宫荣";
            displayName = "";

            this.maxHP = 4;
            this.hp = maxHP;
            this.maxAP = 2;
            this.ap = maxAP;

            this.atk = 2;
            attackRange = 2;

            this.cards = new List<int>() { 10201, 10000, 1000, 100 };
        }
        else if (id == 3)
        {
            //Mage
            this.key = "Unit_10300QS";

            displayName = "宁萍";
            displayName = "";

            this.maxHP = 3;
            this.hp = maxHP;
            this.maxAP = 3;
            this.ap = maxAP;

            this.atk = 1;
            attackRange = 3;

            this.cards = new List<int>() { 10301, 10000, 1000, 100 };
        }
        else if (id == 4)
        {
            //Mage
            this.key = "Unit_10400QS";

            displayName = "鹿小九";
            displayName = "";

            this.maxHP = 4;
            this.hp = maxHP;
            this.maxAP = 2;
            this.ap = maxAP;

            this.atk = 1;
            attackRange = 3;

            this.cards = new List<int>() { 10401, 10000, 1000, 100 };
        }
        else if (id == 16100)
        {
            //Mage
            this.key = "Unit_10400QS";

            displayName = "丐帮弟子";
            displayName = "";

            this.maxHP = 4;
            this.maxAP = 2;
            this.atk = 1;
            this.attackRange = 3;
            
            this.ap = maxAP;
            this.hp = maxHP;

            this.cards = new List<int>() { 16101, 10000, 1000, 100 };
        }

    }


}
