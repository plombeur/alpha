using UnityEngine;
using System.Collections;

public class A_Fuite : Action
{
    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        getAnimal().displayStaticEmoticon(getAnimal().exclamationEmoticonSprite);
        return onUpdate(deltaTime);
    }

    protected override void onPause()
    {
        getAnimal().hideStaticEmoticon();
        base.onPause();
    }

    protected override void onRemove()
    {
        getAnimal().hideStaticEmoticon();
        base.onPause();
    }

    protected override bool onResume(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        getAnimal().displayStaticEmoticon(getAnimal().exclamationEmoticonSprite);
        return onUpdate(deltaTime);
    }

    public override float getPriority()
    {
        return 100;
    }

    public A_Fuite() : base("A_Fuite")
    {
    }

    protected override bool onUpdate(float deltaTime)
    {
        Animal animal = getAnimal();
        float directionDeFuite = animal.getDirectionFuite();
        if(directionDeFuite == -1)
        {
            animal.lt(180);
            animal.fd(.1f, false, false);
            getActionPendlingList().removeAction(this);
            return true;
        }
        else
        {
            animal.direction = directionDeFuite;
            animal.fd(animal.vitesse * 3, false, true);
            return true;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_Fuite action = obj as A_Fuite;
        return action != null;
    }
}