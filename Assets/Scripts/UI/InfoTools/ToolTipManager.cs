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
    public float toFreezeTime;
    private float m_timer;

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
        toFreezeTime = Mathf.Clamp(toFreezeTime, 0.0f, 5.0f);
        m_timer = toFreezeTime;
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
            m_timer -= Time.deltaTime;
            Time.timeScale = m_timer / toFreezeTime;
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
            Time.timeScale = 1.0F;
            m_isFreezing = false;
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
            return true;
        }
        m_timer = toFreezeTime;
        return false;
    }
}
