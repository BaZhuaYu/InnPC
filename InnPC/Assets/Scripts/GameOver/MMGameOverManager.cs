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
        rewards.Add(MMRewardType.Unit);
        rewards.Add(MMRewardType.Skill);

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

        buttons = new List<MMButton>();
        float offset = 200f;
        foreach(var reward in rewards)
        {
            MMButton button = MMButton.Create();
            buttons.Add(button);
            button.SetParent(this);
            button.MoveUp(offset);
            offset -= 100;
            button.SetSize(new Vector2(200, 80));

            switch(reward)
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
            }
            
        }

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
            MMBattleManager.Instance.level += 1;
        }

        this.SetActive(false);
        MMBattleManager.Instance.Clear();
        MMBattleManager.Instance.LoadLevel();
        MMBattleManager.Instance.EnterPhase(MMBattlePhase.Begin);
    }



    public void OnClickRewardGoldButton()
    {
        RemoveReward(MMRewardType.Gold);
        Clear();
    }

    public void OnClickRewardUnitButton()
    {
        MMRewardPanel.instance.OpenUI();
        MMRewardPanel.instance.LoadUnitPanel();
        RemoveReward(MMRewardType.Unit);
        Clear();
    }

    public void OnClickRewardSkillButton()
    {
        MMRewardPanel.instance.OpenUI();
        MMRewardPanel.instance.LoadSkillPanel();
        RemoveReward(MMRewardType.Skill);
        Clear();
    }

    public void OnClickRewardCardButton()
    {
        RemoveReward(MMRewardType.Card);
        Clear();
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
    }
}
