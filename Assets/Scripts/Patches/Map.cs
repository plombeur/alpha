using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Map : MonoBehaviour {
	public int size_x;
	public int size_y;
	protected Patch[] patches;
	public GameObject prefabTerre;
    public GameObject prefabGrass;
	// Use this for initialization

    
	void Start () {
        generateMap();
	}

    void generateMap()
    {
        if (this.transform.childCount != 0)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
        // Create array of patches with script
        patches = new Patch[size_x * size_y];
        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                //patches[i*size_x+j] = new Cailloux();
                Vector3 pos = new Vector3(i + this.transform.position.x, j + this.transform.position.y, -10);
                if ((i < 7 && j < 10) || (i > 40 && j > 20 && j < 35) || (i > 10 && i < 30 && j > 45))
                {
                    patches[i * size_x + j] = ((GameObject)Instantiate(prefabGrass, pos, Quaternion.identity)).GetComponent<Patch>();
                }
                else
                {
                    patches[i * size_x + j] = ((GameObject)Instantiate(prefabTerre, pos, Quaternion.identity)).GetComponent<Patch>();
                }
                patches[i * size_x + j].gameObject.transform.SetParent(this.transform);
            }
        }
        //Destroy(getPatch(1.0f, 1.0f).gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
	}

	public Patch getPatch(float x,float y){
		return patches [Mathf.FloorToInt (x - this.transform.position.x) * this.size_x + Mathf.FloorToInt (y - this.transform.position.y)];
	}

    public float getUpperBorder()
    {

        return this.transform.position.y + size_y - 0.5f;
    }

    public float getLowerBorder()
    {
        return this.transform.position.y - 0.5f;
    }

    public float getLeftBorder()
    {
        return this.transform.position.x - 0.5f;
    }

    public float getRightBorder()
    {
        return this.transform.position.x + size_x - 0.5f;
    }
}
