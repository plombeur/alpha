using UnityEngine;
using System.Collections;

public class TreeManager : MonoBehaviour {
    public GameObject[] prefabs;
    public Map map;
    public float density;
    private Rect m_Bounds;

    void Start()
    {
        if (false || map == null)
        {
            Debug.Log(this.GetType() + " :: Missing Map !");
            Destroy(this.gameObject);
            return;
        }
        m_Bounds = new Rect(map.getLeftBorder(), map.getUpperBorder(), map.getRightBorder() - map.getLeftBorder(), map.getLowerBorder() - map.getUpperBorder());
        density = Mathf.Clamp(density, 0, 100);
        
        generateTrees();
    }

    void Update()
    {     
    }

    void generateTrees()
    {
        float rangeX = Mathf.Sqrt(Mathf.Pow(m_Bounds.xMax - m_Bounds.xMin, 2));
        float rangeY = Mathf.Sqrt(Mathf.Pow(m_Bounds.yMax - m_Bounds.yMin, 2)); 

        int nbTrees = (int)((density / 100) * rangeX * rangeY);
        //Debug.Log("Pop de " + nbTrees + " arbres (max).");

        for (int i = 0; i < nbTrees; ++i)
        {
            Vector2 position = new Vector2(Random.Range(m_Bounds.x, m_Bounds.xMax), Random.Range(m_Bounds.y, m_Bounds.yMax));
            int treeType = Random.Range(0, prefabs.Length);

            bool canpop = true;
            Collider2D[] overlap = Physics2D.OverlapCircleAll(position, 2.0F);
            foreach(Collider2D collide in overlap) {
                if (collide.gameObject.GetComponent<Plant>() != null)
                    canpop = false;
            }

            if (canpop)
            {
                GameObject Tree = (GameObject)Instantiate(prefabs[treeType], new Vector3(position.x, position.y), transform.rotation);
                
                Tree.transform.parent = this.transform;
                Plant plantScript = Tree.GetComponent<Plant>();
                if (plantScript != null)
                {
                    plantScript.setStartingGrowth(Random.Range(0, plantScript.maxGrowth));
                }
            }
        }
    }
}
