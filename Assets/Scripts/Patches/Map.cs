using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	public int size_x;
	public int size_y;
	protected Patch[] patches;
	public GameObject prefabPatch;
	// Use this for initialization

	void Start () {
		patches = new Patch[size_x*size_y];
		for (int i=0; i<size_x; i++) {
			for (int j=0; j<size_y; j++) {
				patches[i*size_x+j] = new Cailloux();
				Vector3 pos = new Vector3(i,j,-10);
				Instantiate(prefabPatch, pos, Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Patch getPatch(float x,float y){
		return patches [Mathf.FloorToInt (x) * this.size_x + Mathf.FloorToInt (y)];
	}
}
