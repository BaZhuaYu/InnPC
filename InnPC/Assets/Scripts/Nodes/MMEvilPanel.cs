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

    public bool CheckReachMax()
    {
        return curEvil >= maxEvil;
    }

    public void Reload()
    {
        
    }


    public void UpdateUI()
    {
        for (int i = 0; i < maxEvil; i++)
        {
            MMNode node = Create("UI/Icon/Icon_Evil");
            node.SetParent(this);
            if (i < curEvil)
            {

            }
            else
            {
                node.LoadColor(Color.gray);
            }
        }
    }

}
