using UnityEngine;
using System.Collections;

public abstract class Animal : Living {

    public int VIE_MAX;
    public int vie;
    public float direction;
    public int FAIM_MAX;
    public int faim;

    public void construct(MindAnimal mind, int vie, int faim)
    {
        if (Living.DEBUG)
            Debug.Log("Animal.construct");
        VIE_MAX = vie;
        vie = VIE_MAX;
        FAIM_MAX = faim;
        faim = FAIM_MAX;
        direction = 0;
        base.construct(mind);
    }

    public void fd(float pas)
    {
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
}