using UnityEngine;
using System.Collections;

public class MindRonce : MindPlant {

    public MindRonce(Ronce agent) : base(agent)
    { }
	public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoup.vivre ...");
        Plant plant = ((Plant)agent);
        //plant.fd(1);
        base.vivre();
    }

}
