using UnityEngine;
using System.Collections;

public class SoundSimple : Sound 
{
    public FMODAsset clipToPlay;
    public SoundInformation information;

    private FMOD_StudioEventEmitter emiter;

    private bool started;
    private CircleCollider2D soundCollider;

    protected override void Start()
    {
        emiter = gameObject.AddComponent<FMOD_StudioEventEmitter>();
        emiter.asset = clipToPlay;
        soundCollider = GetComponent<CircleCollider2D>();
        soundCollider.isTrigger = true;
        started = false;
        soundCollider.enabled = false;
    }

    protected override void Update()
    {
        if (started && emiter.HasFinished())
            Destroy(gameObject);
    }

    public void play(Identity owner)
    {
        soundCollider.enabled = true;
        transform.position = owner.getEntity().transform.position;
        setOwner(owner);
        emiter.Play();
        started = true;
    }
    public override SoundInformation getInformation()
    {
        return information;
    }
}
