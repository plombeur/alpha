using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipWolfHungry : ToolTip
{
    LoupAlpha m_Alpha;

    void Start()
    {
        title = "Un membre de votre meute à faim !";
        description = "Il faut ordonner à votre meute d'aller chasser, sinon il va mourir de faim...";

        m_Alpha = mManager.Alpha.GetComponent<LoupAlpha>();
    }

    void Update()
    {
        if (m_Alpha == null)
            Destroy(this.gameObject);
    }

    protected override void checkTrigger()
    {
        bool isHungry = false;
        List<Living> percepts = m_Alpha.perceptView.getLiving();
        for (int i = 0; i < percepts.Count; ++i)
            if (percepts[i] as Loup != null && (percepts[i] as Loup).faim <= 0)
            {
                isHungry = true;
            }
        if (m_Alpha.faim <= 0)
        {
            isHungry = true;
        }
        if (isHungry) display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}
