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

    public List<MMCardNode> cards;

    public MMUnitState unitState;

    public MMNode iconRage;
    public MMNode iconWeak;


    public void Accept(MMUnit unit)
    {
        this.unit = unit;
        Reload();
    }


    public void Reload()
    {
        LoadImage("Units/" + unit.key);

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

        cards = new List<MMCardNode>();
        foreach (var id in unit.cards)
        {
            MMCard card = MMCard.Create(id);
            MMCardNode node = MMCardNode.Create();
            node.Accept(card);
            cards.Add(node);
        }
        
        EnterState(MMUnitState.Normal);

        UpdateUI();
    }


    public void Clear()
    {
        cards.Clear();
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
            return;
        }

        this.ap -= 1;

        if (this.ap == 0)
        {
            EnterState(MMUnitState.Weak);
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
        textAP.text = ap + "";
        textATK.text = atk + "";
    }



    public List<MMCell> FindMoveCells()
    {
        List<int> rows = new List<int>();
        MMUnitNode unit = MMBattleManager.instance.FindFrontUnit2();
        for(int i = this.cell.row; i < unit.cell.row; i++)
        {
            rows.Add(i);
        }
        return MMMap.instance.FindCellsInRows(rows);

        //return MMMap.instance.FindCellsWithinDistance(this.cell, this.spd);
    }

    public void ShowMoveCells()
    {
        List<MMCell> cells = FindMoveCells();
        foreach (var cell in cells)
        {
            cell.EnterState(MMNodeState.Blue);
        }
    }


    public void HideMoveCells()
    {
        List<MMCell> cells = FindMoveCells();
        foreach (var cell in cells)
        {
            cell.EnterState(MMNodeState.Normal);
        }
    }


    public List<MMCell> FindAttackCells()
    {
        return MMMap.instance.FindCellsInFrontDistance(this.cell, this.attackRange);
        
        //return MMMap.instance.FindCellsWithinDistance(this.cell, this.attackRange);
    }

    public void ShowAttackCells()
    {
        List<MMCell> cells = FindAttackCells();
        foreach (var cell in cells)
        {
            cell.EnterState(MMNodeState.Blue);
            if (cell.nodeUnit != null)
            {
                if (cell.nodeUnit.group != this.group)
                {
                    cell.EnterHighlight(MMNodeHighlight.Red);
                }
                //else
                //{
                //    cell.EnterHighlight(MMNodeHighlight.Green);
                //}
            }
        }
    }


    public void HideAttackCells()
    {
        List<MMCell> cells = FindAttackCells();
        foreach (var cell in cells)
        {
            cell.EnterState(MMNodeState.Normal);
            if (cell.nodeUnit != null)
            {
                cell.EnterHighlight(MMNodeHighlight.Normal);
            }
        }
    }



    public void MoveToCell(MMCell cell)
    {
        int dis = this.cell.FindDistanceFromCell(cell);
        this.spd = 0;
        cell.Accept(this);
    }



    public static MMUnitNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMNodeUnit") as GameObject);
        //obj.name = "MMNodeUnit";
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
