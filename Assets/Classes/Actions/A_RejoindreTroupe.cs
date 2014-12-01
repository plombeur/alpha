using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_RejoindreTroupe : Action
{
    private float vitesse;
    private float cptNouvelleTrajectoire = 0;
    private float time = 0;
    private bool diriged = false;

    private bool animationAttaque = false;
    private float tailleInitiale;

    public A_RejoindreTroupe()
        : base("A_RejoindreTroupe")
    {}

    public override float getPriority()
    {
        return 1;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        getAnimal().displayStaticEmoticon(getAnimal().questionEmoticonSprite);
        return onUpdate(deltaTime);
    }

    protected override bool onResume(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        getAnimal().displayStaticEmoticon(getAnimal().questionEmoticonSprite);
        return onUpdate(deltaTime);
    }

    protected override bool onUpdate(float deltaTime)
    {
        if (Living.DEBUG)
            Debug.Log("A_RejoindreTroupe ...");
        GameObject obj = GameObject.Find("LoupAlpha");
        if (obj == null)
        {
            getAnimal().fd(0);
            return true;
        }

        LoupAlpha alpha = obj.GetComponent<LoupAlpha>();
        PerceptView percepts = getAnimal().perceptView;
        List<Living> list = percepts.getLiving();

        if (!diriged)
        {
            diriged = true;
            MemoryBloc memBlock = getAnimal().GetComponent<Memory>().getMemoryForIdentity(alpha.getIdentity());
            if (memBlock != null)
            {
                getAnimal().faceTo(memBlock.getLastPosition());
                getAnimal().fd(0.01f);
            }
            return true;
        }

        if(list.Contains(alpha))
        {
            getAnimal().hideStaticEmoticon();

            
            getActionPendlingList().removeAction(this);
            return true;
        }

        MemoryBloc mem = getAnimal().GetComponent<Memory>().getMemoryForIdentity(alpha.getIdentity());
        if (mem != null)
        {
            getAnimal().faceTo(mem.getLastPosition());
            getAnimal().wiggle(getAnimal().vitesse * 3f, 2);
        }
        else
        {
            getAnimal().rt(5);
            getAnimal().fd(0.001f,false,false);
        }
        
        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_RejoindreTroupe action = obj as A_RejoindreTroupe;
        return action != null;
    }
}
