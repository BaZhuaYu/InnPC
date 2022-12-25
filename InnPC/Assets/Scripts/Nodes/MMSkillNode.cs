using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMSkillNode : MMNode
{
    /// <summary>
    /// Panel
    /// </summary>
    public MMUnitNode unit;

    public MMSkill skill;

    public MMNode icon;
    public Text textName;
    public Text textNote;
    public Text textCost;
    public Text textATK;
    public Text textDEF;


    /// <summary>
    /// Data
    /// </summary>
    public int id;
    public string key;

    public string displayName;
    public string displayNote;

    public MMSkillType type;
    public int cost;

    public MMArea area;
    public MMEffectTarget target;

    public MMTriggerTime time;

    public List<MMSkillKeyWord> keywords;
    
    public MMEffectType effectType;
    public int value;

    public int tempATK;
    public int tempDEF;

    public MMSkillState state;


    /// <summary>
    /// Battle
    /// </summary>
    public bool isReady;
    public bool isEnabled;


    private void Start()
    {
        textNote.gameObject.SetActive(false);
    }


    public void Accept(MMSkill skill)
    {
        if (this.skill != null)
        {
            Clear();
        }

        this.skill = skill;

        Reload();
    }


    public void Reload()
    {
        if (this.skill == null)
        {
            Clear();
            return;
        }

        this.id = skill.id;
        this.key = skill.key;
        this.displayName = skill.displayName;
        this.displayNote = skill.displayNote;

        this.type = MMUtility.DeserializeSkillType(skill.type);
        if (type == MMSkillType.Power)
        {
            isEnabled = false;
        }
        else
        {
            isEnabled = true;
        }
        this.cost = skill.cost;
        this.area = skill.area;
        
        this.effectType = skill.effect;
        this.target = skill.target;
        this.value = skill.value;

        this.tempATK = skill.tempATK;
        this.tempDEF = skill.tempDEF;

        this.keywords = skill.keywords;

        this.name = "Skill_" + id;

        this.time = skill.time;


        //ConfigReady();
        UpdateUI();

    }


    public void Clear()
    {
        this.skill = null;
        this.textName.text = "";
        this.textNote.text = "";
        this.name = "Skill_0";
        this.gameObject.transform.SetParent(null);
    }


    public void Destroy()
    {
        Destroy(gameObject);
    }


    private void UpdateUI()
    {
        this.textName.text = skill.displayName;
        this.textNote.text = skill.displayNote;
        if (this.type == MMSkillType.Passive)
        {
            this.textCost.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            this.textCost.transform.parent.gameObject.SetActive(true);
            this.textCost.text = skill.cost + "";
        }
        switch (this.type)
        {
            case MMSkillType.Attack:
                this.GetComponent<Image>().color = Color.red;
                textATK.gameObject.SetActive(true);
                textDEF.gameObject.SetActive(true);
                textATK.text = "" + tempATK;
                textDEF.text = "" + tempDEF;
                break;
            case MMSkillType.Spell:
                this.GetComponent<Image>().color = Color.blue;
                textATK.gameObject.SetActive(false);
                textDEF.gameObject.SetActive(false);
                break;
            case MMSkillType.Power:
                this.GetComponent<Image>().color = Color.yellow;
                textATK.gameObject.SetActive(true);
                textDEF.gameObject.SetActive(true);
                textATK.text = "" + tempATK;
                textDEF.text = "" + tempDEF;
                break;
            case MMSkillType.Passive:
                this.GetComponent<Image>().color = Color.gray;
                textATK.gameObject.SetActive(false);
                textDEF.gameObject.SetActive(false);
                break;
            default:
                this.GetComponent<Image>().color = Color.black;
                textATK.gameObject.SetActive(false);
                textDEF.gameObject.SetActive(false);
                break;
        }
        icon.LoadImage("Cards/" + key);
    }


    private void ConfigReady()
    {
        if (this.keywords.Contains(MMSkillKeyWord.Ultimate))
        {
            if (this.unit.state == MMUnitState.Rage)
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
        node.name = "MMSkillNode_" + skill.id;
        return node;
    }


    public static MMSkillNode Create(int id)
    {
        MMSkill skill = MMSkill.Create(id);
        return Create(skill);
    }


}
