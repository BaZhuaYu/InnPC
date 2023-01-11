using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MMPlace
{

    public static MMPlace FindRandomOneInPlaces(List<MMPlace> ps)
    {
        return ps[Random.Range(0,ps.Count)];
    }

    public static MMPlace FindRandomOneWithClss(int value)
    {
        List<MMPlace> places = MMPlace.places.FindAll(p => p.clss == value);
        return FindRandomOneInPlaces(places);
    }




}
