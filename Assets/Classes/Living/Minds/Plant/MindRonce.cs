﻿using UnityEngine;
using System.Collections;

public class MindRonce : MindPlant
{
    public MindRonce(Ronce agent)
        : base(agent)
    {
    }

    public override void vivre()
    {
        if (agent.DEBUG)
            Debug.Log("MindRonce.vivre ...");
        Ronce ronce = ((Ronce)agent);

        ronce.grow();
        ronce.reproduce();

        base.vivre();
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
    }
}