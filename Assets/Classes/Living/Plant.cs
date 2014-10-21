using UnityEngine;
using System.Collections;

public abstract class Plant : Living
{
    protected float health;

    protected float growth;
    protected float maxGrowth;

    protected float growSpeed;

    protected float nutriments;
    protected float maxNutriments;

    public void construct(MindPlant mind, float health)
    {
        if (Living.DEBUG)
            Debug.Log("Plant.construct");
        this.health = health;

        this.growth = 0;
        this.maxGrowth = 250;
        this.growSpeed = 1;

        this.maxNutriments = 100;
        this.nutriments = maxNutriments;

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
    private void checkValues()
    {
        if (nutriments > maxNutriments)
            nutriments = maxNutriments;

        if (growth > maxGrowth)
            growth = maxGrowth;

        if (health <= 0)
            Destroy(this.gameObject);
    }

    public virtual void grow()
    {
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
