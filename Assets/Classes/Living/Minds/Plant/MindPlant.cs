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

    public override void OnCollisionEnter2D(Collision2D other)
    {
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
    }
}
