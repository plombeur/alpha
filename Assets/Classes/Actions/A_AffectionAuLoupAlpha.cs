using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_AffectionAuLoupAlpha : Action
{
    public A_AffectionAuLoupAlpha()
        : base("A_AffectionAuLoupAlpha")
    {
    }

    public override float getPriority()
    {
        return 0.1f;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return base.onStart(deltaTime);
    }

    protected override bool onResume(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override bool onUpdate(float deltaTime)
    {
        GameObject obj = GameObject.Find("LoupAlpha");
        if (obj == null)
        {
            getAnimal().fd(0);
            return true;
        }
        LoupAlpha alpha = obj.GetComponent<LoupAlpha>();
        PerceptView percepts = getAnimal().perceptView;
        List<Living> list = percepts.getLiving();
        if (list.Contains(alpha))
        {
            getAnimal().hideStaticEmoticon();
            if (Vector2.Distance(alpha.GetComponent<Transform>().position, getAnimal().GetComponent<Transform>().position) > 5)
            {
                getAnimal().faceTo(alpha);
                getAnimal().wiggle(getAnimal().vitesse * 3,2);
                return true;
            }

            getAnimal().emoticonSystem.displayAnimatedEmoticon(getAnimal().heartEmoticonSprite);
            getAnimal().lt(180);
            getActionPendlingList().removeAction(this);
            return false;
        }

        getAnimal().rt(4);
        getAnimal().wiggle(0.01f, 2);

        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_AffectionAuLoupAlpha action = obj as A_AffectionAuLoupAlpha;
        return action != null;
    }
}
