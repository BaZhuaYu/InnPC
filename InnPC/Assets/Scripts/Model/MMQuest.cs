using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMQuest
{

    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;


    public static List<MMQuest> all;
    public static List<MMQuest> quests;



    //public MMNode reward;
    public List<string> options;

    public List<MMUnit> units;
    public List<MMCard> cards;
    public List<MMItem> items;
    public MMPlace place;


    public int id;
    public string key;
    public string displayName;
    public string displayNote;

    public MMQuestType type;
    public int day;
    public int prob;




    public static MMQuest Create(int id)
    {
        if (allValues.ContainsKey(id) == false)
        {
            MMDebugManager.FatalError("MMQuest Create: " + id);
        }
        return CreateFromData(allValues[id]);
    }


    public static MMQuest CreateFromData(string s)
    {
        string[] values = s.Split(',');

        MMQuest card = new MMQuest();
        card.id = int.Parse(values[allKeys["ID"]]);
        card.key = values[allKeys["Key"]];
        card.displayName = values[allKeys["Name"]];
        card.displayNote = values[allKeys["Note"]];

        int.TryParse(values[allKeys["Prob"]], out card.prob);
        int.TryParse(values[allKeys["Day"]], out card.day);

        card.options = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string o = values[allKeys["Option" + i]];
            if(o == null)
            {
                continue;
            }

            if(o == "")
            {
                continue;
            }

            card.options.Add(o);
        }

        card.LoadOptions();

        return card;
    }


    public static void Init()
    {
        all = new List<MMQuest>();
        quests = new List<MMQuest>();

        foreach (var temp in allValues.Values)
        {
            MMQuest quest = MMQuest.CreateFromData(temp);
            all.Add(quest);
            if (quest.prob > 0)
            {
                quests.Add(quest);
            }
        }

        if (all.Count == 0)
        {
            MMDebugManager.FatalError("public static List<MMCard> FindAll()");
        }
    }




    private void LoadOptions()
    {

        if (id == 1)
        {
            this.units = new List<MMUnit>();
            this.units.Add(MMUnit.FindRandomOne());

            this.options = new List<string>();
            this.options.Add("招募");
            this.options.Add("放弃");
        }
        else if (id == 2)
        {
            this.cards = MMCard.FindRandomCount(3);

            this.options = new List<string>();
            foreach (var card in this.cards)
            {
                this.options.Add(card.displayName);
            }

        }
        else if (id == 3)
        {
            this.items = new List<MMItem>();
            this.items.Add(MMItem.Create(1));

            this.options = new List<string>();
        }
        else if (id == 5)
        {
            this.items = new List<MMItem>();
            this.items.Add(MMItem.Create(1));

            this.options = new List<string>();
            this.options.Add("获得");
        }
        else if (id == 6)
        {
            this.place = MMPlace.Create(2);

            this.options = new List<string>();
            this.options.Add("获得");
        }
        else if (id == 7)
        {
            this.cards = new List<MMCard>();
            this.cards.Add(MMCard.Create(1003));

            this.options = new List<string>();
            this.options.Add("获得");
        }
        else if (id == 101)
        {
            this.type = MMQuestType.RewardPlace;
            this.place = MMPlace.FindRandomOneWithClss(1);
        }
        else if (id == 102)
        {
            this.type = MMQuestType.RewardUnit;
            this.units = new List<MMUnit>();
            this.units.Add(MMUnit.FindRandomOne());
        }
        else if (id == 103)
        {
            this.type = MMQuestType.RewardItem;
            this.items = MMItem.FindRandomCount(1);
        }
        else if (id == 105)
        {
            this.type = MMQuestType.RewardItem;
            this.items = new List<MMItem>();
            this.items.Add(MMItem.Create(3));
        }
        else if (id == 106)
        {
            this.type = MMQuestType.RewardPlace;
            this.place = MMPlace.FindRandomOne();
        }
        else if (id == 107)
        {
            this.type = MMQuestType.None;
            this.cards = new List<MMCard>();
            this.cards.Add(MMCard.Create(1225));
        }
        else if (id == 110)
        {
            this.type = MMQuestType.None;
        }
    }


}
