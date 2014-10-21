using UnityEngine;
using System.Collections;

public class MindLoupBeta : MindLoup
{

    public MindLoupBeta(LoupBeta agent)
        : base(agent)
    { }
    public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoupBeta.vivre ...");
        //CHECK LOUP ALPHA
        base.vivre();
    }

}