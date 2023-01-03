using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMBattleManager
{

    public void HandlePlaySkill()
    {
        

        MMEffect effect = selectingSkill.CreateEffect();
        effect.target = this.targetUnit;
        

        ExecuteEffect(effect);

        
        this.historySkills[round].Add(selectingSkill);
        

        //Final Card
        if (selectingSkill.keywords.Contains(MMSkillKeyWord.Final))
        {
            EnterPhase(MMBattlePhase.UnitEnd);
        }
        else
        {
            EnterPhase(MMBattlePhase.UnitActing);
        }

    }



    public void DrawCards(int count, bool instance = false)
    {
        MMCardPanel.Instance.DrawCard(count, instance);
    }
    

}
