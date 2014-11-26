using UnityEngine;
using System.Collections;

public class Arbre : Plant {
	protected override void onCreate()
	{
		if (Living.DEBUG)
			Debug.Log("Arbre.Start");
		MindArbre mind = new MindArbre(this);
		base.construct(mind, 100);
	}

    public override void grow()
    {
        base.grow();

        float energyChangeValue = growSpeed * Time.deltaTime;

        if (nutriments >= 0)
        {
            nutriments -= energyChangeValue;
            growth += energyChangeValue;
        }
        else
        {
            health -= energyChangeValue;
        }
    }
}
