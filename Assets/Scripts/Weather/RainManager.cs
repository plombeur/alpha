using UnityEngine;
using System.Collections;

public class RainManager : MonoBehaviour {
    public GameObject RainZonePrefab;
	// Use this for initialization
	void Start () {
        if (RainZonePrefab == null)
        {
            Destroy(this.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
