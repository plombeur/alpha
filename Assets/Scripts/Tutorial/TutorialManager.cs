using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {
    public GameObject Displayer;
    private Stack<Objectif> m_Tips;
    private Objectif m_CurrentTip;
    private InfoWindow m_DisplayerScript;

    // Use this for initialization
    void Start()
    {
        m_Tips = new Stack<Objectif>();
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
    }

    /**
     * Add the ToolTip to the display list.
     * */
    public void askDisplay(Objectif tip)
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
        displayToolTipDescription();
    }
    /**
     * Display the current TollTip.
     * */
    private void displayToolTipDescription()
    {
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
