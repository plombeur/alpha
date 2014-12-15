using UnityEngine;
using System.Collections;

public class EndTutorial : Objectif {
    public ToolTip TipEnd;

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        checkAchievement();
    }

    /**
     * Check display condition on update.
     * Must be overidden.
     * */
    protected override void checkAchievement()
    {
    }

    public override void activate()
    {
        //Debug.Log("Activate fils");
        base.activate();
        TipEnd.enabled = true;
        achieve();
    }
}
