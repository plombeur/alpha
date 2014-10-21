﻿using UnityEngine;
using System.Collections;

public class Ronce : Plant {

    GameObject[] seeds;

    override protected void Awake()
    {
        base.Awake();
    }

    override protected void Update()
    {
        base.Update();
        print(growth.ToString());
    }

    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("Ronce.Start");
        MindRonce mind = new MindRonce(this);
        base.construct(mind, 1000);
    }

    public override void grow()
    {
        if (growth < 100)
            growth += growSpeed * Time.deltaTime;
    }
}
