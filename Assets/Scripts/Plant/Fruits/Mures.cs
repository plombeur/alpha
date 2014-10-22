using UnityEngine;
using System.Collections;

public class Mures : MonoBehaviour {
    public float foodValue;

    public float maxGrowth;
    public float growthSpeed;

    private float growth;

    private Vector3 maxScale;

    void Awake()
    {
        growth = 0;
        maxScale = transform.localScale;
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = maxScale * (growth / maxGrowth);
	}

    public void grow(Plant parent)
    {
        float energy =  growthSpeed * Time.deltaTime;
        parent.addNutriments(-energy);
        growth += energy;
    }
}
