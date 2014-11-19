using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour, MemoryListener
{
    public string description;
    private ToolTipManager mManager;

    // Use this for initialization
    void Start()
    {
        GameObject TTM = GameObject.Find("ToolTipManager");
        if (TTM != null)
        {
            mManager = TTM.GetComponent<ToolTipManager>();
            if (mManager == null)
            {
                Destroy(this);
            }
        }
        else Destroy(this);
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
