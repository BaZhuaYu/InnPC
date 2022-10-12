using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDataManager : MonoBehaviour
{

    public static MMDataManager Instance;


    private void Awake()
    {
        Instance = this;

        string[] skillData = MMDataManager.ReadFile("Data/InnPC - Skill");
        string[] unitData = MMDataManager.ReadFile("Data/InnPC - Unit");
        string[] levelData = MMDataManager.ReadFile("Data/InnPC - Level");
        string[] itemData = MMDataManager.ReadFile("Data/InnPC - Item");

        Deserialize(skillData, out MMSkill.allKeys, out MMSkill.allValues);
        Deserialize(unitData, out MMUnit.allKeys, out MMUnit.allValues);
        Deserialize(levelData, out MMLevel.allKeys, out MMLevel.allValues);
        Deserialize(itemData, out MMItem.allKeys, out MMItem.allValues);
        
    }


    void Start()
    {
        
        MMSkill.Init();
        MMUnit.Init();
        MMItem.Init();
    }


    public static string[] ReadFile(string f)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(f);
        string[] lines = textAsset.text.Split('\n');
        return lines;
    }
    

    public static void Deserialize(string[] ss, out Dictionary<string, int> allKeys, out Dictionary<int, string> allValues)
    {
        allKeys = new Dictionary<string, int>();
        allValues = new Dictionary<int, string>();

        int index = 0;
        foreach (var s in ss)
        {
            string[] values = s.Split(',');

            //if(values[0] == null || values[0] == "")
            //{
            //    continue;
            //}

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
