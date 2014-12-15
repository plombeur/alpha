using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {
    public Objectif[] Objectifs;
    private int m_CurrentIndex;
    private ObjectifWindow m_DisplayerScript;

    // Use this for initialization
    void Start()
    {
        if (Objectifs.Length == 0)
        {
            Debug.Log("Aucun objectif.");
            Destroy(this);
        }
        m_CurrentIndex = -1;
        m_DisplayerScript = GameManager.getInstance().objectifWindow;
        if (m_DisplayerScript == null)
        {
            Debug.Log("Script missing (ObjectifWindow)");
            Destroy(this);
        }
        achieve();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /**
     * Display tutorial objectif.
     * */
    public void displayObjectif(Objectif toDisplay)
    {
        if (toDisplay == null)
        {
            m_DisplayerScript.setObjectif(null, null, null);
        }
        else 
            m_DisplayerScript.setObjectif(toDisplay.title, toDisplay.detail, toDisplay.objectif);
    }
    /**
     * Called by the GUI to display the next ToolTip if existing.
     * Unfreeze + change tip.
     * */
    public void achieve()
    {
        if (getNextObjectif())
        {
            Objectif newObj = Objectifs[m_CurrentIndex].GetComponent<Objectif>();
            displayObjectif(newObj);
            //Debug.Log(newObj);
            newObj.activate();
        }
        else
        {
            displayObjectif(null);
        }
    }

    private bool getNextObjectif()
    {
        ++m_CurrentIndex;
        if (m_CurrentIndex < Objectifs.Length)
        {
            //Debug.Log("Next objectif !");
            return true;
        }
        //Debug.Log("Pas d'autre objectif.");
        return false;
    }
}
