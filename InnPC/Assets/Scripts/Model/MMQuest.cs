using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMQuest
{

    public int id;
    public string key;

    public MMNode reward;
    public List<MMOption> options;
     
    public List<MMUnit> units;
    public List<MMCard> cards;
    public List<MMItem> items;

    public MMQuestType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public static MMQuest Create(int id)
    {
        MMQuest ret = new MMQuest();
        ret.id = id;
        if(id == 1)
        {
            ret.type = MMQuestType.RewardUnit;
            ret.reward = MMHeroNode.Create(10100);
            ret.units = new List<MMUnit>();
            ret.units.Add(MMUnit.Create(10100));

            ret.options = new List<MMOption>();
        }
        else if(id == 2)
        {
            ret.type = MMQuestType.RewardCard;
            ret.reward = MMCardNode.Create(10101);
            ret.cards = new List<MMCard>();
            ret.cards.Add(MMCard.Create(10101));
            ret.cards.Add(MMCard.Create(10201));
            ret.cards.Add(MMCard.Create(10301));

            
            ret.options = new List<MMOption>();
        }
        else if (id == 3)
        {
            ret.type = MMQuestType.RewardItem;
            ret.reward = MMItemNode.Create(1);
            

            ret.options = new List<MMOption>();
        }

        return ret;
    }


}
