using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_RegarderVersLeBruit : Action
{

    private Identity target;
    public A_RegarderVersLeBruit(Identity target)
        : base("A_RegarderVersLeBruit")
    {
        this.target = target;
    }

    public override float getPriority()
    {
        return 80;
    }

    protected override bool onUpdate(float deltaTime)
    {
        Animal animal = getAnimal();

        animal.displayStaticEmoticon(animal.questionEmoticonSprite);

        if(animal.GetComponent<PerceptView>() != null)
        {
            List<Living> livings = animal.GetComponent<PerceptView>().getLiving();
            for (int i = 0; i < livings.Count; ++i)
            {
                if (livings[i].getIdentity() == target)
                {
                    getActionPendlingList().removeAction(this);
                    return false;
                }
            }
        }

        List<SoundPercepted> sounds = animal.perceptHearing.getSounds();
        if (sounds.Count > 0)
        {
            int i;
            for (i = 0; i < sounds.Count; ++i)
            {
                if (sounds[i].identity == target)
                {
                    if(Vector2.Angle(Utils.vectorFromAngle(animal.direction), Utils.vectorFromAngle(animal.getFaceToDirection(sounds[i].lastPosition))) < 10)
                    {
                        getActionPendlingList().removeAction(this);
                        return false;
                    }
                    animal.faceTo(sounds[i].lastPosition);
                    animal.fd(.00001f);
                    return true;
                }
            }

            if(i == sounds.Count)
            {
                getActionPendlingList().removeAction(this);
                return false;
            }
        }

        return true;
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

    protected override bool onStart(float deltaTime)
    {
        getAnimal().displayStaticEmoticon(getAnimal().questionEmoticonSprite);
        return onUpdate(deltaTime);
    }

    protected override bool onResume(float deltaTime)
    {
        getAnimal().displayStaticEmoticon(getAnimal().questionEmoticonSprite);
        return onUpdate(deltaTime);
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_RegarderVersLeBruit action = obj as A_RegarderVersLeBruit;
        if (action == null)
        {
            return false;
        }

        return action.target == target;
    }
}
