using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HUDLobby : MonoBehaviour
{
    NetworkManager manager;

    public int MinimumPlayersForGame = 1;
    public Player LocalPlayer;
    public bool IsGameReady;
    public bool IsGameOver;
    public List<Player> players = new List<Player>();

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    void Update()
    {
        if (NetworkManager.singleton.isNetworkActive)
        {
            GameReadyCheck();

            if (LocalPlayer == null)
            { 
                FindLocalTank();
            }
        }
        else
        {
            //Cleanup state once network goes offline
            IsGameReady = false;
            LocalPlayer = null;
            players.Clear();
        }
    }

    public void StartServerErebor()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            manager.StartHost();
        }
        
    }

    public void JoinServerErebor()
    {
        manager.StartClient();
    }

    public void StartServerCity()
    {
        return;
    }

    public void StartServerCrater()
    {
        return;
    }

    void GameReadyCheck()
    {
        if (!IsGameReady)
        {
            //Look for connections that are not in the player list
            foreach (KeyValuePair<uint, NetworkIdentity> kvp in NetworkIdentity.spawned)
            {
                Player comp = kvp.Value.GetComponent<Player>();

                //Add if new
                if (comp != null && !players.Contains(comp))
                {
                    players.Add(comp);
                }
            }

            //If minimum connections has been check if they are all ready
            if (players.Count >= MinimumPlayersForGame)
            {
                bool AllReady = true;
                foreach (Player tank in players)
                {
                    if (!tank.isReady)
                    {
                        AllReady = false;
                    }
                }
                if (AllReady)
                {
                    IsGameReady = true;
                }
            }
        }
    }

    void FindLocalTank()
    {
        //Check to see if the player is loaded in yet
        if (ClientScene.localPlayer == null)
            return;

        LocalPlayer = ClientScene.localPlayer.GetComponent<Player>();
    }
}
