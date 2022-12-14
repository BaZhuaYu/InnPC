using System.Collections;
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

        if (isPlayerRound == 1)
        {
            units = units1;
        }
        else
        {
            units = units2;
        }

        switch (time)
        {
            case MMTriggerTime.OnBattleBegin:
                foreach (var unit in units)
                {
                    unit.OnBattleBegin();
                }
                break;

            case MMTriggerTime.OnRoundBegin:
                foreach (var unit in units)
                {
                    unit.OnRoundBegin();
                }
                break;

            case MMTriggerTime.OnRoundEnd:
                foreach (var unit in units)
                {
                    unit.OnRoundEnd();
                }
                break;
        }



        //foreach (var unit in units)
        //{
        //foreach (var skill in unit.skills)
        //{
        //    if (skill.time == time && skill.isEnabled)
        //    {
        //        MMTipManager.instance.CreateSkillTip(skill);
        //        MMEffect effect = skill.CreateEffect();
        //        ExecuteEffect(effect);
        //    }
        //}
        //}
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

            case MMEffectType.AddBuff:
                AddBuff(effect);
                break;

            case (MMEffectType)10101:
                Damage(effect);
                effect.source.IncreaseHP(effect.value);
                break;

            case (MMEffectType)1107:
                effect.target.DecreaseHP(1);
                effect.target.IncreaseATK(1);
                break;

            case (MMEffectType)1113:
                effect.value = effect.source.maxHP - effect.source.hp;
                Damage(effect);
                break;

            case (MMEffectType)1117:
                effect.source.IncreaseATK(1);
                effect.source.DecreaseHP(1);
                break;

            case (MMEffectType)1123:
                effect.target.IncreaseTempATK(4);
                break;

            case (MMEffectType)1125:
                effect.source.DecreaseHP(1);
                DrawCards(3);
                break;

            case (MMEffectType)1127:
                effect.target.DecreaseHP(effect.source.hp);
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

            case (MMEffectType)1211:

                break;

            case (MMEffectType)1213:
                Debug.Log(effect.target.hp);
                Attack(effect);
                break;

            case (MMEffectType)1215:

                break;

            case (MMEffectType)1217:
                AddHand(MMCardNode.Create(1201));
                AddHand(MMCardNode.Create(1203));
                break;

            case (MMEffectType)1225:
                Attack(effect);
                break;

            case (MMEffectType)1227:
                Attack(effect);
                break;


            //Clss 3
            case (MMEffectType)1313:
                AddHand(MMCardNode.Create(1303));
                AddHand(MMCardNode.Create(1305));
                break;

            case (MMEffectType)1315:
                effect.target.DecreaseHP(2);
                effect.source.FindTarget().DecreaseHP(2);
                //effect.target.DecreaseHP(2);
                break;

            case (MMEffectType)1325:
                effect.target.DecreaseHP(3);
                break;

            case (MMEffectType)1327:

                break;




            default:
                MMDebugManager.Log("Not Find" + effect.type);
                break;
        }


        switch (effect.type)
        {
            case (MMEffectType)1211:
                if (effect.target.state == MMUnitState.Dead)
                {
                    effect.source.IncreaseAP(1);
                }
                break;

            case (MMEffectType)1325:
                AddHand(MMCardNode.Create(1325));
                break;

            case (MMEffectType)1225:
                if (effect.target.state == MMUnitState.Dead)
                {
                    effect.source.IncreaseAP(1);
                }
                break;

            case (MMEffectType)1227:
                if (effect.target.state == MMUnitState.Dead)
                {
                    DrawCards(1);
                }
                break;
        }

        if(effect.target.state == MMUnitState.Dead)
        {
            effect.source.OnKill(effect.target);
            effect.target.OnDead(effect.source);
            Debug.LogWarning("ExecuteEffect: " + effect.target);
        }

    }



    private void Attack(MMEffect effect)
    {
        int tempATK = effect.userinfo["TempATK"];
        int tempDEF = effect.userinfo["TempDEF"];
        MMUnitNode source = effect.source;
        MMUnitNode target = effect.target;


        source.OnBeforeAttack(target);
        target.OnBeforeBeAttack(source);
        

        int atk = source.atk + tempATK;
        target.DecreaseHP(atk);

        if (effect.area == MMArea.Beside)
        {
            List<MMCell> cells = MMMap.Instance.FindCellsBeside(target.cell);
            foreach (var cell in cells)
            {
                if (cell.unitNode != null)
                {
                    cell.unitNode.DecreaseHP(atk);
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
                    cell.unitNode.DecreaseHP(atk);
                }
            }
        }

        //还击
        int value2 = Mathf.Max(target.atk - tempDEF, 0);
        source.DecreaseHP(value2);
        
        source.OnAfterAttack(target);
        target.OnAfterBeAttack(source);

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
            effect.target.IncreaseAP(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.IncreaseAP(effect.value);
        }
    }

    private void DeAP(MMEffect effect)
    {
        if (effect.target != null)
        {
            effect.target.DecreaseAP(effect.value);
        }
        foreach (var tar in effect.sideTargets)
        {
            tar.DecreaseAP(effect.value);
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
        effect.source.OnSummon(node);
    }


    public void AddBuff(MMEffect effect)
    {
        effect.target.AddBuff((MMBuff)effect.value);
    }


    private void TempATKDEF(MMEffect effect)
    {
        effect.target.tempATK += effect.value;
        effect.target.tempDEF += effect.value;
    }


    private void AddHand(MMCardNode card)
    {
        //card.gameObject.AddComponent<MMCardNode_Battle>();
        MMCardPanel.Instance.hand.Add(card);
        MMCardPanel.Instance.UpdateUI();
    }

}
