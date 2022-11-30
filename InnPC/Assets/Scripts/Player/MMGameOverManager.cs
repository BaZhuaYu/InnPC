using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMGameOverManager : MMNode
{
    public static MMGameOverManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<MMRewardType> rewards;

    public bool isWin;
    public bool isLost;

    public Button mainButton;
    public Text mainText;
    public Text goldText;
    
    public List<MMButton> buttons;

    void Start()
    {
        mainButton = GameObject.Find("PanelGameOver/MainButton").GetComponent<Button>();
        mainText = GameObject.Find("PanelGameOver/MainButton/Text").GetComponent<Text>();
        mainButton.onClick.AddListener(OnClickMainButton);
        
        SetActive(false);
    }


    public void SetWin()
    {
        isWin = true;
        isLost = false;

        rewards = new List<MMRewardType>();

        rewards.Add(MMRewardType.Item);
        rewards.Add(MMRewardType.Card);
        rewards.Add(MMRewardType.Skill);
        
        int coin = MMBattleManager.Instance.level + 2;
        if(coin > 10)
        {
            coin = 10;
        }
        MMPlayerManager.Instance.gold += coin;

        buttons = new List<MMButton>();
        foreach (var reward in rewards)
        {
            MMButton button = MMButton.Create();
            buttons.Add(button);
            button.SetParent(this);
            button.SetSize(new Vector2(200, 80));
            button.userinfo = reward + "";

            switch (reward)
            {
                case MMRewardType.Gold:
                    button.SetText("奖励金币");
                    button.AddClickAction(OnClickRewardGoldButton);
                    break;
                case MMRewardType.Unit:
                    button.SetText("奖励英雄");
                    button.AddClickAction(OnClickRewardUnitButton);
                    break;
                case MMRewardType.Skill:
                    button.SetText("奖励技能");
                    button.AddClickAction(OnClickRewardSkillButton);
                    break;
                case MMRewardType.Card:
                    button.SetText("奖励卡牌");
                    button.AddClickAction(OnClickRewardCardButton);
                    break;
                case MMRewardType.Item:
                    button.SetText("奖励物品");
                    button.AddClickAction(OnClickRewardItemButton);
                    break;
            }
        }


        this.SetActive(true);

        UpdateUI();
    }


    public void SetLost()
    {
        isWin = false;
        isLost = true;

        this.SetActive(true);

        MMRewardPanel.instance.CloseUI();

        UpdateUI();
    }


    public void UpdateUI()
    {
        if (isWin)
        {
            mainText.text = "下一关";
        }
        else
        {
            mainText.text = "重新战斗";
        }

        
        float offset = 200f;
        foreach(var button in buttons)
        {
            button.MoveToCenter();
            button.MoveUp(offset);
            offset -= 100;
        }

        goldText.text = MMPlayerManager.Instance.gold + "";
    }


    public void Clear()
    {
        foreach(var button in buttons)
        {
            button.RemoveFromParent();
        }
    }



    public void OnClickMainButton()
    {
        if (isWin)
        {
            MMPlayerManager.Instance.level += 1;
            MMBattleManager.Instance.level += 1;
        }

        this.SetActive(false);
        MMBattleManager.Instance.LoadLevel();
        //MMBattleManager.Instance.EnterPhase(MMBattlePhase.Begin);
    }



    public void OnClickRewardGoldButton()
    {
        //RemoveReward(MMRewardType.Gold);
    }



    public void OnClickRewardUnitButton()
    {
        if(MMPlayerManager.Instance.gold < 10)
        {
            MMTipManager.instance.CreateTip("金币不足");
            return;
        }

        MMPlayerManager.Instance.gold -= 10;
        
        MMRewardPanel.instance.OpenUI();
        MMRewardPanel.instance.LoadUnitPanel();
        //RemoveReward(MMRewardType.Unit);

        UpdateUI();
    }


    public void OnClickRewardCardButton()
    {
        if (MMPlayerManager.Instance.gold < 5)
        {
            MMTipManager.instance.CreateTip("金币不足");
            return;
        }

        MMPlayerManager.Instance.gold -= 5;

        MMRewardPanel.instance.OpenUI();
        MMRewardPanel.instance.LoadCardPanel();
        //RemoveReward(MMRewardType.Unit);

        UpdateUI();
    }



    public void OnClickRewardSkillButton()
    {
        if (MMPlayerManager.Instance.gold < 5)
        {
            MMTipManager.instance.CreateTip("金币不足");
            return;
        }

        MMPlayerManager.Instance.gold -= 5;


        MMRewardPanel.instance.OpenUI();
        MMRewardPanel.instance.LoadSkillPanel();
        //RemoveReward(MMRewardType.Skill);
        UpdateUI();
    }


    public void OnClickRewardItemButton()
    {
        if (MMPlayerManager.Instance.gold < 3)
        {
            MMTipManager.instance.CreateTip("金币不足");
            return;
        }

        MMPlayerManager.Instance.gold -= 3;

        MMRewardPanel.instance.OpenUI();
        MMRewardPanel.instance.LoadItemPanel();
        //RemoveReward(MMRewardType.Item);
        UpdateUI();
    }




    public void RemoveReward(MMRewardType type)
    {
        foreach (var reward in rewards)
        {
            if (reward == type)
            {
                rewards.Remove(reward);
                break;
            }
        }

        //RemoveButton(type);
        UpdateUI();
    }

    public void RemoveButton(MMRewardType type)
    {
        Debug.Log("RemoveButton " + type);
        foreach (var button in buttons)
        {
            if (button.userinfo == type + "")
            {
                buttons.Remove(button);
                Destroy(button.gameObject);
                break;
            }
        }
    }

}
