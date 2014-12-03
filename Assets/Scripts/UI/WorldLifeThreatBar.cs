using UnityEngine;
using System.Collections;

public class WorldLifeThreatBar : MonoBehaviour 
{
    public LoupBeta target;
    public Vector2 offsetPosition = new Vector2(0, 0.6f);

    public ProgressBar progressBarLife;
    public ProgressBar progressBarThreat;

	protected void Update () 
    {
        if (target != null)
        {
            transform.position = target.transform.position + new Vector3(offsetPosition.x, offsetPosition.y, 0);
            progressBarLife.progress = target.vie / (float)target.VIE_MAX * 100;
            progressBarThreat.progress = target.threat / (float)target.THREAT_MAX * 100;

        }
        else
            Destroy(gameObject);

	}
}
