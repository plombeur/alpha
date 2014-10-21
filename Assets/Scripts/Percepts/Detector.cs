using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detector : MonoBehaviourAdapter
{
    private List<GameObject> objetsToAdd;
    private List<GameObject> objetsToRemove;

	void Start ()
    {
        objetsToAdd = new List<GameObject>();
        objetsToRemove = new List<GameObject>();
	}
	
	void FixedUpdate ()
    {
        objetsToAdd.Clear();
        objetsToRemove.Clear();
	}

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        objetsToAdd.Add(collider.gameObject);
    }
    protected override void OnTriggerExit2D(Collider2D collider)
    {
        base.OnTriggerExit2D(collider);
        objetsToRemove.Add(collider.gameObject);
    }

    public List<GameObject> getEnteringGameObjets()
    {
        return objetsToAdd;
    }
    public List<GameObject> getExitingGameObjects()
    {
        return objetsToRemove;
    }
}
