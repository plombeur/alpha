using UnityEngine;
using System.Collections;

public class Mures : MonoBehaviour {
    public float health;

    public float foodValue;
    public float energyRatio;

    public float maxGrowth;
    public float growthSpeed;

    private float growth;
    private float nutriments;

    private Vector3 maxScale;

    void Awake()
    {
        health = 20;
        growth = 0;
        energyRatio = 0.1f;
        nutriments = 0;
        maxScale = transform.localScale;
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = maxScale * (growth / maxGrowth);
        if (health <= 0)
            Destroy(this.gameObject);

	}

    public void grow(Plant parent)
    {
        float energy =  growthSpeed * Time.deltaTime * energyRatio;
        nutriments -= energy;
        growth += energy / energyRatio;

        if (nutriments <= 0)
        {
            nutriments = 0;
            health -= energy;
        }
    }

    public void addNutriment(float toAdd)
    {
        nutriments += toAdd;
    }
}
