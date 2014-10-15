using UnityEngine;
using System.Collections;

public abstract class Animal : Living {
    protected int vie;
    public void construct(MindAnimal mind, int vie)
    {
        if (Living.DEBUG)
            Debug.Log("Animal.construct");
        this.vie = vie;
        base.construct(mind);
    }
    public void avancer(float pas)
    {
        GetComponent<Rigidbody2D>().velocity = pas;
    }
}