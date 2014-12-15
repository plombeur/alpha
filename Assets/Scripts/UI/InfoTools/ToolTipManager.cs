using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolTipManager : MonoBehaviour
{
    public Loup Alpha;
    private Stack<ToolTip> m_Tips;
    private ToolTip m_CurrentTip;
    private InfoWindow m_DisplayerScript;

    // Use this for initialization
    void Start()
    {
        m_Tips = new Stack<ToolTip>();
        m_CurrentTip = null;
        m_DisplayerScript = GameManager.getInstance().informationWindow;
        if (m_DisplayerScript == null)
        {
            Debug.Log("Script missing (InfoWindow)");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
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
            //Debug.Log("Current : " + m_CurrentTip.name);
            displayToolTip();
        }
    }
    /**
     * Freeze time.
     * Display ToolTip.
     * */
    public void displayToolTip()
    {
        m_DisplayerScript.showInfo(m_CurrentTip.title, m_CurrentTip.description, m_CurrentTip.icon);
        //Debug.Log("Displaying : " + m_CurrentTip.name);
    }
    /**
     * Called by the GUI to display the next ToolTip if existing.
     * Unfreeze + change tip.
     * */
    public void validateReading()
    {
        m_CurrentTip.read();
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
            m_CurrentTip.enabled = false;
            m_CurrentTip = null;
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
