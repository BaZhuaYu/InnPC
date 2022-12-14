using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMPlaceNode : MMNode
{

    public MMNode avatar;
    public Text textName;
    public Text textNote;
    public GameObject bgPrice;
    public Text textPrice;

    [HideInInspector]
    public string key;
    public int id;
    public string displayName;
    public string displayNote;
    public int price;

    bool isEnabled;


    // Start is called before the first frame update
    void Start()
    {
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetEnable(bool flag)
    {
        this.isEnabled = flag;
        UpdateUI();
    }




    void UpdateUI()
    {
        this.textName.text = this.displayName;
        this.textNote.text = this.displayNote;
        this.textPrice.text = "" + this.price;
        this.avatar.LoadImage("Places/Place_" + this.key);
        
        if(isEnabled)
        {
            bgPrice.gameObject.SetActive(true);
        }
        else
        {
            bgPrice.gameObject.SetActive(false);
        }
    }




    public static MMPlaceNode Create(string key)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMPlaceNode") as GameObject);
        obj.name = "MMPlaceNode";
        MMPlaceNode ret = obj.GetComponent<MMPlaceNode>();
        ret.key = key;
        ret.isEnabled = true;

        switch (key)
        {
            case "LuoYangCheng":
                ret.displayName = "������";
                ret.displayNote = "����һ�ſ���";
                ret.price = 5;
                break;

            case "JiShi":
                ret.displayName = "����";
                ret.displayNote = "����һ����Ʒ";
                ret.price = 3;
                break;

            case "YouJianKeZhan":
                ret.displayName = "�м��ջ";
                ret.displayNote = "����һ������";
                ret.price = 8;
                break;

            default:
                ret.displayName = "Ĭ�ϵص�";
                ret.displayNote = "Ĭ�ϵص�";
                ret.price = 99;
                break;
        }

        ret.UpdateUI();
        return ret;
    }


}
