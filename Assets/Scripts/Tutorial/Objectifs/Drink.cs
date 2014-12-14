using UnityEngine;
using System.Collections;

public class Drink : Objectif {
    public GameObject Meute;
    public float InitialRatio;
    public float AchieveRatio;
    private LoupAlpha m_OwnScript;

    // Use this for initialization
    void Start()
    {
        InitialRatio = Mathf.Clamp(InitialRatio, 0, 1);
        AchieveRatio = Mathf.Clamp(AchieveRatio, 0, InitialRatio);

        foreach (Transform child in Meute.transform)
        {
            if (child.gameObject.name == "LoupAlpha")
            {
                //Debug.Log("Alpha found");
                m_OwnScript = child.GetComponent<LoupAlpha>();
            }
        }
        if (m_OwnScript == null)
        {
            //Debug.Log("Aucun alpha ????");
            Destroy(this);
        }

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        checkAchievement();
    }

    void initialize()
    {
        //m_OwnScript.soif = InitialRatio * m_OwnScript.passoif;
    }

    /**
     * Check display condition on update.
     * Must be overidden.
     * */
    protected override void checkAchievement()
    {
        /*if (m_OwnScript.soif <= AchieveRatio * m_OwnScript.passoif)
        {
            //Debug.Log("Achieved.");
            achieve();
        }*/
    }

    public override void activate()
    {
        //Debug.Log("Activate fils");
        initialize();
        base.activate();
    }
}
