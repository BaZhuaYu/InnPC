using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class MMNodeUnit : MMNode
{

    public MMUnit unit;

    public MMCell cell;

    public Text textHP;
    public Text textAP;
    

    public MMNodeUnit target;

    public int group;

    public string key;
    public int id;

    private int maxHP;
    private int hp;
    private int maxAP;
    private int ap;

    private int atk;
    private int def;
    private int mag;
    private int spd;


    public void Accept(MMUnit unit)
    {
        this.unit = unit;
        Reload();
    }


    public void Reload()
    {
        LoadImage("Units/" + unit.key);

        maxHP = unit.maxHP;
        hp = unit.hp;
        maxAP = unit.maxAP;
        ap = unit.ap;

        atk = unit.atk;
        def = unit.def;
        mag = unit.mag;
        spd = unit.spd;

        UpdateUI();
    }

    public void Clear()
    {

    }


    public void IncreaseHP(int value)
    {
        this.hp += value;
        UpdateUI();
    }

    public void DecreaseHP(int value)
    {
        this.hp -= value;
        UpdateUI();
    }

    public void IncreaseAP(int value)
    {
        this.ap += value;
        UpdateUI();
    }

    public void DecreaseAP(int value)
    {
        this.ap -= value;
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
        textHP.text = hp + "/" + maxHP;
        textAP.text = ap + "";
    }




















    public static MMNodeUnit Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMNodeUnit") as GameObject);
        obj.name = "MMNodeUnit";
        return obj.GetComponent<MMNodeUnit>();
    }

}
