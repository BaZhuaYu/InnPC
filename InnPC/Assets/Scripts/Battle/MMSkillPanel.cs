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


    public List<MMSkillNode> skills;

    public MMSkillNode selectingSkill;


    private void Start()
    {
        skills = new List<MMSkillNode>();
    }


    public void Accept(List<MMSkillNode> skills)
    {
        Clear();
        this.skills = skills;
        
        foreach(var s in skills)
        {
            Debug.Log("Accept: " + s);
        }

        Reload();
    }


    public void Reload()
    {
        UpdateUI();
    }


    public void Clear()
    {
        foreach(var skill in skills)
        {
            skill.RemoveFromParent();
        }
        skills = new List<MMSkillNode>();
        selectingSkill = null;
        Reload();
    }


    public void UpdateUI()
    {
        float offset = 0;
        foreach (var skill in skills)
        {
            skill.SetParent(this);
            skill.MoveToCenterY();
            skill.MoveToParentLeftOffset(offset);
            offset += 10 + skill.FindWidth();

            if (this.selectingSkill != null)
            {
                if (this.selectingSkill == skill)
                {
                    skill.MoveUp(20);
                }
            }
        }
    }




    public void SetSelectedSkill(MMSkillNode skill)
    {
        this.selectingSkill = skill;
        Reload();
    }


    public void PlaySkill(MMSkillNode skill)
    {
        this.selectingSkill = null;
        Reload();
    }
    

}
