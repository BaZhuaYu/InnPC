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


    public static bool CheckListHasOne<T>(List<T> all, T one)
    {
        //foreach(var temp in all)
        //{
        //    if (temp == one)
        //    {
        //        return true;
        //    }
        //}

        return false;
    }

    public static bool CheckListNotHasOne<T>(List<T> all, T one)
    {
        return !CheckListHasOne<T>(all, one);
    }


}



public enum MMSkillState
{
    Ready,
    NotReady,
    Used
}




public enum MMBattlePhase
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
    None,
    Single,
    Row,
    Col,
    Beside,
    Behind
}


public enum MMSkillKeyWord
{
    Ultimate
}


public enum MMBattleState
{
    Normal,
    SelectSour,
    SourMoved,
    SelectSkill,
    SourDone
}


public enum MMUnitState
{
    Rage,
    Normal,
    Weak,
    Stunned,
    Dead
}


public enum MMUnitPhase
{
    Normal,
    Combo,
    Actived,
    Stunned
}


public enum MMRewardType
{
    Gold,
    Unit,
    Skill,
    Card
}