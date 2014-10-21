using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptView : MonoBehaviourAdapter
{
    public float shortRangeAngle = 180;
    public Detector shortRangeDetector;
    public float longRangeAngle = 90;
    public Detector longRangeDetector;
    private List<Living> livings,livingsShortRange,livingLongRange;

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
            {
                float angle = Vector3.Angle(transform.up, living.transform.position - transform.position);
                if ((angle > (shortRangeAngle / 2.0) || angle < -(shortRangeAngle / 2.0)) && !livings.Contains(living))
                    livingsShortRange.Add(living);
            }
        }
        foreach (GameObject objetSeen in shortRangeDetector.getEnteringGameObjets())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
            {
                float angle = Vector3.Angle(transform.up, living.transform.position - transform.position);
                if ((angle > (longRangeAngle / 2.0) || angle < -(longRangeAngle / 2.0)) && !livings.Contains(living))
                    livingLongRange.Add(living);
            }
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
        livings.AddRange(livingLongRange);
    }

    public List<Living> getLiving()
    {
        return livings;
    }
}
