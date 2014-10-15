using UnityEngine;
using System.Collections;

public class MonoBehaviorAdapter : MonoBehaviour
{
    protected virtual void OnApplicationFocus(bool focusStatus)
    {
    }

    protected virtual void OnApplicationPause(bool pauseStatus)
    {
    }

    protected virtual void OnApplicationQuit()
    {
    }

    protected virtual void OnLevelWasLoaded(int level)
    {
    }

    protected virtual void OnNetworkInstantiate(NetworkMessageInfo info)
    {
    }

    protected virtual void OnDisconnectedFromServer(NetworkDisconnection info)
    {
    }

    protected virtual void OnMasterServerEvent(MasterServerEvent msEvent)
    {
    }

    protected virtual void OnFailedToConnect(NetworkConnectionError error)
    {
    }

    protected virtual void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
    }

    protected virtual void OnServerInitialized()
    {
    }

    protected virtual void OnPlayerConnected(NetworkPlayer player)
    {
    }

    protected virtual void OnPlayerDisconnected(NetworkPlayer player)
    {
    }

    protected virtual void OnPostRender()
    {
    }

    protected virtual void OnPreCull()
    {
    }

    protected virtual void OnPreRender()
    {
    }

    protected virtual void OnRenderObject()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Awake()
    {
    }

    protected virtual void LateUpdate()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnGUI()
    {
    }

    protected virtual void OnDrawGizmos()
    {
    }

    protected virtual void OnMouseDown()
    {
    }

    protected virtual void OnMouseDrag()
    {
    }

    protected virtual void OnMouseUp()
    {
    }
}