using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMMainPanel : MMNode
{

    public static MMMainPanel Instance;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenPickHeroPanel()
    {
        this.CloseUI();
        MMPickHeroPanel.Instance.OpenUI();
    }

    public void OpenEnrollPanel()
    {
        this.CloseUI();
        MMEnrollPanel.Instance.OpenUI();
    }

    public void OpenBagPanel()
    {
        this.CloseUI();
        MMBagPanel.Instance.OpenUI();
    }

    public void ExitGame()
    {
        
    }


}
