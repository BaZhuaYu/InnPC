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


        Deserialize(skillData, out MMSkillData.allKeys, out MMSkillData.allValues);
        Deserialize(unitData, out MMUnitData.allKeys, out MMUnitData.allValues);
        Deserialize(levelData, out MMLevelData.allKeys, out MMLevelData.allValues);
        Deserialize(itemData, out MMItem.allKeys, out MMItem.allValues);

        MMSkill.all = new List<MMSkill>();
        MMUnit.all = new List<MMUnit>();
        //MMLevel.all = new List<MMLevel>();
        MMItem.all = new List<MMItem>();
        
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
