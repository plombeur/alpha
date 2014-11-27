using UnityEngine;
using System.Collections;

public class WorldLifeBar : MonoBehaviour 
{
    public Animal target;
    public ProgressBar progressBar;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	    if (target != null)
        {
            progressBar.progress = target.vie / (float)target.VIE_MAX * 100;
        }
	}
}
