using UnityEngine;
using System.Collections;

public class A_Promenade : Action {

    private float vitesse;
    private float cptNouvelleTrajectoire = 0;

	public A_Promenade(float vitesse = 1) : base("A_Promenade")
    {
        this.vitesse = vitesse;
    }

    public override float getPriority()
    {
        return 0;
    }

    protected override bool onUpdate(float deltaTime)
    {
        Animal a = getAnimal();
        cptNouvelleTrajectoire -= deltaTime;
        if(cptNouvelleTrajectoire <= 0)
        {
            a.direction = Random.Range(0, 360);
            cptNouvelleTrajectoire = -cptNouvelleTrajectoire + Random.Range(2, 40);
        }
        a.fd(vitesse);
        return true;
    }
}
