using UnityEngine;
using System.Collections;

public class MindRonce : MindPlant {

    public MindRonce(Ronce agent) : base(agent)
    { }
	public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindRonce.vivre ...");
        Plant plant = ((Plant)agent);

        plant.grow();

        base.vivre();
    }

}
