using System;

class MindRabbit : MindAnimal
{

    public MindRabbit(Rabbit rabbit) : base(rabbit)
    {
    
    }

    public override void vivre()
    {
        actionList.addAction(new A_Promenade(((Animal)agent).vitesse));
        base.vivre();
    }
}
