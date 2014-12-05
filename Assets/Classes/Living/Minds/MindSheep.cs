using UnityEngine;
using System.Collections;

public class MindSheep : MindAnimal
{

    public MindSheep(Sheep agent)
        : base(agent)
    { }
    public override void vivre()
    {
        base.vivre();
        Sheep animal = (Sheep) agent;
        float directionDeFuite = animal.getDirectionFuiteLoups();
        if (directionDeFuite != -1)
        {
            actionList.addAction(new A_Fuite());
        }
        actionList.addAction(new A_Promenade(animal.vitesse));
    }

}