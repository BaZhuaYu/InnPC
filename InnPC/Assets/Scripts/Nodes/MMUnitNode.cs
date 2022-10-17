using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class MMUnitNode : MMNode
{

    public MMUnit unit;

    public MMCell cell;
    public MMCell tempCell;

    public Text textHP;
    public Text textAP;
    public Text textATK;


    public MMUnitNode target;

    public int group;

    
    public string key;
    public int id;

    public string displayName;
    public string displayNote;

    public int maxHP;
    public int hp;
    public int maxAP;
    public int ap;

    public int atk;
    public int def;
    public int mag;
    public int spd;

    public int race;
    public int clss;

    public int attackRange;

    public List<MMSkillNode> skills;

    public MMUnitState unitState;

    public MMUnitPhase unitPhase;

    public  List<MMBuff> buffs;

    public int tempATK;
    public int tempDEF;

    public MMNode iconRage;
    public MMNode iconWeak;

    public GameObject backgroundATK;
    public GameObject backgroundHP;
    public MMNode groupSP;
    public List<MMNode> iconsSP;


    public List<MMSkillNode> skillHistory;


    public void Accept(MMUnit unit)
    {
        this.unit = unit;
        this.name = "MMUnitNode_" + unit.id;

        LoadImage("Units/" + unit.key + "QS");

        this.id = unit.id;
        this.key = unit.key;

        displayName = unit.displayName;
        displayNote = unit.displayNote;

        maxHP = unit.maxHP;
        hp = unit.hp;
        maxAP = unit.maxAP;
        ap = unit.ap;
        ap = maxAP;

        atk = unit.atk;
        def = unit.def;
        mag = unit.mag;
        spd = unit.spd;

        race = unit.race;
        clss = unit.clss;

        attackRange = unit.attackRange;

        skills = new List<MMSkillNode>();
        foreach (var id in unit.skills)
        {
            MMSkill card = MMSkill.Create(id);
            MMSkillNode node = MMSkillNode.Create();
            node.Accept(card);
            skills.Add(node);
        }

        buffs = new List<MMBuff>();
        skillHistory = new List<MMSkillNode>();

        EnterState(MMUnitState.Normal);

        Reload();
    }


    public void Reload()
    {
        UpdateUI();
    }


    public void Clear()
    {
        Debug.Log("Unit Clear: " + displayName + " Cell: " + this.cell.index);
        this.cell.Clear();
        
        GameObject aa = GameObject.Find("DeadUnit");
        this.gameObject.transform.SetParent(aa.transform);
        this.SetParent(aa.GetComponent<MMNode>());
    }





    public void IncreaseHP(int value)
    {
        this.hp += value;
        UpdateUI();
    }

    public void DecreaseHP(int value)
    {
        this.hp -= value;

        if(hp <= 0)
        {
            EnterState(MMUnitState.Dead);
        }

        UpdateUI();
    }

    public void IncreaseAP()
    {
        if (ap == maxAP)
        {
            EnterState(MMUnitState.Rage);
        }
        else if (ap == maxAP - 1)
        {
            EnterState(MMUnitState.Rage);
        }
        
        this.ap += 1;

        if (this.ap >= maxAP)
        {
            this.ap = this.maxAP;
            EnterState(MMUnitState.Rage);
        }

        UpdateUI();
    }

    public void DecreaseAP()
    {
        if (ap == 0)
        {
            EnterState(MMUnitState.Stunned);
        }
        else if (ap == 1)
        {
            EnterState(MMUnitState.Weak);
        }

        this.ap -= 1;

        if (this.ap <= 0)
        {
            this.ap = 0;
            EnterState(MMUnitState.Stunned);
        }

        UpdateUI();
    }

    public void IncreaseATK(int value)
    {
        this.atk += value;
        UpdateUI();
    }

    public void DecreaseATK(int value)
    {
        this.atk -= value;
        UpdateUI();
    }

    public void IncreaseDEF(int value)
    {
        this.def += value;
        UpdateUI();
    }

    public void DecreaseDEF(int value)
    {
        this.def -= value;
        UpdateUI();
    }
    
    public void IncreaseSPD(int value)
    {
        this.spd += value;
        UpdateUI();
    }

    public void DecreaseSPD(int value)
    {
        this.spd -= value;
        UpdateUI();
    }





    public void UpdateUI()
    {
        textHP.text = hp + "";
        textATK.text = atk + "";

        //textAP.text = ap + "";

        if(maxAP == 0)
        {
            groupSP.SetActive(false);
        }
        else
        {
            groupSP.SetActive(true);
            if(maxAP == ap)
            {
                groupSP.LoadImage("UI/IconRage");
            }
            else
            {
                groupSP.LoadImage("UI/IconWeak");
            }
        }


        if(this.unitState == MMUnitState.Stunned)
        {
            backgroundATK.SetActive(false);
        }
        else
        {
            backgroundATK.SetActive(true);
        }


        for(int i = 0; i < 5;i++)
        {
            if(i < ap)
            {
                iconsSP[i].SetActive(true);
            }
            else
            {
                iconsSP[i].SetActive(false);
            }
        }
    }




    public static MMUnitNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMUnitNode") as GameObject);
        return obj.GetComponent<MMUnitNode>();
    }

    public static MMUnitNode CreateFromUnit(MMUnit unit)
    {
        MMUnitNode node = MMUnitNode.Create();
        node.Accept(unit);
        return node;
    }

    public static MMUnitNode CreateFromID(int id)
    {
        MMUnit unit = MMUnit.Create(id);
        return MMUnitNode.CreateFromUnit(unit);
    }




    public bool CheckHasAP()
    {
        return this.ap > 0;
    }

    public bool CheckNoAP()
    {
        return this.ap <= 0;
    }

    public void IncreaspAPToMax()
    {
        for (int i = 0; i < maxAP; i++)
        {
            IncreaseAP();
        }
    }


    public void PlaySkill(MMSkillNode skill)
    {
        this.skillHistory.Add(skill);

        List<MMSkillNode> aa = skillHistory.FindAll( a => a.effectType == MMEffectType.Attack);

        if(skill.time == MMTriggerTime.NormalAttackNum2)
        {
            if((aa.Count % 2) == 0)
            {
                MMEffect effect = skill.Create(null, null);

            }
        }
    }


    public List<MMEffect> CreateEffect(MMTriggerTime time)
    {
        List<MMEffect> ret = new List<MMEffect>();

        foreach (var skill in skills)
        {
            if(skill.time == time)
            {
                MMEffect effect = skill.Create(this, null);
                ret.Add(effect);
            }
        }

        return ret;
    }














    public bool HasSkill(int id)
    {
        return true;
    }


}
