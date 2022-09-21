﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMDebugManager : MonoBehaviour
{

    public static MMDebugManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static void Log(string s)
    {
        Debug.Log(s);
    }


    public static void FatalError(string s = "")
    {

    }

    public void OnClick() {
        ShowSourceTarget();
    }
        


    public void ShowSourceTarget()
    {
        Debug.Log(MMBattleManager.instance.cellSource.nodeUnit.key);
        Debug.Log(MMBattleManager.instance.cellTarget.nodeUnit.key);
    }


}
