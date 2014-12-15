using UnityEngine;
using System.Collections;

public class Threat : Objectif {
    public float InitialThreatRatio;
    public float AchieveThreatRatio;
    private GameObject m_Threat;
    private LoupBeta m_OwnScript;

	// Use this for initialization
	void Start () {
        InitialThreatRatio = Mathf.Clamp(InitialThreatRatio, 0, 1);
        AchieveThreatRatio = Mathf.Clamp(AchieveThreatRatio, 0, InitialThreatRatio);

        foreach (Transform child in GameManager.getInstance().tutorialManager.Meute.transform)
        {
            if (child.gameObject.name == "LoupBeta")
            {
                if (m_Threat == null)
                {
                    //Debug.Log("Beta found");
                    m_OwnScript = child.GetComponent<LoupBeta>();
                    m_Threat = child.gameObject;
                }
                else
                {
                    LoupBeta script = child.GetComponent<LoupBeta>();
                    if (script)
                    {
                        if (script.threat >= m_OwnScript.threat)
                        {
                            //Debug.Log("Beta found");
                            m_Threat = child.gameObject;
                        }
                    }
                }
            }
        }
        if (m_Threat == null)
        {
            //Debug.Log("Aucun alpha ????");
            Destroy(this);
        }

        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        checkAchievement();
	}

    void initialize()
    {
        m_OwnScript.threat = InitialThreatRatio * m_OwnScript.THREAT_MAX;

        m_Marqueur = Instantiate(m_Manager.Marqueur, m_OwnScript.transform.position, m_OwnScript.transform.rotation) as GameObject;
        m_Marqueur.transform.parent = m_OwnScript.transform;
        m_Marqueur.transform.localPosition = Vector3.zero;
    }

    /**
     * Check display condition on update.
     * Must be overidden.
     * */
    protected override void checkAchievement()
    {
        if (m_OwnScript.threat <= AchieveThreatRatio * m_OwnScript.THREAT_MAX)
        {
            //Debug.Log("Achieved.");
            achieve();
        }
    }

    public override void activate()
    {
        //Debug.Log("Activate fils");
        initialize();
        base.activate();
    }
}
