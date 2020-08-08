using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HUDLobby : MonoBehaviour
{
    NetworkManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    public void StartServerErebor()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
                manager.StartHost();
        }
    }

    public void StartServerCity()
    {
        return;
    }

    public void StartServerCrater()
    {
        return;
    }
}
