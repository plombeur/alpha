using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController2D : MonoBehaviour
{
    public bool followTarget = true;
    public Transform target;
    private bool mouseDown = false;
    private Vector3 lastMousePositonOnWorld;
    private Plane groundPlane = new Plane(-Vector3.forward, Vector3.zero);

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            float distanceCast;
            groundPlane.Raycast(mouseRay, out distanceCast);
            Vector3 worldPosition = mouseRay.GetPoint(distanceCast);
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                mouseDown = true;
            }
            else if (mouseDown)
            {
                followTarget = false;
                Vector3 delta = worldPosition - lastMousePositonOnWorld;
                if (delta.magnitude != 0)
                    camera.transform.Translate(-delta);
            }
            mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            groundPlane.Raycast(mouseRay, out distanceCast);
            worldPosition = mouseRay.GetPoint(distanceCast);
            lastMousePositonOnWorld = worldPosition;
        }
        else
            mouseDown = false;
    }
    void LateUpdate()
    {
        if (followTarget && target != null)
        {
            Vector3 position = target.position;
            position.z = -1;
            transform.position = position;
        }
    }

    public void setFollowTarget(bool value)
    {
        followTarget = value;
    }
}
