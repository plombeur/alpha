using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AU_CalmerBeta : A_ActionUser
{
    private LoupBeta cible;
    private bool goAtk = false;
    private bool goRetourAtk = false;
    private Vector3 tailleInitiale;
    private GameObject cibleSprite;

    public AU_CalmerBeta(LoupBeta cible)
        : base("AU_CalmerBeta")
    {
        this.cible = cible;
    }

    protected override bool onStart(float deltaTime)
    {
        cibleSprite = (GameObject)GameObject.Instantiate(LoupInferieur.alpha.prefabYellowTarget);
        cibleSprite.GetComponent<FollowTarget>().target = cible.gameObject;
        cibleSprite.transform.parent = cible.transform;
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override bool onUpdate(float deltaTime)
    {
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
                cible.threat = 0;
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


    protected override void onPause()
    {
        if (cibleSprite != null)
            GameObject.Destroy(cibleSprite.gameObject);
        base.onPause();
    }

    protected override void onRemove()
    {
        if (cibleSprite != null)
            GameObject.Destroy(cibleSprite.gameObject);
        base.onPause();
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        AU_CalmerBeta action = obj as AU_CalmerBeta;
        return action != null && cible == action.cible;
    }
}

