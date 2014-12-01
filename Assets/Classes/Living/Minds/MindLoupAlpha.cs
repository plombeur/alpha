using UnityEngine;
using System.Collections;

public class MindLoupAlpha : MindLoup
{

    public MindLoupAlpha(LoupAlpha agent) : base(agent)
    {
    }

    public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoupAlpha.vivre ...");
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

    public void addActionUserAction(A_ActionUser action)
    {
        if (getCurrentAction() as A_ActionUser != null)
        {
            if (action as AU_MoveTo != null && getCurrentAction() as AU_MoveTo != null)
                ((AU_MoveTo)getCurrentAction()).changeTarget(((AU_MoveTo)action).getTarget().x, ((AU_MoveTo)action).getTarget().y);
            else
                removeCurrentAction();
        }
        actionList.addAction(action);
    }

}