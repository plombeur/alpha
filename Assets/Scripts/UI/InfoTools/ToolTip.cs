using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour {
    public string description;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        checkTrigger();
	}

    /**
     * Check display condition.
     * Must be overidden.
     * */
    public virtual void checkTrigger()
    {
    }

    /**
     * Updates ToolTipManager + lock or destroy.
     * */
    public void display()
    {
        GameObject TTM = GameObject.Find("TollTipManager");
        if (TTM != null)
        {
            ToolTipManager manager = TTM.GetComponent<ToolTipManager>();
            if (manager != null)
            {
                manager.askDisplay(this);
                this.enabled = false;
            }
            else Destroy(this);
        }
        else Destroy(this);
    }
}
