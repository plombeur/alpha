using UnityEngine;
using System.Collections;

public class Objectif : MonoBehaviour {
    public string title;
    public string description;
    public TutorialManager mManager;

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
     * Updates Manager + lock or destroy.
     * */
    protected void display()
    {
        if (mManager != null)
        {
            mManager.askDisplay(this);
            this.enabled = false;
        }
    }
}
