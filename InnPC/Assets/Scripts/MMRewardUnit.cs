using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMRewardUnit : MonoBehaviour, IPointerClickHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MMRewardUnit");
        MMUnitNode node = GetComponent<MMUnitNode>();
        MMUnit unit = node.unit;
        MMPlayerManager.instance.units.Add(unit);
        Destroy(this.gameObject);

        MMRewardPanel.instance.CloseUI();
        MMGameOverManager.instance.UpdateUI();
    }

}
