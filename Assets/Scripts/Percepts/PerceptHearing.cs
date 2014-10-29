using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptHearing : MonoBehaviourAdapter
{
    public Detector hearingDetector;
    private List<Sound> sounds;

    protected override void Start()
    {
        sounds = new List<Sound>();
    }

    protected override void Update()
    {
        foreach (GameObject objetSeen in hearingDetector.getEnteringGameObjets())
        {
            Sound living = objetSeen.GetComponent<Sound>();
            if (living != null)
                sounds.Add(living);
        }

        foreach (GameObject objetSeen in hearingDetector.getExitingGameObjects())
        {
            Sound living = objetSeen.GetComponent<Sound>();
            if (living != null)
                sounds.Remove(living);
        }
    }

    public List<Sound> getSounds()
    {
        return sounds;
    }
}
