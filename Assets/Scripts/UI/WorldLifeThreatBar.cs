using UnityEngine;
using System.Collections;

public class WorldLifeThreatBar : MonoBehaviour 
{
    public LoupBeta target;

    public ProgressBar progressBarLife;
    public ProgressBar progressBarThreat;

	protected void Update () 
    {
        if (target != null)
        {
            transform.position = target.transform.position + new Vector3(0, 0.5f);
            progressBarLife.progress = target.vie / (float)target.VIE_MAX * 100;
            //progressBarThreat.progress = target.men / (float)target.VIE_MAX * 100;

        }
        else
            Destroy(gameObject);

	}
}
