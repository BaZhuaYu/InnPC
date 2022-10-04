using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class MMCard
{

    private MMCard() { }

    public string key;
    public int id;
    public string displayName;
    public string displayNote;
    public int cost;

    public int value;
    public MMArea area;
    public List<MMSkillKeyWord> keywords;

    public int tempATK;
    public int tempDEF;




    public static MMCard Create(int id)
    {
        return new MMCard();
    }

}
