using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMCardIndexPanel : MMNode
{
    public static MMCardIndexPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Button buttonClose;
    List<MMCardNode> cards;


    // Start is called before the first frame update
    void Start()
    {
        buttonClose = gameObject.transform.Find("Close").GetComponent<Button>();
        buttonClose.onClick.AddListener(CloseUI);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Accept(List<MMCardNode> cards)
    {
        this.cards = cards;
        float xoffset = 0;
        float yoffset = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetParent(this);
            cards[i].SetActive(true);
            cards[i].MoveToParentLeftOffset(xoffset);
            cards[i].MoveToParentTopOffset(yoffset);

            xoffset += cards[i].FindWidth() * 1.1f;
            
            if (i % 5 == 4)
            {
                xoffset = 0;
                yoffset += cards[i].FindHeight() * 1.1f;
            }
        }

    }


    public void Clear()
    {
        foreach(var card in cards)
        {
            card.SetActive(false);
        }
    }


    public override void CloseUI()
    {
        Clear();
        base.CloseUI();
    }


}
