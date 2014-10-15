using UnityEngine;
using System.Collections;

public abstract class Mind {

    protected Living agent;
    public Mind(Living agent)
    {
        this.agent = agent;
    }
    public abstract void vivre();

    public virtual void OnTriggerEnter2D(Collider2D other)
    { }

    public virtual void OnCollisionEnter2D(Collision2D other)
    { }
}