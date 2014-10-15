using UnityEngine;
using System.Collections;

/*
 * IMPORTANT: Un object Living doit être un rigidbody!!
 */
public abstract class Living : MonoBehaviorAdapter
{
    public const bool DEBUG = true;
    private Mind mind;
    private float direction;

    public void construct(Mind mind)
    {
        if (DEBUG)
            Debug.Log("Living.construct");
        this.mind = mind;
    }
	
	void Update () {
        mind.vivre();
	}

    /*
     * Start devra appeller le base.construct(...)
    */
    void Start()
    {
        onCreate();
    }
    protected abstract void onCreate();
    protected void fd(float pas)
    {
        if (Living.DEBUG)
            Debug.Log("Living.fd " + pas);
    }
    protected void lt(float pas)
    {
        direction += pas;
    }
    protected void rt(float pas)
    {
        direction -= pas;
    }
}