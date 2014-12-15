using UnityEngine;
using System.Collections;

public class Objectif : MonoBehaviour {
    public string title;
    public string objectif;
    public string detail;
    protected TutorialManager m_Manager;
    protected GameObject m_Marqueur;

    // Use this for initialization
    protected void Start()
    {
        m_Manager = GameManager.getInstance().tutorialManager;
        enabled = false;
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    /**
     * Check display condition on update.
     * Must be overidden.
     * */
    protected virtual void checkAchievement()
    {
        Debug.Log("Achieve base (ne doit pas être appelé !).");
    }

    public virtual void activate()
    {
        //Debug.Log("Activate mère.");
        StartCoroutine(WaitForUpdate());
    }

    protected void achieve()
    {
        m_Manager.achieve();
        if (m_Marqueur != null)
        {
            Destroy(m_Marqueur);
        }
        this.enabled = false;
    }

    IEnumerator WaitForUpdate()
    {
        yield return new WaitForSeconds(1);
        enabled = true;
    }
}
