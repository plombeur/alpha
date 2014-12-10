using UnityEngine;
using System.Collections;

public class MindSheep : MindAnimal
{

    public MindSheep(Sheep agent)
        : base(agent)
    { }
    public override void vivre()
    {
        actionList.addAction(new A_PromenadeEtFlocking());
        base.vivre();
    }

}