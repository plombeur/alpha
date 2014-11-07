using UnityEngine;
using System.Collections;

public class Ronce : Plant {
    public GameObject fruitPrefab;

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
        checkValues();
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

            foreach (Transform child in transform) {
                if (child.gameObject.name == "Mures")
                {
                    Mures script = child.GetComponent<Mures>();
                    script.addNutriment(0);
                    script.grow(this);
                }
            }
        }
        else
        {
            health -= energyChangeValue;
        }
    }

    public override void reproduce()
    {
        base.reproduce();

        if (base.isAdult && nutriments >= 90.0 * maxNutriments / 100.0)
        {
            if (fruitPrefab != null)
            {
                nutriments /= 2;
                GameObject child = Instantiate(fruitPrefab) as GameObject;
                child.transform.parent = transform;
                child.gameObject.name = "Mures";
            }
        }
    }
}
