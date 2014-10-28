using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviourAdapter 
{
    private Identity identity;

    public Entity()
    {
        identity = new Identity(this);
    }

    public Identity getIdentity()
    {
        return identity;
    }

    protected override void OnDestroy()
    {
        identity.setAlive(false);
    }
    
}
