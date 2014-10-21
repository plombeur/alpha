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
    private float timer;
    private bool timerLocker;

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
    /// <summary>
    /// This method is used to lock the action while the time in the timer is not reached
    /// </summary>
    /// <param name="timerIsLocking">true to lock</param>
    public void setTimerLock(bool timerIsLocking)
    {
        this.timerLocker = timerIsLocking;
    }
    /// <summary>
    /// Set the time remaining in the timer
    /// </summary>
    /// <param name="secondsToSet">The time in seconds to set</param>
    public void setTimer(float secondsToSet)
    {
        this.timer = secondsToSet;
    }
    /// <summary>
    /// Add seconds from the current time in the timer
    /// </summary>
    /// <param name="secondsToAdd">amount seconds to add</param>
    public void addTimer(float secondsToAdd)
    {
        this.timer += secondsToAdd;
    }
    /// <summary>
    /// 
    /// Withdraw seconds from the current time in the timer
    /// </summary>
    /// <param name="secondsToRemove">amount seconds to withdraw</param>
    public void removeTimer(float secondsToRemove)
    {
        this.timer = Mathf.Max(timer - secondsToRemove, 0);
    }
    /// <summary>
    /// Used to get the state of the timer.
    /// </summary>
    /// <returns>true if the time is reached (= 0)</returns>
    public bool timerReached()
    {
        return timer <= 0;
    }
    /// <summary>
    /// Allow to know the number of seconds remaining
    /// </summary>
    /// <returns>the seconds remaining</returns>
    public float getActualTimer()
    {
        return timer;
    }
    /// <summary>
    /// Don't call this method, only the ActionPendingList must call this method
    /// </summary>
    /// <param name="deltaTime">the time elapsed between the previous frame and this frame</param>
    /// <returns>true if the function did something (action token used)</returns>
    public bool execute(float deltaTime)
    {
        timer = Mathf.Max(timer - deltaTime, 0);
        if (timerLocker && !timerReached())
            return true;
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
    /// <summary>
    /// Don't call this method, only the ActionPendingList must call this method
    /// </summary>
    public void pause()
    {
        isEnabled = false;
        onPause();
    }
    /// <summary>
    /// Remove this action from the ActionPendingList
    /// </summary>
    public void remove()
    {
        if (pendingList == null)
            return;

        pendingList.removeAction(this);
        onRemove();
    }
    public abstract float getPriority();
    /// <summary>
    /// Called the first time this action is executed
    /// If this method is not override, she call onUpdate(float deltaTime)
    /// </summary>
    /// <param name="deltaTime">the time elapsed between the previous frame and this frame</param>
    /// <returns>true if the function did something (action token used)</returns>
    protected bool onStart(float deltaTime)
    {
        return onUpdate(deltaTime);
    }
    /// <summary>
    /// Called each time this action is executed
    /// Except the first time if onStart(float deltaTime) is overrided, and when the action is resumed if onResume(float deltaTime) is overrided
    /// </summary>
    /// <param name="deltaTime">the time elapsed between the previous frame and this frame</param>
    /// <returns>true if the function did something (action token used)</returns>
    protected abstract bool onUpdate(float deltaTime);
    /// <summary>
    /// Called when the action is removed from the ActionPendingList (dont consume a action tocken)
    /// </summary>
    protected void onRemove()
    {

    }
    /// <summary>
    /// Called when another action get the focus of the ActionPendingList
    /// </summary>
    protected void onPause()
    {

    }
    /// <summary>
    /// the action return on the top of the ActionPendingList and is executed
    /// Consume a action token
    /// </summary>
    /// <param name="deltaTime">the time elapsed between the previous frame and this frame</param>
    /// <returns>true if the function did something (action token used)</returns>
    protected bool onResume(float deltaTime)
    {
        return onUpdate(deltaTime);
    }
    public Animal getAnimal()
    {
        return pendingList.getAnimal();
    }
    /// <summary>
    /// This action MUST BE CORRECTLY OVERRIDED, this method must determine if this action is equal an other action with THE SAME CLASS
    /// this method is used by the ActionPendingList when addAction() is called, if the action is similar to another action, it's not necessary to add it.
    /// </summary>
    /// <param name="obj">The other action with the same class</param>
    /// <returns>true if the action in parameter have exactly the same goal of this action</returns>
    public override abstract bool Equals(System.Object obj);
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