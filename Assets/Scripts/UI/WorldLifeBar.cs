using UnityEngine;
using System.Collections;

public class WorldLifeBar : MonoBehaviour 
{
    public Animal target;
    public Vector2 offsetPosition = new Vector2(0, 0);

    public ProgressBar progressBarLife;
	
	protected virtual void Update () 
    {
        if (target != null)
        {
            transform.position = target.transform.position + new Vector3(offsetPosition.x,offsetPosition.y,0);
            progressBarLife.progress = target.vie / (float)target.VIE_MAX * 100;
        }
        else
            Destroy(gameObject);
	}
}
