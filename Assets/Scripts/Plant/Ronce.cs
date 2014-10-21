using UnityEngine;
using System.Collections;

public class Ronce : Plant {

    GameObject[] seeds;

    override protected void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        health = 10;
    }

    override protected void Update()
    {
        base.Update();
        /*print("Health : " + health.ToString());
        print("Nutriments :" + nutriments.ToString());
        print("Growth : " + growth.ToString());*/
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
        base.grow();

        float energyChangeValue = growSpeed * Time.deltaTime;

        if (nutriments >= 0)
        {
            nutriments -= energyChangeValue;
            growth += energyChangeValue;
        }
        else
        {
            health -= energyChangeValue;
        }
    }
}
