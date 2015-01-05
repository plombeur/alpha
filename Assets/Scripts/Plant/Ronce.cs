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
    }

    protected override void onCreate()
    {
        if (DEBUG)
            Debug.Log("Ronce.Start");
        MindRonce mind = new MindRonce(this);
        base.construct(mind);
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

        if (base.isAdult && nutriments >= 2 * maxHealth / 3)
        {
            if (fruitPrefab != null)
            {
                nutriments /= 2;
                GameObject child = Instantiate(fruitPrefab) as GameObject;
                child.transform.parent = transform;
                Vector2 pos = transform.position;
                pos.x += (Random.Range(-1, 1) * 0.25F);
                pos.y += (Random.Range(-1, 1) * 0.25F);

                child.transform.position = pos;
                child.gameObject.name = "Mures";
            }
        }
    }

    public void setStartingGrowth(float startGrowth)
    {
        base.setStartingGrowth(startGrowth);
    }
   void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.GetComponent<Animal>() != null)
            GetComponentInChildren<FMOD_StudioEventEmitter>().Play();
   }
   void OnTriggerExit2D(Collider2D collision)
   {
       if (collision.gameObject.GetComponent<Animal>() != null)
           GetComponentInChildren<FMOD_StudioEventEmitter>().Play();
    }
}
