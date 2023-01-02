using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMTipManager : MMNode
{

    public static MMTipManager instance;


    private void Awake()
    {
        instance = this;
    }


    public void CreateTip(string s)
    {
        Debug.LogWarning(s);
        GameObject obj = Resources.Load("Prefabs/MMTipNode") as GameObject;
        MMTipNode tip = Instantiate(obj).GetComponent<MMTipNode>();
        AddChild(tip);
        tip.Show(s);
    }

    
    public void CreateSkillTip(MMSkillNode skill)
    {
        GameObject obj = Resources.Load("Prefabs/MMSkillTip") as GameObject;
        

        GameObject tip = Instantiate(obj);
        tip.transform.SetParent(MMBattleManager.Instance.background.transform);
        tip.transform.localPosition = new Vector3(-800, 0, 0);
        Text text = tip.GetComponentInChildren<Text>();
        text.text = skill.unit.displayName + " " + skill.displayName + "\n" + skill.displayNote;
    }

}
