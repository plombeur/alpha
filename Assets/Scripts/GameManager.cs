using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public LoupAlpha alphaWolf;
    public EventManager eventManager;
    public HUD hud;
    public ToolTipManager toolTipManager;
    public CameraController2D cameraController;

    public Transform mapDelimiterBottomLeft, mapDelimiterTopRight;
    private Rect dimensions;

    void Awake()
    {
        instance = this;

        if (mapDelimiterBottomLeft == null)
            Debug.LogError("No delimiter Up Left Linked !!!");
        if (mapDelimiterTopRight == null)
            Debug.LogError("No delimiter Bottom Right Linked !!!");
        if (mapDelimiterBottomLeft != null && mapDelimiterTopRight != null)
            dimensions = new Rect(mapDelimiterBottomLeft.position.x, mapDelimiterBottomLeft.position.y, mapDelimiterTopRight.position.x - mapDelimiterBottomLeft.position.x, mapDelimiterTopRight.position.y - mapDelimiterBottomLeft.position.y);
        if (alphaWolf == null)
            Debug.LogError("No Alpha Wolf Linked !!!");
        if (eventManager == null)
            Debug.LogError("No EventManager Linked !!!");
        if (eventManager == null)
            Debug.LogError("No HUD Linked !!!");
        if (toolTipManager == null)
            Debug.LogError("No ToolTipManager Linked !!!");
        if (cameraController == null)
            Debug.LogError("No Camera Controller 2D Linked !!!");

        eventManager.addEventManagerListener(hud);
        eventManager.addEventManagerListener(cameraController);
    }

    void Start()
    {


    }

    void Update()
    {

    }

    public static GameManager getInstance()
    {
        return instance;
    }

    public EventManager getEventManager()
    {
        return eventManager;
    }
    public HUD getHUD()
    {
        return hud;
    }
    public ToolTipManager getToolTipManager()
    {
        return toolTipManager;
    }

    public Rect getDimensions()
    {
        return dimensions;
    }

}
