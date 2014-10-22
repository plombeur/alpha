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
        /**** CODE A MODIFIER QUAND LES PERCEPTS SERONT FONCTIONNELS *****/
        GameObject obj = GameObject.Find("AlphaWolf");
        Vector2 posAlpha = obj.GetComponent<Transform>().position;
        Vector2 posThis = agent.GetComponent<Transform>().position;
        float dist = Mathf.Sqrt(Mathf.Pow(posAlpha.x - posThis.x, 2) + Mathf.Pow(posAlpha.y - posThis.y, 2));
        if( dist > 10 )
        {
            actionList.addAction(new A_RejoindreTroupe());
        }
        /*****************************************************************/
        base.vivre();
    }

}