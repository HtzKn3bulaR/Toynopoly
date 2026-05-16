using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.Collections;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;


public class LobbyHandler : MonoBehaviour
{
    public static LobbyHandler Instance;

    private Lobby hostLobby;
    private Lobby joinedLobby;
    
    private float heartbeatTimer;
    private float lobbyUpdateTimer;
    private float delayTimer;
    private string playerName;
    private string lobbyTitle ="Friendly Match";
    private int maxPlayers = 5;

    [SerializeField] private TextMeshProUGUI roomCodeText;

    public static event Action OnLobbyJoined;

    private string originalHostId;

    private bool gameHasStarted = false;
    private bool deviceHasJoinedRelay = false;

    private async void Start()
    {               

        Instance = this;               

        Debug.LogError("Console");

        await UnityServices.InitializeAsync();
        
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.ClearSessionToken(); // DELETE WHEN I AM DONE, THIS IS IMPORTANT. THIS IS PRETTY MUCH MAKING IT SO WE CREATE A NEW AUTHENTIFICATION FOR THE PLAYER EACH BUILD
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            string playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log(playerId);
        }

        /*
        AuthenticationService.Instance.SignedIn += () => { Debug.Log("Signed In : " + AuthenticationService.Instance.PlayerId); };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        
        */

    }

    private void Callbacks_LobbyChanged(ILobbyChanges obj)
    {

        if(deviceHasJoinedRelay)
        {
            OnlineManager.Instance.GetPlayerCountConnectedToRelayRpc();
            return;
        }

        if(joinedLobby == null)
        {
            OnlineManager.Instance.LoadMainScene();
        }

        Debug.Log("Lobby Change");
                
        StartCoroutine(WaitAfterPlayerJoined());
    }


    private void AdjustRoundsThresholdToPlayerNumber(int players)
    {
        switch (players)
        {
            case 2:

                if (MainManager.shortMatch)
                {
                    MainManager.raceThreshold = 9;
                }

                else

                    MainManager.raceThreshold = 13;
                break;

            case 3:


                if (MainManager.shortMatch)
                {
                    MainManager.raceThreshold = 7;
                }

                else

                    MainManager.raceThreshold = 13;
                break;

            case 4:

                if (MainManager.shortMatch)
                {
                    MainManager.raceThreshold = 9;
                }

                else
                    MainManager.raceThreshold = 13;
                break;

            case 5:

                if (MainManager.shortMatch)
                {
                    MainManager.raceThreshold = 6;
                }

                else
                    MainManager.raceThreshold = 11;
                break;

            default:
                
                break;               

        }
    }

    private IEnumerator WaitAfterPlayerJoined()
    {
        yield return new WaitForSeconds(5f);

        if (!deviceHasJoinedRelay)
        {
            Debug.Log(joinedLobby.Players.Count);
            PrintPlayers();
            AdjustRoundsThresholdToPlayerNumber(joinedLobby.Players.Count);
            MainManager.playerNumber = joinedLobby.Players.Count;

            if (!gameHasStarted)
            {
                LobbyUIHandler.Instance.UpdatePlayerNumber(joinedLobby.Players.Count);
            }


            if (joinedLobby.HostId == originalHostId)
            {
                if (joinedLobby.Players.Count > 1)
                {
                    if (!gameHasStarted)
                    {
                        LobbyUIHandler.Instance.ShowStartGameButton();
                    }
                }
            }
        }
               
    }

    private void InvitePlayersToJoinRelay()
    {
        if (joinedLobby.Data["RelayJoinCode"].Value != "0")
        //Game was started by Host
        {
            if (joinedLobby.HostId == originalHostId)
                return;

            ToynopolyRelay.Instance.JoinRelay(joinedLobby.Data["RelayJoinCode"].Value);

            deviceHasJoinedRelay = true;                                              

            PreGameFlowManager.Instance.CloseLobbyWindow();
        }
    }


    private bool CheckIfLobbyHost()
    {
        Debug.Log("Local Player ID " + AuthenticationService.Instance.PlayerId);
        Debug.Log("Is Host " + AuthenticationService.Instance.PlayerId == joinedLobby.HostId);
        return originalHostId == joinedLobby.HostId;
                
    }
     

    public void SetLobbyPlayerName(string lobbyPlayerName)
    {
        playerName = lobbyPlayerName;

    }

    private void Update()
    {
        HandleLobbyHeartbeat();
        HandleLobbyPollForUpdate();
    }

    private async void HandleLobbyHeartbeat()
    {        
            if (hostLobby != null && !deviceHasJoinedRelay)
            {
                heartbeatTimer -= Time.deltaTime;
                if (heartbeatTimer < 0f)
                {
                    float heartbeatTimerMax = 15f;
                    heartbeatTimer = heartbeatTimerMax;
                    await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);

                    PrintPlayers();

                }
            }        
    }

    private async void HandleLobbyPollForUpdate()
    {
        if (joinedLobby != null && !deviceHasJoinedRelay)
        {
            lobbyUpdateTimer -= Time.deltaTime;
            if (lobbyUpdateTimer < 0f)
            {
                float lobbyUpdateTimerMax = 1.1f;
                lobbyUpdateTimer = lobbyUpdateTimerMax;
                try
                {
                    Lobby lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                    joinedLobby = lobby;
                }
                catch (Exception ex) { Debug.Log(ex); }

                if (!deviceHasJoinedRelay)
                {
                    InvitePlayersToJoinRelay();
                }
            }
        }
    }

    public async void CreateNewLobby()
    {
        try
        {
            lobbyTitle = MainManager.matchTitle;
            
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = true,
                Player = GetPlayer(),                               

                Data = new Dictionary<string, DataObject>
                {
                    {"CarClass", new DataObject(DataObject.VisibilityOptions.Public, "Rookie") },
                    {"MatchLength", new DataObject(DataObject.VisibilityOptions.Public, "Short") },
                    {"RelayJoinCode", new DataObject(DataObject.VisibilityOptions.Member, "0") }

                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyTitle, maxPlayers, createLobbyOptions);

            hostLobby = lobby;
            joinedLobby = hostLobby;

            var callbacks = new LobbyEventCallbacks();
            callbacks.LobbyChanged += Callbacks_LobbyChanged;
            try
            {
                var m_LobbyEvents = await Lobbies.Instance.SubscribeToLobbyEventsAsync(joinedLobby.Id, callbacks);
            }
            catch (LobbyServiceException ex)
            {
                switch (ex.Reason)
                {
                    case LobbyExceptionReason.AlreadySubscribedToLobby: Debug.LogWarning($"Already subscribed to lobby[{lobby.Id}]. We did not need to try and subscribe again. Exception Message: {ex.Message}"); break;
                    case LobbyExceptionReason.SubscriptionToLobbyLostWhileBusy: Debug.LogError($"Subscription to lobby events was lost while it was busy trying to subscribe. Exception Message: {ex.Message}"); throw;
                    case LobbyExceptionReason.LobbyEventServiceConnectionError: Debug.LogError($"Failed to connect to lobby events. Exception Message: {ex.Message}"); throw;
                    default: throw;
                }
            }

            Debug.Log("Lobby created  " + lobbyTitle + " " + maxPlayers + " " + lobby.Id + " " + lobby.LobbyCode);
        }catch (LobbyServiceException e) { Debug.Log(e.ToString()); }

        MainManager.localMultiplayerName = GetPlayer().Data["PlayerName"].Value;

        MainManager.roomCode = hostLobby.LobbyCode;
        Debug.Log("Room Code sent to Main Manager " + MainManager.roomCode);

        originalHostId = hostLobby.HostId;
        Debug.Log("Host ID " +  originalHostId);

        PrintPlayers();              

        SetLobbyParameters();

    }

    

    private void SetLobbyParameters()
    {
        Debug.Log("Match Length Short is set to " + MainManager.shortMatch);

        LobbyUIHandler.Instance.SetLobbyParameters(MainManager.playerNumber, lobbyTitle);

        switch(MainManager.shortMatch)
        {
            case true:
                UpdateLobbyLength("Short");
                LobbyUIHandler.Instance.UpdateLobbyDuration("Short");
                break;
            case false:
                UpdateLobbyLength("Regular");
                LobbyUIHandler.Instance.UpdateLobbyDuration("Regular");
                break;
        }

        switch(MainManager.classSelected)
        {
            case 0:
                UpdateLobbyCarClass("Rookie");
                LobbyUIHandler.Instance.UpdateLobbyCarClass("Rookie");
                break;
            case 1:
                UpdateLobbyCarClass("Amateur");
                LobbyUIHandler.Instance.UpdateLobbyCarClass("Amateur");
                break;
            case 2:
                UpdateLobbyCarClass("Advanced");
                LobbyUIHandler.Instance.UpdateLobbyCarClass("Advanced");
                break;
            case 3:
                UpdateLobbyCarClass("Semi-Pro");
                LobbyUIHandler.Instance.UpdateLobbyCarClass("Semi-Pro");
                break;
            case 4:
                UpdateLobbyCarClass("Pro");
                LobbyUIHandler.Instance.UpdateLobbyCarClass("Pro");
                break;
            case 5:
                UpdateLobbyCarClass("Super-Pro");
                LobbyUIHandler.Instance.UpdateLobbyCarClass("Super-Pro");
                break;
        }
    }

    public async void ListLobbies()
    {
        try
        {
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found  " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e) { Debug.Log(e.ToString()); }

    }

    public async void JoinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions()
            {
                Player = GetPlayer()
            };

            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions);

            joinedLobby = lobby;

            Debug.Log("Joined lobby by Code " + lobbyCode);

            OnLobbyJoined?.Invoke();

            MainManager.localMultiplayerName = GetPlayer().Data["PlayerName"].Value;

            PrintPlayers();

            SetCarClassOnClients();

            SetMatchLengthOnClients();

            var callbacks = new LobbyEventCallbacks();
            callbacks.LobbyChanged += Callbacks_LobbyChanged;
            try
            {
                var m_LobbyEvents = await Lobbies.Instance.SubscribeToLobbyEventsAsync(joinedLobby.Id, callbacks);
            }
            catch (LobbyServiceException ex)
            {
                switch (ex.Reason)
                {
                    case LobbyExceptionReason.AlreadySubscribedToLobby: Debug.LogWarning($"Already subscribed to lobby[{lobby.Id}]. We did not need to try and subscribe again. Exception Message: {ex.Message}"); break;
                    case LobbyExceptionReason.SubscriptionToLobbyLostWhileBusy: Debug.LogError($"Subscription to lobby events was lost while it was busy trying to subscribe. Exception Message: {ex.Message}"); throw;
                    case LobbyExceptionReason.LobbyEventServiceConnectionError: Debug.LogError($"Failed to connect to lobby events. Exception Message: {ex.Message}"); throw;
                    default: throw;
                }
            }

        }
        catch (LobbyServiceException e) { Debug.Log(e.ToString()); }
    }

    private void SetCarClassOnClients()
    {
        switch (LobbyHandler.Instance.ReturnJoinedLobby().Data["CarClass"].Value)

        {
            case "Rookie":
                MainManager.classSelected = 0;
                break;

            case "Amateur":
                MainManager.classSelected = 1;
                break;

            case "Advanced":
                MainManager.classSelected = 2;
                break;

            case "Semi-Pro":
                MainManager.classSelected = 3;
                break;

            case "Pro":
                MainManager.classSelected = 4;
                break;

            case "Super-Pro":
                MainManager.classSelected = 5;
                break;
        }
    }

    private void SetMatchLengthOnClients()
    {
        switch (joinedLobby.Data["MatchLength"].Value)
        {
            case "Short":
                MainManager.shortMatch = true;
                break;

            case "Regular":
                MainManager.shortMatch = false;
                break;
        }

        AdjustRoundsThresholdToPlayerNumber(joinedLobby.Players.Count);
    }

    public Lobby ReturnJoinedLobby()
    {
        return joinedLobby;
    }

    private void PrintPlayers()
    {
        if (!gameHasStarted)
        {
            PrintPlayers(joinedLobby);
        }
    }

    private void PrintPlayers(Lobby lobby)
    {
        Debug.Log("Players in Lobby " + lobby.Name + " " + lobby.Data["CarClass"].Value + " " + lobby.Data["MatchLength"].Value);
        
        LobbyUIHandler.Instance.ResetPlayerBoxes();  

        for (int i = 0; i <  lobby.Players.Count; i++)
        {
            Debug.Log(lobby.Players[i].Id + " " + lobby.Players[i].Data["PlayerName"].Value);


            LobbyUIHandler.Instance.SetPlayerBox(i, lobby.Players[i].Data["PlayerName"].Value);

        }

    }

    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
                    {
                        { "PlayerName" , new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) }
                    }
        };
    }

    private async void UpdateLobbyCarClass(string carClass)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject> {
        { "CarClass", new DataObject(DataObject.VisibilityOptions.Public, carClass)  }
            }
            });

            joinedLobby = hostLobby;

            PrintPlayers(hostLobby);
        } catch (LobbyServiceException e) { Debug.Log(e.ToString()); }
    }

    private async void UpdateLobbyLength(string matchLength)
    {
        try
        {
            hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject> {
        { "MatchLength", new DataObject(DataObject.VisibilityOptions.Public, matchLength)  }
            }
            });

            joinedLobby = hostLobby;

            PrintPlayers(hostLobby);
        }
        catch (LobbyServiceException e) { Debug.Log(e.ToString()); }
    }


    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
            joinedLobby = null;
        }
        catch (LobbyServiceException e) { Debug.Log(e.ToString()); }
    }

    public async void StartGame()
    {
        if(CheckIfLobbyHost())
        {
            try
            {
                Debug.Log("Starting Game");

                string relayCode = await ToynopolyRelay.Instance.CreateRelay();

                hostLobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>
                    {
                    { "RelayJoinCode", new DataObject(DataObject.VisibilityOptions.Member, relayCode)  }
                    }
                });

            }
            catch
                (LobbyServiceException e)
            { Debug.Log(e); 
                }
                        
        }

        gameHasStarted = true;
        deviceHasJoinedRelay = true;

        //PreGameFlowManager.Instance.CloseLobbyWindow();
        Debug.Log("Game Started");

        //PreGameFlowManager.Instance.ContinueToMain();
    }

            

    public async void DeleteLobby()
    {                
        try
        {
            await LobbyService.Instance.DeleteLobbyAsync(joinedLobby.Id);
            Debug.Log("Lobby Deleted");
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

        joinedLobby = null;
        hostLobby = null;
    }
        

}
