using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TipLoss : ToolTip
{

    void Start()
    {
        title = " Vous avez perdu !";
        description = " Le loup alpha est mort de faim, ou le loup beta l'a tué pour prendre sa place après avoir douté sa position...";
    }

    protected override void checkTrigger()
    {
        if (GameManager.getInstance().toolTipManager.Alpha.GetComponent<LoupAlpha>().estMort()) display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}
