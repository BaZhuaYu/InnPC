using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class MMUnitNode : MMNode
{

    public MMUnit unit;

    public MMNode avatar;

    public MMCell cell;
    public MMCell tempCell;

    public Text textHP;
    public Text textAP;
    public Text textATK;
    public MMNode border;
    public MMNode iconRage;

    [HideInInspector]
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
    public List<MMCardNode> cards;

    public MMUnitState state;
    
    public  List<MMBuff> buffs;

    public int tempATK;
    public int tempDEF;

    
    public GameObject backgroundATK;
    public GameObject backgroundHP;
    public MMNode groupSP;
    public List<MMNode> iconsSP;


    public Animator m_DamageAnimator;
    public Text m_DamageText;


    /// <summary>
    /// Battle Props
    /// </summary>
    public bool isActived;
    public bool isMoved = false;


    void Start()
    {
        //m_DamageAnimator = transform.Find("Damage").GetComponent<Animator>();
        //m_DamageText = transform.Find("Damage/Text").GetComponent<Text>();
        //m_DamageAnimator.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if(this.id == 10200)
        //{
        //    Debug.Log(this.tempATK);
        //}
    }


    public void Accept(MMUnit unit)
    {
        this.unit = unit;
        this.name = "MMUnitNode_" + unit.id;

        avatar.LoadImage("Units/" + unit.key + "QS");

        this.id = unit.id;
        this.key = unit.key;

        displayName = unit.displayName;
        displayNote = unit.displayNote;

        maxHP = unit.maxHP;
        hp = unit.hp;
        maxAP = unit.maxAP;
        ap = unit.ap;

        atk = unit.atk;
        def = unit.def;
        mag = unit.mag;
        spd = unit.spd;

        race = unit.race;
        clss = unit.clss;

        attackRange = unit.attackRange;


        skills = new List<MMSkillNode>();
        //foreach(var s in unit.skills)
        //{
        //    MMSkill skill = MMSkill.Create(s);
        //    MMSkillNode nodeSkill = MMSkillNode.Create();
        //    nodeSkill.Accept(skill);
        //    nodeSkill.name = this.displayName + "_" + "skill_" + skill.id;
        //    skills.Add(nodeSkill);
        //    nodeSkill.unit = this;
        //    nodeSkill.SetParent(this);
        //}
        
        
        cards = new List<MMCardNode>();
        //foreach (var c in unit.cards)
        //{
        //    MMCard card = MMCard.Create(c);
        //    MMCardNode nodeCard = MMCardNode.Create();
        //    nodeCard.Accept(card);
        //    nodeCard.name = this.displayName + "_" + "card_" + card.id;
        //    cards.Add(nodeCard);
        //    nodeCard.unit = this;
        //    nodeCard.SetParent(this);
        //}
            
        
        buffs = new List<MMBuff>();


        EnterState(MMUnitState.Normal);
        HandleHighlight(MMNodeHighlight.Normal);

        m_DamageAnimator.gameObject.SetActive(false);

        Reload();
    }


    public void Reload()
    {
        UpdateUI();
    }


    public void Clear()
    {
        Destroy(gameObject);
    }

    

    public void IncreaseHP(int value)
    {
        this.hp += value;
        UpdateUI();
    }

    public void DecreaseHP(int value)
    {
        Debug.Log(this.displayName + " DecreaseHP: " + value);
        this.hp -= value;
        PlayAnimationHurt(value);

        if(hp <= 0)
        {
            if(HasBuff(MMBuff.BuQu))
            {
                hp = 1;
                RemoveBuff(MMBuff.BuQu);
            }
            else
            {
                EnterState(MMUnitState.Dead);
            }
        }

        UpdateUI();
    }

    public void IncreaseAP(int value, bool animated = false)
    {
        if(this.id == 10200)
        {
            Debug.LogWarning("IncreaseAP xxxxxxxxxxxxxxxxxxxx");
        }

        this.ap += value;

        if(animated)
        {
            MMAPNode node = MMAPNode.Create();
            node.ShowIncrease();
            node.SetParent(this);
            node.MoveUp(50);
        }
        

        if (this.ap >= maxAP)
        {
            this.ap = this.maxAP;
            EnterState(MMUnitState.Rage);
        }

        UpdateUI();
    }

    public void DecreaseAP(int value, bool animated = false)
    {
        this.ap -= value;

        if(animated)
        {
            MMAPNode node = MMAPNode.Create();
            node.ShowDecrease();
            node.SetParent(this);
            node.MoveUp(50);
        }
        
        if (this.ap <= 0)
        {
            this.ap = 0;
        }

        UpdateUI();
    }

    public void DecreaseAPAll()
    {
        this.ap = 0;
        EnterState(MMUnitState.Weak);
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

    public void IncreaseTempATK(int value)
    {
        this.tempATK += value;
        this.atk += value;
        UpdateUI();
    }

    public void DecreaseTempATK(int value)
    {
        this.tempATK -= value;
        this.atk -= value;
        UpdateUI();
        Debug.Log(this.displayName + " " + this.atk);
    }



    public void UpdateUI()
    {
        textHP.text = hp + "";
        textATK.text = atk + "";


        if (maxAP == 0)
        {
            iconRage.SetActive(false);
        }
        else
        {
            if (maxAP == ap)
            {
                iconRage.SetActive(true);
            }
            else
            {
                iconRage.SetActive(false);
            }
        }


        for (int i = 0; i < 5;i++)
        {
            if(i < ap)
            {
                iconsSP[i].SetActive(true);
                iconsSP[i].LoadImage("UI/aaa/Icon_AP");
                if (clss == 1)
                {
                    //c = MMUtility.FindColorRed();
                    LoadImage("UI/aaa/IconCircle_Red");
                }
                else if (clss == 2)
                {
                    //c = MMUtility.FindColorYellow();
                    LoadImage("UI/aaa/IconCircle_Yellow");
                }
                else if (clss == 3)
                {
                    //c = MMUtility.FindColorBlue();
                    LoadImage("UI/aaa/IconCircle_Blue");
                }
                else if (clss == 4)
                {
                    //c = MMUtility.FindColorGreen();
                    LoadImage("UI/aaa/IconCircle_Green");
                }
                else
                {
                    //c = MMUtility.FindColorBlack();
                    LoadImage("");
                }
                
            }
            else if (i < maxAP)
            {
                iconsSP[i].SetActive(true);
                iconsSP[i].LoadImage("UI/aaa/Icon_AP_Gray");
            }
            else
            {
                iconsSP[i].SetActive(false);
            }
            
        }
        
    }


    public void MoveToCell(MMCell cell)
    {
        int dis = this.cell.FindDistanceFromCell(cell);
        this.spd = 0;
        cell.Accept(this);
    }


    public void HandleHighlight(MMNodeHighlight state)
    {
        this.nodeHighlight = state;
        this.border.gameObject.SetActive(true);
        switch(state)
        {
            case MMNodeHighlight.Normal:
                //this.border.LoadImage("");
                this.border.gameObject.SetActive(false);
                break;

            case MMNodeHighlight.Red:
                this.border.LoadImage("UI/aaa/CellBorder_Red");
                break;

            case MMNodeHighlight.Yellow:
                this.border.LoadImage("UI/aaa/CellBorder_Yellow");
                break;

            case MMNodeHighlight.Blue:
                this.border.LoadImage("UI/aaa/CellBorder_Blue");
                break;

            case MMNodeHighlight.Green:
                this.border.LoadImage("UI/aaa/CellBorder_Green");
                break;

        }
    }




    public static MMUnitNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMUnitNode") as GameObject);
        return obj.GetComponent<MMUnitNode>();
    }

    public static MMUnitNode Create(MMUnit unit)
    {
        MMUnitNode node = MMUnitNode.Create();
        node.Accept(unit);
        return node;
    }

    public static MMUnitNode Create(int id)
    {
        MMUnit unit = MMUnit.Create(id);
        return MMUnitNode.Create(unit);
    }

    

    public bool CheckHasAP()
    {
        return this.ap > 0;
    }

    public bool CheckNoAP()
    {
        return this.ap <= 0;
    }
    

    
    public List<MMSkillNode> FindAllHistorySkills()
    {
        List<MMSkillNode> ret = new List<MMSkillNode>();
        foreach(var (round, skills) in MMBattleManager.Instance.historySkills)
        {
            foreach(var skill in skills)
            {
                ret.Add(skill);
            }
        }
        return ret;
    }
    

    public List<MMEffect> CreateEffect(MMTriggerTime time)
    {
        List<MMEffect> ret = new List<MMEffect>();

        foreach (var skill in skills)
        {
            if(skill.time == time)
            {
                MMEffect effect = skill.CreateEffect();
                ret.Add(effect);
            }
        }

        return ret;
    }

    
    public bool HasSkillEnabled(int id)
    {
        foreach(var skill in skills)
        {
            if(skill.id == id)
            {
                return true;
            }
        }
        return false;
    }


    public bool HasSkillEffect(int id)
    {
        foreach (var skill in skills)
        {
            if (skill.effectType == (MMEffectType)(id))
            {
                return true;
            }
        }
        return false;
    }


    public bool HasBuff(MMBuff buff)
    {
        foreach(var b in buffs)
        {
            if(b == buff)
            {
                return true;
            }
        }

        return false;
    }


    public void AddBuff(MMBuff b)
    {
        if(HasBuff(b))
        {
            return;
        }
        this.buffs.Add(b);
    }


    public void RemoveBuff(MMBuff b)
    {
        this.buffs.Remove(b);
    }


    public bool CheckRage()
    {
        if(this.ap == this.maxAP && this.isActived == false)
        {
            return true;
        }

        return false;
    }
}
