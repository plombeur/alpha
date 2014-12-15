using UnityEngine;
using System.Collections;

public class TipSheep : ToolTip
{
    Memory m_Memory;

    void Start()
    {
        title = "Vous avez croisé un mouton !";
        description = " Mammifère ruminant herbivore, le mouton mesure  entre 1-1,5m et pèse entre 40-160kg. Un des premiers animaux domestiqués par l’homme (il y a entre 9000-11000 années), il est apprécié pour son lait, sa viande, sa peau et sa laine.\n Le mouton a beaucoup de prédateurs. Les responsables de la mort des moutons sont principalement les canidés : loup, chien, renard, chacal… mais aussi les félins, les ours, les oiseaux de proie, les porcs sauvages… Les moutons on peu de moyens de défense, autre que regrouper quand ils se sentent la menace. Même s’ils survivent à une attaque, il peuvent mourir par la suite de blessure ou de panique.";

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
