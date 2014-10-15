using UnityEngine;
using System.Collections;

public class MindPlant : Mind
{
    public MindPlant(Plant agent)
        : base(agent)
    {
    }
    public override void vivre()
    {
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
