﻿using UnityEngine;
using System.Collections;

public class MindLoup : MindAnimal {

    public MindLoup(Loup agent) : base(agent)
    { }
	public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoup.vivre ...");
        Loup loup = ((Loup)agent);
        if (loup.estMort())
            return;
        if (Living.DEBUG)
            Debug.Log("Animal.construct Faim: " + loup.faim + ", Vie: " + loup.vie);
        actionList.addAction(new A_Promenade(1));
        base.vivre();
    }

}