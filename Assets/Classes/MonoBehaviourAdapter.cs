using UnityEngine;
using System.Collections;

public class MonoBehaviourAdapter : MonoBehaviour
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

    protected virtual void OnCollisionEnter(Collision collision)
    {

    }
    protected virtual void OnCollisionStay(Collision collision)
    {

    }
    protected virtual void OnCollisionExit(Collision collision)
    {

    }
    protected virtual void OnTriggerEnter(Collider collider)
    {

    }
    protected virtual void OnTriggerStay(Collider collider)
    {

    }
    protected virtual void OnTriggerExit(Collider collider)
    {

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {

    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {

    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {

    }
    protected virtual void OnTriggerStay2D(Collider2D collider)
    {

    }
    protected virtual void OnTriggerExit2D(Collider2D collider)
    {

    }
}