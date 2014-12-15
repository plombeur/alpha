using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, EventManagerListener, MemoryListener
{
    private static GameManager instance;

    public LoupAlpha alphaWolf;
    public EventManager eventManager;
    public HUD hud;
    public UIWorld uiWorld;
    public ToolTipManager toolTipManager;
    public ObjectifWindow objectifWindow;
    public InfoWindow informationWindow;
    public TutorialManager tutorialManager;
    public CameraController2D cameraController;
    public float slowTimeSpeed = 4;

    private bool gameOver = false;
    private bool gameWin = false;
    public bool WolvesModeHunt = false;

    public Transform mapDelimiterBottomLeft, mapDelimiterTopRight;
    private Rect dimensions;

    private Plane groundPlane = new Plane(-Vector3.forward, Vector3.zero);

    public bool stopTheTime = false;
    private float lastTime = 0;

    public bool drawMemoryOnMap = true;
    public GameObject prefabMemoryDrawer;
    private Dictionary<MemoryBloc, GameObject> memoryDrawers = new Dictionary<MemoryBloc,GameObject>();

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
        if (informationWindow == null)
            Debug.LogError("No Information Window Linked !!!");
        if (objectifWindow == null)
            Debug.LogError("No Objectif Window Linked !!!");
        if (tutorialManager == null)
            Debug.LogError("No Tutorial Manager Linked !!!");
        if (uiWorld == null)
            Debug.LogError("No UI World Linked !!!");

        eventManager.addEventManagerListener(hud);
        eventManager.addEventManagerListener(this);
        eventManager.addEventManagerListener(cameraController);
    }

    void Start()
    {
        alphaWolf.GetComponent<Memory>().addMemoryListener(this);
    }

    void Update()
    {
        float realDeltaTime = Time.realtimeSinceStartup - lastTime;
        if (stopTheTime)
        {
            if (Time.timeScale <= 0.1)
                Time.timeScale = 0;
            else
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0, realDeltaTime * slowTimeSpeed);
        }
        else
        {
            if (Time.timeScale >= 1)
                Time.timeScale = 1;
            else
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1, realDeltaTime * slowTimeSpeed);
        }
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        lastTime += realDeltaTime;
        upateMemoryDrawer();
    }
    private void upateMemoryDrawer()
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


    public bool onMouseButtonDown(int button)
    {
        if (button == 1)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            if (hit.collider != null)
            {
                LoupBeta beta = hit.collider.gameObject.GetComponent<LoupBeta>();
                if (beta != null)
                {
                    GameObject userActionSlapObject = new GameObject();
                    UserActionSlap slapAction = userActionSlapObject.AddComponent<UserActionSlap>();
                    slapAction.alphaWolf = alphaWolf;
                    slapAction.betaWolf = beta;
                    userActionSlapObject.name = slapAction.getActionLabel();
                    UserActionManager.getInstance().executeUserAction(slapAction);
                    return true;
                }
            }

            float distanceCast;
            groundPlane.Raycast(mouseRay, out distanceCast);
            Vector3 worldPosition = mouseRay.GetPoint(distanceCast);
            GameObject userActionObject = new GameObject();
            if (WolvesModeHunt)
            {
                UserActionHunt huntAction = userActionObject.AddComponent<UserActionHunt>();
                huntAction.alphaWolf = alphaWolf;
                huntAction.position = worldPosition;
                userActionObject.name = huntAction.getActionLabel();
                UserActionManager.getInstance().executeUserAction(huntAction);
            }
            else
            {
                UserActionMoveTo moveAction = userActionObject.AddComponent<UserActionMoveTo>();
                moveAction.alphaWolf = alphaWolf;
                moveAction.position = worldPosition;
                userActionObject.name = moveAction.getActionLabel();
                UserActionManager.getInstance().executeUserAction(moveAction);
            }
            return true;
        }
        return false;
    }

    public bool onMouseButtonUp(int button)
    {
        return false;
    }

    public void slowAndStopTime()
    {
        stopTheTime = true;
    }
    public void stopTime()
    {
        stopTheTime = true;
        Time.timeScale = 0;
    }
    public void restartTime()
    {
        stopTheTime = false;
    }

    public void setGameLost(string reasonText)
    {
        gameOver = true;
        gameWin = false;
    }

    public void setGameWin()
    {
        gameOver = true;
        gameWin = true;

        objectifWindow.hideObjectifMiniWindow();
        objectifWindow.hideObjectifWindow();
        GameManager.getInstance().informationWindow.hideInfoPanel();

        stopTime();
    }

    public bool isGameOver()
    {
        return gameOver;
    }
    public bool isGameWon()
    {
        return gameWin;
    }

    public void setModeHunt(bool hunt)
    {
        this.WolvesModeHunt = hunt;
    }

    public void onMemoryAdd(Memory memory, MemoryBloc bloc)
    {
       // Debug.LogError(bloc.getEntity());
        if (bloc.getEntity() as Plant != null || bloc.getEntity() as Loup != null)
            return;
        GameObject drawer = GameObject.Instantiate(prefabMemoryDrawer) as GameObject;
        drawer.transform.GetChild(0).GetComponent<Image>().sprite = bloc.getEntity().GetComponent<SpriteRenderer>().sprite;
        drawer.transform.SetParent(uiWorld.transform);
        drawer.transform.position = bloc.getLastPosition();
        drawer.SetActive(true);
        memoryDrawers.Add(bloc, drawer);
    }

    public void onMemoryRemove(Memory memory, MemoryBloc bloc)
    {
        if (!memoryDrawers.ContainsKey(bloc))
            return;
        GameObject drawer = memoryDrawers[bloc];
        memoryDrawers.Remove(bloc);
        Destroy(drawer);
    }
}
