using UnityEngine;
using System.Collections;

public abstract class Animal : Living {

    private int vie;
    private float direction;

    public void construct(MindAnimal mind, int vie)
    {
        if (Living.DEBUG)
            Debug.Log("Animal.construct");
        this.vie = vie;
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
<<<<<<< HEAD
        direction += pas;
=======
        GetComponent<Rigidbody2D>().velocity = pas;
>>>>>>> origin/master
    }
}