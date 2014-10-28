using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptView : MonoBehaviourAdapter
{
    public Detector shortRangeDetector;
    public Detector longRangeDetector;
    private List<Living> livings, livingsShortRange, livingLongRange;

    protected override void Start()
    {
        livings = new List<Living>();
        livingsShortRange = new List<Living>();
        livingLongRange = new List<Living>();
    }

    protected override void Update()
    {
        foreach (GameObject objetSeen in shortRangeDetector.getEnteringGameObjets())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livingsShortRange.Add(living);
        }
        foreach (GameObject objetSeen in longRangeDetector.getEnteringGameObjets())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livingLongRange.Add(living);
        }
        foreach (GameObject objetSeen in shortRangeDetector.getExitingGameObjects())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livingsShortRange.Remove(living);
        }
        foreach (GameObject objetSeen in longRangeDetector.getExitingGameObjects())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livingLongRange.Remove(living);
        }
        livings.Clear();
        livings.AddRange(livingsShortRange);
        foreach (Living living in livingLongRange)
        {
            if (!livings.Contains(living))
                livings.Add(living);
        }
    }

    public List<Living> getLiving()
    {       
        return livings;
    }
}
