using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Percept : MonoBehaviourAdapter
{
    public Detector hearingDetector;
    private List<Living> livings;

    protected override void Start()
    {
        livings = new List<Living>();
    }

    protected override void Update()
    {
        foreach (GameObject objetSeen in hearingDetector.getEnteringGameObjets())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livings.Add(living);
        }

        foreach (GameObject objetSeen in hearingDetector.getExitingGameObjects())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livings.Remove(living);
        }
    }

    public List<Living> getLiving()
    {
        return livings;
    }
}
