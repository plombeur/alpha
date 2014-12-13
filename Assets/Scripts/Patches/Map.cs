using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	void Start () {
	}

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
