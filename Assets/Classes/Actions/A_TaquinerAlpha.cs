using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_TaquinerAlpha : Action
{
    private Loup cible;
    private bool goAtk = false;
    private bool goRetourAtk = false;
    private Vector3 tailleInitiale;

    public A_TaquinerAlpha()
        : base("A_TaquinerAlpha")
    {
        this.cible = LoupInferieur.alpha;
    }

    public override float getPriority()
    {
        return 300;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override bool onResume(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override bool onUpdate(float deltaTime)
    {
        GameManager.getInstance().setCameraFocus(getAnimal().transform);
        Animal a = getAnimal();
        if (!goAtk && Vector2.Distance(a.GetComponent<Transform>().position, cible.GetComponent<Transform>().position) <= 1)
        {
            goAtk = true;
            tailleInitiale = getAnimal().transform.localScale;
            getAnimal().faceTo(cible);
            getAnimal().fd(0.001f, false, false);
        }

        if (goAtk)
        {
            if (a.animationAttaque(cible, a.getFaceToDirection(cible.transform.position)))
            {
                getAnimal().GetComponentInChildren<Voice>().makeSound(getAnimal().getIdentity(), SoundInformation.WolfAttack);
                GameManager.getInstance().setGameLost("Un loup beta s'est rebellé!");
                getActionPendlingList().removeAction(this);
                return true;
            }

            return true;
        }

        a.setAgentToDontDodge(cible);
        a.faceTo(cible);
        a.wiggle(getAnimal().vitesse, 2);
        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_TaquinerAlpha action = obj as A_TaquinerAlpha;
        return action != null;
    }
}

