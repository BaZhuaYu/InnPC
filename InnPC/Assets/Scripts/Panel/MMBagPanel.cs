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


    public GameObject content;


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
            MMHeroNode node = MMHeroNode.Create(hero);
            node.transform.SetParent(content.transform);
            //AddChild(node);
            node.MoveLeft(offset);
            offset -= node.FindWidth() * 1.1f;
        }
    }

    
    public void OnClickButtonBack()
    {
        CloseUI();
        MMMainPanel.Instance.OpenUI();
    }


}
