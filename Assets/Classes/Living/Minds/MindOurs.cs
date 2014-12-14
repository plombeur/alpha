using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MindOurs : MindAnimal
{
    public MindOurs(Ours ours) : base(ours)
    { }

    public override void vivre()
    {
        actionList.addAction(new A_Promenade());
        base.vivre();
    }
}