using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
    public GameObject Alpha;
    private Stack<ToolTip> m_Tips;
    private ToolTip m_CurrentTip;
    private Text m_Displayer;

    // Use this for initialization
    void Start()
    {
        m_Tips = new Stack<ToolTip>();
        GameObject UIText = GameObject.Find("UI/ToolTipDisplayer");
        if (UIText == null)
        {
            Debug.Log("TTM::Impossible de trouver UI/ToolTipDisplayer.");
            Destroy(this);
        }
        else
        {
            m_Displayer = UIText.GetComponent<Text>();
            if (m_Displayer == null)
            {
                Debug.Log("TTM::Aucun component Text trouvé.");
                Destroy(this);
            }
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
    }
    /**
     * Freeze time.
     * Display ToolTip.
     * Destroy ToolTip.
     * Check next ToolTIp to display.
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
        if (Time.timeScale == 0.0F)
        {
            isFrozen = true;
        }
        else if (Time.timeScale < 0.25F)
        {
            Time.timeScale = 0.0F;

            Color displayColor = m_Displayer.color;
            displayColor.a = 1.0F;
            m_Displayer.color = displayColor;

            isFrozen = true;
        }
        else
        {
            Time.timeScale = Time.timeScale / 2.0F;

            Color displayColor = m_Displayer.color;
            displayColor.a = 1 - Time.timeScale;
            m_Displayer.color = displayColor;
        }
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
        return isFrozen;
    }
    /**
     * Display the current TollTip.
     * */
    private void displayToolTipDescription()
    {
        m_Displayer.text = m_CurrentTip.description;
    }
    /**
     * Called by the GUI to display the next ToolTip if existing.
     * */
    public void validateReading()
    {
        if (m_CurrentTip != null)
        {
            Destroy(m_CurrentTip.gameObject);
            m_CurrentTip = m_Tips.Pop();
        }
        Color baseColor = m_Displayer.color;
        baseColor.a = 0.0F;
        m_Displayer.color = baseColor;
        Time.timeScale = 1.0F;
    }
}
