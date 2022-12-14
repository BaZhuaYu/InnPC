using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMBagPanel : MMNode
{
    public static MMBagPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CloseUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OpenUI()
    {
        base.OpenUI();
        UpdateUI();
    }
    



    public void UpdateUI()
    {
        float offset = 200f;
        foreach(var hero in MMPlayerManager.Instance.heroes)
        {
            MMHeroNode node = MMHeroNode.CreateFromUnit(hero);
            AddChild(node);
            node.MoveLeft(offset);
            offset -= 100;
        }
    }



    public void OnClickButtonBack()
    {
        CloseUI();
        MMMainPanel.Instance.OpenUI();
    }
}
