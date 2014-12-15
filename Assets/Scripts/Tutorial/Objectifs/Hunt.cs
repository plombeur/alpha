using UnityEngine;
using System.Collections;

public class Hunt : Objectif {
    public GameObject Mouton;
    private GameObject m_Alpha;
    private GameObject m_Mouton;
    private Animal m_Script;

    // Use this for initialization
    protected void Start()
    {
        detail = "Pour te nourrir, tu dois chasser le mouton entouré d'un cercle orange.\n\nPour se faire, passe en mode \"Chasse\" (icône rouge de ta barre d'action) et déplace ta meute en direction du mouton.\n\nTes loups s'attaqueront automatiquement au mouton lorsqu'ils seront à portée.";

        m_Alpha = null;
        foreach(Transform child in GameManager.getInstance().tutorialManager.Meute.transform) {
            if (child.gameObject.name == "LoupAlpha")
            {
                //Debug.Log("Alpha found at start");
                m_Alpha = child.gameObject;
            }
        }
        if (m_Alpha == null)
        {
            //Debug.Log("Aucun alpha at start ????");
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
        if (m_Alpha == null)
        {
            //Debug.Log("Aucun alpha ????");
            Destroy(this);
            return;
        }
        Vector2 pos = new Vector2();
        pos.x = m_Alpha.transform.position.x + Random.Range(-5, 5);
        pos.y = m_Alpha.transform.position.y + Random.Range(-5, 5);

        //Debug.Log("Instantiate");
        m_Mouton = Instantiate(Mouton, pos, m_Alpha.transform.rotation) as GameObject;

        m_Marqueur = Instantiate(m_Manager.Marqueur, pos, m_Alpha.transform.rotation) as GameObject;
        m_Marqueur.transform.parent = m_Mouton.transform;
        m_Marqueur.transform.localPosition = Vector3.zero;

        m_Script = m_Mouton.GetComponent<Animal>();

        GameManager.getInstance().se
    }

    /**
     * Check display condition on update.
     * Must be overidden.
     * */
    protected override void checkAchievement()
    {
        if (m_Script.estMort())
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
