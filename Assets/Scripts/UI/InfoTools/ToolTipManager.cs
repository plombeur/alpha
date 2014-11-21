using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolTipManager : MonoBehaviour {
    public GameObject Alpha;
    private Stack<ToolTip> mTips;
    private ToolTip mCurrentTip;

	// Use this for initialization
	void Start () {
        mTips = new Stack<ToolTip>();
	}
	
	// Update is called once per frame
	void Update () {
        	
	}

    /**
     * Add the ToolTip to the display list.
     * */
    public void askDisplay(ToolTip tip)
    {
        mTips.Push(tip);
    }
    /**
     * Freeze time.
     * Display ToolTip.
     * Destroy ToolTip.
     * Check next ToolTIp to display.
     * */
    public void displayToolTip()
    {
    }

    private void freezeTime()
    {
    }
    private void displayToolTipDescription()
    {
    }
    /**
     * Called by the GUI to display the next ToolTip if existing.
     * */
    public void validateReading()
    {
        if (mCurrentTip != null)
        {
            Destroy(mCurrentTip.gameObject);
            mCurrentTip = mTips.Pop();
        }
    }
}
