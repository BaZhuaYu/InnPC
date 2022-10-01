using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class MMSkill
{
    public string key;
    public int id;
    public string displayName;
    public string displayNote;
    public string icon;


    public int value;
    public MMArea area;
    public List<MMSkillKeyWord> keywords;

    public int tempATK;
    public int tempDEF;

    

    public static MMSkill Create(int id)
    {
        MMSkill ret = new MMSkill();
        ret.keywords = new List<MMSkillKeyWord>();
        ret.LoadData(id);
        return ret;
    }

    
    public void ExecuteEffect()
    {

    }


}
