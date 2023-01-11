using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;

public class MMEvilPanel : MMNode
{

    public static MMEvilPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    List<MMNode> nodes;
    public int maxEvil;
    public int curEvil;


    // Start is called before the first frame update
    void Start()
    {
        this.nodes = new List<MMNode>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Increase()
    {
        curEvil += 1;
        UpdateUI();
    }

    public void Decrease()
    {
        curEvil -= 1;
        UpdateUI();
    }

    public void AcceptEvil(int cur, int max)
    {
        Clear();
        this.curEvil = cur;
        this.maxEvil = max;

        for (int i = 0; i < maxEvil; i++)
        {
            MMNode node = Create("UI/Icon/Icon_Evil");
            node.LoadSize(new Vector2(this.FindHeight() * 0.8f, this.FindHeight() * 0.8f));
            this.nodes.Add(node);
            node.SetParent(this);
        }

        UpdateUI();
    }

    public void Clear()
    {
        if(this.nodes == null)
        {
            this.nodes = new List<MMNode>();
            return;
        }
        foreach(var node in this.nodes)
        {
            Destroy(node.gameObject);
        }
        this.nodes = new List<MMNode>();
    }


    public bool CheckReachMax()
    {
        return curEvil >= maxEvil;
    }

    public void Reload()
    {
        
    }


    public void UpdateUI()
    {
        float offset = (this.FindWidth() - this.FindHeight()) * 0.5f;

        for (int i = 0; i < nodes.Count; i++)
        {
            MMNode node = nodes[i];
            node.MoveLeft(offset);
            offset -= this.FindHeight();
            if (i < curEvil)
            {

            }
            else
            {
                node.LoadColor(Color.black);
            }
        }
    }


}
