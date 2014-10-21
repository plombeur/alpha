using UnityEngine;
using System.Collections;

public abstract class Plant : Living
{
    protected int health;
    protected float growth;
    protected float growSpeed;

    public void construct(MindPlant mind, int health)
    {
        if (Living.DEBUG)
            Debug.Log("Plant.construct");
        this.health = health;
        this.growth = 0;
        this.growSpeed = 1;
        base.construct(mind);
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
