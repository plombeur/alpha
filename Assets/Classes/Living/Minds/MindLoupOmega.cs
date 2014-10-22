using UnityEngine;
using System.Collections;

public class MindLoupOmega : MindLoup
{

    public MindLoupOmega(LoupOmega agent)
        : base(agent)
    { }

    public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoupOmega.vivre ...");
        base.vivre();
    }

}