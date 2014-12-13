using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipWolfHungry : ToolTip
{

    void Start()
    {
        title = "Un membre de votre meute à faim !";
        description = "Il faut ordonner à votre meute d'aller chasser, sinon il va mourir de faim...";
    }

    protected override void checkTrigger()
    {
        bool isHungry = false;
        Loup alpha = mManager.Alpha.GetComponent<Loup>();
        List<Living> percepts = alpha.perceptView.getLiving();
        for (int i = 0; i < percepts.Count; ++i)
            if (percepts[i] as Loup != null && (percepts[i] as Loup).faim <= 0)
            {
                isHungry = true;
            }
        if (alpha.faim <= 0)
        {
            isHungry = true;
        }
        if (isHungry) display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}
