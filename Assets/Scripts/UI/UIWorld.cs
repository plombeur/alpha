using UnityEngine;
using System.Collections;

public class UIWorld : MonoBehaviour 
{
    public WorldLifeBar prefabLifeBar;

    public void registerWorldLifeBar(Animal target)
    {
        GameObject worldLifeBarObject = GameObject.Instantiate(prefabLifeBar) as GameObject;
        WorldLifeBar worldLifeBarScript = worldLifeBarObject.GetComponent<WorldLifeBar>();
        worldLifeBarScript.target = target;
    }
}
