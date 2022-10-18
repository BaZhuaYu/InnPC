using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMRewardSkill : MonoBehaviour, IPointerClickHandler
{

    public MMSkillNode skill;

    public void OnPointerClick(PointerEventData eventData)
    {
        MMUnitNode node = GetComponent<MMUnitNode>();
        MMUnit unit = node.unit;
        unit.skills.Add(skill.id);


        if(skill.time == MMTriggerTime.Gain)
        {
            MMEffect effect = skill.CreateEffect();
            effect.target = effect.source;
            MMBattleManager.Instance.ExecuteEffectOnGain(effect);
        }
        

        MMRewardPanel.instance.CloseUI();
        MMGameOverManager.Instance.UpdateUI();
    }

    
}
