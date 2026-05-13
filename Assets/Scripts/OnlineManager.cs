using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnlineManager : NetworkBehaviour
{
    public static OnlineManager Instance;

    public enum PlayerStatus
    {
        None,
        Active,
        Inactive
    }

    public enum PlayerNumber
    {
        Player1,
        Player2,
        Player3,
        Player4,
        Player5,
    }

    public PlayerNumber localPlayerNumber;

    public PlayerStatus localPlayerStatus;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnNetworkSpawn()
    {
        Debug.Log("Local ID is " + NetworkManager.Singleton.LocalClientId);
        if (NetworkManager.Singleton.LocalClientId == 0)
        {           

            localPlayerStatus = PlayerStatus.None;                   

            NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;

            GridGenerator3P.Instance.TrackSelect();
            
        }
        else
        {
            localPlayerStatus = PlayerStatus.None;
                       

        }

        GridGenerator3P.Instance.CarSelect();

    }

    private void NetworkManager_OnClientConnectedCallback(ulong obj)
    {
        throw new NotImplementedException();
    }
}
