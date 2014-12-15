using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolTipManager : MonoBehaviour
{
    public Loup Alpha;
    public GameObject Displayer;
    private Stack<ToolTip> m_Tips;
    private ToolTip m_CurrentTip;
    private InfoWindow m_DisplayerScript;

    // Use this for initialization
    void Start()
    {
        m_Tips = new Stack<ToolTip>();
        m_CurrentTip = null;
        m_DisplayerScript = Displayer.GetComponent<InfoWindow>();
        if (m_DisplayerScript == null)
        {
            Debug.Log("Script missing (InfoWindow)");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.getInstance().stopTheTime)
        {
            displayToolTip();
        }
    }

    /**
     * Add the ToolTip to the display list.
     * */
    public void askDisplay(ToolTip tip)
    {
        m_Tips.Push(tip);        
        if (m_CurrentTip == null)
        {
            getNextTip();
            displayToolTip();
        }
    }
    /**
     * Freeze time.
     * Display ToolTip.
     * */
    public void displayToolTip()
    {
        if (freezeTime())
        {
            displayToolTipDescription();
        }
    }
    /**
     * Slow the time till pause and change the display color to smoothly appear.
     * */
    private bool freezeTime()
    {
        bool isFrozen = false;
        GameManager.getInstance().slowAndStopTime();
        if (GameManager.getInstance().stopTheTime)
        {
            isFrozen = true;
        }
   
        return isFrozen;
    }
    /**
     * Display the current TollTip.
     * */
    private void displayToolTipDescription()
    {
        m_DisplayerScript.showInfo(m_CurrentTip.title, m_CurrentTip.description, m_CurrentTip.icon);
    }
    /**
     * Called by the GUI to display the next ToolTip if existing.
     * Unfreeze + change tip.
     * */
    public void validateReading()
    {
        if (getNextTip())
        {
            displayToolTip();
        }
        else
        {
            GameManager.getInstance().restartTime();
        }
    }

    private bool getNextTip()
    {
        if (m_CurrentTip != null)
        {
            Destroy(m_CurrentTip.gameObject);
        }
        if (m_Tips.Count != 0)
        {
            m_CurrentTip = m_Tips.Pop();
            //Debug.Log("Next tip !");
            return true;
        }

        return false;
    }
}
