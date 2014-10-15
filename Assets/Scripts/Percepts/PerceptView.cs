using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptView : MonoBehaviorAdapter 
{
    public float angle = 180;
    private List<Living> livings;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        float angle = Quaternion.Angle(collider.transform.rotation,transform.rotation);
        if (angle > (this.angle / 2.0) || angle < -(this.angle / 2.0))
            return;
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
