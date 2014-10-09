using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Action
{
    private string name;
    private ActionPendingList pendingList;
    private bool isInit;
    private bool isEnabled;

    public Action(string name)
    {
        this.name = name;
        isEnabled = true;
    }
    public string getName()
    {
        return name;
    }
    public ActionPendingList getActionPendlingList()
    {
        return pendingList;
    }
    public void setActionPendingList(ActionPendingList pendingList) // reserved to ActionPendingList class
    {
        this.pendingList = pendingList;
    }
    public bool execute(float deltaTime)
    {
        if (!isInit)
        {
            isInit = true;
            return onStart(deltaTime);
        }
        else if (!isEnabled)
        {
            isEnabled = true;
            return onResume(deltaTime);
        }
        else
            return onUpdate(deltaTime);
    }
    public void pause()
    {
        isEnabled = false;
        onPause();
    }
    public void remove()
    {
        if (pendingList == null)
            return;

        pendingList.removeAction(this);
        onRemove();
    }
    public abstract float getPriority();
    protected abstract bool onStart(float deltaTime);
    protected abstract bool onUpdate(float deltaTime);
    protected abstract void onRemove();
    protected abstract void onPause();
    protected abstract bool onResume(float deltaTime);
}
class ActionComparer : IComparer<Action>
{
    public int Compare(Action x, Action y)
    {
        if (x.getPriority() > y.getPriority())
            return 1;
        else if (x.getPriority() < y.getPriority())
            return -1;
        return 0;
    }
}