using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HUD : MonoBehaviour
{
    public UserActionWindow userActionWindow;
    public LoupAlpha alphaWolf;

    public ProgressBar lifeBar;
    public ProgressBar MoralBar;

    void Start()
    {

    }

    void Update()
    {
        lifeBar.progress = alphaWolf.vie / (float)alphaWolf.VIE_MAX * 100;
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            if (hit.collider != null)
            {
                UserActionContainer container = hit.collider.GetComponent<UserActionContainer>();
                if (container != null)
                {
                    userActionWindow.gameObject.SetActive(true);
                    return;
                }
            }

            Plane ground = new Plane(-Vector3.forward, 0);
            float distance;
            ground.Raycast(mouseRay, out distance);
            Vector3 worldPosition = mouseRay.GetPoint(distance);
            GameObject moveToActionObject = new GameObject();
            UserActionMoveTo action = moveToActionObject.AddComponent<UserActionMoveTo>();
            moveToActionObject.name = action.getActionLabel();
            action.alphaWolf = alphaWolf;
            action.position = new Vector2(worldPosition.x, worldPosition.y);
            UserActionManager.getInstance().executeUserAction(action);
        }
    }
}
