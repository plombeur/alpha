using UnityEngine;
using System.Collections;

public class Hunt : Objectif {
    public GameObject Mouton;
    public GameObject Meute;
    private GameObject m_Alpha;
    private GameObject m_Mouton;
    private Animal m_Script;

    // Use this for initialization
    protected void Start()
    {
        foreach(Transform child in Meute.transform) {
            if (child.gameObject.name == "LoupAlpha")
            {
                //Debug.Log("Alpha found");
                m_Alpha = child.gameObject;
            }
        }
        if (m_Alpha == null)
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
        Vector2 pos = new Vector2();
        pos.x = m_Alpha.transform.position.x + Random.Range(-7, 7);
        pos.y = m_Alpha.transform.position.y + Random.Range(-7, 7);

        m_Mouton = Instantiate(Mouton, pos, m_Alpha.transform.rotation) as GameObject;
        m_Script = m_Mouton.GetComponent<Animal>();
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
