using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMCard
{
    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;

    public void LoadData()
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
    }

    
}
