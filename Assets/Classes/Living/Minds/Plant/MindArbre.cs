using UnityEngine;
using System.Collections;

public class MindArbre : MindPlant {
	
	public MindArbre(Arbre agent) : base(agent)
	{ }
	public override void vivre()
	{
		if (Living.DEBUG)
			Debug.Log("MindArbre.vivre ...");
		Plant plant = ((Plant)agent);
		
		plant.grow();
		
		base.vivre();
	}

    public override void OnCollisionEnter2D(Collision2D other)
    {
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
    }
}
