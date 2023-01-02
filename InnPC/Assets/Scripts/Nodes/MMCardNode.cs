using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMCardNode : MMNode
{

    public MMCard card;
    /// <summary>
    /// Panel
    /// </summary>
    public MMUnitNode unit;

    public MMNode topBar;
    public MMNode botBar;

    public MMNode icon;
    public Text textName;
    public Text textNote;
    public Text textCost;
    public Text textATK;
    public Text textDEF;
    public GameObject bgATK;
    public GameObject bgDEF;


    /// <summary>
    /// Data
    /// </summary>
    public int id;
    public string key;

    public string displayName;
    public string displayNote;

    public int cost;
    public int clss;
    public MMSkillType type;
    public int sortingOrder;

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


    public void Accept(MMCard skill)
    {
        if (this.card != null)
        {
            Clear();
        }

        this.card = skill;

        Reload();
    }


    public void Reload()
    {
        if (this.card == null)
        {
            Clear();
            return;
        }

        this.id = card.id;
        this.key = card.key;
        this.displayName = card.displayName;
        this.displayNote = card.displayNote;

        this.clss = card.clss;
        this.sortingOrder = this.clss;
        this.type = MMUtility.DeserializeSkillType(card.type);
        if (type == MMSkillType.Power)
        {
            isEnabled = false;
        }
        else
        {
            isEnabled = true;
        }
        this.cost = card.cost;
        this.area = card.area;

        this.effectType = card.effect;
        this.target = card.target;
        this.value = card.value;

        this.tempATK = card.tempATK;
        this.tempDEF = card.tempDEF;

        this.keywords = card.keywords;

        this.name = "Card_" + id;

        this.time = card.time;


        UpdateUI();

    }


    public void Clear()
    {
        this.card = null;
        this.textName.text = "";
        this.textNote.text = "";
        this.name = "Card_0";
        this.gameObject.transform.SetParent(null);
    }

    public void DestroySelf()
    {
        StopAnimation();
        Destroy(gameObject);
    }


    private void UpdateUI()
    {
        this.textName.text = card.displayName;
        this.textNote.text = card.displayNote;
        if (this.type == MMSkillType.Passive)
        {
            this.textCost.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            this.textCost.transform.parent.gameObject.SetActive(true);
            this.textCost.text = card.cost + "";
        }
        switch (this.type)
        {
            case MMSkillType.Attack:
                bgATK.gameObject.SetActive(true);
                bgDEF.gameObject.SetActive(true);
                textATK.text = "" + tempATK;
                textDEF.text = "" + tempDEF;
                break;
            case MMSkillType.Spell:
                bgATK.gameObject.SetActive(false);
                bgDEF.gameObject.SetActive(false);
                break;
            case MMSkillType.Power:
                bgATK.gameObject.SetActive(true);
                bgDEF.gameObject.SetActive(true);
                textATK.text = "" + tempATK;
                textDEF.text = "" + tempDEF;
                break;
            case MMSkillType.Passive:
                bgATK.gameObject.SetActive(false);
                bgDEF.gameObject.SetActive(false);
                break;
            default:
                bgATK.gameObject.SetActive(false);
                bgDEF.gameObject.SetActive(false);
                break;
        }

        Color c = Color.black;
        switch (this.clss)
        {
            case 1:
                c = MMUtility.FindColorRed();
                break;
            case 2:
                c = MMUtility.FindColorYellow();
                break;
            case 3:
                c = MMUtility.FindColorBlue();
                break;
            case 4:
                c = MMUtility.FindColorGreen();
                break;
            default:
                c = MMUtility.FindColorBlack();
                break;

        }
        
        this.GetComponent<Image>().color = c;
        topBar.SetColor(c);
        //botBar.SetColor(c);

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










    public static MMCardNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMCardNode") as GameObject);
        obj.name = "MMCardNode";
        return obj.GetComponent<MMCardNode>();
    }

    public static MMCardNode Create(MMCard card)
    {
        MMCardNode node = MMCardNode.Create();
        node.Accept(card);
        node.name = "MMCardNode" + card.id;
        return node;
    }

    public static MMCardNode Create(int id)
    {
        MMCard card = MMCard.Create(id);
        return MMCardNode.Create(card);
    }

    
}
