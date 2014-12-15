using UnityEngine;
using System.Collections;

public class TipStart : ToolTip {
    void Awake()
    {
        title = "Bienvenue sur Alpha !";
        description = "Vous incarnez un loup alpha. Fort et fier, vous êtes à la tête d'une meute de loup.\nVotre rôle est de faire survivre votre meute dans un environnement sauvage tout en conservant votre place de chef de meute.\n\nVotre aventure sera rythmée par la recherche de nourriture, l'exploration, la reproduction et la défense de votre meute...\n\nUtilisez votre connaissances et prouvez que vous êtes l'alpha !";   
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
        StartCoroutine(startTips());
        StartCoroutine(startTuto());
    }

    IEnumerator startTuto()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.getInstance().tutorialManager.gameObject.SetActive(true);
    }
    IEnumerator startTips()
    {
        yield return new WaitForFixedUpdate();
        foreach (Transform child in transform.parent)
        {
            if (child.name != this.name && child.name != transform.parent.name)
            {
                //Debug.Log("Activate " + child.name);
                ToolTip tip = child.GetComponent<ToolTip>();
                //Debug.Log("Activate " + tip);
                if (!tip.isActivatedElsewhere)
                    tip.enabled = true;
            }
        }
    }
}
