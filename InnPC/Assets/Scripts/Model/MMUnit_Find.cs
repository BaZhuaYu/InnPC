using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMUnit
{

    public static List<MMUnit> all;


    public static void Init()
    {
        all = new List<MMUnit>();
        foreach (var temp in MMUnitData.allValues.Values)
        {
            all.Add(MMUnit.CreateFromString(temp));
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMUnit> FindAll()");
        }
    }


    public static List<MMUnit> FindAll()
    {
        if(all == null)
        {
            all = new List<MMUnit>();
            foreach (var temp in MMUnitData.allValues.Values)
            {
                all.Add(MMUnit.CreateFromString(temp));
            }

            if(all.Count == 0)
            {
                MMDebugManager.FatalError("public static List<MMUnit> FindAll()");
            }
        }
        
        return all;
    }


    public static MMUnit FindRandomOne()
    {
        all = FindAll();
        return all[Random.Range(0, all.Count)];
    }
    

    public static List<MMUnit> FindRandomCount(int count)
    {
        all = FindAll();
        if (count > all.Count)
        {
            MMDebugManager.FatalError("FindRandom: " + count);
        }

        List<MMUnit> ret = new List<MMUnit>();

        while(ret.Count < count)
        {
            MMUnit unit = FindRandomOne();
            if(MMUtility.CheckListNotHasOne<MMUnit>(ret, unit))
            {
                ret.Add(unit);
            }
        }

        return ret;
    }
    

    //public static bool CheckListHasOne(List<MMUnit> units, MMUnit one)
    //{
    //    foreach(var unit in units)
    //    {
    //        if(unit.id == one.id)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}








    //void LoadData()
    //{
    //    if (id == 10100)
    //    {
    //        //Knight
    //        this.key = "Unit_10100QS";

    //        displayName = "赵太公";
    //        displayName = "";

    //        this.maxHP = 5;
    //        this.hp = maxHP;
    //        this.maxAP = 2;
    //        this.ap = 0;

    //        this.atk = 1;
    //        attackRange = 1;

    //        this.skills = new List<int>() { 10101, 10000, 1000, 100 };
    //    }
    //    else if (id == 10200)
    //    {
    //        //Hunter
    //        this.key = "Unit_10200QS";

    //        displayName = "南宫荣";
    //        displayName = "";

    //        this.maxHP = 4;
    //        this.hp = maxHP;
    //        this.maxAP = 2;
    //        this.ap = 0;

    //        this.atk = 2;
    //        attackRange = 2;

    //        this.skills = new List<int>() { 10201, 10000, 1000, 100 };
    //    }
    //    else if (id == 10300)
    //    {
    //        //Mage
    //        this.key = "Unit_10300QS";

    //        displayName = "宁萍";
    //        displayName = "";

    //        this.maxHP = 3;
    //        this.hp = maxHP;
    //        this.maxAP = 3;
    //        this.ap = 0;

    //        this.atk = 1;
    //        attackRange = 3;

    //        this.skills = new List<int>() { 10301, 10000, 1000, 100 };
    //    }
    //    else if (id == 10400)
    //    {
    //        //Mage
    //        this.key = "Unit_10400QS";

    //        displayName = "鹿小九";
    //        displayName = "";

    //        this.maxHP = 4;
    //        this.hp = maxHP;
    //        this.maxAP = 2;
    //        this.ap = 0;

    //        this.atk = 1;
    //        attackRange = 3;

    //        this.skills = new List<int>() { 10401, 10000, 1000, 100 };
    //    }
    //    else if (id == 10500)
    //    {
    //        //Mage
    //        this.key = "Unit_10500QS";

    //        displayName = "柳歌";
    //        displayName = "";

    //        this.maxHP = 4;
    //        this.hp = maxHP;
    //        this.maxAP = 2;
    //        this.ap = 0;

    //        this.atk = 1;
    //        attackRange = 3;

    //        this.skills = new List<int>() { 10401, 10000, 1000, 100 };
    //    }
    //    else if (id == 10600)
    //    {
    //        //Mage
    //        this.key = "Unit_10600QS";

    //        displayName = "陆壬";
    //        displayName = "";

    //        this.maxHP = 4;
    //        this.hp = maxHP;
    //        this.maxAP = 2;
    //        this.ap = 0;

    //        this.atk = 1;
    //        attackRange = 3;

    //        this.skills = new List<int>() { 10401, 10000, 1000, 100 };
    //    }
    //    else if (id == 10700)
    //    {
    //        //Mage
    //        this.key = "Unit_10700QS";

    //        displayName = "魏裘";
    //        displayName = "";

    //        this.maxHP = 4;
    //        this.hp = maxHP;
    //        this.maxAP = 2;
    //        this.ap = 0;

    //        this.atk = 1;
    //        attackRange = 3;

    //        this.skills = new List<int>() { 10401, 10000, 1000, 100 };
    //    }
    //    else if (id == 10800)
    //    {
    //        //Mage
    //        this.key = "Unit_10800QS";

    //        displayName = "尹时生";
    //        displayName = "";

    //        this.maxHP = 4;
    //        this.hp = maxHP;
    //        this.maxAP = 2;
    //        this.ap = 0;

    //        this.atk = 1;
    //        attackRange = 3;

    //        this.skills = new List<int>() { 10401, 10000, 1000, 100 };
    //    }
    //    else if (id == 16100)
    //    {
    //        //Mage
    //        this.key = "Unit_16100QS";

    //        displayName = "丐帮弟子";
    //        displayName = "";

    //        this.maxHP = 4;
    //        this.maxAP = 2;
    //        this.atk = 1;
    //        this.attackRange = 3;
            
    //        this.ap = maxAP;
    //        this.hp = maxHP;

    //        this.skills = new List<int>() { 16101, 10000, 1000, 100 };
    //    }
    //    else if (id == 15100)
    //    {
    //        //Mage
    //        this.key = "Unit_16100QS";

    //        displayName = "稻草人";
    //        displayName = "";

    //        this.maxHP = 1;
    //        this.maxAP = 0;
    //        this.atk = 1;
    //        this.attackRange = 1;

    //        this.ap = maxAP;
    //        this.hp = maxHP;

    //        this.skills = new List<int>() { 16101, 10000, 1000, 100 };
    //    }
    //    else if (id == 16101)
    //    {
    //        Load16100();

    //        this.maxHP = 3;
    //        this.maxAP = 0;
    //        this.atk = 1;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }
    //    else if (id == 16102)
    //    {
    //        Load16100();

    //        this.maxHP = 4;
    //        this.maxAP = 0;
    //        this.atk = 1;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }
    //    else if (id == 16103)
    //    {
    //        Load16100();

    //        this.maxHP = 5;
    //        this.maxAP = 0;
    //        this.atk = 1;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }
    //    else if (id == 17101)
    //    {
    //        Load17100();

    //        this.maxHP = 7;
    //        this.maxAP = 0;
    //        this.atk = 2;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }
    //    else if (id == 17102)
    //    {
    //        Load17100();

    //        this.maxHP = 8;
    //        this.maxAP = 0;
    //        this.atk = 2;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }
    //    else if (id == 17103)
    //    {
    //        Load17100();

    //        this.maxHP = 9;
    //        this.maxAP = 0;
    //        this.atk = 2;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }
    //    else if (id == 18101)
    //    {
    //        Load18100();

    //        this.maxHP = 10;
    //        this.maxAP = 0;
    //        this.atk = 3;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }
    //    else if (id == 18102)
    //    {
    //        Load18100();

    //        this.maxHP = 11;
    //        this.maxAP = 0;
    //        this.atk = 3;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }
    //    else if (id == 18103)
    //    {
    //        Load18100();

    //        this.maxHP = 12;
    //        this.maxAP = 0;
    //        this.atk = 3;

    //        this.ap = maxAP;
    //        this.hp = maxHP;
    //    }

    //}











    //public void Load16100()
    //{
    //    this.key = "Unit_16100QS";

    //    displayName = "丐帮弟子";
    //    displayNote = "";

    //    this.maxHP = 4;
    //    this.maxAP = 2;
    //    this.atk = 1;
    //    this.attackRange = 1;

    //    this.ap = maxAP;
    //    this.hp = maxHP;

    //    this.skills = new List<int>() { 16101, 10000, 1000, 100 };
    //}

    //public void Load17100()
    //{
    //    this.key = "Unit_17100QS";

    //    displayName = "丐帮精英";
    //    displayNote = "";

    //    this.maxHP = 7;
    //    this.maxAP = 0;
    //    this.atk = 2;
    //    this.attackRange = 1;

    //    this.ap = maxAP;
    //    this.hp = maxHP;

    //    this.skills = new List<int>() { 16101, 10000, 1000, 100 };
    //}

    //public void Load18100()
    //{
    //    this.key = "Unit_18100QS";

    //    displayName = "丐帮长老";
    //    displayNote = "";

    //    this.maxHP = 10;
    //    this.maxAP = 0;
    //    this.atk = 3;
    //    this.attackRange = 1;

    //    this.ap = maxAP;
    //    this.hp = maxHP;

    //    this.skills = new List<int>() { 16101, 10000, 1000, 100 };
    //}


}
