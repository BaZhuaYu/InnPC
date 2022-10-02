using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMConfigManager 
{
    protected static MMConfigManager _instance = null;

    public static MMConfigManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MMConfigManager();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    
}
