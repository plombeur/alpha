using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptHearing : MonoBehaviourAdapter
{
    public Detector hearingDetector;
    private List<SoundPercepted> soundsPercepted;
    private Dictionary<Sound, SoundPercepted> soundPerceptLinks;

    protected override void Start()
    {
        soundsPercepted = new List<SoundPercepted>();
        soundPerceptLinks = new Dictionary<Sound, SoundPercepted>();
    }

    protected override void Update()
    {
        foreach (GameObject objetSeen in hearingDetector.getEnteringGameObjets())
        {
            Sound sound = objetSeen.GetComponent<Sound>();
            if (sound != null)
            {
                SoundPercepted percepted = new SoundPercepted();
                soundPerceptLinks[sound] = percepted;
                percepted.identity = sound.getOwner();
                percepted.lastPosition = sound.transform.position;
                percepted.soundInformation = sound.getInformation();
                soundsPercepted.Add(percepted);
            }
        }

        foreach (GameObject objetSeen in hearingDetector.getExitingGameObjects())
        {
            Sound sound = objetSeen.GetComponent<Sound>();
            if (sound != null)
            {
                soundsPercepted.Remove(soundPerceptLinks[sound]);
                soundPerceptLinks.Remove(sound);
            }
        }

        foreach (Sound sound in soundPerceptLinks.Keys) // sound peut etre null si detruit en dehor ????
        {
            SoundPercepted sp = soundPerceptLinks[sound];
            sp.lastPosition = sound.transform.position;
            sp.soundInformation = sound.getInformation();
        }
    }

    public List<SoundPercepted> getSounds()
    {
        return soundsPercepted;
    }
}

public struct SoundPercepted
{
    public Identity identity;
    public SoundInformation soundInformation;
    public Vector2 lastPosition;
}