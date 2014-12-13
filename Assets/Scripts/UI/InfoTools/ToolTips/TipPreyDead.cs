﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipPreyDead : ToolTip
{
    void Start()
    {
        title = "Vous avez abbatue votre proie !";
        description = "Félicitation ! Votre chasse est un succès ! \nVos loups ont réussi à se nourrir, ils vont pouvoir survivre ! \nSeulement 10% des proies pourchassées sont rattrapées par le loup, parfois après des centaines de mètres de chasse, et la moitié de celles-ci sont finalement mises à mort avec succès. Le loup a besoin de 4kg de viande par jour. Il dévore ses proies en totalité, poils et os compris.";
    }

    protected override void checkTrigger()
    {
        bool isDead = false;
        Loup alpha = mManager.Alpha.GetComponent<Loup>();
        List<Living> percepts = alpha.perceptView.getLiving();
        for (int i = 0; i < percepts.Count; ++i)
            if (percepts[i] as Animal != null && percepts[i] as Loup == null && (percepts[i] as Animal).estMort())
            {
                isDead = true;
            }

        if (isDead) display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}
