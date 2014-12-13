using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class AutoClearRT : MonoBehaviour {

	public bool NoClearAfterStart = false;
    public Color fogColor = new Color(1,1,1,0.25f);

	void Start () 
	{
        GetComponent<Camera>().backgroundColor = fogColor;
		GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
	}

	void OnPostRender () 
	{
		if(!NoClearAfterStart)
		{
			GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
		}
	}
}
