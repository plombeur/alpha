using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public Sprite heartEmoticonSprite;

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

    public void faceTo(Living agent)
    {
        faceTo(agent.GetComponent<Transform>().position);
    }

    public void faceTo(Vector2 positionToLook)
    {
        Vector2 up = new Vector2(0, 1);
        Transform transform = GetComponent<Transform>();
        Vector2 pointToLook = positionToLook - new Vector2(transform.position.x,transform.position.y);
        direction = Vector2.Angle(up, pointToLook);
        float determinant = up.x * pointToLook.y - up.y * pointToLook.x;
        if (determinant < 0)
            direction *= -1;
    }

    public LoupOmega randomLoupOmegaSeen()
    {
        List<Living> percepts = perceptView.getLiving();
        int nbOmega = 0;
        for (int i = 0; i < percepts.Count; ++i)
            if (percepts[i] as LoupOmega != null)
                nbOmega++;
        int indiceOmega = Random.Range(1, nbOmega);
        LoupOmega result = null;
        if (nbOmega > 0)
        {
            for (int i = 0; i < percepts.Count; ++i)
            {
                result = percepts[i] as LoupOmega;
                if (result != null)
                {
                    indiceOmega--;
                    if (indiceOmega == 0)
                        break;
                }
            }
        }
        return result;
    }

    public Loup randomLoupSeen()
    {
        List<Living> percepts = perceptView.getLiving();
        int nbLoups = 0;
        for (int i = 0; i < percepts.Count; ++i)
            if (percepts[i] as Loup != null)
                nbLoups++;
        int indiceLoup = Random.Range(1, nbLoups);
        Loup result = null;
        if (nbLoups > 0)
        {
            for (int i = 0; i < percepts.Count; ++i)
            {
                result = percepts[i] as Loup;
                if (result != null)
                {
                    indiceLoup--;
                    if (indiceLoup == 0)
                        break;
                }
            }
        }
        return result;
    }
}