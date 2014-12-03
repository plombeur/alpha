using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour, MemoryListener
{
    public string title;
    public string description;
    public Sprite icon;
    public ToolTipManager mManager;

    // Use this for initialization
    void Start()
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
        if (mManager != null)
        {
            mManager.askDisplay(this);
            this.enabled = false;
        }
    }

    /**
     * Memory Listener things.
     * */
    public void onMemoryAdd(Memory memory, MemoryBloc bloc)
    {
        checkMemoryModificationTrigger(bloc);
    }

    public void onMemoryRemove(Memory memory, MemoryBloc bloc)
    {
    }
}
