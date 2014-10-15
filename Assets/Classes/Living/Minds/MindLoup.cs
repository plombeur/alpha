using UnityEngine;
using System.Collections;

public class MindLoup : MindAnimal {

    public MindLoup(Loup agent) : base(agent)
    { }
	public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoup.vivre ...");
        Animal animal = ((Animal)agent);
        animal.fd(1);
        base.vivre();
    }

}