using UnityEngine;
using System.Collections;

/*
 * IMPORTANT: Un object Living doit être un rigidbody!!
 */
public abstract class Living : Entity
{
    public const bool DEBUG = false;
    public Mind mind;

    public void construct(Mind mind)
    {
        if (DEBUG)
            Debug.Log("Living.construct");
        this.mind = mind;
    }
	
	protected override void Update () {
        if (mind == null)
        {
            if (Living.DEBUG)
                Debug.Log("[Living] Mind est null !!");
        }
        else
            mind.vivre();
	}

    /*
     * Start devra appeller le base.construct(...)
    */
    protected override void Awake()
    {
        onCreate();
    }
    protected abstract void onCreate();
    protected void fd(float pas)
    {
        if (Living.DEBUG)
            Debug.Log("Living.fd " + pas);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        mind.OnTriggerEnter2D(other);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        mind.OnCollisionEnter2D(other);
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Living agent = obj as Living;
        return agent != null && this.getIdentity().getID() == agent.getIdentity().getID() ;
    }
}