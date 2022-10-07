using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMSkill
{

    public static List<MMSkill> all;


    public static void Init()
    {
        all = new List<MMSkill>();
        foreach (var temp in MMSkillData.allValues.Values)
        {
            all.Add(MMSkill.CreateFromString(temp));
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMSkill> FindAll()");
        }
    }
    

    public static List<MMSkill> FindAll()
    {
        if (all == null)
        {
            all = new List<MMSkill>();
            foreach (var temp in MMSkillData.allValues.Values)
            {
                all.Add(MMSkill.CreateFromString(temp));
            }

            if (all.Count == 0)
            {
                MMDebugManager.FatalError("public static List<MMSkill> FindAll()");
            }
        }

        return all;
    }


    public static MMSkill FindRandomOne()
    {
        all = FindAll();
        return all[Random.Range(0, all.Count)];
    }


    public static List<MMSkill> FindRandomCount(int count)
    {
        all = FindAll();
        if (count > all.Count)
        {
            MMDebugManager.FatalError("FindRandom: " + count);
        }

        List<MMSkill> ret = new List<MMSkill>();

        while (ret.Count < count)
        {
            MMSkill skill = FindRandomOne();
            if (MMUtility.CheckListNotHasOne<MMSkill>(ret, skill))
            {
                ret.Add(skill);
            }
        }

        return ret;
    }


    //public static bool CheckHasOne(List<MMSkill> units, MMSkill one)
    //{
    //    foreach (var unit in units)
    //    {
    //        if (unit.id == one.id)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}






    //public void LoadData(int id)
    //{
    //    if (id == 1)
    //    {
    //        this.id = 1;
    //        this.key = "Card_1";
    //        this.displayName = "普通攻击";
    //        this.displayNote = "造成1点伤害";
    //        value = 1;
    //        area = MMArea.Single;
    //    }
    //    else if (id == 2)
    //    {
    //        this.id = 2;
    //        this.key = "Card_2";
    //        this.displayName = "普通防御";
    //        this.displayNote = "获得1点防御";
    //        area = MMArea.Beside;
    //    }
    //    else if (id == 3)
    //    {
    //        this.id = 3;
    //        this.key = "Card_3";
    //        this.displayName = "普通移动";
    //        this.displayNote = "移动一格";
    //        area = MMArea.Behind;
    //    }
    //    else if (id == 100)
    //    {
    //        this.id = 100;
    //        this.key = "Card_100";
    //        this.displayName = "眩晕";
    //        this.displayNote = "进入眩晕状态，下回合进入狂暴状态";
    //        area = MMArea.None;
    //    }
    //    else if (id == 1000)
    //    {
    //        this.id = 1000;
    //        this.key = "Card_1000";
    //        this.displayName = "待命";
    //        this.displayNote = "恢复1点行动力";
    //        area = MMArea.None;
    //    }
    //    else if (id == 10000)
    //    {
    //        this.id = 10000;
    //        this.key = "Card_" + id;
    //        this.displayName = "攻击";
    //        this.displayNote = "对目标造成伤害";
    //        area = MMArea.Single;
    //    }
    //    else if (id == 10101)
    //    {
    //        this.id = id;
    //        this.key = "Card_" + id;
    //        this.displayName = "黑白判官";
    //        this.displayNote = "对目标造成2点伤害，恢复2点生命";
    //        area = MMArea.Single;
    //        this.keywords.Add(MMSkillKeyWord.Ultimate);
    //    }
    //    else if (id == 10201)
    //    {
    //        this.id = id;
    //        this.key = "Card_" + id;
    //        this.displayName = "临危不乱";
    //        this.displayNote = "对横排目标造成伤害";
    //        area = MMArea.Beside;
    //        this.keywords.Add(MMSkillKeyWord.Ultimate);
    //    }
    //    else if (id == 10301)
    //    {
    //        this.id = id;
    //        this.key = "Card_" + id;
    //        this.displayName = "恩赐劫";
    //        this.displayNote = "对目标以及身后单位造成伤害";
    //        area = MMArea.Behind;
    //        this.keywords.Add(MMSkillKeyWord.Ultimate);
    //    }
    //    else if (id == 10401)
    //    {
    //        this.id = id;
    //        this.key = "Card_" + id;
    //        this.displayName = "母辛后裔";
    //        this.displayNote = "召唤鹿小九";
    //        area = MMArea.None;
    //        this.keywords.Add(MMSkillKeyWord.Ultimate);
    //    }
    //    else if (id == 15101)
    //    {
    //        this.id = id;
    //        this.key = "Card_" + id;
    //        this.displayName = "稻草人";
    //        this.displayNote = "无";
    //        area = MMArea.None;
    //        this.keywords.Add(MMSkillKeyWord.Ultimate);
    //    }
    //    else if (id == 16101)
    //    {
    //        this.id = id;
    //        this.key = "Card_" + id;
    //        this.displayName = "丐帮弟子";
    //        this.displayNote = "近战攻击";
    //        area = MMArea.Single;

    //        tempATK = 2;
    //        tempDEF = 2;
    //    }
    //    else
    //    {
    //        MMDebugManager.Log("LoadData: " + id);
    //    }
    //}

    
}
