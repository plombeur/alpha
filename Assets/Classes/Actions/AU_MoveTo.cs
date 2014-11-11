using UnityEngine;
using System.Collections;

public class AU_MoveTo : A_ActionUser
{
    private float xTarget;
    private float yTarget;

    public AU_MoveTo(float x, float y)
        : base("AU_MoveTo")
    {
        xTarget = x;
        yTarget = y;
    }

    protected override bool onUpdate(float deltaTime)
    {
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
}
