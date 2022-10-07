using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMUnitData
{
    
    public static Dictionary<string, int> allKeys;
    public static Dictionary<int, string> allValues;

    
    //public static void Deserialize(string[] ss)
    //{
    //    allKeys = new Dictionary<string, int>();
    //    allValues = new Dictionary<int, string>();

    //    MMUnit.all = new List<MMUnit>();

    //    int index = 0;
    //    foreach (var s in ss)
    //    {
    //        string[] values = s.Split(',');
    //        if (index == 0)
    //        {
    //            for (int i = 0; i < values.Length; i++)
    //            {
    //                if (values[i] == "End")
    //                {
    //                    break;
    //                }
    //                allKeys.Add(values[i], i);
    //            }
    //        }
    //        else
    //        {
    //            int id = int.Parse(values[allKeys["ID"]]);
    //            allValues.Add(id, s);
    //        }
    //        index++;
    //    }
    //}

    
}
