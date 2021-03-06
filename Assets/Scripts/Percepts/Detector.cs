﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detector : MonoBehaviourAdapter
{
    public float angle;
    public SpriteRenderer fieldDrawer; // percept shader required
    private List<GameObject> objectsInTemp;
    private List<GameObject> objectsOutTemp;

    private List<GameObject> objectsToAdd;
    private List<GameObject> objectsToRemove;

    protected override void Start()
    {
        objectsToAdd = new List<GameObject>();
        objectsToRemove = new List<GameObject>();
        objectsInTemp = new List<GameObject>();
        objectsOutTemp = new List<GameObject>();
    }

    public bool isInDetector(Vector3 position)
    {
        Vector3 delta = position - transform.position;
        delta.z = 0;

        float scale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);

        if (delta.magnitude > GetComponent<CircleCollider2D>().radius * scale)
            return false;

        if (this.angle < 360)
        {
            float angle = Vector3.Angle(transform.up, delta);
            if (angle <= (this.angle / 2.0))
                return true;
        }
        else
            return true;
        return false;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (this.angle < 360)
        {
            float angle = Vector3.Angle(transform.up, collider.gameObject.transform.position - transform.position);
            if (angle <= (this.angle / 2.0))
            {
                if (!objectsInTemp.Contains(collider.gameObject) && !objectsOutTemp.Contains(collider.gameObject))
                {
                    objectsToAdd.Add(collider.gameObject);
                    objectsInTemp.Add(collider.gameObject);
                }
            }
            else
            {
                if (!objectsInTemp.Contains(collider.gameObject) && !objectsOutTemp.Contains(collider.gameObject))
                    objectsOutTemp.Add(collider.gameObject);
            }
        }
        else
        {
            if (!objectsInTemp.Contains(collider.gameObject) && !objectsOutTemp.Contains(collider.gameObject))
            {
                objectsToAdd.Add(collider.gameObject);
                objectsInTemp.Add(collider.gameObject);
            }
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
    protected override void Update()
    {
        base.Update();
        int index = 0;

        while (index < objectsInTemp.Count)
        {
            bool removed = false;
            GameObject o = objectsInTemp[index];

            Vector3 direction = o.transform.position - transform.position;
            direction.z = 0;
            float angle = Vector3.Angle(transform.up, direction);
           
            if (angle > (this.angle / 2.0))
            {
                removed = objectsInTemp.Remove(o);
                objectsToRemove.Add(o);
                objectsOutTemp.Add(o);
            }
            if (!removed)
                ++index;
        }
        index = 0;
        while (index < objectsOutTemp.Count)
        {
            bool removed = false;
            GameObject o = objectsOutTemp[index];
            Vector3 direction = o.transform.position - transform.position;
            direction.z = 0;
            float angle = Vector3.Angle(transform.up, direction);
            if (angle <= (this.angle / 2.0))
            {
                removed = objectsOutTemp.Remove(o);
                objectsInTemp.Add(o);
                objectsToAdd.Add(o);
            }
            if (!removed)
                ++index;
        }

        if (fieldDrawer != null)
        {
            fieldDrawer.material.SetFloat("_angle", angle);
        }

      
    }
   
    protected override void LateUpdate()
    {
        base.LateUpdate();
       
        objectsToAdd.Clear();
        objectsToRemove.Clear();
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
