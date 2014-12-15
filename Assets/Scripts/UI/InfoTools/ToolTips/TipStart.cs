using UnityEngine;
using System.Collections;

public class TipStart : ToolTip {
    void Start()
    {
        title = "Bienvenue sur Alpha !";
        description = "Vous prenez la place d’un loup alpha. Choisi par votre meute sur votre force, votre intelligence, votre sagesse, votre reconnaissance…,  vous devez donner des directions pour la faire survivre. L'extension de la territoire, la recherche de la nourriture, la protection des petits … vous défient tout au long du jeu. Utilisez votre connaissances et prouvez que vous êtes l'alpha!";
    }

    protected override void checkTrigger()
    {
        display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {
    }

    public override void read()
    {
        StartCoroutine(startTuto());
    }

    IEnumerator startTuto()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.getInstance().tutorialManager.gameObject.SetActive(true);
    }
}
