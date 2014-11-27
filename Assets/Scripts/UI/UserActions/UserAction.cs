using UnityEngine;
using System.Collections;

public abstract class UserAction : MonoBehaviourAdapter
{
    public enum UserActionResult
    {
        SUCESS,
        FAILED,
        RUNNING
    }
    private bool running = false;

    public void executeAction()
    {
        running = true;
    }
    void Update()
    {
        if (running)
            switch (onExecuteAction())
            {
                case UserActionResult.SUCESS:
                    UserActionManager.getInstance().onResultAction(true);
                    running = false;
                    break;
                case UserActionResult.FAILED:
                    UserActionManager.getInstance().onResultAction(false);
                    running = false;
                    break;
            }
    }
    public abstract string getActionLabel();
    public abstract bool isCancelable();

    protected abstract UserActionResult onExecuteAction();
}
