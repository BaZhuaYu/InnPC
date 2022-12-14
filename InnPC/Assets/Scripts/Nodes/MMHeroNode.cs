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
                this.SetColor(MMUtility.FindColorRed());
                break;
            case 2:
                this.SetColor(MMUtility.FindColorYellow());
                break;
            case 3:
                this.SetColor(MMUtility.FindColorBlue());
                break;
            case 4:
                this.SetColor(MMUtility.FindColorGreen());
                break;
            default:
                this.SetColor(MMUtility.FindColorBlack());
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
        return obj.GetComponent<MMHeroNode>();
    }

    public static MMHeroNode CreateFromUnit(MMUnit unit)
    {
        MMHeroNode node = MMHeroNode.Create();
        node.Accept(unit);
        return node;
    }

    public static MMHeroNode CreateFromID(int id)
    {
        MMUnit unit = MMUnit.Create(id);
        return MMHeroNode.CreateFromUnit(unit);
    }




}
