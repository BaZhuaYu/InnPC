using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPlace : MonoBehaviour
{

    public static List<string> places = new List<string>() { "LuoYangCheng", "JiShi", "YouJianKeZhan" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public static string FindOne()
    {
        //MMPlaceNode place1 = MMPlaceNode.Create("LuoYangCheng");
        //MMPlaceNode place2 = MMPlaceNode.Create("JiShi");
        //MMPlaceNode place3 = MMPlaceNode.Create("YouJianKeZhan");

        
        return places[Random.Range(0, places.Count)];
    }


}
