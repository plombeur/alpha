using UnityEngine;
using System.Collections;

public class MindRonce : MindPlant {

    public MindRonce(Ronce agent) : base(agent)
    { }

	public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindRonce.vivre ...");
        Ronce ronce = ((Ronce)agent);

        ronce.grow();

        base.vivre();
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
