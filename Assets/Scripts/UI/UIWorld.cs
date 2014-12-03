using UnityEngine;
using System.Collections;

public class UIWorld : MonoBehaviour 
{
    public WorldLifeBar prefabLifeBar;
    public WorldLifeThreatBar prefabLifeThreatBar;

    private static UIWorld instance;

    void Awake()
    {
        instance = this;
    }

    public static UIWorld getInstance()
    {
        return instance;
    }

    public void registerWorldLifeThreatBar(Loup target)
    {
        GameObject worldLifeBarObject = GameObject.Instantiate(prefabLifeThreatBar.gameObject) as GameObject;
        WorldLifeBar worldLifeBarScript = worldLifeBarObject.GetComponent<WorldLifeThreatBar>();
        worldLifeBarScript.target = target;
        worldLifeBarObject.transform.parent = transform;
        Canvas.ForceUpdateCanvases();
    }

    public void registerWorldLifeBar(Animal target)
    {
        GameObject worldLifeBarObject = GameObject.Instantiate(prefabLifeBar.gameObject) as GameObject;
        WorldLifeBar worldLifeBarScript = worldLifeBarObject.GetComponent<WorldLifeBar>();
        worldLifeBarScript.target = target;
        worldLifeBarObject.transform.parent = transform;
        Canvas.ForceUpdateCanvases();
    }
}
