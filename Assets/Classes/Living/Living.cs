﻿using UnityEngine;
using System.Collections;

/*
 * IMPORTANT: Un object Living doit être un rigidbody!!
 */
public abstract class Living : MonoBehaviourAdapter
{
    public const bool DEBUG = true;
    private Mind mind;

    public void construct(Mind mind)
    {
        if (DEBUG)
            Debug.Log("Living.construct");
        this.mind = mind;
    }
	
	protected override void Update () {
        mind.vivre();
	}

    /*
     * Start devra appeller le base.construct(...)
    */
    protected override void Awake()
    {
        onCreate();
    }
    protected abstract void onCreate();
    protected void fd(float pas)
    {
        if (Living.DEBUG)
            Debug.Log("Living.fd " + pas);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        mind.OnTriggerEnter2D(other);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        mind.OnCollisionEnter2D(other);
    }
}