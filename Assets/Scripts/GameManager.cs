using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, EventManagerListener, MemoryListener
{
    private static GameManager instance;

    public FMOD_StudioEventEmitter musicEmitter;
    public LoupAlpha alphaWolf;
    public EventManager eventManager;
    public HUD hud;
    public UIWorld uiWorld;
    public GameObject uiWorldPostFog;
    public ToolTipManager toolTipManager;
    public ObjectifWindow objectifWindow;
    public InfoWindow informationWindow;
    public TutorialManager tutorialManager;
    public CameraController2D cameraController;
    public float slowTimeSpeed = 4;

    private bool gameOver = false;
    private bool gameWin = false;
    public bool WolvesModeHunt = false;
    private float fadeMusic = 0;
    public bool isMusicFight = false;

    public Transform mapDelimiterBottomLeft, mapDelimiterTopRight;
    private Rect dimensions;

    private Plane groundPlane = new Plane(-Vector3.forward, Vector3.zero);

    public bool stopTheTime = false;
    private float lastTime = 0;

    public float fogRadius = 3;
    public GameObject herd;

    public GameObject prefabMemoryDrawer;
    private Dictionary<MemoryBloc, MemoryDrawer> memoryDrawers = new Dictionary<MemoryBloc, MemoryDrawer>();

    public void setCameraFocus(Transform transform)
    {
        cameraController.setFollowTarget(transform);
    }
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
        if (uiWorldPostFog == null)
            Debug.LogError("No UI World POST FOG Linked !!!");
        if (herd == null)
            Debug.LogError("No Herd Linked !!!");

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
        if (isMusicFight)
            fadeMusic = Mathf.Lerp(0, 1, fadeMusic + Time.deltaTime);
        else
            fadeMusic = Mathf.Lerp(1, 0, (1-fadeMusic) + Time.deltaTime);

        fadeMusic = Mathf.Clamp(fadeMusic, 0, 1);
        musicEmitter.getParameter("Fight").setValue(fadeMusic);
        /*
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
        }*/
        //FMOD_StudioSystem.instance.System.getBus()
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        //lastTime += realDeltaTime;
        upateMemoryDrawer();

        if (isGameOver())
            stopTime();

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
    private void upateMemoryDrawer()
    {
        foreach (MemoryDrawer drawer in memoryDrawers.Values)
        {
            Vector3 position = drawer.target.getLastPosition();
            position.z = drawer.icon.transform.position.z;
            drawer.icon.transform.position = position;

            bool drawerActive = true;
            foreach (Loup wolf in herd.transform.GetComponentsInChildren<Loup>())
            {
                Vector3 delta = wolf.transform.position - position;
                delta.z = 0;
                if (delta.magnitude <= GameManager.getInstance().fogRadius)
                {
                    drawerActive = false;
                    drawer.icon.transform.GetChild(0).GetComponent<Image>().sprite = drawer.target.getEntity().GetComponent<SpriteRenderer>().sprite;

                    break;
                }
            }
            drawer.icon.SetActive(drawerActive);
        }
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

        objectifWindow.hideObjectifMiniWindow();
        objectifWindow.hideObjectifWindow();
        GameManager.getInstance().informationWindow.hideInfoPanel();

        hud.setGameOver("Tu as perdu !", reasonText);

        stopTime();
    }

    public void setGameWin()
    {
        gameOver = true;
        gameWin = true;

        objectifWindow.hideObjectifMiniWindow();
        objectifWindow.hideObjectifWindow();
        GameManager.getInstance().informationWindow.hideInfoPanel();

        hud.setGameOver("Tu as gagné !", "Bravo, tu as réussi a ....");

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

        AU_Chasse chasse = alphaWolf.getCurrentAction() as AU_Chasse;
        AU_MoveTo moving = alphaWolf.getCurrentAction() as AU_MoveTo;

        if (hunt && moving != null)
        {
            GameObject userActionSlapObject = new GameObject();
            UserActionHunt huntAction = userActionSlapObject.AddComponent<UserActionHunt>();
            huntAction.alphaWolf = alphaWolf;
            huntAction.position = moving.getActionChasseConverti().targetPosition;
            userActionSlapObject.name = huntAction.getActionLabel();
            UserActionManager.getInstance().executeUserAction(huntAction);
        }
        else if (!hunt && chasse != null)
        {
            GameObject userActionSlapObject = new GameObject();
            UserActionMoveTo moveTo = userActionSlapObject.AddComponent<UserActionMoveTo>();
            moveTo.alphaWolf = alphaWolf;
            moveTo.position = chasse.targetPosition;
            userActionSlapObject.name = moveTo.getActionLabel();
            UserActionManager.getInstance().executeUserAction(moveTo);
        }
    }

    public void onMemoryAdd(Memory memory, MemoryBloc bloc)
    {
        // Debug.LogError(bloc.getEntity());
        if (bloc.getEntity() as Plant != null || bloc.getEntity() as Loup != null)
            return;
        GameObject drawer = GameObject.Instantiate(prefabMemoryDrawer) as GameObject;
        drawer.transform.GetChild(0).GetComponent<Image>().sprite = bloc.getEntity().GetComponent<SpriteRenderer>().sprite;
        drawer.transform.SetParent(uiWorldPostFog.transform);
        Vector3 position = bloc.getLastPosition();
        position.z = drawer.transform.position.z;
        drawer.transform.position = position;
        drawer.SetActive(true);
        memoryDrawers.Add(bloc, new MemoryDrawer(bloc, drawer));
    }
   
    public void onMemoryRemove(Memory memory, MemoryBloc bloc)
    {
        if (!memoryDrawers.ContainsKey(bloc))
            return;
        MemoryDrawer drawer = memoryDrawers[bloc];
        memoryDrawers.Remove(bloc);
        Destroy(drawer.icon);
    }
}
struct MemoryDrawer
{
    public MemoryBloc target;
    public GameObject icon;

    public MemoryDrawer(MemoryBloc target, GameObject icon)
    {
        this.target = target;
        this.icon = icon;
    }
}