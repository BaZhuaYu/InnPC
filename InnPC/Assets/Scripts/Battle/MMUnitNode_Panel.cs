using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMUnitNode_Panel : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    MMUnitNode unit;
    MMCell tempCell;
    Vector2 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        unit = this.GetComponent<MMUnitNode>();
        tempPos = unit.gameObject.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MMCell cell = MMMap.Instance.FindCellAtPosition(Input.mousePosition);

        if(cell == null)
        {
            TryHandleUnitToPanel();
            return;
        }

        cell.Accept(unit);
        AddUnit(unit);
        HandleCellBorder(null);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        unit.gameObject.transform.position = Input.mousePosition;

        MMCell cell = MMMap.Instance.FindCellAtPosition(Input.mousePosition);
        if (cell == null)
        {
            return;
        }

        HandleCellBorder(cell);
    }


    void HandleCellBorder(MMCell cell)
    {
        if (tempCell != null)
        {
            tempCell.HandleHighlight(MMNodeHighlight.Normal);
        }

        if(cell == null)
        {
            return;
        }

        tempCell = cell;
        cell.HandleHighlight(MMNodeHighlight.Green);
    }



    void TryHandleUnitToPanel()
    {
        if(unit == null)
        {
            return;
        }

        unit.transform.position = tempPos;
    }


    void AddUnit(MMUnitNode unit)
    {
        MMBattleManager.Instance.units1.Add(unit);
        unit.group = 1;
        Destroy(unit.gameObject.GetComponent<MMUnitPanel>());
        unit.gameObject.AddComponent<MMUnitNode_Battle>();
    }
}
