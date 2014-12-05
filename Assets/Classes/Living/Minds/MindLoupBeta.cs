using UnityEngine;
using System.Collections;

public class MindLoupBeta : MindLoupInferieur
{
    private bool worldBarInit = false;

    public MindLoupBeta(LoupBeta agent)
        : base(agent)
    { }
    public override void vivre()
    {
        LoupBeta beta = (LoupBeta)agent;
        beta.threat += Mathf.Min(Time.deltaTime * beta.getAggressivite(), beta.THREAT_MAX - beta.threat);
        if (!worldBarInit)
        {
            UIWorld.getInstance().registerWorldLifeThreatBar((LoupBeta)agent);
            worldBarInit = true;
        }
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