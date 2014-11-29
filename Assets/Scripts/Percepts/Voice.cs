using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
public class Voice : Sound
{
    public InformationVoiceLink[] songs;

    private AudioSource audio;
    private InformationVoiceLink soundPlaying;
    private CircleCollider2D soundCollider;

    protected override void Start()
    {
        soundCollider = GetComponent<CircleCollider2D>();
        audio = GetComponent<AudioSource>();
        soundCollider.isTrigger = true;
        soundCollider.enabled = false;
    }

    protected override void Update()
    {
        if (soundPlaying != null && !audio.isPlaying)
        {
            soundCollider.enabled = false;
            soundPlaying = null;
        }
    }

    public void makeSound(Identity owner, SoundInformation info)
    {
        InformationVoiceLink soundLink = null;
        foreach (InformationVoiceLink song in songs)
        {
            if (song.information == info)
            {
                soundLink = song;
                break;
            }
        }
        if (soundLink == null)
            Debug.LogError("Sound link not found : " + info + " for " + owner);
        else
        {
            setOwner(owner);
            if (audio.isPlaying)
                audio.Stop();
            soundPlaying = soundLink;
            audio.clip = soundLink.clip;
            audio.Play();
            soundCollider.enabled = true;
        }
    }
    public bool isPlaying()
    {
        return audio.isPlaying;
    }
    public override SoundInformation getInformation()
    {
        if (soundPlaying == null)
            return SoundInformation.None;
        return soundPlaying.information;
    }
}
[Serializable]
public class InformationVoiceLink
{
    public SoundInformation information;
    public AudioClip clip;
}