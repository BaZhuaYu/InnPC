using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMEnrollPanel : MMNode
{
    public static MMEnrollPanel Instance;

    private void Awake()
    {
        Instance = this;
    }


    List<MMHeroNode> heroes;

    // Start is called before the first frame update
    void Start()
    {
        heroes = new List<MMHeroNode>();
        CloseUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void OnClickButtonEnroll()
    {
        if (MMPlayerManager.Instance.diamond < 10)
        {
            return;
        }

        MMPlayerManager.Instance.diamond -= 10;
        Clear();
        EnrollHero();
    }


    
    public void OnClickButtonBack()
    {
        CloseUI();
        MMMainPanel.Instance.OpenUI();
    }



    void EnrollHero()
    {
        MMUnit unit = MMUnit.FindRandomOne();
        MMHeroNode node = MMHeroNode.CreateFromUnit(unit);
        MMPlayerManager.Instance.AddHero(unit);
        node.SetParent(this);
        this.heroes.Add(node);
        node.MoveUp(200);
    }

    void Clear()
    {
        foreach(var unit in heroes)
        {
            unit.Clear();
        }
        heroes = new List<MMHeroNode>();
    }

}
