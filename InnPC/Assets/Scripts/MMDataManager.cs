using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDataManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //ReadFile("body");

        MMSkill skill = MMSkill.Create(1);
        string s = JsonUtility.ToJson(skill, true);
        Debug.Log(s);


        MMSkill skill2 = JsonUtility.FromJson<MMSkill>(s);
        Debug.Log(skill2.id);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public string[] ReadFile(string f)
    {
        Debug.Log(f);
        TextAsset textAsset = Resources.Load<TextAsset>(f);
        string[] lines = textAsset.text.Split('\n');
        Debug.Log(textAsset.text);
        return lines;
    }


    



}
