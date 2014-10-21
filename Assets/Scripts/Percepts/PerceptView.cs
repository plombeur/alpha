using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptView : MonoBehaviourAdapter
{
    public float shortRangeAngle = 180;
    public Detector shortRangeDetector;
    public float longRangeAngle = 90;
    public Detector longRangeDetector;
    private List<Living> livings;

    void Start()
    {
        livings = new List<Living>();
    }

    void Update()
    {
        foreach (GameObject objetSeen in shortRangeDetector.getEnteringGameObjets())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
            {
                float angle = Quaternion.Angle(living.transform.rotation, transform.rotation);
                if ((angle > (shortRangeAngle / 2.0) || angle < -(shortRangeAngle / 2.0)) && !livings.Contains(living))
                    livings.Add(living);
            }
        }
        foreach (GameObject objetSeen in shortRangeDetector.getEnteringGameObjets())
        {
            Living living = objetSeen.GetComponent<Living>();
            if (living != null)
            {
                float angle = Quaternion.Angle(living.transform.rotation, transform.rotation);
                if ((angle > (longRangeAngle / 2.0) || angle < -(longRangeAngle / 2.0)) && !livings.Contains(living))
                    livings.Add(living);
            }
        }
        foreach (GameObject objetSeen in shortRangeDetector.getExitingGameObjects())
        {

        }
        foreach (GameObject objetSeen in longRangeDetector.getEnteringGameObjets())
        {

        }
    }
}
