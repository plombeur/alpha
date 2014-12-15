using UnityEngine;
using System.Collections;

public class Move : Objectif, UserActionListener
{
    void Awake()
    {
        detail = "Les loups sont des animaux nomades, tu dois donc apprendre à déplacer ta meute : pour se faire, utilise le clic droit.";
    }
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
        UserActionManager.getInstance().addUserActionListener(this);
        base.activate();
    }

    public void onExecuteUserAction(UserAction action)
    {
        if ((action as UserActionMoveTo) != null)
        {
            StartCoroutine(disconnect());
        }
    }

    public void onCancelUserAction(UserAction action)
    {
    }

    public void onFinishUserAction(UserAction action, bool result)
    {
    }

    public void onRejectAction(UserAction action)
    {
    }

    IEnumerator disconnect()
    {
        yield return new WaitForFixedUpdate();
        UserActionManager.getInstance().removeUserActionListener(this);
        yield return new WaitForSeconds(2.0f);
        achieve();
    }
}
