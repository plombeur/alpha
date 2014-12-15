using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipWolfHungry : ToolTip
{
    LoupAlpha m_Alpha;

    void Awake()
    {
        title = "Un membre de votre meute à faim !";
        description = "Il faut ordonner à votre meute d'aller chasser, sinon il va mourir de faim...\n\nRappel : Pour chasser, pense à activer le mode \"chasse\" (icône rouge de ta barre d'action) !";
    }

    /*void Update()
    {
        if (m_Alpha == null)
            Destroy(this.gameObject);
    }*/

    protected override void checkTrigger()
    {
        if (Loup.getGESTION_FAIM())
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
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {
    }
}
