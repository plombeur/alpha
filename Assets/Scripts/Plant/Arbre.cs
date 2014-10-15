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
}
