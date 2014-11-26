using UnityEngine;
using System.Collections;

public class RainManager : MonoBehaviour {
    public GameObject rainZonePrefab;
    public Map map;
    public float zoneCount;
    public float range;
    public float probability;
    private Rect m_Bounds;

    public float delay;
    private float m_Timer;

	void Start () {
        if (rainZonePrefab == null /*|| map == null*/)
        {
            Destroy(this.gameObject);
            return;
        }
        //m_Bounds = new Rect(map.getLeftBorder(), map.getUpperBorder(), map.getRightBorder() - map.getLeftBorder(), map.getLowerBorder() - map.getUpperBorder());
        probability = Mathf.Clamp(probability, 0, 100);
        zoneCount = Mathf.Clamp(zoneCount, 0, zoneCount);
	}
	
	void Update () {
        if (m_Timer <= 0 && Random.Range(0, 100) <= probability)
        {
            print("Pop pluie");
            m_Timer = delay;
            Vector2 center = new Vector2(Random.Range(m_Bounds.x, m_Bounds.xMax), Random.Range(m_Bounds.y, m_Bounds.yMax));
            Vector2 mvt = new Vector2(Random.Range(25, 75), Random.Range(25, 75));

            for (int i = 0; i < zoneCount; ++i)
            {
                float ray = Random.Range(1, range);
                float tetha = Random.Range(0, 2 * Mathf.PI);
                Vector2 pos = new Vector2(center.x + ray * Mathf.Cos(tetha), center.y + ray * Mathf.Sin(tetha));
                GameObject zone = (GameObject)Instantiate(rainZonePrefab, new Vector3(pos.x, pos.y), transform.rotation);
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
