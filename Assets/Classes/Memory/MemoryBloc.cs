using UnityEngine;
using System.Collections;

public class MemoryBloc 
{
    private Identity identity;
    private Vector3 lastPosition;
    private float timeElapsed;

    
    public MemoryBloc(Identity identity)
    {
        this.identity = identity;
        lastPosition = identity.getEntity().transform.position;
    }

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
    public void update(float deltaTime)
    {
        timeElapsed += deltaTime;
    }
    public void updatePosition(Vector3 position)
    {
        timeElapsed = 0;
        lastPosition = position;
    }
}
