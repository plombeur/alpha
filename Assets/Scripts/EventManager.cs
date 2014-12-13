using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    private List<EventManagerListener> listeners = new List<EventManagerListener>();

    void Start()
    {

    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                for (int i=0;i<listeners.Count;++i)
                {
                    if (listeners[i].onMouseButtonDown(0))
                        return;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                for (int i = 0; i < listeners.Count; ++i)
                {
                    if (listeners[i].onMouseButtonDown(1))
                        return;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < listeners.Count; ++i)
                {
                    if (listeners[i].onMouseButtonUp(0))
                        return;
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                for (int i = 0; i < listeners.Count; ++i)
                {
                    if (listeners[i].onMouseButtonUp(1))
                        return;
                }
            }
        }
    }

    public void addEventManagerListener(EventManagerListener listener)
    {
        listeners.Add(listener);
    }
}
public interface EventManagerListener
{
    bool onMouseButtonDown(int button);
    bool onMouseButtonUp(int button);
}