using UnityEngine;
using System.Collections;

public class TipEndGame : ToolTip {

    void Awake()
    {
        title = " Vous avez perdu !";
        description = " Le loup alpha est mort de faim, ou un loup beta l'a évincé de son rôle de chef...\n\nCause : ";
    }

    protected override void checkTrigger()
    {
        if (GameManager.getInstance().isGameOver() && !GameManager.getInstance().isGameWon())
        {
            description += GameManager.getInstance().hud.descriptionWin.text;
            display();
        }
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}
