﻿using System;
using System.Collections;

public abstract class MindAnimal : Mind
{
    public MindAnimal(Animal agent) : base(agent)
    {
    }
    public override void vivre()
    {
        Animal animal = (Animal) agent;
        animal.faim-= Time;
    }
}