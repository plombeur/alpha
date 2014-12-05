using UnityEngine;
using System.Collections;

public abstract class Plant : Living
{
    Vector3 maxScale;

    protected float health;
    public float maxHealth;

    protected float growth;
    public float maxGrowth;

    public float growSpeed;
    public float nutriments;
    public bool isAdult;

    public void construct(MindPlant mind)
    {
        if (DEBUG)
            Debug.Log("Plant.construct");
        health = maxHealth;

        this.growth = 0;

        this.nutriments = maxHealth / 2;

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

    public void setStartingGrowth(float startGrowth)
    {
        if (growth == 0)
            growth = startGrowth;
        else Debug.Log("Can't change a plant's growth, must be done at creation.");
    }
}
