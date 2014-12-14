using UnityEngine;
using System.Collections;

public class LoupAlpha : Loup
{

    //Pour la chasse
    public GameObject prefabTarget;
    public GameObject prefabBlackTarget;
    public GameObject prefabYellowTarget;

    protected override void onCreate()
    {
        if (DEBUG)
            Debug.Log("LoupAlpha.Start");
        MindLoup mind = new MindLoupAlpha(this);
        base.construct(mind);
    }

    public GameObject cibler(Living living)
    {
        GameObject cible = (GameObject)GameObject.Instantiate(prefabTarget);
        cible.transform.parent = living.transform;
        cible.GetComponent<FollowTarget>().target = living.gameObject;
        return cible;
    }
}