using UnityEngine;
using System.Collections;

public class AU_MoveTo : A_ActionUser
{
    private float xTarget;
    private float yTarget;
    private float timeRugissement = 1.2f;

    public AU_MoveTo(float x, float y)
        : base("AU_MoveTo")
    {
        xTarget = x;
        yTarget = y;
    }

    public AU_MoveTo(Vector2 position)
        : base("AU_MoveTo")
    {
        xTarget = position.x;
        yTarget = position.y;
    }

    protected override bool onUpdate(float deltaTime)
    {
        if(timeRugissement > 0)
        {
            timeRugissement -= Time.deltaTime;
            if (timeRugissement <= 0)
                getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
            return true;
        }

        Vector2 position = getAnimal().GetComponent<Transform>().position;
        Vector2 positionTarget = new Vector2(xTarget, yTarget);

        if(Vector2.Distance(position,positionTarget)<getAnimal().vitesse)
        {
            getActionPendlingList().removeAction(this);
            return false;
        }

        getAnimal().faceTo(positionTarget);
        getAnimal().wiggle(getAnimal().vitesse,2);

        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        AU_MoveTo action = obj as AU_MoveTo;
        return action != null && action.xTarget == xTarget && action.yTarget == yTarget;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().rugirSprite;
        return base.onStart(deltaTime);
    }
}
