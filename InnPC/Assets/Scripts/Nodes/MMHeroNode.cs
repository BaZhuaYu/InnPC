using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMHeroNode : MMNode
{
    public MMNode avatar;
    public Text textName;
    public Text textATK;
    public Text textHP;
    public MMSkillNode nodeSkill;


    //
    public MMUnit unit;
    public int clss;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Accept(MMUnit unit)
    {
        this.unit = unit;
        this.clss = unit.clss;

        MMSkill s = MMSkill.Create(unit.skills[0]);
        this.nodeSkill.Accept(s);
        AddChild(nodeSkill);
        nodeSkill.MoveLeft(FindWidth() * 0.25f);
        nodeSkill.MoveDown(FindHeight() * 0.2f);

        Reload();
    }

    public void Reload()
    {
        textName.text = unit.displayName;
        textATK.text = unit.atk + "";
        textHP.text = unit.hp + "";
        avatar.LoadImage("Heroes/Hero_" + this.unit.id);

        switch (clss)
        {
            case 1:
                this.LoadColor(MMUtility.FindColorRed());
                break;
            case 2:
                this.LoadColor(MMUtility.FindColorYellow());
                break;
            case 3:
                this.LoadColor(MMUtility.FindColorBlue());
                break;
            case 4:
                this.LoadColor(MMUtility.FindColorGreen());
                break;
            default:
                this.LoadColor(MMUtility.FindColorBlack());
                break;
        }

    }

    public void Clear()
    {
        Destroy(gameObject);
    }



    public static MMHeroNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMHeroNode") as GameObject);
        MMHeroNode ret = obj.GetComponent<MMHeroNode>();
        ret.AddChild(ret.nodeSkill);
        ret.nodeSkill.MoveLeft(ret.FindWidth() * 0.25f);
        ret.nodeSkill.MoveDown(ret.FindHeight() * 0.2f);
        return ret;
    }

    public static MMHeroNode Create(MMUnit unit)
    {
        MMHeroNode node = MMHeroNode.Create();
        node.Accept(unit);
        return node;
    }

    public static MMHeroNode Create(int id)
    {
        MMUnit unit = MMUnit.Create(id);
        return MMHeroNode.Create(unit);
    }




}
