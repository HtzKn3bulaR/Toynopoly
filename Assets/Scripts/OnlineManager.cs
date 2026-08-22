using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Xml;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
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
    public NetworkVariable<bool> level2RaceIsInProgress = new NetworkVariable<bool>(false);

    //RANKING NETWORK LISTS

    public NetworkList<FixedString32Bytes> playerNamesRankingNetworkList;

    public NetworkList<FixedString32Bytes> carNamesRankingNetworkList;

    public NetworkList<FixedString32Bytes> timesRankingNetworkList;

    public NetworkList<int> gapsRankingNetworkList;

    public NetworkVariable<FixedString32Bytes> trackInfoNetworkVariable;

    //OTHER NETWORK VARIABLES


    public NetworkList<int> actualDividendsNetworkList;


    public NetworkVariable<int> randomMarketDelta;

    public NetworkVariable<int> defenderPlayerIDNetworkVariable = new NetworkVariable<int>(9);

    //PLAYER STATES NETWORK VARIABLES

    public NetworkVariable<int> numberOfPlayersReportedReadyForNextRound = new NetworkVariable<int>(0);
    public NetworkVariable<int> numberOfPlayersReadyForSellingRound = new NetworkVariable<int>(0);
    public NetworkVariable<int> numberOfPlayersReportedReadyForNextLevel = new NetworkVariable<int>(0);

    public NetworkVariable<bool> skippingRoundNetworkVariable = new NetworkVariable<bool>(false);


    private bool trackListReady = false;
    private bool carListReady = false;
    private bool playerListReady = false;

    private int winnerLevel1;
    private int secondPlaceLevel1;
    private bool activePlayerWin;

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
        randomMarketDelta = new NetworkVariable<int>();

        actualDividendsNetworkList = new NetworkList<int>();

        PlayerManager3P.OnActivePlayerHasBoughtCar += PlayerManager3P_OnActivePlayerHasBoughtCar;
        PlayerManager3P.OnInactivePlayersHaveBuyOption += PlayerManager3P_OnInactivePlayersHaveBuyOptionRpc;

        PlayerManager3P.OnActivePlayerCanProtectAfterChallenge += PlayerManager3P_OnActivePlayerCanProtectAfterChallengeRpc;
       
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

                PassInformationToWebSocketClientsRpc(MainManager.raceThreshold);
                                
                LoadMainScene();
                               
            }
        }
    }


    [Rpc(SendTo.ClientsAndHost)]
    public void PassInformationToWebSocketClientsRpc(int threshold)
    {
        if(NetworkManager.Singleton.GetComponent<UnityTransport>().UseWebSockets == true)
        {
            MainManager.raceThreshold = threshold;
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
    internal void ReportRaceLevel1InProgressRpc(bool level1RaceStatus)
    {
        if (level1RaceStatus == true)
            randomMarketDelta.Value = UnityEngine.Random.Range(0, 15);

        level1RaceIsInProgress.Value = level1RaceStatus;
    }

    [Rpc(SendTo.Server)]
    internal void ReportRaceLevel2InProgressRpc(bool level2RaceStatus)
    {
        level2RaceIsInProgress.Value = level2RaceStatus;
        Debug.Log("Race In Progress Network Variable Set To " + level2RaceIsInProgress.Value);
    }

    public int GetRandomMarketDelta()
    { return randomMarketDelta.Value; }


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


    //Results Manual Reporting

    [Rpc(SendTo.Server)]
    public void Level1ReportManualResultsToServerRpc(int raceWinnerLevel1, int runnerUpLevel1, bool activePlayerWon)
    {
       winnerLevel1 = raceWinnerLevel1;
       secondPlaceLevel1 = runnerUpLevel1;
       activePlayerWin = activePlayerWon;

       AckManualResultsLevel1ToClientRpc(winnerLevel1, secondPlaceLevel1, activePlayerWin);       
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void Level2ReportManualResultsToServerRpc(int v1, int v2, bool activePlayerWins, int timeBattleSeconds)
    {
        if(!GameManager.Instance.LocalIsActivePlayer())
        {
            MainManager.activePlayerWins = activePlayerWins;
            MainManager.timeBattleSeconds = timeBattleSeconds;
            GameManager.Instance.Level2Scoring();
        }
    }





    [Rpc(SendTo.ClientsAndHost)]
    private void AckManualResultsLevel1ToClientRpc(int winnerLevel1, int secondPlaceLevel1, bool activePlayerWon)
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            if (MainManager.playerNumber > 2)
            {
                PlayerManager3P.Instance.raceWinnerLevel1 = winnerLevel1;
                PlayerManager3P.Instance.runnerUpLevel1 = secondPlaceLevel1;
            }

            MainManager.activePlayerWins = activePlayerWon;

            MainManager.autoResultsValid = true;

            //PlayerManager3P.Instance.RegisterResults();
        }
    }

    //DIVIDENDS

    public void AddValueToDividendsNetworkList(int dividendCarIndexNumber)
    {
        actualDividendsNetworkList.Add(dividendCarIndexNumber);
    }

    public int GetDividendCarIndexNumberFromNetworkList(int roundIndex)
    {
        return actualDividendsNetworkList[roundIndex];
    }

    //CHALLENGES

    [Rpc(SendTo.Server)]
    internal void ReportDefendingPlayerToNetworkRpc(int defender)
    {
        defenderPlayerIDNetworkVariable.Value = defender;

        AckChallengeDefenderToInactivesRpc(defender);
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void AckChallengeDefenderToInactivesRpc(int defendingPlayer)
    {
        if (defendingPlayer == 9)
            return;

        if (!PlayerManager3P.Instance.LocalIsActivePlayer())
        {
            MainManager.defendingPlayer = defendingPlayer;
            PlayerManager3P.Instance.SetDefenderAndContinue(defendingPlayer);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void PlayerManager3P_OnActivePlayerCanProtectAfterChallengeRpc()
    {
        if(PlayerManager3P.Instance.LocalIsActivePlayer())
        {
            PlayerManager3P.Instance.ShowProtectionOptionAfterChallengePanel();
            IdleCountdown.Instance.StartIdleCountdownMax(60f);
            PlayerManager3P.Instance.SetPromptText("You can enable protection for one of your cars");
        }

        else
        {
            IdleCountdown.Instance.StartIdleCountdownMax(60f);
            PlayerManager3P.Instance.SetPromptText("Waiting for " + MainManager.playerNames[MainManager.activePlayer]);
        }
    }

    //Challenge Manual Reporting

    [Rpc(SendTo.ClientsAndHost)]
    internal void ManualReportChallengeWinToServerRpc(bool win)
    {
        if(!NetworkManager.Singleton.IsHost)
        PlayerManager3P.Instance.GetChallengeResultWin(win);
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ManualReportChallengeLossToServerRpc(bool loss)
    {
        if (!NetworkManager.Singleton.IsHost)
            PlayerManager3P.Instance.GetChallengeResultLoss(loss);
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ManualReportStolenWinToServerRpc(bool stolenWin)
    {
        if (!NetworkManager.Singleton.IsHost)
            PlayerManager3P.Instance.SetStolenWinBoolAfterManualReport(stolenWin);
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportManualChallengeGapsRpc(float value1, float value2, float value3)
    {
        Debug.Log("Reported Gaps " + value1 + ", " + value2 + " , " + value3);
                     
        MainManager.manualReportingGapToDefender = System.Convert.ToInt32(value1);
        MainManager.manualReportingGapToChallenger = System.Convert.ToInt32(value2);
        MainManager.manualReportingGapStolenWin = System.Convert.ToInt32(value3);   
        
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ManualReportStealToClientsRpc(int stealer)
    {
        if (!NetworkManager.Singleton.IsHost)
            PlayerManager3P.Instance.SetStealerManualReporting(stealer);
    }

    //TIME BATTLE

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportTimeBattleCarIndexToNetworkRpc(int whichCar)
    {
       MainManager.TimeBattleCarIndex = whichCar;
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportCarBuffToClientsRpc()
    {
        if (!PlayerManager3P.Instance.IsTimeBattleWinner())
        {
            PlayerManager3P.Instance.BuffCarAndContinue();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void DuelReportCarBuffToClientsRpc(int car)
    {
        if (!GameManager.Instance.IsTimeBattleWinner())
        {
            MainManager.TimeBattleCarIndex = car;
            GameManager.Instance.BuffCarAndContinue();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportCarNerfToClientsRpc()
    {
        if (!PlayerManager3P.Instance.IsTimeBattleWinner())
        {
            
            PlayerManager3P.Instance.NerfCarAndContinue();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void DuelReportCarNerfToClientsRpc(int car)
    {
        if (!GameManager.Instance.IsTimeBattleWinner())
        {
            MainManager.TimeBattleCarIndex = car;
            GameManager.Instance.NerfCarAndContinue();
        }
    }


    //AUTO RESULT VALIDATION------------------------------------

    [Rpc(SendTo.ClientsAndHost)]
    internal void ClientsSetAutoResultsValidRpc()
    {
        if(!NetworkManager.Singleton.IsHost)
            MainManager.autoResultsValid = true;
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ClientsSetAutoResultsInvalidRpc()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            MainManager.autoResultsValid = false;

            if (MainManager.playerNumber > 2)
            {
                PlayerManager3P.Instance.GetChallengeResultWin(true);
            }
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ClientsSetToynopolyAutoResultsInvalidRpc()
    {
        MainManager.autoResultsValid = false;
    }

    //---------------------------------------------------------------


    //TOYNOPOLY TIME BATTLE RESULTS

    [Rpc(SendTo.ClientsAndHost)]
    internal void ExecuteToynopolyTimeBattleResultsOnClientsRpc()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            PlayerManager3P.Instance.ToynopolyTimeBattleResult();
            PlayerManager3P.Instance.ToynopolyTimeBattleConclude();
            CSVFileReader.Instance.LeaderboardClose();
            Debug.Log("Manual Toynopoly Result Registered On This Client");
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportChangeValueToClientsRpc(int changeValue)
    {
        if(!NetworkManager.Singleton.IsHost)
        {
            MainManager.changeValue = changeValue;
            Debug.Log("Client received Toynopoly change Value " + changeValue);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void MarkNextRaceAsToynopolyBattleRpc()
    {
        MainManager.IsToynopolyBattle = true;
    }

    //PLAYER STATES
       

    [Rpc(SendTo.Server)]
    internal void ReportReadyForNextRoundToServerRpc()
    {
        numberOfPlayersReportedReadyForNextRound.Value++;
        Debug.Log("Player Reported Ready For Next Round - Reported " + numberOfPlayersReportedReadyForNextRound.Value);

        if (numberOfPlayersReportedReadyForNextRound.Value == MainManager.playerNumber)
        {
            SendAllClientsToNextRoundRpc();
            numberOfPlayersReportedReadyForNextRound.Value = 0;
        }
    }

    [Rpc(SendTo.Server)]
    internal void ReportReadyForNextLevelRpc()
    {
        numberOfPlayersReportedReadyForNextLevel.Value++;
        Debug.Log("Player Reported Ready For Next Level - Reported " + numberOfPlayersReportedReadyForNextRound.Value);

        if (numberOfPlayersReportedReadyForNextLevel.Value == MainManager.playerNumber)
        {
            SendAllClientsToLevel2Rpc();            
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SendAllClientsToLevel2Rpc()
    {
        PlayerManager3P.Instance.CallActivePlayer();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SendAllClientsToNextRoundRpc()
    {
        if (MainManager.playerNumber > 2)
        {
            PlayerManager3P.Instance.RoundChangeover();
        }
        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.RoundChangeover();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void CheckAllPlayersSyncedToNewRoundRpc()
    {
        if (PlayerState.Instance.GetPlayerReadinessStatus() == false && numberOfPlayersReportedReadyForNextRound.Value == 0)
        {
            PlayerManager3P.Instance.RoundChangeover();
            Debug.Log("One or more clients lagging behind - forcing Round Changeover");
        }
        else
            Debug.Log("All Clients Synced");
    }

    //NETWORK CAR MARKET

    private void PlayerManager3P_OnActivePlayerHasBoughtCar()
    {
        AckCarPurchaseToClientsRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void AckCarPurchaseToClientsRpc()
    {
        if (!PlayerManager3P.Instance.LocalIsActivePlayer())
        {
            MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
            PlayerManager3P.Instance.PlayerWinsCar(MainManager.activePlayer);

            PlayerManager3P.Instance.UpdateCashDisplay();
            PlayerManager3P.Instance.UpdateInventoryDisplay();

            PlayerManager3P.Instance.SetPromptText(MainManager.playerNames[MainManager.activePlayer] + " has bought a " + MainManager.cars[MainManager.currentCarIndex]);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void DuelAckCarPurchaseToClientsRpc()
    {
        if (!GameManager.Instance.LocalIsActivePlayer())
        {
            MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
            GameManager.Instance.PlayerWinsCar(MainManager.activePlayer);

            GameManager.Instance.UpdateCashDisplay();
            GameManager.Instance.UpdateInventoryDisplay();

            GameManager.Instance.SetPromptText(MainManager.playerNames[MainManager.activePlayer] + " has bought a " + MainManager.cars[MainManager.currentCarIndex]);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void DuelInactivePlayerHasBuyOptionRpc(int playerWithBuyOption)
    {
        if (!GameManager.Instance.LocalIsActivePlayer())
        {
            MainManager.playerWithBuyOption = playerWithBuyOption;
            GameManager.Instance.OfferBuyOption();
        }
    }


    [Rpc(SendTo.ClientsAndHost)]
    private void PlayerManager3P_OnInactivePlayersHaveBuyOptionRpc()
    {
        if(!PlayerManager3P.Instance.LocalIsActivePlayer())
        {
            PlayerManager3P.Instance.OfferBuyOption();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportPurchaseToClientsRpc(int buyerIndex)
    {
        PlayerManager3P.Instance.PlayerWinsCar(buyerIndex);
        MainManager.playerCash[buyerIndex] -= MainManager.carPrizes[MainManager.currentCarIndex];       

        PlayerManager3P.Instance.UpdateInventoryDisplay();
        PlayerManager3P.Instance.UpdateCashDisplay();

        PlayerManager3P.Instance.SetPromptText(MainManager.playerNames[buyerIndex] + " has bought a " + MainManager.cars[MainManager.currentCarIndex]);
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportEmptyInventoryToClientsRpc(int emptyPlayer)
    {
        PlayerManager3P.Instance.SetPromptText(MainManager.playerNames[emptyPlayer] + " has no more cars in their inventory!");
        IdleCountdown.Instance.StartIdleCountdownMax(60f);

        PlayerManager3P.Instance.BlockFields();
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportForcedCarPurchaseToClientsRpc(int car, int buyer)
    {
        MainManager.currentCarIndex = car;
        MainManager.playerInventory[buyer, car]++;
        MainManager.playerCash[buyer] -= MainManager.carPrizes[car];
        PlayerManager3P.Instance.UpdateInventoryDisplay();
        PlayerManager3P.Instance.cashDisplay[buyer].text = MainManager.playerCash[MainManager.buyer].ToString();
        EmptyInventoryHandler.Instance.HideEmptyInventoryPanel();
        IdleCountdown.Instance.HideIdleCountdown();

        PlayerManager3P.Instance.SetPromptText(MainManager.playerNames[buyer] + " has bought a " + MainManager.cars[car].ToString());

        int numberOfOwners = 0;

        for (int j = 0; j < MainManager.playerNumber; j++)
        {
            if (MainManager.playerInventory[j, car] > 0)
            {
                numberOfOwners++;
            }
        }

        if (numberOfOwners == 1)
        {
            PlayerManager3P.Instance.OfferBuyOptionAfterForcedBuy(car, buyer);
        }
        PlayerManager3P.Instance.PerformLevel2Check();

        if (PlayerManager3P.Instance.LocalIsActivePlayer())
            PlayerManager3P.Instance.UnlockFields();

    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportCarSaleToClientsRpc(int car, int seller)
    {
        if (MainManager.localMultiplayerName != MainManager.playerNames[seller])
        {
            MainManager.playerInventory[seller, car]--;
            MainManager.playerCash[seller] += MainManager.carPrizes[car];

            if (MainManager.playerNumber > 2)
            {
                PlayerManager3P.Instance.UpdateInventoryDisplay();
                PlayerManager3P.Instance.cashDisplay[seller].text = MainManager.playerCash[seller].ToString();
            }
            if (MainManager.playerNumber < 3)
            {
               GameManager.Instance.cashDisplay[seller].text = MainManager.playerCash[seller].ToString();
               GameManager.Instance.UpdateInventoryDisplay();
            }            
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void SendProtectionToClientsRpc()
    {
        ProtectionHandler.Instance.ProtectionEnable();
    }

   
    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportChallengeAutoResultToClientsRpc(bool challengeWon, bool challengeLost, int raceWinner)
    {
        PlayerManager3P.Instance.SetChallengeOutcomeBools(challengeWon, challengeLost);
        MainManager.raceWinner = raceWinner;
    }

    [Rpc(SendTo.Server)]
    internal void SkippingRoundNetworkVariableChangeRpc(bool v)
    {
        skippingRoundNetworkVariable.Value = v;
    }

    //MATCH SCORE EDITOR NETWORK HANDOUT

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportPlayerCashBalanceChangeToClientsRpc(int newCash, int playerIndex)
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            MainManager.playerCash[playerIndex] = newCash;

            if (MainManager.playerNumber < 3)
            {
                GameManager.Instance.AcceptDividend();
            }

            else
                PlayerManager3P.Instance.AcceptDividend();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportCarValueChangeToClientsRpc(int price, int carIndex)
    {
        if (!NetworkManager.Singleton.IsHost)
        {

            MainManager.carPrizes[carIndex] = price;


            if (MainManager.playerNumber < 3)
            {
                GameManager.Instance.UpdateCarPrizesDisplay();
            }

            else
                PlayerManager3P.Instance.UpdateCarPrizesDisplay();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void InventorySubmitRpc(int inventoryPlayerIndex, int inventoryCarIndex, int inventoryContent)
    {
        MainManager.playerInventory[inventoryPlayerIndex, inventoryCarIndex] = inventoryContent;

        if (MainManager.playerNumber > 2)
        {
            PlayerManager3P.Instance.UpdateInventoryDisplay();
        }

        else

            GameManager.Instance.UpdateInventoryDisplay();

    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportAcceptedBuyOptionToActiveRpc()
    {
        if(GameManager.Instance.LocalIsActivePlayer())
        {
            GameManager.Instance.AcceptBuyOption();
        }
    }

    [Rpc(SendTo.Server)]
    internal void ReportReadyForSellingRoundToServerRpc()
    {
        numberOfPlayersReadyForSellingRound.Value++;
        Debug.Log("Player Reported Ready For Selling - Reported " + numberOfPlayersReadyForSellingRound.Value);

        if (numberOfPlayersReadyForSellingRound.Value == MainManager.playerNumber)
        {
            SendAllClientsToSellingRoundRpc();
            numberOfPlayersReadyForSellingRound.Value = 0;
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SendAllClientsToSellingRoundRpc()
    {
        GameManager.Instance.StartSellingRound();
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void EndGameOnAllClientsRpc()
    {
        Debug.Log("Ending Game");

        if(!NetworkManager.Singleton.IsHost)
        PlayerManager3P.Instance.EndGame();
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void ReportReverseTrackToAllPlayersRpc()
    {
        if (!GameManager.Instance.LocalIsActivePlayer())
        {
            MainManager.nextTrackReverse = true;
            GameObject.Find("NextRaceComingUpPanel").GetComponent<ReverseHandler>().RemoveIcon();
            Debug.Log("Next Track Is Reversed");
        }
    }



}

