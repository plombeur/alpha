using UnityEngine;
using System.Collections;

public class TipSheep : ToolTip
{
    Memory m_Memory;

    void Awake()
    {
        title = "Vous avez croisé un mouton !";
        description = "Mammifère ruminant herbivore, le mouton mesure entre 1 mètre et 1,5 mètre et pèse entre 40kg et 160kg. Son espérance de vie est d'environ 10 ans, même si certains individus vivent jusqu'à 20 ans.\n\nCe fut l'un des premiers animaux domestiqué par l’homme (il y a environ 10000 ans), il est apprécié pour son lait, sa viande, sa peau et sa laine.\n\nLe mouton a beaucoup de prédateurs, principalement les canidés : loup, chien, renard, chacal… mais aussi les félins, les ours, les oiseaux de proie et même les cochons sauvage, mais possèdent peu de moyens de défense, autre que de se regrouper quand ils se sentent menacés. Même s’ils survivent à une attaque, il peuvent mourir des suites d'une blessure ou de panique (en sautant d'une falaise).\n\nIls forment une bonne source de nourriture pour ta meute !";
    }

    void Start()
    {
        Transform meute = GameManager.getInstance().toolTipManager.Alpha.transform.parent.transform;
        for (int iChild = 0; iChild < meute.childCount; iChild++)
        {
            m_Memory = meute.GetChild(iChild).GetComponent<Memory>();
            if (m_Memory == null)
            {
                Debug.Log("Pas de mémoire de loup.");
                Destroy(this.gameObject);
            }
            m_Memory.addMemoryListener(this);
        }
    }
    protected override void checkTrigger()
    {

    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {
        Animal mouton = bloc.getEntity() as Sheep;
        if (mouton != null)
        {
            m_Memory.removeMemoryListener(this);
            display();
        }
    }
}
