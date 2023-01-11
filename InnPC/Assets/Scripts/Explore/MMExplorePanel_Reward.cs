using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMExplorePanel : MMNode
{
    
    public void GainMinion(MMUnit unit)
    {
        this.units.Add(unit);
        MMTipManager.instance.CreateTip("����µ����");
        UpdateUI();
    }

    public void GainCard(MMCard card)
    {
        this.cards.Add(card);
        MMTipManager.instance.CreateTip("����µĿ���");
        UpdateUI();
    }

    public void GainPlace(MMPlace place)
    {
        this.places.Add(MMPlaceNode.Create(place));
        MMTipManager.instance.CreateTip("�����µص�");
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
        MMTipManager.instance.CreateTip("���" + value + "�㾭��");

        if (tansuoExp >= 10)
        {
            tansuoExp -= 10;
            textEventCards.text = "" + Random.Range(0,30);
            MMTipManager.instance.CreateTip("�µĽ����¼�����");
        }

        UpdateUI();
    }
    
    public void GainGold(int value)
    {
        this.tansuoGold += value;
        MMTipManager.instance.CreateTip("���" + value + "������");
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
