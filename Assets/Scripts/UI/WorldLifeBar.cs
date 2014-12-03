using UnityEngine;
using System.Collections;

public class WorldLifeBar : MonoBehaviour 
{
    public Animal target;
    public ProgressBar progressBarLife;
	
	protected virtual void Update () 
    {
        if (target != null)
        {
            transform.position = target.transform.position + new Vector3(0, 0.5f);
            progressBarLife.progress = target.vie / (float)target.VIE_MAX * 100;
        }
        else
            Destroy(gameObject);
	}
}
