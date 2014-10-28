using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionPendingList
{
    public static bool DEBUG_ALL = false;
    private Animal animal;
    private static string DEBUG_TAG = "[ActionPendingList]";
    private List<Action> actions = new List<Action>();
    private bool debug = false;
    private ActionComparer actionComparer = new ActionComparer();

    public void addAction(Action action)
    {
        if (actions.Contains(action))
        {
            if (debug || DEBUG_ALL)
                Debug.Log(DEBUG_TAG + "Action already exist : " + action.getName()+", dont adding it");
            return;
        }
        if (debug || DEBUG_ALL)
            Debug.Log(DEBUG_TAG + " Adding new Action : " + action.getName());
        actions.Add(action);
        action.setActionPendingList(this);
    }

    public void removeAction(Action action)
    {

        if (actions.Remove(action))
        {
            if (debug || DEBUG_ALL)
                Debug.Log(DEBUG_TAG + " Removing Action : " + action.getName());
            action.remove();
        }
    }
    /// <summary>
    /// Execute the first action in the list
    /// </summary>
    /// <param name="deltaTime">the time elapsed between the previous frame and this frame</param>
    /// <returns>true if a action is executed</returns>
    public bool execute(float deltaTime)
    {
        if (debug && actions.Count == 0)
            Debug.Log(DEBUG_TAG + " Executing : Nothing to execute (empty)");
        bool result = false;
        while (actions.Count > 0 && !result)
        {
            sortActions();
            if (debug || DEBUG_ALL)
                Debug.Log(DEBUG_TAG + " Executing : " + getActualAction().getName());
            result = actions[0].execute(deltaTime);
            if (!result && (debug || DEBUG_ALL))
                Debug.Log(DEBUG_TAG + " The action didn't consumed the token" + (actions.Count > 0 ? ", looping for execute the next action" : " and the pending list is now empty"));
        }
        return result;
    }
    private void sortActions()
    {
        if (actions.Count > 0)
        {
            Action save = actions[0];
            actions.Sort(actionComparer);
            if (save != actions[0])
                save.pause();
        }
    }
    public void setDebug(bool debug)
    {
        this.debug = debug;
    }

    public int size()
    {
        return actions.Count;
    }
    public Animal getAnimal()
    {
        return animal;
    }
    public void setAnimal(Animal animal)
    {
        this.animal = animal;
    }
    /// <summary>
    /// Return the first action in the list
    /// </summary>
    /// <returns>the first action or null if no action found</returns>
    public Action getActualAction()
    {
        if (actions.Count > 0)
            return actions[0];
        return null;
    }
}
