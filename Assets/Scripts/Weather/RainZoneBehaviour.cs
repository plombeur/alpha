using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RainZoneBehaviour : MonoBehaviour {
    private float radius;
    private Collider2D[] plantsToUpdate;
    private float intensity;
    private float timer;

    void Awake()
    {
        radius = 5.0f;
        intensity = 10.0f;

        timer = Random.Range(10, 60);
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
                print("is plant");
                toUpdate.addNutriments(intensity);
            }

        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    }
}
