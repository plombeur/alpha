using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ours : Animal
{
    public bool enColere = false;
    public override float getDirectionFuite()
    {
        throw new System.NotImplementedException();
    }

    public override bool besoinDeFuir()
    {
        throw new System.NotImplementedException();
    }

    public override List<SoundInformation> getSonsInterpellant()
    {
        return new List<SoundInformation>();
    }

    protected override void onCreate()
    {
        peutAvoirPeur = false;
        MindOurs mind = new MindOurs(this);
        base.construct(mind);
    }

    public virtual void blesse(float damage)
    {
        enColere = true;
        base.blesse(damage);
    }
}