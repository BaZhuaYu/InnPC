using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMSkillNode : MMNode
{

    public MMUnitNode unit;

    public MMSkill skill;

    public MMNode icon;
    public Text textName;
    public Text textNote;


    public int id;
    public string key;

    public string displayName;
    public string displayNote;

    public MMArea area;

    public MMTriggerTime time;

    public List<MMSkillKeyWord> keywords;

    public MMEffectType effectType;
    public int value;

    public int tempATK;
    public int tempDEF;

    public MMSkillState state;


    public bool isReady;


    private void Start()
    {
        keywords = new List<MMSkillKeyWord>();
    }


    public void Accept(MMSkill skill)
    {

        if(this.skill != null)
        {
            Clear();
        }

        this.skill = skill;
        
        Reload();
    } 


    public void Reload()
    {
        if(this.skill == null)
        {
            Clear();
            return;
        }

        this.id = skill.id;
        this.key = skill.key;
        this.displayName = skill.displayName;
        this.displayNote = skill.displayNote;
        this.area = skill.area;

        this.effectType = skill.effect;
        this.value = skill.value;

        this.tempATK = skill.tempATK;
        this.tempDEF = skill.tempDEF;

        this.keywords = skill.keywords;

        this.name = "Card_" + id;

        this.time = skill.time;

        //ConfigReady();
        UpdateUI();

    }


    public void Clear()
    {
        this.skill = null;
        this.textName.text = "";
        this.textNote.text = "";
        this.name = "Card_0";
        this.gameObject.transform.SetParent(null);
    }


    private void UpdateUI()
    {
        this.textName.text = skill.displayName;
        this.textNote.text = skill.displayNote;
        icon.LoadImage("Cards/" + key);
    }


    private void ConfigReady()
    {
        if(this.keywords.Contains(MMSkillKeyWord.Ultimate))
        {
            if(this.unit.unitState == MMUnitState.Rage)
            {
                isReady = true;
            }
            else
            {
                isReady = false;
            }
        }
        else
        {
            isReady = true;
        }
    }


    public void EnterState(MMSkillState s)
    {
        this.state = s;

    }









    public static MMSkillNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMSkillNode") as GameObject);
        obj.name = "MMSkillNode";
        return obj.GetComponent<MMSkillNode>();
    }


    public static MMSkillNode Create(MMSkill skill)
    {
        MMSkillNode node = MMSkillNode.Create();
        node.Accept(skill);
        return node;
    }


    public static MMSkillNode Create(int id)
    {
        MMSkill skill = MMSkill.Create(id);
        return Create(skill);
    }


}
