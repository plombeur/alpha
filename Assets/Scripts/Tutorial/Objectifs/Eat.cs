using UnityEngine;
using System.Collections;

public class Eat : Objectif {
    public float HungryRatio;
    public float NoMoreHungryRatio;
    public ToolTip TipHungry;

    private LoupAlpha m_OwnScript;

    // Use this for initialization
    void Start()
    {
        NoMoreHungryRatio = Mathf.Clamp(NoMoreHungryRatio, 0, 1);
        HungryRatio = Mathf.Clamp(HungryRatio, 0, NoMoreHungryRatio);

        foreach (Transform child in GameManager.getInstance().tutorialManager.Meute.transform)
        {
            if (child.gameObject.name == "LoupAlpha")
            {
                m_OwnScript = child.GetComponent<LoupAlpha>();
            }
        }
        if (m_OwnScript == null)
        {
            //Debug.Log("Aucun alpha ????");
            Destroy(this);
        }

        detail = "Maintenant que tu sais chasser, tu dois nourrir tes loups !\n\nL'icône de la cuisse de mouton au dessus de tes loups indique que ce loup à faim. Pour les nourrir, place les autour des carcasses (comme le mouton que tu viens de tuer) et attend de voir ton loup se nourrir.";

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        checkAchievement();
    }

    void initialize()
    {
        LoupBeta.GESTION_FAIM = true;
        m_OwnScript.faim = HungryRatio * m_OwnScript.FAIM_MAX;
    }

    /**
     * Check display condition on update.
     * Must be overidden.
     * */
    protected override void checkAchievement()
    {
        if (m_OwnScript.faim >= NoMoreHungryRatio * m_OwnScript.FAIM_MAX)
        {
            //Debug.Log("Achieved.");
            StartCoroutine(enableTip());
            achieve();
        }
    }

    public override void activate()
    {
        //Debug.Log("Activate fils");
        initialize();
        base.activate();
    }

    IEnumerator enableTip()
    {
        yield return new WaitForSeconds(1.0f);
        TipHungry.enabled = true;
    }
}
