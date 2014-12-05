using UnityEngine;
using System.Collections;

public class A_Repos : Action {

    private float duree;
    private float time = 0;
    private const float durationBetweenSleepEmoticons = 0.6f;
    private float timeSleepIcon = durationBetweenSleepEmoticons;

    public A_Repos(float duree) : base("A_Repos")
    {
        this.duree = duree;
    }

    public override float getPriority()
    {
        return 0.1f;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().sleepSprite;
        return base.onStart(deltaTime);
    }
    protected override bool onResume(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().sleepSprite;
        return base.onStart(deltaTime);
    }


    protected override bool onUpdate(float deltaTime)
    {
        timeSleepIcon += deltaTime;
        while (timeSleepIcon >= 0.8)
        {
            timeSleepIcon -= 0.8f;
            getAnimal().displayAnimatedEmoticon(getAnimal().sleepEmoticonSprite);
        }
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().sleepSprite;
        if (getAnimal().DEBUG)
            Debug.Log("Repos " + time);
        time += deltaTime;
        Animal a = getAnimal();
        if(time >= duree)
        {
            getActionPendlingList().removeAction(this);
            return true;
        }
        a.dors();
        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_Repos action = obj as A_Repos;
        return action != null;
    }
}
