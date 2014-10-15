using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptView : MonoBehaviorAdapter 
{
    public float angle;
    private List<Living> livings;

    protected override void Update()
    {
        base.Update();

        
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        Living living = collider.gameObject.GetComponent<Living>();
        if (living != null)
            livings.Add(living);
    }

    protected override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);

        livings.Remove(collider.gameObject.GetComponent<Living>());
    }

    public List<Living> getLivings()
    {
        return livings;
    }
}
