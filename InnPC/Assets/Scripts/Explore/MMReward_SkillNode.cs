using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMReward_SkillNode : MonoBehaviour, IPointerClickHandler
{

    public MMSkillNode skill;

    public void OnPointerClick(PointerEventData eventData)
    {
        MMUnitNode node = GetComponent<MMUnitNode>();
        MMUnit unit = node.unit;
        unit.skills.Add(skill.id);
        
        MMExplorePanel.Instance.UpdateUI();
    }

    
}
