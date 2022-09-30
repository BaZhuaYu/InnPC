using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMSkillNode : MMNode
{
    
    public MMSkill skill;

    public MMNode icon;
    public Text textName;
    public Text textNote;


    public int id;
    public string key;

    public MMArea area;

    public List<MMSkillKeyWord> keywords;

    public int tempATK;
    public int tempDEF;



    public void Accept(MMSkill skill)
    {
        this.skill = skill;

        this.id = skill.id;
        this.key = skill.key;
        this.area = skill.area;

        this.tempATK = skill.tempATK;
        this.tempDEF = skill.tempDEF;

        this.keywords = skill.keywords;

        this.name = "Card_" + id;
        
        Reload();
    } 


    public void Reload()
    {
        if(this.skill == null)
        {
            Clear();
            return;
        }


        this.textName.text = skill.displayName;
        this.textNote.text = skill.displayNote;
        icon.LoadImage("Cards/" + key);
    }


    public void Clear()
    {
        this.skill = null;
        this.textName.text = "";
        this.textNote.text = "";
        this.name = "Card_0";
    }
    

    public static MMSkillNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMSkillNode") as GameObject);
        obj.name = "MMSkillNode";
        return obj.GetComponent<MMSkillNode>();
    }

    
}
