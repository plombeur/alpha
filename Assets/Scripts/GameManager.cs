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

    public Transform mapDelimiterUpLeft, mapDelimiterBottomRight;
    private Rect dimensions;

    void Awake()
    {
        instance = this;

        if (mapDelimiterUpLeft == null)
            Debug.LogError("No delimiter Up Left Linked !!!");
        if (mapDelimiterBottomRight == null)
            Debug.LogError("No delimiter Bottom Right Linked !!!");
        if (mapDelimiterUpLeft != null && mapDelimiterBottomRight != null)
            dimensions = new Rect(mapDelimiterUpLeft.position.x, mapDelimiterUpLeft.position.y, mapDelimiterBottomRight.position.x - mapDelimiterUpLeft.position.x, mapDelimiterBottomRight.position.y - mapDelimiterUpLeft.position.y);
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
