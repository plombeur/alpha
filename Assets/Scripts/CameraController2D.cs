using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController2D : MonoBehaviour, EventManagerListener
{
    public bool followTarget = true;
    private Transform target;
    private bool mouseDown = false;
    private Vector3 lastMousePositonOnWorld;
    private Plane groundPlane = new Plane(-Vector3.forward, Vector3.zero);

    void Start()
    {
        target = GameManager.getInstance().alphaWolf.transform;
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
                transform.Translate(-delta);

            Vector3 position = transform.position;
            Rect dimensions = GameManager.getInstance().getDimensions();

            float cameraWidthHalf = camera.orthographicSize * camera.aspect;

            position.x = Mathf.Clamp(position.x, dimensions.xMin + cameraWidthHalf, dimensions.xMin + dimensions.width - cameraWidthHalf);
            position.y = Mathf.Clamp(position.y, dimensions.yMin + camera.orthographicSize, dimensions.yMin + dimensions.height - camera.orthographicSize);

            transform.position = position;

            mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            groundPlane.Raycast(mouseRay, out distanceCast);
            worldPosition = mouseRay.GetPoint(distanceCast);
            lastMousePositonOnWorld = worldPosition;
        }
        if (!Input.GetMouseButton(0))
            mouseDown = false;
    }
    void LateUpdate()
    {
        if (followTarget && target != null)
        {
            Vector3 position = target.position;
            position.z = -5;
            transform.position = position;
        }
    }

    public void setFollowAlpha()
    {
        followTarget = true;
        target = GameManager.getInstance().alphaWolf.transform;
    }

    public void setFollowTarget(Transform transformToFollow)
    {
        followTarget = true;
        target = transformToFollow;
    }

    public bool onMouseButtonDown(int button)
    {
        if (button == 0)
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
        }
        return false;
    }
    public bool onMouseButtonUp(int button)
    {
        return false;
    }
}
