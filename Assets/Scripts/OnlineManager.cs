using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineManager : NetworkBehaviour
{
    public static OnlineManager Instance;

    private NetworkList<FixedString32Bytes> trackNetworkList;

    private NetworkList<FixedString32Bytes> carNetworkList;

    public NetworkVariable<FixedString32Bytes> networkBonusTrack;

    private NetworkList<FixedString32Bytes> playerNetworkList;

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

        trackNetworkList = new NetworkList<FixedString32Bytes>();
        carNetworkList = new NetworkList<FixedString32Bytes>();
        networkBonusTrack = new NetworkVariable<FixedString32Bytes>();
        playerNetworkList = new NetworkList<FixedString32Bytes>();
    }

    private void Start()
    {
        localPlayerStatus = PlayerStatus.None;
    }

    public override void OnNetworkSpawn()
    {
        SceneManager.LoadScene(LobbyHandler.Instance.ReturnJoinedLobby().Players.Count - 1);

        Debug.Log("Network Spawn Event fired");


    }

    public void SendDataToTrackNetworkList(List<FixedString32Bytes> tracksCurrentMatch, FixedString32Bytes bonusTrack)
    {
        trackNetworkList.Clear();
        for (int i = 0; i < tracksCurrentMatch.Count; i++)
        {
            trackNetworkList.Add(tracksCurrentMatch[i]);
            Debug.Log("Track Added to Network List " + trackNetworkList[i].Value);
        }

        networkBonusTrack.Value = bonusTrack;
    }

    public void SendDataToCarNetworkList(List<FixedString32Bytes> carsCurrentMatch)
    {
        carNetworkList.Clear();
        for (int i = 0; i < carsCurrentMatch.Count; i++)
        {
            carNetworkList.Add(carsCurrentMatch[i]);
            Debug.Log("Track Added to Network List " + carNetworkList[i].Value);
        }
    }

    public void SendDataToPlayerNetworkList(List<FixedString32Bytes> playersCurrentMatch)
    {
        playerNetworkList.Clear();
        for (int i = 0; i < playersCurrentMatch.Count; i++)
        {
            playerNetworkList.Add(playersCurrentMatch[i]);
            Debug.Log("Track Added to Network List " + playerNetworkList[i].Value);
        }
    }

    public NetworkList<FixedString32Bytes> ReturnTrackNetworkList()
    {
        return trackNetworkList;
    }

    public NetworkList<FixedString32Bytes> ReturnCarNetworkList()
    {
        return carNetworkList;
    }

    public NetworkList<FixedString32Bytes> ReturnPlayerNetworkList()
    {
        return playerNetworkList;
    }

    public NetworkVariable<FixedString32Bytes> ReturnNetworkBonusTrack()
    {
        return networkBonusTrack;
    }


}
