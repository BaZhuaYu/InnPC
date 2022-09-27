using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMCard
{

    public void LoadData(int id)
    {
        if (id == 1)
        {
            this.id = 1;
            this.key = "Card_1";
            this.displayName = "普通攻击";
            this.displayNote = "造成1点伤害";
            value = 1;
            area = MMArea.Single;
        }
        else if (id == 2)
        {
            this.id = 2;
            this.key = "Card_2";
            this.displayName = "普通防御";
            this.displayNote = "获得1点防御";
            area = MMArea.Beside;
        }
        else if (id == 3)
        {
            this.id = 3;
            this.key = "Card_3";
            this.displayName = "普通移动";
            this.displayNote = "移动一格";
            area = MMArea.Behind;
        }
        else if (id == 100)
        {
            this.id = 100;
            this.key = "Card_100";
            this.displayName = "眩晕";
            this.displayNote = "进入眩晕状态，下回合进入狂暴状态";
            area = MMArea.None;
        }
        else if (id == 1000)
        {
            this.id = 1000;
            this.key = "Card_1000";
            this.displayName = "待命";
            this.displayNote = "恢复1点行动力";
            area = MMArea.None;
        }
        else if (id == 10000)
        {
            this.id = 10000;
            this.key = "Card_" + id;
            this.displayName = "攻击";
            this.displayNote = "对目标造成伤害";
            area = MMArea.Single;
        }
        else if (id == 10101)
        {
            this.id = id;
            this.key = "Card_" + id;
            this.displayName = "黑白判官";
            this.displayNote = "对目标造成2点伤害，恢复2点生命";
            area = MMArea.Single;
        }
        else if (id == 10201)
        {
            this.id = id;
            this.key = "Card_" + id;
            this.displayName = "临危不乱";
            this.displayNote = "对横排目标造成伤害";
            area = MMArea.Beside;
        }
        else if (id == 10301)
        {
            this.id = id;
            this.key = "Card_" + id;
            this.displayName = "恩赐劫";
            this.displayNote = "对目标以及身后单位造成伤害";
            area = MMArea.Behind;
        }
        else if (id == 10401)
        {
            this.id = id;
            this.key = "Card_" + id;
            this.displayName = "母辛后裔";
            this.displayNote = "召唤鹿小九";
            area = MMArea.None;
        }
        else if (id == 16101)
        {
            this.id = id;
            this.key = "Card_" + id;
            this.displayName = "丐帮弟子";
            this.displayNote = "近战攻击";
            area = MMArea.Single;
        }
        else
        {
            MMDebugManager.Log("LoadData: " + id);
        }
    }

    
}
