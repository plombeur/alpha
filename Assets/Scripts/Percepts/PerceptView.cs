using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptView : MonoBehaviourAdapter
{
    public Detector shortRangeDetector;
    public Detector longRangeDetector;
    private List<Living> livings, livingsShortRange, livingLongRange;

    void Start()
    {
        livings = new List<Living>();
        livingsShortRange = new List<Living>();
        livingLongRange = new List<Living>();
    }

    void Update()
    {
        foreach (GameObject objetSeen in shortRangeDetector.getEnteringGameObjets())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livingsShortRange.Add(living);
            Debug.Log("add short");
        }
        foreach (GameObject objetSeen in longRangeDetector.getEnteringGameObjets())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livingLongRange.Add(living);
            Debug.Log("add long");
        }
        foreach (GameObject objetSeen in shortRangeDetector.getExitingGameObjects())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livingsShortRange.Remove(living);
            Debug.Log("rm short");
        }
        foreach (GameObject objetSeen in longRangeDetector.getExitingGameObjects())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
                livingLongRange.Remove(living);
            Debug.Log("rm long");
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
       // Debug.Log("s "+livingsShortRange.Count);
       // if (livingsShortRange.Count > 0)
         //   Debug.Log(livingsShortRange[0]);
       // Debug.Log("l "+livingLongRange.Count);
        //if (livingLongRange.Count > 0)
          //  Debug.Log(livingLongRange[0]);
       
        return livings;
    }
}
