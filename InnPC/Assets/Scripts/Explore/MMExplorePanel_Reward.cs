using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMExplorePanel : MMNode
{
    
    public void GainMinion(MMUnit unit)
    {
        this.units.Add(unit);
        MMTipManager.instance.CreateTip("获得新的随从");
        UpdateUI();
    }

    public void GainCard(MMCard card)
    {
        this.cards.Add(card);
        MMTipManager.instance.CreateTip("获得新的卡牌");
        UpdateUI();
    }

    public void GainPlace(MMPlace place)
    {
        this.places.Add(MMPlaceNode.Create(place));
        MMTipManager.instance.CreateTip("开启新地点");
        UpdateUI();
    }

    public void GainQuest(MMQuest quest)
    {
        this.quests.Add(quest);
        UpdateUI();
    }

    public void GainExp(int value)
    {
        tansuoExp += value;
        MMTipManager.instance.CreateTip("获得" + value + "点经验");

        if (tansuoExp >= 10)
        {
            tansuoExp -= 10;
            textEventCards.text = "" + Random.Range(0,30);
            MMTipManager.instance.CreateTip("新的江湖事件加入");
        }

        UpdateUI();
    }
    
    public void GainGold(int value)
    {
        this.tansuoGold += value;
        MMTipManager.instance.CreateTip("获得" + value + "两银子");
        UpdateUI();
    }



    public bool CheckTime(int value)
    {
        if(tansuoTime + value > 12)
        {
            return false;
        }

        return true;
    }

    public void GainTime(int value)
    {
        tansuoTime += value;
        UpdateUI();
    }


}
