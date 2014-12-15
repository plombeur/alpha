using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserActionManager
{
    private static UserActionManager instance;
    private List<UserActionListener> listeners;
    private bool lockActions = false;

    private UserAction actionInExecute;

    private UserActionManager()
    {
        listeners = new List<UserActionListener>();
    }

    public static UserActionManager getInstance()
    {
        if (instance == null)
            instance = new UserActionManager();
        return instance;
    }

    public bool executeUserAction(UserAction action)
    {
        if (lockActions || (actionInExecute != null && !actionInExecute.isCancelable()))
        {
            foreach (UserActionListener listener in listeners)
                listener.onRejectAction(action);
            return false;
        }
        if (actionInExecute != null)
        {
            foreach (UserActionListener listener in listeners)
                listener.onCancelUserAction(actionInExecute);
            GameObject.Destroy(actionInExecute.gameObject);
        }

        actionInExecute = action;
        action.executeAction();
        foreach (UserActionListener listener in listeners)
            listener.onExecuteUserAction(action);
        return true;
    }

    public void onResultAction(bool result)
    {
        foreach (UserActionListener listener in listeners)
            listener.onFinishUserAction(actionInExecute,result);
        GameObject.Destroy(actionInExecute.gameObject);
        actionInExecute = null;
    }
    public void lockUserActions()
    {
        lockActions = true;
    }
    public void unlockUserActions()
    {
        lockActions = false;
    }
    public void setLockUserActions(bool lockState)
    {
        if (lockState)
            lockUserActions();
        else
            unlockUserActions();
    }
    public void addUserActionListener(UserActionListener listener)
    {
        listeners.Add(listener);
    }
    public void removeUserActionListener(UserActionListener listener)
    {
        listeners.Remove(listener);
    }
}

public interface UserActionListener
{
    void onExecuteUserAction(UserAction action);
    void onCancelUserAction(UserAction action);
    void onFinishUserAction(UserAction action, bool result);
    void onRejectAction(UserAction action);
}