using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipPreyDead : ToolTip
{
    void Start()
    {

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
