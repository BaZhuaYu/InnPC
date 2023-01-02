using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMSkillPanel : MMNode
{

    public static MMSkillPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    public MMNode avatar;

    public GameObject skillBorder;

    public List<MMSkillNode> skills;
    


    private void Start()
    {
        skills = new List<MMSkillNode>();
    }


    public void Accept(MMUnitNode unit)
    {
        avatar.LoadImage("Units/" + unit.key + "A");
        this.skills = unit.skills;

        Reload();
    }

    
    public void Reload()
    {
        UpdateUI();
    }


    public void Clear()
    {
        this.avatar.LoadImage("");
        foreach (var skill in skills)
        {
            skill.Clear();
        }
        skills = new List<MMSkillNode>();
        Reload();
    }


    public void UpdateUI()
    {
        float offset = 0;
        foreach (var skill in skills)
        {
            skill.SetParent(this);
            skill.MoveToCenterY();
            skill.MoveDown(this.FindHeight() * 0.25f);
            skill.MoveToParentLeftOffset(offset);
            offset += 10 + skill.FindWidth();

            skill.transform.position = skillBorder.transform.position;
            //if (this.selectingSkill != null)
            //{
            //    if (this.selectingSkill == skill)
            //    {
            //        skill.MoveUp(20);
            //    }
            //}
        }
    }

    
}
