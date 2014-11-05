using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RainZoneBehaviour : MonoBehaviour {
    public float radius;
    private Collider2D[] plantsToUpdate;
    public float intensity;
    public float minTimer;
    public float maxTimer;
    private float timer;

    void Awake()
    {

        timer = Random.Range(minTimer, maxTimer);
        print("Pluie pendant : " + timer.ToString());

        plantsToUpdate = Physics2D.OverlapCircleAll(transform.position, radius); // Find itself
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }

        foreach (Collider2D collider in plantsToUpdate)
        {
            //print(collider.gameObject.name);
            Plant toUpdate = collider.gameObject.GetComponent<Plant>();
            if (toUpdate != null)
            {
                toUpdate.addNutriments(intensity * Time.deltaTime);
            }

        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    }
}
