using UnityEngine;
using System.Collections;

public abstract class Plant : Living
{
    protected float health;
    public float maxHealth;

    protected float growth;
    public float maxGrowth;

    protected float growSpeed;

    /*protected*/public float nutriments;
    public float maxNutriments;
    public Vector3 maxScale;
    public bool isAdult;

    public void construct(MindPlant mind, float health)
    {
        if (Living.DEBUG)
            Debug.Log("Plant.construct");
        this.health = health;
        maxHealth = health;

        this.growth = 0;
        this.maxGrowth = 50;
        this.growSpeed = 1;

        this.maxNutriments = 100;
        this.nutriments = maxNutriments;

        maxScale = transform.localScale;
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        isAdult = false;

        base.construct(mind);
    }

    override protected void Update()
    {
        base.Update();
        checkValues();
    }

    public void addNutriments(float value)
    {
        nutriments += value;
    }
    protected void checkValues()
    {
        if (nutriments > maxNutriments)
            nutriments = maxNutriments;

        if (growth > maxGrowth)
            growth = maxGrowth;

        if (!isAdult && growth == maxGrowth)
            isAdult = true;

        if (health <= 0)
            Destroy(this.gameObject);
    }

    public virtual void grow()
    {
        if (!isAdult)
            transform.localScale = maxScale * (growth / maxGrowth);
    }
	public virtual void reproduce()
	{
	}

    protected override void OnCollisionEnter2D(Collision2D other)
    {
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
    }
}
