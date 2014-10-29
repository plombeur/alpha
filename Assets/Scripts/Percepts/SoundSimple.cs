using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundSimple : Sound 
{
    public AudioClip clipToPlay;
    public SoundInformation information;

    private bool started;
    private AudioSource audio;
    private CircleCollider2D soundCollider;

    protected override void Start()
    {
        soundCollider = GetComponent<CircleCollider2D>();
        soundCollider.isTrigger = true;
        started = false;
        this.audio = GetComponent<AudioSource>();
        audio.clip = clipToPlay;
        audio.loop = false;
    }

    protected override void Update()
    {
        if (started && !audio.isPlaying)
            Destroy(gameObject);
    }

    public void play(Identity owner)
    {
        transform.position = owner.getEntity().transform.position;
        setOwner(owner);
        audio.Play();
        started = true;
    }
    public override SoundInformation getInformation()
    {
        return information;
    }
}
