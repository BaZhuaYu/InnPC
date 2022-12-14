using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMNode : MonoBehaviour
{
    [HideInInspector]
    public MMNodeHighlight nodeHighlight;

    [HideInInspector]
    public string userinfo;


    private void Start()
    {
        nodeHighlight = MMNodeHighlight.Normal;
    }


    public void LoadImage(string key)
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>(key);
    }


    public bool ContainsPoints(Vector2 pos)
    {
        RectTransform rect = GetComponent<RectTransform>();

        float x =transform.position.x;
        float y = transform.position.y;
        float w = rect.rect.width;
        float h = rect.rect.height;
        
        if (pos.x > x - w/2f && pos.x < x + w/2f) { 
            if (pos.y > y - h / 2f && pos.y < y + h / 2f)
            {
                return true;
            }
            
        }

        return false;
    }


    public virtual void OpenUI()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void CloseUI()
    {
        this.gameObject.SetActive(false);
    }

    

    public MMNode FindParent()
    {
        MMNode ret = transform.parent.GetComponent<MMNode>();
        if (ret == null)
        {
            MMDebugManager.FatalError("");
        }

        return ret;
    }

    public void RemoveFromParent()
    {
        this.gameObject.transform.SetParent(null);
    }


    public float FindWidth()
    {
        return GetComponent<RectTransform>().rect.width;
    }


    public float FindHeight()
    {
        return GetComponent<RectTransform>().rect.height;
    }


    public void MoveToParentLeftOffset(float offset = 0f)
    {
        MMNode parent = FindParent();

        float x = -parent.FindWidth() / 2 + this.FindWidth() / 2 + offset;
        float y = transform.localPosition.y;
        transform.localPosition = new Vector2(x, y);
    }

    public void MoveToParentRightOffset(float offset = 0f)
    {
        MMNode parent = FindParent();

        float x = parent.FindWidth() / 2 - this.FindWidth() / 2 - offset;
        float y = transform.localPosition.y;
        transform.localPosition = new Vector2(x, y);
    }
    
    public void MoveToParentBottomOffset(float offset = 0f)
    {
        MMNode parent = FindParent();

        float x = transform.localPosition.x;
        float y = -parent.FindHeight() / 2 + this.FindHeight() / 2 + offset;
        transform.localPosition = new Vector2(x, y);
    }


    public void MoveToParentTopOffset(float offset = 0f)
    {
        MMNode parent = FindParent();

        float x = transform.localPosition.x;
        float y = parent.FindHeight() / 2 - this.FindHeight() / 2 - offset;
        transform.localPosition = new Vector2(x, y);
    }


    public void MoveUp(float delta)
    {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y + delta;
        transform.localPosition = new Vector2(x, y);
    }

    public void MoveDown(float delta)
    {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y - delta;
        transform.localPosition = new Vector2(x, y);
    }

    public void MoveLeft(float delta)
    {
        float x = transform.localPosition.x - delta;
        float y = transform.localPosition.y;
        transform.localPosition = new Vector2(x, y);
    }

    public void MoveRight(float delta)
    {
        float x = transform.localPosition.x + delta;
        float y = transform.localPosition.y;
        transform.localPosition = new Vector2(x, y);
    }

    public void MoveToCenter()
    {
        transform.localPosition = new Vector2(0, 0);
    }

    public void MoveToCenterY()
    {
        float x = transform.localPosition.x;
        transform.localPosition = new Vector2(x, 0);
    }

    public void MoveToCenterX()
    {
        float y = transform.localPosition.y;
        transform.localPosition = new Vector2(0, y);
    }

    public void SetSize(Vector2 size)
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }

    public void AddChild(MMNode child)
    {
        child.transform.SetParent(this.transform);
        child.transform.localPosition = Vector3.zero;
    }

    public void SetParent(MMNode parent)
    {
        this.transform.SetParent(parent.transform);
        this.transform.localPosition = Vector3.zero;
    }

    public void SetColor(Color c)
    {
        GetComponent<Image>().color = c;
    }

    public void SetActive(bool flag)
    {
        this.gameObject.SetActive(flag);
    }

    
}
