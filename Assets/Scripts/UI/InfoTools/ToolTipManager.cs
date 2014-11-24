using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
    public GameObject Alpha;
    public GameObject Displayer;
    private Stack<ToolTip> m_Tips;
    private ToolTip m_CurrentTip;
    private InfoWindow m_DisplayerScript;
    private bool m_isFreezing;

    // Use this for initialization
    void Start()
    {
        m_Tips = new Stack<ToolTip>();
        m_CurrentTip = null;
        m_isFreezing = false;
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
        if (m_isFreezing)
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
        m_isFreezing = true;
        if (freezeTime())
        {
            displayToolTipDescription();
            m_isFreezing = false;
        }
    }
    /**
     * Slow the time till pause and change the display color to smoothly appear.
     * */
    private bool freezeTime()
    {
        bool isFrozen = false;
        if (Time.timeScale == 0.0F)
        {
            isFrozen = true;
        }
        else if (Time.timeScale < 0.25F)
        {
            Time.timeScale = 0.0F;
            isFrozen = true;
        }
        else
        {
            Time.timeScale = Time.timeScale / 2.0F;
        }
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
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
        getNextTip();
        Time.timeScale = 1.0F;
    }

    private void getNextTip()
    {
        if (m_CurrentTip != null)
        {
            Destroy(m_CurrentTip.gameObject);
        }
        m_CurrentTip = m_Tips.Pop();
    }
}
