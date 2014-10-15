﻿using UnityEngine;
using System.Collections;

public abstract class Plant : Living
{
    protected int vie;
    public void construct(MindAnimal mind, int vie)
    {
        if (Living.DEBUG)
            Debug.Log("Animal.construct");
        this.vie = vie;
        base.construct(mind);
    }
}
