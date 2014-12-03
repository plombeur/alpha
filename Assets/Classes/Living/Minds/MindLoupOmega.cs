using UnityEngine;
using System.Collections;

public class MindLoupOmega : MindLoupInferieur
{
    private bool worldBarInit = false;

    public MindLoupOmega(LoupOmega agent)
        : base(agent)
    { }

    public override void vivre()
    {
        if (!worldBarInit)
        {
            UIWorld.getInstance().registerWorldLifeBar((Loup)agent);
            worldBarInit = true;
        }
        base.vivre();
    }

}