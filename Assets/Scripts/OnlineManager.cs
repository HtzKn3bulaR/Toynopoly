using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Lobbies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineManager : NetworkBehaviour
{
    private Dictionary<string, ulong> playerID = new Dictionary<string, ulong>();

    public static OnlineManager Instance;

    private NetworkList<FixedString32Bytes> trackNetworkList;

    private NetworkList<FixedString32Bytes> carNetworkList;

    public NetworkVariable<FixedString32Bytes> networkBonusTrack;

    private NetworkList<FixedString32Bytes> playerNetworkList;

    public NetworkVariable<int> pendingFieldNetwork;

    public NetworkVariable<FixedString32Bytes> selectedTrackNetwork = new NetworkVariable<FixedString32Bytes>();

    public NetworkVariable<bool> level1RaceIsInProgress = new NetworkVariable<bool>(false);

    //RANKING NETWORK LISTS

    public NetworkList<FixedString32Bytes> playerNamesRankingNetworkList;

    public NetworkList<FixedString32Bytes> carNamesRankingNetworkList;

    public NetworkList<FixedString32Bytes> timesRankingNetworkList;

    public NetworkList<int> gapsRankingNetworkList;

    public NetworkVariable<FixedString32Bytes> trackInfoNetworkVariable;


    private bool trackListReady = false;
    private bool carListReady = false;
    private bool playerListReady = false;

    public NetworkVariable<bool> networkListsReady = new NetworkVariable<bool>(false);


    public void Awake()
    {

    }

    private void Start()
    {
        trackNetworkList = new NetworkList<FixedString32Bytes>();
        carNetworkList = new NetworkList<FixedString32Bytes>();
        networkBonusTrack = new NetworkVariable<FixedString32Bytes>();
        playerNetworkList = new NetworkList<FixedString32Bytes>();
        pendingFieldNetwork = new NetworkVariable<int>(99);

        playerNamesRankingNetworkList = new NetworkList<FixedString32Bytes>();
        carNamesRankingNetworkList = new NetworkList<FixedString32Bytes>();
        timesRankingNetworkList = new NetworkList<FixedString32Bytes>();
        gapsRankingNetworkList = new NetworkList<int>();


    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        Debug.Log("Client Connected! - Reporting Credentials");

        ReportPlayerDataToNetworkRpc(MainManager.localMultiplayerName, NetworkManager.Singleton.LocalClientId);

        if (NetworkManager.Singleton.LocalClientId == 0)
        {
            foreach (var item in playerID)
            {
                ReportPlayerDataToNetworkRpc(item.Key, item.Value);
            }
        }
        

        if (NetworkManager.Singleton.LocalClientId == 0)
        {
            if (CheckAllClientsConnected())
            {
                Debug.Log("All Clients Connected!");
                GetPlayerCountConnectedToRelayRpc();
                Debug.Log("Players Registered in Main Manager " + MainManager.playerNumber);
                LoadMainScene();
            }
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(MainManager.playerNumber - 1);
    }

    private bool CheckAllClientsConnected()
    {
        Debug.Log("Number of Clients Connected " + NetworkManager.Singleton.ConnectedClientsList.Count);
        Debug.Log("Number of Players Signed Up For Match " + MainManager.playerNumber);

        return NetworkManager.Singleton.ConnectedClientsList.Count == MainManager.playerNumber;
    }

    public override void OnNetworkSpawn()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;

        ReportPlayerDataToNetworkRpc(MainManager.localMultiplayerName, NetworkManager.Singleton.LocalClientId);

        Debug.Log("Network Spawn Event fired");

        LobbyUIHandler.Instance.ShowLoadingPanel();

        networkListsReady.OnValueChanged += OnNetworkListsReady;

    }

    private void OnNetworkListsReady(bool previousValue, bool newValue)
    {
        if (NetworkManager.Singleton.LocalClientId != 0)
        {
            if (newValue == true)
            {
                Debug.Log("All Network Lists Ready - Loading Main Scene");

                Debug.Log("Player Number " + MainManager.playerNumber);

                LoadMainScene();
            }
        }
    }

    [Rpc(SendTo.Everyone)]
    public void ReportPlayerDataToNetworkRpc(string localName, ulong localID)
    {
        if (!playerID.ContainsKey(localName))
        {
            playerID.Add(localName, localID);

            Debug.Log("Player Data Added To Network Dictionary " + localName + " " + localID);
        }
    }

    public void SendDataToTrackNetworkList(List<FixedString32Bytes> tracksCurrentMatch, FixedString32Bytes bonusTrack)
    {
        trackNetworkList.Clear();
        for (int i = 0; i < tracksCurrentMatch.Count; i++)
        {
            trackNetworkList.Add(tracksCurrentMatch[i]);
            Debug.Log("Track Added to Network List " + trackNetworkList[i].Value);
            if (trackNetworkList.Count == 9)
            {
                trackListReady = true;
                Debug.Log("Track List Ready");
            }
        }

        networkBonusTrack.Value = bonusTrack;

        CheckReadiness();
    }

    public void SendDataToCarNetworkList(List<FixedString32Bytes> carsCurrentMatch)
    {
        carNetworkList.Clear();
        for (int i = 0; i < carsCurrentMatch.Count; i++)
        {
            carNetworkList.Add(carsCurrentMatch[i]);
            Debug.Log("Car Added to Network List " + carNetworkList[i].Value);

            if (carNetworkList.Count == 6)
            {
                carListReady = true;
                Debug.Log("Car List Ready");
            }
        }

        CheckReadiness();
    }

    public void SendDataToPlayerNetworkList(List<FixedString32Bytes> playersCurrentMatch)
    {
        playerNetworkList.Clear();
        for (int i = 0; i < playersCurrentMatch.Count; i++)
        {
            playerNetworkList.Add(playersCurrentMatch[i]);
            Debug.Log("Player Added to Network List " + playerNetworkList[i].Value);

            if (playerNetworkList.Count == MainManager.playerNumber)
            {
                playerListReady = true;
                Debug.Log("Player List Ready");
            }
        }

        CheckReadiness();
    }

    private void CheckReadiness()
    {
        if (playerListReady && carListReady && trackListReady)
        {
            networkListsReady.Value = true;
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

    //PLAYER ID MANAGEMENT


    internal void ReadPlayerIDs()
    {
        if (NetworkManager.LocalClientId == 0)
        {
            foreach (FixedString32Bytes playerName in playerNetworkList)
            {
                Debug.Log("Player " + playerName.Value + "has ID " + playerID[playerName.Value]);
            }
        }
    }



    internal ulong GetLocalClientID()
    {
        return NetworkManager.Singleton.LocalClientId;
    }

    internal ulong GetPlayerID(string playerName)
    {
        return playerID[playerName];
    }

    internal List<FixedString32Bytes> ReturnPlayerNamesList()
    {
        List<FixedString32Bytes> playerNamesList = new List<FixedString32Bytes>();

        foreach (var item in playerID)
        {
            playerNamesList.Add(item.Key);
        }

        return playerNamesList;
    }

    [Rpc(SendTo.Server)]
    public void GetPlayerCountConnectedToRelayRpc()
    {
        AckRelayConnectedPlayersCountToClientsRpc(NetworkManager.Singleton.ConnectedClients.Count);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void AckRelayConnectedPlayersCountToClientsRpc(int count)
    {
        MainManager.playerNumber = count;
    }

    //GAME RUNNING EVENTS


    [Rpc(SendTo.Server)]
    internal void ReportPendingFieldRpc(int field)
    {
        pendingFieldNetwork.Value = field;
    }

    [Rpc(SendTo.Server)]
    internal void ReportRaceLevel1InProgressRpc()
    {
        level1RaceIsInProgress.Value = true;
    }


    //RESULTS

    [Rpc(SendTo.Server)]
    public void SendResultListToServerRpc()
    {
        playerNamesRankingNetworkList.Clear();

        foreach (string name in CSVFileReader.Instance.GetPlayerRankingResultsList())
        {
            playerNamesRankingNetworkList.Add(name.ToSafeString());
        }

        foreach (FixedString32Bytes name in playerNamesRankingNetworkList)
        { Debug.Log("Name added to network List : " + name.Value); }

        carNamesRankingNetworkList.Clear();

        foreach (string car in CSVFileReader.Instance.GetCarRankingResultsList())
        {
            carNamesRankingNetworkList.Add(car.ToSafeString());
        }

        timesRankingNetworkList.Clear();

        foreach (string time in CSVFileReader.Instance.GetTimeRankingResultsList())
        {
            timesRankingNetworkList.Add(time.ToSafeString());
        }

        gapsRankingNetworkList.Clear();

        foreach (int gap in CSVFileReader.Instance.GetGapsList())
        {
            gapsRankingNetworkList.Add(gap);
        }

        StartCoroutine(WaitAfterResultsSentToNetwork());

        trackInfoNetworkVariable.Value = CSVFileReader.Instance.GetTrackInfo();

    }

    private IEnumerator WaitAfterResultsSentToNetwork()
    {
        yield return new WaitForSeconds(2f);

        trackInfoNetworkVariable.Value = CSVFileReader.Instance.GetTrackInfo();

    }
}
