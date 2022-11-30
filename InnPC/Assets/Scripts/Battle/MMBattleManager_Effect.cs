﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class MMBattleManager : MonoBehaviour
{

    public List<MMUnitNode> FindAllUnits()
    {
        List<MMUnitNode> ret = new List<MMUnitNode>();

        foreach (var unit in units1)
        {
            ret.Add(unit);
        }

        foreach (var unit in units2)
        {
            ret.Add(unit);
        }

        return ret;
    }


    public void BroadCast(MMTriggerTime time)
    {
        MMDebugManager.Warning("BroadCast: " + time);

        List<MMUnitNode> units = null;

        //if (this.phase == MMBattlePhase.RoundBegin || this.phase == MMBattlePhase.RoundEnd)
        //{
        //    units = units1;
        //}
        //else
        //{
        //    units = units2;
        //}
        if (isPlayerRound == 1)
        {
            units = units1;
        }
        else
        {
            units = units2;
        }


        foreach (var unit in units)
        {
            foreach (var skill in unit.skills)
            {
                if (skill.time == time && skill.isEnabled)
                {
                    MMTipManager.instance.CreateSkillTip(skill);
                    MMEffect effect = skill.CreateEffect();
                    ExecuteEffect(effect);
                }
            }
        }

    }


    public void BroadCastOnDead(MMUnitNode unit)
    {
        List<MMEffect> effects = unit.CreateEffect(MMTriggerTime.OnDead);

        foreach (var effect in effects)
        {
            switch (effect.type)
            {
                case MMEffectType.Summon:
                    effect.target = MMUnitNode.CreateFromID(effect.value);
                    break;
                default:
                    break;
            }
            ExecuteEffect(effect);
        }
    }


    public void BroadCastUnitSkill(MMTriggerTime time, MMUnitNode unit)
    {
        foreach (var skill in unit.skills)
        {
            if (skill.time == time)
            {
                MMEffect effect = skill.CreateEffect();

                switch (effect.type)
                {
                    case MMEffectType.Summon:
                        effect.destCell = unit.cell;
                        break;
                    default:
                        break;
                }

                ExecuteEffect(effect);
            }
        }

    }


    public void ExecuteEffectOnGain(MMEffect effect)
    {
        switch (effect.type)
        {
            case MMEffectType.InATK:
                effect.target.unit.atk += 1;
                break;
            case MMEffectType.InHP:
                effect.target.unit.maxHP += 1;
                effect.target.unit.hp += 1;
                break;
            case MMEffectType.InAP:
                effect.target.unit.ap += 1;
                break;
            default:
                break;
        }
    }


    public void ExecuteEffectOnSummon(MMEffect effect)
    {

    }

    public void ExecuteEffectOnDead(MMEffect effect)
    {

    }



    public void ExecuteEffect(MMEffect effect)
    {

        if (effect.target == null)
        {
            MMDebugManager.Warning("Source:" + effect.source.displayName +
                                    " Target: null" +
                                 " Effect: " + effect.type);
            MMTipManager.instance.CreateTip("没有有效目标");
            return;
        }

        MMDebugManager.Warning("Source:" + effect.source.displayName + " Cell: " + effect.source.unit.id +
                               " Target: " + effect.target.displayName +
                               " Effect: " + effect.type);

        switch (effect.type)
        {
            case MMEffectType.Attack:
                Attack(effect);
                break;
            case MMEffectType.InHP:
                this.InHP(effect);
                break;
            case MMEffectType.DeHP:
                this.DeHP(effect);
                break;

            case MMEffectType.InAP:
                this.InAP(effect);
                break;
            case MMEffectType.DeAP:
                this.DeAP(effect);
                break;

            case MMEffectType.InATK:
                this.InATK(effect);
                break;
            case MMEffectType.DeATK:
                this.DeATK(effect);
                break;

            case MMEffectType.Damage:
                Damage(effect);
                break;

            case MMEffectType.Summon:
                Summon(effect);
                break;

            case MMEffectType.TempATKDEF:
                InATK(effect);
                InHP(effect);
                break;

            case (MMEffectType)10101:
                Damage(effect);
                effect.source.IncreaseHP(effect.value);
                break;

            case (MMEffectType)1067:
                effect.value = effect.source.maxHP - effect.source.hp;
                Damage(effect);
                break;

            case (MMEffectType)1068:
                effect.source.IncreaseATK(1);
                effect.source.DecreaseHP(1);
                break;

            case (MMEffectType)1071:

                break;

            case (MMEffectType)1074:
                Debug.Log("1074");
                Debug.Log(effect.target.hp);
                Attack(effect);
                break;

            case (MMEffectType)1078:
                foreach (var unit in units1)
                {
                    if (unit == effect.source)
                    {

                    }
                    else
                    {
                        unit.IncreaseAP(1);
                    }
                }
                break;

            case (MMEffectType)1107:
                effect.target.DecreaseHP(1);
                effect.target.IncreaseATK(1);
                break;

            case (MMEffectType)1201:
                effect.target.IncreaseATK(1);
                AddHand(MMCardNode.Create(1200));
                break;

            case (MMEffectType)1203:
                effect.target.IncreaseHP(1);
                AddHand(MMCardNode.Create(1200));
                break;

            case (MMEffectType)1207:
                effect.target.IncreaseTempATK(1);
                break;

            //case (MMEffectType)1203:
            //    effect.target.IncreaseHP(1);
            //    AddHand(MMCardNode.Create(1200));
            //    break;


            default:
                MMDebugManager.Log("Not Find" + effect.type);
                break;
        }


        switch (effect.type)
        {
            case (MMEffectType)1074:
                Debug.Log(effect.target.hp);
                if (effect.target.state == MMUnitState.Dead)
                {
                    effect.source.IncreaseAP(1);
                    Debug.Log("!!!!!!!!!!!!!!!selectingSkill: " + selectingSkill);
                }
                else
                {
                    Debug.Log("&&&&&&&&&&&&&&&selectingSkill: " + selectingSkill);
                }
                break;
        }

    }



    private void Attack(MMEffect effect)
    {
        int tempATK = effect.userinfo["TempATK"];
        int tempDEF = effect.userinfo["TempDEF"];
        MMUnitNode source = effect.source;
        MMUnitNode target = effect.target;

        BroadCastUnitSkill(MMTriggerTime.BeforeAttack, source);
        BroadCastUnitSkill(MMTriggerTime.BeforeBeAttack, target);


        target.DecreaseHP(source.atk + tempATK);
        //if (source.hengsao > 0)
        //{
        //    List<MMCell> cells = MMMap.Instance.FindCellsBeside(target.cell);
        //    foreach (var cell in cells)
        //    {
        //        if (cell.unitNode != null)
        //        {
        //            cell.unitNode.DecreaseHP(source.hengsao);
        //        }
        //    }
        //}

        if (effect.area == MMArea.Beside)
        {
            List<MMCell> cells = MMMap.Instance.FindCellsBeside(target.cell);
            foreach (var cell in cells)
            {
                if (cell.unitNode != null)
                {
                    cell.unitNode.DecreaseHP(source.atk);
                }
            }
        }

        if (effect.area == MMArea.Behind)
        {
            List<MMCell> cells = MMMap.Instance.FindCellsBehind(target.cell);
            foreach (var cell in cells)
            {
                if (cell.unitNode != null)
                {
                    cell.unitNode.DecreaseHP(source.atk);
                }
            }
        }


        //if (source.guanchuan > 0)
        //{
        //    List<MMCell> cells = MMMap.Instance.FindCellsBehind(target.cell);
        //    foreach (var cell in cells)
        //    {
        //        if (cell.unitNode != null)
        //        {
        //            cell.unitNode.DecreaseHP(source.guanchuan);
        //        }
        //    }
        //}



        //if (target.unitState != MMUnitState.Stunned)
        //{
        //    //眩晕状态不还击    
        //}
        //else
        //{
        //    //还击
        //    int value2 = Mathf.Max(target.atk - tempDEF, 0);
        //    source.DecreaseHP(value2);
        //}
        //还击
        int value2 = Mathf.Max(target.atk - tempDEF, 0);
        source.DecreaseHP(value2);

        BroadCastUnitSkill(MMTriggerTime.AfterAttack, source);
        BroadCastUnitSkill(MMTriggerTime.AfterBeAttack, target);

        //Target从Weak状态进入Stunned的状态时，Source可以连击
        //bool flag1 = (target.unitState == MMUnitState.Weak);
        //if (target.group == 2)
        //{
        //    target.DecreaseAP();
        //}
        //bool flag2 = (target.unitState == MMUnitState.Stunned);
        //if (flag1 && flag2)
        //{
        //    source.EnterPhase(MMUnitPhase.Combo);
        //}

        source.DecreaseTempATK(source.tempATK);
    }


    private void InHP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.IncreaseHP(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.IncreaseHP(effect.value);
        }
    }

    private void DeHP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseHP(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseHP(effect.value);
        }
    }

    private void InAP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.IncreaseAP(1);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.IncreaseAP(1);
        }
    }

    private void DeAP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseAP(1);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseAP(1);
        }
    }

    private void InATK(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.IncreaseATK(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.IncreaseATK(effect.value);
        }
    }

    private void DeATK(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseATK(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseATK(effect.value);
        }
    }

    private void Damage(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseHP(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseHP(effect.value);
        }
    }

    private void Summon(MMEffect effect)
    {
        effect.target.group = effect.source.group;
        MMUnitNode node = effect.target;
        node.gameObject.AddComponent<MMUnitNode_Battle>();
        MMCell c;
        int row;

        if (node.group == 1)
        {
            row = 3 - node.clss;
            int index = MMMap.Instance.FindFrontRowOfGroup(1);
            row = Mathf.Min(row, index);

            c = MMMap.Instance.FindRandomEmptyCellInRow(row);
            units1.Add(node);
        }
        else
        {
            row = node.clss + 3;
            int index = MMMap.Instance.FindFrontRowOfGroup(2);
            row = Mathf.Max(row, index);

            c = MMMap.Instance.FindRandomEmptyCellInRow(row);
            units2.Add(node);
        }

        if (c == null)
        {
            MMTipManager.instance.CreateTip("没有更多格子");
            return;
        }

        c.Accept(node);
        effect.target = node;
        //BroadCast(MMTriggerTime.OnSummon, effect);
        ExecuteEffectOnSummon(effect);

    }


    private void TempATKDEF(MMEffect effect)
    {
        effect.target.tempATK += effect.value;
        effect.target.tempDEF += effect.value;
    }


    private void AddHand(MMCardNode card)
    {
        card.gameObject.AddComponent<MMCardNode_Battle>();
        MMCardPanel.Instance.hand.Add(card);
        MMCardPanel.Instance.UpdateUI();
    }

}
