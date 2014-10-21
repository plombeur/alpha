using UnityEngine;
using System.Collections;

public class MindLoupAlpha : MindLoup
{

    public MindLoupAlpha(LoupAlpha agent) : base(agent)
    { }

    public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoupAlpha.vivre ...");
        base.vivre();
    }

}