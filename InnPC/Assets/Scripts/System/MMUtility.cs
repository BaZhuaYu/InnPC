using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMUtility : MonoBehaviour
{
    

    public static Text CreateText(string name)
    {
        GameObject obj = new GameObject(name);
        Text ret = obj.AddComponent<Text>();
        ret.fontSize = 34;
        ret.SetNativeSize();
        return ret;
    }

    
    
}


public enum MMBattleState
{
    Begin,
    PlayerRound,
    EnemyRound,
    End
}


public enum MMNodeHighlight
{
    Normal,
    Yellow,
    Red,
    Blue,
    Green
}


public enum MMNodeState
{
    Normal,
    Yellow,
    Red,
    Blue,
    Green
}


public enum MMArea
{
    Single,
    Row,
    Col,
    Beside,
    Behind
}


public enum MMBattleUXState
{
    Normal,
    SelectSour,
    SourMoved,
    SelectCard,
}