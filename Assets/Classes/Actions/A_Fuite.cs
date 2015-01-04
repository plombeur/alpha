using UnityEngine;
using System.Collections;

public class A_Fuite : Action
{

    private float time = 0;
    private float lastDirection = -1;
    private SoundInformation sound;

    public A_Fuite(SoundInformation sound) : base("A_Fuite")
    {
        this.sound = sound;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponentInChildren<Voice>().makeSound(getAnimal().getIdentity(), sound);
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
        return 90;
    }
    protected override bool onUpdate(float deltaTime)
    {
        Animal animal = getAnimal();

        getAnimal().displayStaticEmoticon(getAnimal().exclamationEmoticonSprite);

        if(time <= 0)
        {
            time = Random.Range(10, 25) * .1f;
            lastDirection = animal.getDirectionFuite();
        }
        else
            time -= deltaTime;

        if(animal.DEBUG)
            Debug.Log("A_Fuite : time = " + time + ", direction = " + lastDirection);

        Rabbit lapin = animal as Rabbit;
        if (lapin != null && lapin.aCoteDuTerrier())
        {
            if (time <= 0)
            {
                getActionPendlingList().removeAction(this);
                lapin.GetComponent<SpriteRenderer>().sortingOrder = 1;
                return false;
            }
            lapin.GetComponent<SpriteRenderer>().sortingOrder = -10;
            return true;
        }
        else
        {
            getAnimal().GetComponent<SpriteRenderer>().sortingOrder = 1;
            if (time <= 0)
            {
                animal.lt(180);
                animal.fd(.1f, false, false);
                getActionPendlingList().removeAction(this);
                return true;
            }
            else
            {
                animal.direction = lastDirection;
                animal.fd(animal.vitesse * 3, false, true);
                return true;
            }
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