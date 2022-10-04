using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMRewardSkill : MonoBehaviour, IPointerClickHandler
{

    public MMSkill skill;

    public void OnPointerClick(PointerEventData eventData)
    {
        MMUnitNode node = GetComponent<MMUnitNode>();
        MMUnit unit = node.unit;
        unit.skills.Add(skill.id);

        foreach(var skill in unit.skills)
        {
            Debug.Log(skill);
        }

        MMRewardPanel.instance.CloseUI();
        MMGameOverManager.Instance.UpdateUI();
    }

    
}
