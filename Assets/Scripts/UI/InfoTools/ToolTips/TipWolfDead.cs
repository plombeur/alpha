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
        Transform meute = mManager.Alpha.transform.parent.transform;
        for (int iChild = 0; iChild < meute.childCount; iChild++)
        {
            Loup currentLoup = meute.GetChild(iChild).GetComponent<Loup>();
            if (currentLoup.estMort())
            {
                isDead = true;
            }
        }
        if (isDead) display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}
