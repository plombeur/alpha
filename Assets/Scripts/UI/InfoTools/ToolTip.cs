using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour, MemoryListener
{
    public string title;
    public string description;
    public Sprite icon;

    public bool isActivatedElsewhere = false;

    // Use this for initialization
    protected void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        checkTrigger();
    }

    /**
     * Check display condition on update.
     * Must be overidden.
     * */
    protected virtual void checkTrigger()
    {
    }
    /**
     * Check display condition on Memory modification (add only).
     * Must be overidden.
     * */
    protected virtual void checkMemoryModificationTrigger(MemoryBloc bloc)
    {
    }
    /**
     * Updates ToolTipManager + lock or destroy.
     * */
    protected void display()
    {
        GameManager.getInstance().toolTipManager.askDisplay(this);
        //Debug.Log("Add " + this);
        this.enabled = false;
    }

    /**
     * Memory Listener things.
     * */
    public void onMemoryAdd(Memory memory, MemoryBloc bloc)
    {
        if (!this.enabled)
        {
            //Debug.Log("Disabled");
            return;
        }
        else
        {
            //Debug.Log("Enabled");
            checkMemoryModificationTrigger(bloc);
        }
    }

    public void onMemoryRemove(Memory memory, MemoryBloc bloc)
    {
    }

    public virtual void read()
    {
    }
}
