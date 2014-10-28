using UnityEngine;
using System.Collections;

public class MindLoupBeta : MindLoupInferieur
{

    public MindLoupBeta(LoupBeta agent)
        : base(agent)
    { }
    public override void vivre()
    {
        base.vivre();
    }
    protected override void randomAction()
    {
        LoupOmega random = ((Loup)agent).randomLoupOmegaSeen();
        if (random != null && Random.Range(1, 3) == 1)
            actionList.addAction(new A_TaquinerOmega(random));
        else
            base.randomAction();
    }

}