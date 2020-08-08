using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class FightManager : MonoBehaviour
{
    public int MinimumPlayersForGame = 1;

    public Player LocalPlayer;
    public List<Tank> players = new List<Tank>();

    void Update()
    {
        if (NetworkManager.singleton.isNetworkActive)
        {

            if (LocalPlayer == null)
            {
                FindLocalTank();
                print("Fund - " + LocalPlayer);
            }
        }
        else
        {
            //Cleanup state once network goes offline
            LocalPlayer = null;
            players.Clear();
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
