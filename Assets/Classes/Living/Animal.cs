using UnityEngine;
using System.Collections;

public abstract class Animal : Living {
    public PerceptView perceptView;
    public EmoticonSystem emoticonSystem;

    public int VIE_MAX;
    public float vie;
    public float direction;
    public int FAIM_MAX;
    public float faim;
    public float vitesse = 1;

    //Sprites ..
    public Sprite normalSprite;
    public Sprite sleepSprite;
    //Emoticon sprites
    public Sprite sleepEmoticonSprite;
    public Sprite questionEmoticonSprite;

    public void displayAnimatedEmoticon(Sprite sprite)
    {
        if (emoticonSystem != null)
            emoticonSystem.displayAnimatedEmoticon(sprite);
    }

    public void displayStaticEmoticon(Sprite sprite)
    {
        if (emoticonSystem != null)
            emoticonSystem.displayStaticEmoticon(sprite);
    }

    public void hideStaticEmoticon()
    {
        if (emoticonSystem != null)
            emoticonSystem.hideStaticEmoticon();
    }

    public void construct(MindAnimal mind)
    {
        if (emoticonSystem != null)
            emoticonSystem.setAnimal(this);
        direction = 0;
        base.construct(mind);
    }

    public void fd(float pas)
    {
        GetComponent<Rigidbody2D>().rotation = direction;
        GetComponent<Rigidbody2D>().velocity = transform.up * pas;
    }

    public void wiggle(float pas, float wiggleValue)
    {
        lt(Random.Range(0, wiggleValue));
        rt(Random.Range(0, wiggleValue));
        fd(pas);
    }

    public void rt(float pas)
    {
        direction -= pas;
    }

    public void lt(float pas)
    {
        direction += pas;
    }

    public bool estMort()
    {
        return vie <= 0;
    }

    public void dors()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void reveil()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void faceTo(Living agent)
    {
        faceTo(agent.GetComponent<Transform>().position);
    }

    public void faceTo(Vector2 positionToLook)
    {
        /***** TODO *****/
    }
}