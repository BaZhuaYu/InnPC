using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDebugManager : MonoBehaviour
{

    public static void Log(string s)
    {
        Debug.Log(s);
    }


    public static void FatalError(string s)
    {
        Debug.LogError(s);
    }

    
}
