using UnityEngine;
using System.Collections;

public class TipRabbit : ToolTip
{
    Memory m_Memory;

    void Awake()
    {
        title = "Vous avez croisé un lapin !";
        description = "Petits herbivores d'environ 25cm pour un poids d'environ 400g, les lapins vivent en groupe dans des terriers. Grâce à leurs longues oreilles, ils peuvent détecter les prédateurs de loin.\n\nEn cas d'alerte, il reste immobile pour éviter d'être repéré et ne fuit qu'au dernier moment.\n\nCourant en zigzag pour semer le poursuivant, sa vitesse peut atteindre 48km/h.";

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
        Animal lapin = bloc.getEntity() as Rabbit;
        if (lapin != null)
        {
            m_Memory.removeMemoryListener(this);
            display();
        }
    }
}
