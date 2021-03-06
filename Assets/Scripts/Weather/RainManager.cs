﻿using UnityEngine;
using System.Collections;

public class RainManager : MonoBehaviour {
    public GameObject rainZonePrefab;
    public float zoneCount;
    public float range;
    public float probability;
    private Rect m_Bounds;

    public float delay;
    private float m_Timer;

	void Start () {
        if (rainZonePrefab == null)
        {
            Debug.Log(this.GetType() + " :: Missing prefab !");
            Destroy(this.gameObject);
            return;
        }
        m_Bounds = GameManager.getInstance().getDimensions();
        probability = Mathf.Clamp(probability, 0, 100);
        zoneCount = Mathf.Clamp(zoneCount, 0, zoneCount);
	}
	
	void Update () {
        if (m_Timer <= 0 && Random.Range(0, 100) <= probability)
        {
            m_Timer = delay;
            Vector2 center = new Vector2(Random.Range(m_Bounds.x, m_Bounds.xMax), Random.Range(m_Bounds.y, m_Bounds.yMax));
            Vector2 mvt = new Vector2(Random.Range(25, 50), Random.Range(25, 50));

            //Debug.Log("Pop pluie : " + center);

            for (int i = 0; i < zoneCount; ++i)
            {
                float ray = Random.Range(1, range);
                float tetha = Random.Range(0, 2 * Mathf.PI);
                Vector2 pos = new Vector2(center.x + ray * Mathf.Cos(tetha) - 5.0f, center.y + ray * Mathf.Sin(tetha) - 5.0f);
                GameObject zone = (GameObject)Instantiate(rainZonePrefab, new Vector3(pos.x, pos.y, -1.0f), transform.rotation);
                zone.transform.parent = this.transform;
                Rigidbody2D body = zone.GetComponent<Rigidbody2D>();
                if (body != null)
                {
                    body.AddRelativeForce(mvt);
                }
                //print("Pluie en : " + pos.ToString());
            }
        }
        else
        {
            m_Timer -= Time.deltaTime;
        }
	}
}
