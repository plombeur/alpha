using UnityEngine;
using System.Collections;

public class WorldLifeThreatBar : WorldLifeBar 
{
    public ProgressBar progressBarThreat;

	protected override void Update () 
    {
        base.Update();
	    if (target != null)
        {
            //progressBarThreat.progress = target. / (float)target.VIE_MAX * 100;
        }
	}
}
