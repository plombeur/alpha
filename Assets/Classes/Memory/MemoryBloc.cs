using UnityEngine;
using System.Collections;

public class MemoryBloc 
{
    private Identity identity;
    private Vector3 lastPosition;
    private float timeElapsed;

    public Entity getEntity()
    {
        return identity.getEntity();
    }
    public Vector3 getLastPosition()
    {
        return lastPosition;
    }
    public float getTimeElapsed()
    {
        return timeElapsed;
    }
}
