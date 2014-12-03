using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	//private int size_x = 50;
	//private int size_y = 50;
    public GameObject grass;
	//protected Patch[] patches;
	// Use this for initialization
	void Start () {
        //generateMap();
	}

    /*void generateMap()
    {
        if (this.transform.childCount != 0)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                DestroyImmediate(this.transform.GetChild(i).gameObject);
            }
        }
        // Create array of patches with script
        patches = new Patch[size_x * size_y];
        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_y; j++)
            {
                //patches[i*size_x+j] = new Cailloux();
                Vector3 pos = new Vector3(i + this.transform.position.x, j + this.transform.position.y, 0);
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
    }*/
    // Update is called once per frame
    void Update()
    {
        
	}

	/*public Patch getPatch(float x,float y){
		return patches [Mathf.FloorToInt (x - this.transform.position.x) * this.size_x + Mathf.FloorToInt (y - this.transform.position.y)];
	}*/

    public float getUpperBorder()
    {
        Transform patches = this.transform.FindChild("Patches").transform.FindChild("Navigateable");
        float maxY = patches.GetChild(0).transform.position.y;
        float currentY;
        for (int i = 1; i < patches.childCount; i++)
        {
            currentY = patches.GetChild(i).transform.position.y;
            if (maxY < currentY)
            {
                maxY = currentY;
            }
        }
        return maxY + patches.GetChild(0).gameObject.renderer.bounds.size.y / 2;
    }

    public float getLowerBorder()
    {
        Transform patches = this.transform.FindChild("Patches").transform.FindChild("Navigateable");
        float minY = patches.GetChild(0).transform.position.y;
        float currentY;
        for (int i = 1; i < patches.childCount; i++)
        {
            currentY = patches.GetChild(i).transform.position.y;
            if (minY > currentY)
            {
                minY = currentY;
            }
        }
        return minY - patches.GetChild(0).gameObject.renderer.bounds.size.y / 2;
    }

    public float getLeftBorder()
    {
        Transform patches = this.transform.FindChild("Patches").transform.FindChild("Navigateable");
        float minX = patches.GetChild(0).transform.position.x;
        float currentX;
        for (int i = 1; i < patches.childCount; i++)
        {
            currentX = patches.GetChild(i).transform.position.x;
            if (minX > currentX)
            {
                minX = currentX;
            }
        }
        return minX - patches.GetChild(0).gameObject.renderer.bounds.size.x / 2;
    }

    public float getRightBorder()
    {
        Transform patches = this.transform.FindChild("Patches").transform.FindChild("Navigateable");
        float maxX = patches.GetChild(0).transform.position.x;
        float currentX;
        for (int i = 1; i < patches.childCount; i++)
        {
            currentX = patches.GetChild(i).transform.position.x;
            if (maxX < currentX)
            {
                maxX = currentX;
            }
        }
        return maxX + patches.GetChild(0).gameObject.renderer.bounds.size.x / 2;
    }
}
