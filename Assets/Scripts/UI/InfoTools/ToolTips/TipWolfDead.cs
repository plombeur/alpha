using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipWolfDead : ToolTip
{

    void Start()
    {
        title = "Un membre de votre meute est mort !";
        description = "Vos loups ne sont pas immortels ! Cet évenement pousse votre meute à douter de votre position de chef…";
    }

    protected override void checkTrigger()
    {
        bool isDead = false;
        Loup alpha = mManager.Alpha.GetComponent<Loup>();
        List<Living> percepts = alpha.perceptView.getLiving();
        for (int i = 0; i < percepts.Count; ++i)
            if (percepts[i] as Loup != null && (percepts[i] as Loup).estMort())
            {
                isDead = true;
            }
        if (isDead) display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}
