using UnityEngine;
using System.Collections;

public class A_Promenade : Action {

    private float vitesse;
    private float cptNouvelleTrajectoire = 0;
    private float time = 0;
    private bool dontMove = false;
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
        if (Living.DEBUG)
            Debug.Log("[" + getName() + "] onUpdate");
        Animal a = getAnimal();
        cptNouvelleTrajectoire -= deltaTime;
        if (cptNouvelleTrajectoire <= 0)
        {
            dontMove = false;
            if (Random.Range(1, 5) == 1)
            {
                dontMove = true;
                cptNouvelleTrajectoire = -cptNouvelleTrajectoire + Random.Range(1, 13);
            }
            else
            {
                a.direction = Random.Range(0, 360);
                cptNouvelleTrajectoire = -cptNouvelleTrajectoire + Random.Range(2, 30);
            }
                
        }
        time += deltaTime;
        while (time >= 0.04f)
        {
            if(!dontMove)
                a.wiggle(vitesse,2);
            time -= 0.04f;
        }
        return true;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override bool onResume(float deltaTime)
    {
        cptNouvelleTrajectoire = 0;
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_Promenade action = obj as A_Promenade;
        if (action == null)
        {
            return false;
        }

        return this.vitesse == action.vitesse;
    }
}
