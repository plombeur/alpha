using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HUD : MonoBehaviour,EventManagerListener
{
    public UserActionWindow userActionWindow;

    public ProgressBar lifeBar;
    public ProgressBar MoralBar;

    void Start()
    {

    }

    void Update()
    {
        lifeBar.progress = GameManager.getInstance().alphaWolf.vie / (float)GameManager.getInstance().alphaWolf.VIE_MAX * 100;
    }

    public bool onMouseButtonDown(int button)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
        if (hit.collider != null)
        {
            UserActionContainer container = hit.collider.GetComponent<UserActionContainer>();
            if (container != null)
            {
                userActionWindow.gameObject.SetActive(true);
                return true;
            }
        }

        return false;
    }

    public bool onMouseButtonUp(int button)
    {
        return false;
    }
}
