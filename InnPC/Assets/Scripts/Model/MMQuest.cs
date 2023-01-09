using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMQuest
{

    public int id;
    public string key;

    public MMNode reward;
    public List<string> options;

    public List<MMUnit> units;
    public List<MMCard> cards;
    public List<MMItem> items;
    public MMPlace place;

    public MMQuestType type;

    public string displayName;
    public string displayNote;


    public static MMQuest Create(int id)
    {
        MMQuest ret = new MMQuest();
        ret.id = id;
        ret.key = "Card_10000";
        if (id == 1)
        {
            ret.type = MMQuestType.RewardUnit;
            ret.displayName = "招募侠客";
            ret.displayNote = "招募一名侠客";
            ret.reward = MMHeroNode.Create(10100);

            ret.units = new List<MMUnit>();
            ret.units.Add(MMUnit.Create(10100));

            ret.options = new List<string>();
            ret.options.Add("招募");
            ret.options.Add("放弃");
        }
        else if (id == 2)
        {
            ret.type = MMQuestType.RewardCard;
            ret.displayName = "卡牌";
            ret.displayNote = "获得一张卡牌";
            ret.reward = MMCardNode.Create(10101);

            ret.cards = new List<MMCard>();
            MMCard card1 = MMCard.Create(10101);
            MMCard card2 = MMCard.Create(10201);
            MMCard card3 = MMCard.Create(10301);

            ret.cards.Add(card1);
            ret.cards.Add(card2);
            ret.cards.Add(card3);


            ret.options = new List<string>();
            ret.options.Add(card1.displayName);
            ret.options.Add(card2.displayName);
            ret.options.Add(card3.displayName);
        }
        else if (id == 3)
        {
            ret.type = MMQuestType.RewardItem;
            ret.displayName = "物品";
            ret.displayNote = "获得一个物品";
            ret.reward = MMItemNode.Create(1);

            ret.items = new List<MMItem>();
            ret.items.Add(MMItem.Create(1));

            ret.options = new List<string>();
        }
        else if (id == 4)
        {
            ret.type = MMQuestType.RewardPlace;
            ret.displayName = "场景";
            ret.displayNote = "发现一个地点";
            ret.reward = MMPlaceNode.Create(2);
            ret.place = MMPlace.Create(2);


            ret.options = new List<string>();
            ret.options.Add("获得");
        }

        return ret;
    }


}
