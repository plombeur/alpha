using UnityEngine;
using System.Collections;

public class TipRabbit : ToolTip
{
    void Start()
    {
        title = "Vous avez croisé un lapin !";
        description = "Petits herbivores d'environ 25cm pour un poids de 400g, les lapins vivent en groupe dans des terriers. Grâce à leurs longues oreilles, ils peuvent détecter les prédateurs de loin.\n\nEn cas d'alerte, il reste immobile pour éviter d'être repéré et ne fuit qu'au dernier moment.\n\nCourant en zigzag pour semer le poursuivant, sa vitesse peut atteindre 48km/h.";
            
        Memory memoryAlpha = mManager.Alpha.GetComponent<Memory>();
        if (memoryAlpha == null)
        {
            Debug.Log("Pas de mémoire du loup alpha.");
            Destroy(this.gameObject);
        }
        memoryAlpha.addMemoryListener(this);
    }
    protected override void checkTrigger()
    {
        
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {
        Animal lapin = bloc.getEntity() as Rabbit;
        if (lapin != null)
        {
            display();
        }
    }
}
