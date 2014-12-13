using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController2D : MonoBehaviour, EventManagerListener
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
        if (mouseDown)
        {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            float distanceCast;
            groundPlane.Raycast(mouseRay, out distanceCast);
            Vector3 worldPosition = mouseRay.GetPoint(distanceCast);

            followTarget = false;
            Vector3 delta = worldPosition - lastMousePositonOnWorld;
            if (delta.magnitude != 0)
                camera.transform.Translate(-delta);

            mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            groundPlane.Raycast(mouseRay, out distanceCast);
            worldPosition = mouseRay.GetPoint(distanceCast);
            lastMousePositonOnWorld = worldPosition;
        }
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

    public bool onMouseButtonDown(int button)
    {
        Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        float distanceCast;
        groundPlane.Raycast(mouseRay, out distanceCast);
        Vector3 worldPosition = mouseRay.GetPoint(distanceCast);
        mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        groundPlane.Raycast(mouseRay, out distanceCast);
        worldPosition = mouseRay.GetPoint(distanceCast);
        lastMousePositonOnWorld = worldPosition;
        mouseDown = true;
        return false;
    }
    public bool onMouseButtonUp(int button)
    {
        mouseDown = false;
        return false;
    }
}
