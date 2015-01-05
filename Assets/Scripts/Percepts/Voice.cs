using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyEmitter))]
//[RequireComponent(typeof(CircleCollider2D))]
public class Voice : Sound
{
    public InformationVoiceLink[] songs;
    public float maturity = 0;

    private InformationVoiceLink soundPlaying;
   // private CircleCollider2D soundCollider;

    private MyEmitter emitter;

    protected override void Start()
    {
        //soundCollider = GetComponent<CircleCollider2D>();
       // soundCollider.isTrigger = true;
      //  soundCollider.enabled = false;
        emitter = GetComponent<MyEmitter>();
    }

    protected override void Update()
    {
        if (soundPlaying != null && emitter.HasFinished())
        {
           // soundCollider.enabled = false;
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
        if (info == SoundInformation.None)
            emitter.Stop();
        else if (soundLink == null)
            Debug.LogError("Sound link not found : " + info + " for " + owner);
        else
        {
            setOwner(owner);
            if (!emitter.HasFinished())
                emitter.Stop();
            soundPlaying = soundLink;
            emitter.asset = soundLink.clip;
            emitter.PlayWithRefresh();
            emitter.getParameter("Maturity").setValue(maturity);
         //   soundCollider.enabled = true;
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
    public FMODAsset clip;
}