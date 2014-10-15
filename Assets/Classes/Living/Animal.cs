using UnityEngine;
using System.Collections;

public abstract class Animal : Living {

    public int VIE_MAX;
    public float vie;
    public float direction;
    public int FAIM_MAX;
    public float faim;

    public void construct(MindAnimal mind)
    {
        direction = 0;
        base.construct(mind);
    }

    public void fd(float pas)
    {
        GetComponent<Rigidbody2D>().rotation = direction;
        GetComponent<Rigidbody2D>().velocity = transform.right * pas;
    }

    public void wiggle(float pas, float wiggleValue)
    {
        lt(Random.Range(0, wiggleValue));
        rt(Random.Range(0, wiggleValue));
        GetComponent<Rigidbody2D>().rotation = direction;
        GetComponent<Rigidbody2D>().velocity = transform.right * pas;
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
}