using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MMNode
{

    public MMUnitNode FindRandomUnit1()
    {
        int index = Random.Range(0, units1.Count);
        return units1[index];
    }


    MMUnitNode FindMaxAPUnit2()
    {
        foreach (var unit in units2)
        {
            if (unit.ap == unit.maxAP && unit.isActived == false)
            {
                return unit;
            }
        }

        return null;
    }


    public void AutoUnitActing()
    {
        Debug.Log(sourceUnit.displayName + " AutoUnitActing");
        MMUnitNode dest = sourceUnit.FindTarget();
        sourceUnit.isActived = true;
        if (dest == null)
        {
            sourceUnit.DecreaseAP(sourceUnit.maxAP);
            MMExplorePanel.Instance.hp -= 10;
        }
        else
        {
            sourceUnit.ShowCard();
            StartCoroutine(aaa(sourceUnit));
            //sourceUnit.cards[0]
            TryEnterStateSelectingCard(MMCardNode.Create(sourceUnit.unit.cards[0]));
            //TryEnterStateSelectedTargetUnit(dest);
        }
    }


    IEnumerator aaa(MMUnitNode unit)
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("aaaaaabbbbbbbbbcccccccccc");
        unit.HideCard();
    }





    public MMUnitNode FindFrontUnitOfGroup(int group)
    {
        MMUnitNode ret;

        if (group == 1)
        {
            if (units1.Count == 0)
            {
                return null;
            }

            ret = units1[0];
            foreach (var unit in units1)
            {
                if (unit.cell.index > ret.cell.index)
                {
                    ret = unit;
                }
            }
        }
        else
        {
            if (units2.Count == 0)
            {
                return null;
            }

            ret = units2[0];
            foreach (var unit in units2)
            {
                if (unit.cell.index < ret.cell.index)
                {
                    ret = unit;
                }
            }
        }

        return ret;
    }




    public MMUnitNode FindMainTarget(MMUnitNode source, MMEffectTarget tar)
    {
        switch (tar)
        {
            case MMEffectTarget.Source:
                return source;

            case MMEffectTarget.Target:
                return source.FindTarget();

            case MMEffectTarget.MinHPTeam:
                return source.FindMinHP();

            case MMEffectTarget.MaxHPTeam:
                return source.FindMaxHP();

            case MMEffectTarget.MinHPEnemy:
                return source.FindMinHPEnemy();

            case MMEffectTarget.MaxHPEnemy:
                return source.FindMaxHPEnemy();
        }

        return source;
    }


    public List<MMUnitNode> FindSideTargets(MMUnitNode source, MMUnitNode target, MMArea area)
    {
        MMCell cell = target.cell;

        List<MMCell> cells = new List<MMCell>();
        switch (area)
        {
            case MMArea.Single:
                break;
            case MMArea.Row:
                cells = MMMap.Instance.FindCellsInRow(cell);
                break;
            case MMArea.Col:
                cells = MMMap.Instance.FindCellsInCol(cell);
                break;
            case MMArea.Beside:
                cells = MMMap.Instance.FindCellsBeside(cell);
                break;
            case MMArea.Behind:
                cells = MMMap.Instance.FindCellsBehind(cell);
                break;
            case MMArea.Target:
                cells.Add(source.FindTarget().cell);
                break;
            case MMArea.RaceUnits:
                cells = MMMap.Instance.FindCellsWithUnitRace(target.race);
                break;
            case MMArea.TeamUnits:
                cells = MMMap.Instance.FindCellsTeamCells(target.cell);
                break;
            case MMArea.Nine:
                cells = MMMap.Instance.FindCellsNine(target.cell);
                break;
            case MMArea.All:
                cells = MMMap.Instance.FindCellsAll();
                break;
        }

        List<MMUnitNode> ret = new List<MMUnitNode>();
        foreach (var c in cells)
        {
            if (c.unitNode == null)
            {
                continue;
            }

            ret.Add(c.unitNode);
        }

        return ret;
    }



    public void ShowAllUnitAttackCells()
    {
        foreach (var unit in units2)
        {
            if (unit.CheckRage())
            {
                bool flag = false;

                List<MMCell> cells = unit.FindAttackCells();
                foreach (var cell in cells)
                {
                    if (cell.unitNode != null)
                    {
                        if (cell.unitNode.group != unit.group)
                        {
                            flag = true;
                            //MMNode node = MMNode.Create("CellBorder_Red");
                            //node.name = "ShowAllUnitAttackCells";
                            //MMMap.Instance.AddChild(node);
                            //node.transform.position = cell.transform.position;
                            //node.LoadSize(cell.FindSize());
                            //Debug.Log(node.FindSize());
                            break;
                        }
                    }
                }

                if (flag)
                {

                }
                else
                {
                    foreach (var cell in cells)
                    {
                        MMNode node = MMNode.Create("UI/aaa/CellFill_Red");
                        node.name = "ShowAllUnitAttackCells";
                        MMMap.Instance.AddChild(node);
                        node.transform.position = cell.transform.position;
                        node.LoadSize(cell.FindSize());
                    }
                }
            }
        }
    }

    public void HideAllUnitAttackCells()
    {
        for (int i = 0; i < MMMap.Instance.gameObject.transform.childCount; i++)
        {
            if (MMMap.Instance.gameObject.transform.GetChild(i).name == "ShowAllUnitAttackCells")
            {
                Destroy(MMMap.Instance.gameObject.transform.GetChild(i).gameObject);
            }
        }
    }


    public void ShowUnitWillMove()
    {

    }




}
