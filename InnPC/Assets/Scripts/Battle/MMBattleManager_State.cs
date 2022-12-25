using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MMBattleState
{
    None,
    Normal,
    SelectedSourceUnit,
    SelectingCard,
    SelectingSkill,
    SelectedTargetUnit,
    PlayCard,
    PlaySkill,
}


public partial class MMBattleManager
{
    int index = 0;
    bool isLock = false;

    public void EnterState(MMBattleState s)
    {
        if (s == MMBattleState.SelectedSourceUnit)
        {
            if (sourceUnit == null)
            {
                Debug.Log("From: " + state.ToString() + " To: " + s.ToString());
                return;
            }

            if (sourceUnit.state == MMUnitState.Dead)
            {
                Debug.Log("From: " + state.ToString() + " To: " + s.ToString());
                return;
            }
        }


        //Normal,
        //SelectedSourceUnit,
        //SelectingCard,
        //SelectingSkill,
        //SelectedTargetUnit,
        //PlayCard,
        //PlaySkill,
        int temp = index++;

        OnExitState();
        this.state = s;
        OnEnterState();
        UpdateStateUI();

        AutoRoute_State();

    }


    void AutoRoute_State()
    {
        switch (state)
        {
            case MMBattleState.None:
                break;

            case MMBattleState.Normal:
                break;

            case MMBattleState.SelectedSourceUnit:
                break;

            case MMBattleState.SelectingCard:
                if (this.targetUnit == null)
                {
                    //MMTipManager.instance.CreateTip("没有目标");
                    //EnterState(MMBattleState.SelectedSourceUnit);
                }
                else
                {
                    Debug.Log("aaaaaaaaaaaaaaaaaaa: " + this.targetUnit.displayName);
                    EnterState(MMBattleState.SelectedTargetUnit);
                }
                break;

            case MMBattleState.SelectingSkill:
                HandleStateSelectingSkill();
                break;

            case MMBattleState.SelectedTargetUnit:
                //使用技能
                if (selectingSkill != null)
                {

                }
                //使用卡牌
                else if (selectingCard != null)
                {
                    if (targetUnit.state == MMUnitState.Dead)
                    {
                        ClearDeadUnits();
                        sourceUnit.isMoved = false;
                    }

                    if (sourceUnit.state == MMUnitState.Dead)
                    {
                        ClearDeadUnits();
                        EnterPhase(MMBattlePhase.UnitEnd);
                        break;
                    }

                    //Final Card
                    if (selectingCard.keywords.Contains(MMSkillKeyWord.Final))
                    {
                        EnterPhase(MMBattlePhase.UnitEnd);
                        break;
                    }

                    if (sourceUnit.group == 1)
                    {
                        EnterState(MMBattleState.SelectedSourceUnit);
                        EnterPhase(MMBattlePhase.UnitActing);
                    }
                    else
                    {
                        //EnterPhase(MMBattlePhase.UnitEnd);
                    }

                }
                break;

            case MMBattleState.PlayCard:
                break;

            case MMBattleState.PlaySkill:
                break;

        }
    }


    void OnExitState()
    {

    }


    void OnEnterState()
    {
        switch (state)
        {
            case MMBattleState.None:
                ClearUnitEnd();
                break;

            case MMBattleState.Normal:
                break;

            case MMBattleState.SelectedSourceUnit:
                ClearTarget();
                ClearSelectSkill();
                ClearSelectCard();
                EnterPhase(MMBattlePhase.UnitBegin);
                break;

            case MMBattleState.SelectingCard:
                MMCardPanel.Instance.SetSelectingCard(selectingCard);
                HandleStateSelectingCard();
                break;

            case MMBattleState.SelectingSkill:
                MMSkillPanel.Instance.SetSelectedSkill(selectingSkill);
                //HandleStateSelectingSkill();
                break;

            case MMBattleState.SelectedTargetUnit:
                if (selectingSkill != null)
                {
                    sourceUnit.tempCell = sourceUnit.cell;
                    MMBattleManager.Instance.HandlePlaySkill();
                    //EnterState(MMBattleState.PlaySkill);
                }
                else if (selectingCard != null)
                {
                    sourceUnit.tempCell = sourceUnit.cell;
                    MMBattleManager.Instance.HandlePlayCard();
                    //EnterState(MMBattleState.PlayCard);
                }
                break;

            case MMBattleState.PlayCard:
                //sourceUnit.tempCell = sourceUnit.cell;
                //MMBattleManager.Instance.HandlePlayCard();
                break;

            case MMBattleState.PlaySkill:
                //sourceUnit.tempCell = sourceUnit.cell;
                //MMBattleManager.Instance.HandlePlaySkill();
                break;

        }
    }


    void UpdateStateUI()
    {

        if (state == MMBattleState.SelectedSourceUnit)
        {
            if (sourceUnit.state == MMUnitState.Dead)
            {
                return;
            }
        }


        if (sourceUnit != null)
        {
            sourceUnit.HideMoveCells();
            sourceUnit.HideAttackCells();
            sourceUnit.HideSelected();
        }


        switch (state)
        {
            case MMBattleState.None:
                break;

            case MMBattleState.SelectedSourceUnit:
                MMCardPanel.Instance.UpdateUI();
                sourceUnit.ShowMoveCells();
                sourceUnit.ShowSelected();
                break;

            case MMBattleState.SelectingCard:
            case MMBattleState.SelectingSkill:
                sourceUnit.ShowAttackCells();
                break;

            default:
                break;
        }

        textPhase.text = phase.ToString() + " " + state.ToString();
    }


    public void TryEnterStateSelectedSourceUnit(MMUnitNode unit)
    {
        sourceUnit = unit;
        //EnterPhase(MMBattlePhase.UnitBegin);
        EnterState(MMBattleState.SelectedSourceUnit);
    }


    public bool TryEnterStateSelectingCard(MMCardNode card)
    {
        if (card.type == MMSkillType.Passive)
        {
            MMTipManager.instance.CreateTip("被动技能，无法使用");
            return false;
        }

        if (card.state == MMSkillState.Used)
        {
            MMTipManager.instance.CreateTip("已使用，不能再次使用");
            return false;
        }

        if (card.state == MMSkillState.NotReady)
        {
            MMTipManager.instance.CreateTip("这个技能还没有准备好");
            return false;
        }

        if (sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("需要选中己方英雄");
            return false;
        }

        if (card.cost > sourceUnit.ap)
        {
            MMTipManager.instance.CreateTip("行动力不足");
            return false;
        }
        
        selectingCard = card;
        EnterState(MMBattleState.SelectingCard);
        return true;
    }


    public void TryEnterStateSelectingSkill(MMSkillNode skill)
    {
        if (sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("需要选中己方英雄");
            return;
        }

        if (skill.cost > sourceUnit.ap)
        {
            MMTipManager.instance.CreateTip("行动力不足");
            return;
        }
        
        selectingSkill = skill;
        EnterState(MMBattleState.SelectingSkill);
    }


    public void TryEnterStateSelectedTargetUnit(MMUnitNode unit)
    {
        targetUnit = unit;
        EnterState(MMBattleState.SelectedTargetUnit);
    }


    public void TryEnterStatePlayCard()
    {
        if (this.selectingCard == null)
        {
            MMTipManager.instance.CreateTip("没有选择卡牌");
            return;
        }

        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("没有己方英雄");
            return;
        }
        
        EnterState(MMBattleState.PlayCard);
    }

    

    void HandleStateSelectingCard()
    {
        switch (selectingCard.target)
        {
            case MMEffectTarget.Source:
                this.targetUnit = this.sourceUnit;
                break;

            case MMEffectTarget.Target:
                this.targetUnit = this.sourceUnit.FindTarget();
                break;

            case MMEffectTarget.MinHPTeam:
                this.targetUnit = this.sourceUnit.FindMinHP();
                break;

            case MMEffectTarget.MaxHPTeam:
                this.targetUnit = this.sourceUnit.FindMaxHP();
                break;

            case MMEffectTarget.MinHPEnemy:
                this.targetUnit = this.sourceUnit.FindMinHPEnemy();
                break;

            case MMEffectTarget.MaxHPEnemy:
                this.targetUnit = this.sourceUnit.FindMaxHPEnemy();
                break;

            case MMEffectTarget.Cell:
            case MMEffectTarget.Unit:
                break;
            case MMEffectTarget.None:
                this.targetUnit = this.sourceUnit;
                break;
        }
        
    }


    void HandleStateSelectingSkill()
    {
        if (selectingSkill.target == MMEffectTarget.None)
        {
            //MMBattleManager.Instance.EnterState(MMBattleState.SelectingSkill);
        }
        else
        {
            switch (selectingSkill.target)
            {
                case MMEffectTarget.Source:
                    this.targetUnit = this.sourceUnit;
                    break;

                case MMEffectTarget.Target:
                    this.targetUnit = this.sourceUnit.FindTarget();
                    break;

                default:
                    MMDebugManager.Warning("SelectSkill: " + selectingSkill.target);
                    break;
            }

            EnterState(MMBattleState.SelectedTargetUnit);
        }
    }





    public void HandleUnitActionDone()
    {
        Debug.Log("Clear Source");
        this.ClearSource();
        this.ClearTarget();
        selectingCard = null;
        selectingSkill = null;
    }



    public void ClearSource()
    {

        if (sourceUnit == null)
        {
            return;
        }

        if (sourceUnit.cell == null)
        {
            sourceUnit = null;
            return;
        }

        sourceUnit.cell.HandleHighlight(MMNodeHighlight.Normal);
        sourceUnit.HideMoveCells();
        sourceUnit.HideAttackCells();
        sourceUnit = null;
    }


    public void ClearTarget()
    {
        if (targetUnit == null)
        {
            return;
        }

        if (targetUnit.cell == null)
        {
            targetUnit = null;
            return;
        }
        
        targetUnit.HandleHighlight(MMNodeHighlight.Normal);
        targetUnit.cell.HandleHighlight(MMNodeHighlight.Normal);
        targetUnit = null;
    }
    

    public void ClosePanels()
    {
        MMCardPanel.Instance.CloseUI();
        MMSkillPanel.Instance.CloseUI();
        MMUnitPanel.Instance.CloseUI();
    }

}
