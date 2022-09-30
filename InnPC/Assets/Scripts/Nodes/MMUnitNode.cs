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

    public int attackRange;

    public List<MMSkillNode> cards;

    public MMUnitState unitState;

    public MMUnitPhase unitPhase;

    public MMNode iconRage;
    public MMNode iconWeak;

    public GameObject backgroundATK;
    public GameObject backgroundHP;
    public MMNode groupSP;
    public List<MMNode> iconsSP;


    public void Accept(MMUnit unit)
    {
        this.unit = unit;
        Reload();
    }


    public void Reload()
    {
        LoadImage("Units/" + unit.key);

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

        attackRange = unit.attackRange;

        cards = new List<MMSkillNode>();
        foreach (var id in unit.skills)
        {
            MMSkill card = MMSkill.Create(id);
            MMSkillNode node = MMSkillNode.Create();
            node.Accept(card);
            cards.Add(node);
        }
        
        EnterState(MMUnitState.Normal);

        UpdateUI();
    }


    public void Clear()
    {
        this.cell.Clear();
        this.gameObject.transform.SetParent(null);
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
            return;
        }
        this.ap += 1;

        if (this.ap == maxAP)
        {
            EnterState(MMUnitState.Rage);
        }

        UpdateUI();
    }

    public void DecreaseAP()
    {

        if (ap == 0)
        {
            EnterState(MMUnitState.Stunned);
            return;
        }

        this.ap -= 1;

        if (this.ap == 0)
        {
            EnterState(MMUnitState.Stunned);
        }

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

}
