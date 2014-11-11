using UnityEngine;
using System.Collections;

public abstract class Sound : MonoBehaviourAdapter 
{
    private Identity owner;

    protected void setOwner(Identity identity)
    {
        this.owner = identity;
    }

    public Identity getOwner()
    {
        return owner;
    }

    public abstract SoundInformation getInformation();
}
