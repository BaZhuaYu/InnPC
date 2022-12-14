﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPlayerManager : MonoBehaviour
{

    public static MMPlayerManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public List<MMUnit> heroes;

    public int diamond;


    
    void Start()
    {
        heroes = new List<MMUnit>();
        diamond = 100;
    }

    
    public void AddHero(MMUnit unit)
    {
        this.heroes.Add(unit);
    }




}