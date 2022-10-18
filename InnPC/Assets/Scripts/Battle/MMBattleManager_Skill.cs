using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager : MonoBehaviour
{
    
    public void PlaySkill()
    {
        if (this.selectingSkill == null)
        {
            MMTipManager.instance.CreateTip("没有选择技能");
            return;
        }

        if (this.sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("没有己方英雄");
            return;
        }


        if (selectingSkill.area == MMArea.None)
        {
            targetUnit = sourceUnit;
        }
        else
        {

        }
        MMEffect effect = selectingSkill.CreateEffect();
        effect.target = targetUnit;
        ExecuteEffect(effect);

        //MMCardPanel.Instance.PlayCard(selectedSkill);
        MMSkillPanel.Instance.PlaySkill(selectingSkill);
        MMBattleManager.Instance.EnterState(MMBattleState.SourDone);
    }



    public void DrawSkill()
    {
        //MMCardPanel.Instance.Draw(count);

        MMSkillPanel.Instance.Accept(sourceUnit.skills);
    }


    public void ClearSelectSkill()
    {
        this.selectingSkill = null;
        MMSkillPanel.Instance.selectingSkill = null;
    }



    public void SelectSkill(MMSkillNode skill)
    {

        if(sourceUnit == null)
        {
            MMTipManager.instance.CreateTip("需要选中己方英雄");
            return;
        }

        if(skill.cost > sourceUnit.ap)
        {
            MMTipManager.instance.CreateTip("行动力不足");
            return;
        }

        MMSkillPanel.Instance.selectingSkill = skill;
        selectingSkill = skill;
        
        if (skill.area == MMArea.None)
        {
            MMBattleManager.Instance.PlaySkill();
        }
        else
        {
            MMBattleManager.Instance.EnterState(MMBattleState.SelectSkill);
        }
        
    }


    //public void SetSelectingSkill(MMSkillNode skill)
    //{
    //    this.selectingSkill = skill;
    //    skill.MoveUp(20);
    //    sourceUnit.ShowAttackCells();

    //    if (skill.area == MMArea.None)
    //    {
    //        MMBattleManager.Instance.PlaySkill();
    //    }
    //    else
    //    {
    //        MMBattleManager.Instance.EnterState(MMBattleState.SelectSkill);
    //    }
    //}



}
