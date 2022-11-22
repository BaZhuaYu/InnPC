using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MMBattleState
{
    Normal,
    SelectedSourceUnit,
    SelectingCard,
    SelectingSkill,
    SelectedTargetUnit,
    PlayCard,
    PlaySkill,
}


public partial class MMBattleManager : MonoBehaviour
{

    public void EnterState(MMBattleState state)
    {

        if(state == MMBattleState.SelectedSourceUnit)
        {
            if (sourceUnit == null)
            {
                return;
            }

            if (sourceUnit.state == MMUnitState.Dead)
            {
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


        this.state = state;

        switch (state)
        {
            case MMBattleState.SelectedSourceUnit:
                break;

            case MMBattleState.SelectingCard:
                MMCardPanel.Instance.SetSelectingCard(selectingCard);
                HandleStateSelectingCard();
                break;

            case MMBattleState.SelectingSkill:
                MMSkillPanel.Instance.SetSelectedSkill(selectingSkill);
                HandleStateSelectingSkill();
                break;

            case MMBattleState.SelectedTargetUnit:
                if (selectingSkill != null)
                {
                    EnterState(MMBattleState.PlaySkill);
                }
                else if (selectingCard != null)
                {
                    EnterState(MMBattleState.PlayCard);
                }
                break;

            case MMBattleState.PlayCard:
                sourceUnit.tempCell = sourceUnit.cell;
                MMBattleManager.Instance.HandlePlayCard();
                break;

            case MMBattleState.PlaySkill:
                sourceUnit.tempCell = sourceUnit.cell;
                MMBattleManager.Instance.HandlePlaySkill();
                break;

            default:
                break;
        }

        OnEnterState();
    }





    void OnEnterState()
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
            case MMBattleState.Normal:
                ClearUnitEnd();
                break;

            case MMBattleState.SelectedSourceUnit:
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
    }







    public void TryEnterStateSelectedSourceUnit(MMUnitNode unit)
    {






        sourceUnit = unit;
        EnterPhase(MMBattlePhase.UnitBegin);
        EnterState(MMBattleState.SelectedSourceUnit);
    }


    public bool TryEnterStateSelectingCard(MMCardNode card)
    {
        if (card.type == MMSkillType.Passive)
        {
            MMTipManager.instance.CreateTip("�������ܣ��޷�ʹ��");
            return false;
        }

        if (card.state == MMSkillState.Used)
        {
            MMTipManager.instance.CreateTip("��ʹ�ã������ٴ�ʹ��");
            return false;
        }

        if (card.state == MMSkillState.NotReady)
        {
            MMTipManager.instance.CreateTip("������ܻ�û��׼����");
            return false;
        }

        if (sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("��Ҫѡ�м���Ӣ��");
            return false;
        }

        if (card.cost > sourceUnit.ap)
        {
            MMTipManager.instance.CreateTip("�ж�������");
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
            MMTipManager.instance.CreateTip("��Ҫѡ�м���Ӣ��");
            return;
        }

        if (skill.cost > sourceUnit.ap)
        {
            MMTipManager.instance.CreateTip("�ж�������");
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
            MMTipManager.instance.CreateTip("û��ѡ����");
            return;
        }

        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("û�м���Ӣ��");
            return;
        }


        EnterState(MMBattleState.PlayCard);
    }






    void HandleStateSelectingCard()
    {
        //ѡ��Ŀ��
        if (selectingCard.target == MMEffectTarget.None)
        {
            //EnterState(MMBattleState.SelectingCard);
        }
        //�Զ��ͷ�
        else
        {
            switch (selectingCard.target)
            {
                case MMEffectTarget.Source:
                    this.targetUnit = this.sourceUnit;
                    break;

                case MMEffectTarget.Target:
                    this.targetUnit = this.sourceUnit.FindTarget();
                    break;

                default:
                    MMDebugManager.Warning("SelectSkill: " + selectingCard.target);
                    break;
            }

            EnterState(MMBattleState.SelectedTargetUnit);
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

        sourceUnit.cell.HandleFill(MMNodeState.Normal);
        sourceUnit.cell.HandleBorder(MMNodeHighlight.Normal);
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

        targetUnit.cell.HandleFill(MMNodeState.Normal);
        targetUnit.cell.HandleBorder(MMNodeHighlight.Normal);
        targetUnit = null;
    }


    public void UnselectSourceCell()
    {
        this.ClearSource();
        this.ClearTarget();
        selectingCard = null;
        selectingSkill = null;
    }


    public void HandleSourceActionDone()
    {
        sourceUnit.tempCell = sourceUnit.cell;
    }


    public void ClosePanels()
    {
        MMCardPanel.Instance.CloseUI();
        MMSkillPanel.Instance.CloseUI();
        MMUnitPanel.Instance.CloseUI();
    }

}
