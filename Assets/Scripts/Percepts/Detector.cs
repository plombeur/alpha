using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detector : MonoBehaviourAdapter
{
    public float angle;
    private List<GameObject> objectsInTemp;
    private List<GameObject> objectsOutTemp;

    private List<GameObject> objectsToAdd;
    private List<GameObject> objectsToRemove;

	void Start ()
    {
        objectsToAdd = new List<GameObject>();
        objectsToRemove = new List<GameObject>();
        objectsInTemp = new List<GameObject>();
        objectsOutTemp = new List<GameObject>();
	}
	
	void FixedUpdate ()
    {
        objectsToAdd.Clear();
        objectsToRemove.Clear();
	}

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (this.angle < 360)
        {
          
            float angle = Vector3.Angle(transform.up, collider.gameObject.transform.position - transform.position);
           
            if (angle <= (this.angle / 2.0))
            {
                if (objectsInTemp.Contains(collider.gameObject)) 
                objectsToAdd.Add(collider.gameObject);
                objectsInTemp.Add(collider.gameObject);
            }
            else
            {
                objectsOutTemp.Add(collider.gameObject);
               
            }
        }
        else
        {
            objectsToAdd.Add(collider.gameObject);
            objectsInTemp.Add(collider.gameObject);
        }        
    }
    protected override void Update()
    {
        
        base.Update();
        int index = 0;
        while (index < objectsInTemp.Count)
        {
            
            GameObject o = objectsInTemp[index];
              float angle = Vector3.Angle(transform.up, o.transform.position - transform.position);
              if (angle > (this.angle / 2.0))
              {
                 
                  objectsInTemp.Remove(o);
                  objectsToRemove.Add(o);
                  objectsOutTemp.Add(o);
              }
              ++index;
        }
        index = 0;
        while (index < objectsOutTemp.Count)
        {
            
            GameObject o = objectsOutTemp[index];
            float angle = Vector3.Angle(transform.up, o.transform.position - transform.position);
            if (angle <= (this.angle / 2.0))
            {
                
                objectsOutTemp.Remove(o);
                objectsInTemp.Add(o);
                objectsToAdd.Add(o);
            }
            ++index;
        }
    }
    protected override void OnTriggerExit2D(Collider2D collider)
    {
        base.OnTriggerExit2D(collider);
        if (!objectsOutTemp.Remove(collider.gameObject))
        {
            objectsInTemp.Remove(collider.gameObject);
            objectsToRemove.Add(collider.gameObject);
        }
    }

    public List<GameObject> getEnteringGameObjets()
    {
        return objectsToAdd;
    }
    public List<GameObject> getExitingGameObjects()
    {
        return objectsToRemove;
    }
}
