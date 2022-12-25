using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMMainPanel : MMNode
{

    public static MMMainPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Button buttonPick;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonPick.interactable = (MMPlayerManager.Instance.heroes.Count >= 3);
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
