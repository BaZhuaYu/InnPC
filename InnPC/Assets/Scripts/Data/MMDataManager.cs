using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDataManager : MonoBehaviour
{


    private void Awake()
    {

        string[] skillData = MMDataManager.ReadFile("Data/InnPC - Skill");
        Debug.Log(skillData);
        MMSkillData.Deserialize(skillData);

        string[] unitData = MMDataManager.ReadFile("Data/InnPC - Unit");
        Debug.Log(unitData);
        MMUnitData.Deserialize(unitData);

        //string[] levelData = MMDataManager.ReadFile("Data/InnPC - Level");
        //Deserialize(levelData, out MMLevelData.allKeys, out MMLevelData.allValues);

    }


    void Start()
    {

    }


    public static string[] ReadFile(string f)
    {
        Debug.Log(f);
        TextAsset textAsset = Resources.Load<TextAsset>(f);
        string[] lines = textAsset.text.Split('\n');
        Debug.Log(textAsset.text);
        return lines;
    }
    

    public static void Deserialize(string[] ss, out Dictionary<string, int> allKeys, out Dictionary<int, string> allValues)
    {
        allKeys = new Dictionary<string, int>();
        allValues = new Dictionary<int, string>();

        MMUnit.all = new List<MMUnit>();

        int index = 0;
        foreach (var s in ss)
        {
            string[] values = s.Split(',');
            if (index == 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == "End")
                    {
                        break;
                    }
                    allKeys.Add(values[i], i);
                }
            }
            else
            {
                int id = int.Parse(values[allKeys["ID"]]);
                allValues.Add(id, s);
            }
            index++;
        }
    }
}
