using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipWolfHungry : ToolTip
{
    LoupAlpha m_Alpha;

    void Awake()
    {
        title = "Un membre de votre meute à faim !";
        description = "Il faut ordonner à votre meute d'aller chasser, sinon il va mourir de faim...";
        
        //m_Alpha = mManager.Alpha.GetComponent<LoupAlpha>();
    }

    /*void Update()
    {
        if (m_Alpha == null)
            Destroy(this.gameObject);
    }*/

    protected override void checkTrigger()
    {
        bool isHungry = false;
        Transform meute = GameManager.getInstance().toolTipManager.Alpha.transform.parent.transform;
        for (int iChild = 0; iChild < meute.childCount; iChild++)
        {
            Loup currentLoup = meute.GetChild(iChild).GetComponent<Loup>();
            if (currentLoup.faim <= 50)
            {
                isHungry = true;
            }
        }
        if (isHungry) display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}
