using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor;



public class PlayerManager3P : MonoBehaviour
{
    public static PlayerManager3P Instance;

    public Button[] fields;
    public Button[] playerSellButton;

    [SerializeField] GameObject[] carNameButtons;


    public ToggleGroup toggleGroup;

    public Button challengeButtonFirstInactive;
    public Button challengeButtonSecondInactive;
    public Button challengeButtonThirdInactive;
    public Button challengeButtonFourthInactive;

    public Button challengeRaceProgressCar;
    public Button challengeRaceProgressTrack;

    public GameObject[] rows;

    private bool l2SelectionIsOkay = true;
    //private bool l3SelectionIsOkay = true;
    private bool buyingPossible = true;
    private bool playerHasBoughtCarThisRound = false;
    public bool activePlayerHasToynopoly = false;

    private bool[] wantsToBuy = { false, false, false, false, false };
    public bool stolenWin = false;

    public bool challengeWon = true;
    public bool challengeLost = false;

    private int[] carValueChangeOptions = { -10, -7, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 7, 10 };

    //private List<int> roundsWithCarSellingOption = new List<int> { 3, 6, 9, 12 };

    public int lastChangebeforeDefault = 0;

    public AudioSource audioSource;
    public AudioClip panelOpen;
    public AudioClip stageReady;
    public AudioClip coinFalling;
    public AudioClip heartbeat;
    public AudioClip success;

    public TextMeshProUGUI[] cashDisplay;

    [SerializeField] Button[] invDisplayP1;
    [SerializeField] Button[] invDisplayP2;
    [SerializeField] Button[] invDisplayP3;
    [SerializeField] Button[] invDisplayP4;
    [SerializeField] Button[] invDisplayP5;

    [SerializeField] Button buyCarButton;
    [SerializeField] Button protectButton;

    public Button[] carPic;

    public TextMeshProUGUI[] carPrizeDisplays;


    public TextMeshProUGUI statusInfoTextBar;

    public string selectedTrack;
    public string selectedCar;

    [SerializeField] GameObject[] turnIndicator;

    public GameObject nextRaceComingUpPanel;
    public GameObject cancelNextRaceButton;

    [SerializeField] GameObject raceInProgressPanel;
    [SerializeField] GameObject raceInProgressPanelChallenge;
    [SerializeField] GameObject raceResultsPanelL1;
    [SerializeField] GameObject postRaceMarketPanel;

    [SerializeField] GameObject priceUpArrow;
    [SerializeField] GameObject priceDownArrow;

    [SerializeField] GameObject level2StartPanel;
    [SerializeField] GameObject startRaceButton;
    [SerializeField] GameObject preProtectButton;
    [SerializeField] GameObject protectOptionPanel;
    [SerializeField] TextMeshProUGUI protectionOptionCarName;

    [SerializeField] GameObject continueToChallengeButton;

    [SerializeField] GameObject buyOptionPanel;
    [SerializeField] TextMeshProUGUI buyOptionText;
    [SerializeField] Image carToBuyImage;

    [SerializeField] GameObject challengePanel;

    [SerializeField] GameObject continueButtonToynopoly;
    [SerializeField] GameObject continueButtonToynopolyAuto;
    [SerializeField] GameObject continueButtonNormal;
    [SerializeField] GameObject getManualResultsButtonL2;
    [SerializeField] GameObject getAutoResultsButtonNormal;
    [SerializeField] GameObject getAutoResultsButtonNormalL1;


    [SerializeField] GameObject raceResultsPanelL2;

    [SerializeField] GameObject challengeOutcomePanel;

    [SerializeField] GameObject timeBattlePanel;

    [SerializeField] GameObject buffNerfPanel;

    [SerializeField] GameObject preSellingInfoPanel;

    [SerializeField] GameObject raceResultsPanelL2T;

    [SerializeField] GameObject gameOverPanel;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject hostOptionsPanel;

    [SerializeField] GameObject timerPanel;

    [SerializeField] ParticleSystem[] finale;

    //[SerializeField] Button[] sellButtons = { };

    public TMP_Dropdown winnerDropdown;
    public TMP_Dropdown runnerUpDropdown;
    public TMP_Dropdown thirdPlaceDropdown;

    public TMP_Dropdown playersL2WinnerDropdown;

    [SerializeField] TextMeshProUGUI resultsPanelL2ActivePlayerDisplay;

    [SerializeField] Slider gapToDefender;
    [SerializeField] Slider gapToChallenger;
    [SerializeField] Slider gapStolenWin;
    [SerializeField] GameObject winnerGapSlider;

    [SerializeField] Slider gapToFirst;
    [SerializeField] Slider gapToLast;

    [SerializeField] TextMeshProUGUI gapSecondsDisplaySteal;
    [SerializeField] TextMeshProUGUI gapSecondsDisplayWin;
    [SerializeField] TextMeshProUGUI gapSecondsDisplayLoss;

    [SerializeField] TextMeshProUGUI gapSecondsDisplayToLast;
    [SerializeField] TextMeshProUGUI gapSecondsDisplayToFirst;



    [SerializeField] TextMeshProUGUI activePlayerMessage;
    public TextMeshProUGUI helpText;



    [SerializeField] TextMeshProUGUI nextTrackDisplay;
    [SerializeField] TextMeshProUGUI nextCarDisplay;
    [SerializeField] TextMeshProUGUI currentRaceInfoRound;
    [SerializeField] TextMeshProUGUI currentRaceInfoTrack;
    [SerializeField] TextMeshProUGUI currentRaceInfoCar;
    [SerializeField] TextMeshProUGUI currentRaceOpponent1;

    [SerializeField] TextMeshProUGUI currentCarNameMarketPanel;
    [SerializeField] TextMeshProUGUI currentCarPrizeMarketPanel;
    [SerializeField] TextMeshProUGUI carValueChangeDisplay;
    [SerializeField] TextMeshProUGUI valueChangeMessage;

    [SerializeField] GameObject carInDefaultPanel;
    [SerializeField] GameObject defaultDownArrow;

    [SerializeField] Button SellDoneButton;



    [SerializeField] TextMeshProUGUI defaultCarName;
    [SerializeField] TextMeshProUGUI defaultCarValueChange;
    [SerializeField] TextMeshProUGUI defaultPanelTextMessage;


    [SerializeField] TextMeshProUGUI firstinactivePlayerName;
    [SerializeField] TextMeshProUGUI secondinactivePlayerName;
    [SerializeField] TextMeshProUGUI thirdinactivePlayerName;
    [SerializeField] TextMeshProUGUI fourthinactivePlayerName;

    [SerializeField] TextMeshProUGUI challengefirstInactive;
    [SerializeField] TextMeshProUGUI challengeSecondInactive;
    [SerializeField] TextMeshProUGUI challengeThirdInactive;
    [SerializeField] TextMeshProUGUI challengeFourthInactive;

    [SerializeField] TextMeshProUGUI challengeProgressTextInfo;

    [SerializeField] TextMeshProUGUI challengerNameL2;
    [SerializeField] TextMeshProUGUI defenderNameL2;

    [SerializeField] TextMeshProUGUI challengerNameResultsChallenge;
    [SerializeField] TextMeshProUGUI defenderNameResultsChallenge;

    [SerializeField] TextMeshProUGUI challengeWinnerDisplay;
    [SerializeField] TextMeshProUGUI challengeDefeatedDisplay;
    [SerializeField] TextMeshProUGUI challengeCarDisplay;

    [SerializeField] TextMeshProUGUI timeBattleWinnerDisplay;
    [SerializeField] TextMeshProUGUI timeBattleSecondsDisplay;

    [SerializeField] TextMeshProUGUI[] timeBattleNameDisplay;
    [SerializeField] TextMeshProUGUI[] timeBattlePrizeDisplay;
    [SerializeField] GameObject[] timeBattleButtons;

    [SerializeField] TextMeshProUGUI toynopolyHolderName;

    [SerializeField] TextMeshProUGUI resultsP1Name;
    [SerializeField] TextMeshProUGUI resultsP2Name;
    [SerializeField] TextMeshProUGUI resultsP3Name;
    [SerializeField] TextMeshProUGUI resultsP4Name;
    [SerializeField] TextMeshProUGUI resultsP5Name;

    [SerializeField] TextMeshProUGUI resultsP1cashTotal;
    [SerializeField] TextMeshProUGUI resultsP2cashTotal;
    [SerializeField] TextMeshProUGUI resultsP3cashTotal;
    [SerializeField] TextMeshProUGUI resultsP4cashTotal;
    [SerializeField] TextMeshProUGUI resultsP5cashTotal;


    [SerializeField] Sprite carDefaultSprite;

    [SerializeField] TextMeshProUGUI promptText;

    public List<Toggle> challengeToggles = new();

    public int raceWinnerLevel1 = 0;
    public int runnerUpLevel1 = 0;
    public int thirdLevel1 = 0;

    public int stealer;

    private DividendGenerator dividendScript;
    private EmptyInventoryHandler emptyInventoryScript;
    private Timer timerScript;
    private ProtectionHandler protectionScript;
    private CountUpHandler countUpScript;
    private LapDataReader lapCountScript;

    public static event Action OnLevel2Start;
    public static event Action OnRoundChangeover;
    public static event Action OnActivePlayerRaceStarted;
    public static event Action OnRaceConcluded;
    public static event Action OnReadyForRoundChangeover;

    public static event Action OnActivePlayerHasBoughtCar;
    public static event Action OnInactivePlayersHaveBuyOption;
    public static event Action OnPlayerHasDecidedBuyOption;

    public static event Action OnActivePlayerCalled;
    public static event Action OnStartSellingRound;
    public static event Action OnActivePlayerCanProtectAfterChallenge;
    

    private bool resultsRegisteredForRound = false;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {MainManager.raceThreshold - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");


        toggleGroup = GetComponent<ToggleGroup>();
        dividendScript = GameObject.Find("DividendGenerator").GetComponent<DividendGenerator>();
        emptyInventoryScript = GameObject.Find("EmptyInventoryHandler").GetComponent<EmptyInventoryHandler>();
        timerScript = GameObject.Find("Timer").GetComponent<Timer>();
        protectionScript = GameObject.Find("ProtectionHandler").GetComponent<ProtectionHandler>();
        countUpScript = GameObject.Find("CountUpHandler").GetComponent<CountUpHandler>();
        lapCountScript = GameObject.Find("LapDataReader").GetComponent<LapDataReader>();


        audioSource.PlayOneShot(heartbeat);

        for (int i = 0; i < MainManager.fieldAvailable.Length; i++)

        {
            MainManager.fieldAvailable[i] = true;
        }

        GridGenerator3P.OnGameTableReady += CallActivePlayer;

        OnlineManager.Instance.pendingFieldNetwork.OnValueChanged += OnPendingFieldChanged;

    }

    private void Start()
    {
        Instance =this;

        DontDestroyOnLoad(this.gameObject);

        GridGenerator3P.OnGameTableReady += CallActivePlayer;

        OnlineManager.Instance.pendingFieldNetwork.OnValueChanged += OnPendingFieldChanged;

        OnlineManager.Instance.level1RaceIsInProgress.OnValueChanged += OnRaceLevel1InProgress;

        OnlineManager.Instance.level2RaceIsInProgress.OnValueChanged += OnRaceLevel2InProgress;

        BlockFields();
    }


    public bool LocalIsActivePlayer()
    {
        ulong activePlayerID = OnlineManager.Instance.GetPlayerID(MainManager.playerNames[MainManager.activePlayer]);

        return OnlineManager.Instance.GetLocalClientID() == activePlayerID;
    }


    private void CallActivePlayer()
    {
        ulong activePlayerID;

        activePlayerID = OnlineManager.Instance.GetPlayerID(MainManager.playerNames[MainManager.activePlayer]);
        Debug.Log("Active Player ID is " + activePlayerID);
        Debug.Log("Local Player ID is " + OnlineManager.Instance.GetLocalClientID());

        if (OnlineManager.Instance.GetLocalClientID() != activePlayerID)
        {
            SetPromptText("Waiting for " + MainManager.playerNames[MainManager.activePlayer] + "s turn");

            BlockFields();

            OnActivePlayerCalled?.Invoke();
        }

        else
        {
            SetPromptText("It's your turn! Select a field");

            UnlockFields();
        }
    }

    public void BlockFields()
    {
        foreach (Button field in fields)
        {
            if (field != null)
                field.interactable = false;
        }
    }

    public void UnlockFields()
    {
        foreach (Button field in fields)
        {
            if (field != null)
                field.interactable = true;
        }
    }

    public void SetPromptText(string message)
    {
        promptText.text = message.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            pausePanel.GetComponent<PausePanelHandler>().HideGameHostOptionsForClient();          
        }
    }


    public void Resume()

    {
        pausePanel.SetActive(false);
    }

    //FIRST NETWORK EVENT START---------------------------------------------------

    private void OnPendingFieldChanged(int previousValue, int newValue)
    {
        Debug.Log("Network Event Invoked: Pending Field Changed");

        if (!LocalIsActivePlayer())
        {
            Debug.Log("Field Clicked Method Activated On Client");
            FieldClicked(newValue);
        }
    }

    //FIRST NETWORK EVENT FINISHED--------------------------------------------------


    public void FieldClicked(int fieldNumber)
    {        

        Debug.Log("In Field Clicked Method - Local Is Active Player " + LocalIsActivePlayer());
        if (LocalIsActivePlayer())
        {
            OnlineManager.Instance.ReportPendingFieldRpc(fieldNumber);
            Debug.Log("Pending Field Changed by Active Player");
        }

        MainManager.pendingField = fieldNumber;

        if (fieldNumber <= 9)

        {
            selectedCar = MainManager.cars[0];
            MainManager.currentCarIndex = 0;
        }

        else if (fieldNumber <= 19)

        {
            selectedCar = MainManager.cars[1];
            MainManager.currentCarIndex = 1;
        }

        else if (fieldNumber <= 29)
        {
            selectedCar = MainManager.cars[2];
            MainManager.currentCarIndex = 2;
        }

        else if (fieldNumber <= 39)
        {
            selectedCar = MainManager.cars[3];
            MainManager.currentCarIndex = 3;
        }

        else if (fieldNumber <= 49)
        {
            selectedCar = MainManager.cars[4];
            MainManager.currentCarIndex = 4;
        }

        else

        {
            selectedCar = MainManager.cars[5];
            MainManager.currentCarIndex = 5;
        }

        if (fieldNumber == 0 || fieldNumber == 10 || fieldNumber == 20 || fieldNumber == 30 || fieldNumber == 40 || fieldNumber == 50)

        { selectedTrack = MainManager.activeTracks[0]; }

        else if (fieldNumber == 1 || fieldNumber == 11 || fieldNumber == 21 || fieldNumber == 31 || fieldNumber == 41 || fieldNumber == 51)

        { selectedTrack = MainManager.activeTracks[1]; }

        else if (fieldNumber == 2 || fieldNumber == 12 || fieldNumber == 22 || fieldNumber == 32 || fieldNumber == 42 || fieldNumber == 52)

        { selectedTrack = MainManager.activeTracks[2]; }

        else if (fieldNumber == 3 || fieldNumber == 13 || fieldNumber == 23 || fieldNumber == 33 || fieldNumber == 43 || fieldNumber == 53)

        { selectedTrack = MainManager.activeTracks[3]; }

        else if (fieldNumber == 4 || fieldNumber == 14 || fieldNumber == 24 || fieldNumber == 34 || fieldNumber == 44 || fieldNumber == 54)

        { selectedTrack = MainManager.activeTracks[4]; }

        else if (fieldNumber == 5 || fieldNumber == 15 || fieldNumber == 25 || fieldNumber == 35 || fieldNumber == 45 || fieldNumber == 55)

        { selectedTrack = MainManager.activeTracks[5]; }

        else if (fieldNumber == 6 || fieldNumber == 16 || fieldNumber == 26 || fieldNumber == 36 || fieldNumber == 46 || fieldNumber == 56)

        { selectedTrack = MainManager.activeTracks[6]; }

        else if (fieldNumber == 7 || fieldNumber == 17 || fieldNumber == 27 || fieldNumber == 37 || fieldNumber == 47 || fieldNumber == 57)

        { selectedTrack = MainManager.activeTracks[7]; }

        else if (fieldNumber == 8 || fieldNumber == 18 || fieldNumber == 28 || fieldNumber == 38 || fieldNumber == 48 || fieldNumber == 58)

        { selectedTrack = MainManager.activeTracks[8]; }

        else

        { selectedTrack = MainManager.bonusTrack; }

        helpText.text = "";

        ShowNextRacePanel();


        if (MainManager.matchTimeDisplayed)
        {
            timerScript.DisplayToggle(false);
            timerPanel.gameObject.SetActive(false);
        }
    }
       

    void ShowNextRacePanel()
    {
        BlockFields();

        try
        {
            nextRaceComingUpPanel.transform.localScale = new Vector3(0.824999988f, 0.738307893f, 1);
        }
        catch (Exception e) { Debug.Log("Game Object Busy " + e);
                              
                              return;
        };

        try
        {
            audioSource.PlayOneShot(panelOpen); ;
        }
        catch (Exception e) { Debug.Log("Game Object Busy " + e); };        
            
        nextTrackDisplay.text = selectedTrack;
        nextCarDisplay.text = selectedCar;

        switch (MainManager.levelCounter)
        {
            case 1:
                if (!LocalIsActivePlayer())
                {
                    startRaceButton.SetActive(false);
                    cancelNextRaceButton.SetActive(false);
                }

                if (LocalIsActivePlayer())
                {
                    startRaceButton.SetActive(true);
                    cancelNextRaceButton.SetActive(true);
                }

                activePlayerMessage.text = ($"{MainManager.playerNames[MainManager.activePlayer]} has selected:");
                break;

            case 2:
                if (LocalIsActivePlayer())
                {
                    buyCarButton.gameObject.SetActive(true);
                    cancelNextRaceButton.SetActive(true);
                    startRaceButton.SetActive(true);
                }

                if (!LocalIsActivePlayer())
                {
                    cancelNextRaceButton.SetActive(false);
                    buyCarButton.gameObject.SetActive(false);
                    startRaceButton.SetActive(false);
                }

                    activePlayerHasToynopoly = false;

                FillInactivePlayersArray();
                PerformLevel2Check();

                switch (activePlayerHasToynopoly)
                {
                    case true:
                        if(LocalIsActivePlayer())
                        startRaceButton.SetActive(true);

                        continueToChallengeButton.SetActive(false);
                        activePlayerMessage.text = ("You have a Toynopoly for this car.");

                        if (MainManager.shieldAvailable[MainManager.activePlayer] == true)
                        {
                            if(LocalIsActivePlayer())
                            protectButton.gameObject.SetActive(true);
                        }

                        if (MainManager.protection[MainManager.currentCarIndex] == true || playerHasBoughtCarThisRound == true)
                        {
                            buyCarButton.gameObject.SetActive(false);
                        }
                        break;

                    case false:
                        startRaceButton.SetActive(false);
                        protectButton.gameObject.SetActive(false);

                        if(LocalIsActivePlayer())
                        continueToChallengeButton.SetActive(true);
                        break;
                }


                if (l2SelectionIsOkay == false)
                {
                    if (buyingPossible == true && playerHasBoughtCarThisRound == false)
                    {
                        if(LocalIsActivePlayer())
                        activePlayerMessage.text = ("You don't own this car. Would you like to buy it?");

                        startRaceButton.SetActive(false);
                        continueToChallengeButton.SetActive(false);
                        protectButton.gameObject.SetActive(false);
                    }

                    if (buyingPossible == true && playerHasBoughtCarThisRound == true)
                    {
                        if(LocalIsActivePlayer())
                        activePlayerMessage.text = ("You don't own this car.");

                        buyCarButton.gameObject.SetActive(false);
                        continueToChallengeButton.SetActive(false);
                        startRaceButton.SetActive(false);
                        protectButton.gameObject.SetActive(false);
                    }

                    else if (buyingPossible == false)
                    {
                        if(LocalIsActivePlayer())
                        activePlayerMessage.text = ("An opponent has a protected Toynopoly for this car. Please choose a different car");

                        continueToChallengeButton.SetActive(false);
                        startRaceButton.SetActive(false);
                        buyCarButton.gameObject.SetActive(false);
                        protectButton.gameObject.SetActive(false);
                    }

                }

                else if (activePlayerHasToynopoly)
                {
                    activePlayerMessage.text = ($"{MainManager.playerNames[MainManager.activePlayer]} has made their selection:");

                    if (MainManager.protection[MainManager.currentCarIndex] == true || playerHasBoughtCarThisRound == true)
                    {
                        buyCarButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        if(LocalIsActivePlayer())
                        buyCarButton.gameObject.SetActive(true);
                    }
                    continueToChallengeButton.SetActive(false);

                    if(LocalIsActivePlayer())
                    startRaceButton.SetActive(true);
                }

                else
                {
                    activePlayerMessage.text = ($"{MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
                    startRaceButton.SetActive(false);

                    if (playerHasBoughtCarThisRound)

                    {
                        buyCarButton.gameObject.SetActive(false);
                    }

                }
                break;

            case 3:
                //fill for Level 3
                break;
        }
    }
    
    public void ReportNextRaceIsToynopolyBattleToClients()
    {
        if(MainManager.levelCounter == 2)
        {
            OnlineManager.Instance.MarkNextRaceAsToynopolyBattleRpc();
            Debug.Log("Active Player Reported Next Round As Toynopoly Battle");
        }
    }


    public void OpenChallengePanel()
    {
        challengeButtonFirstInactive.gameObject.SetActive(true);
        challengeButtonSecondInactive.gameObject.SetActive(true);

        if (MainManager.playerNumber > 3)
        {
            challengeButtonThirdInactive.gameObject.SetActive(true);
        }

        if (MainManager.playerNumber > 4)
        {
            challengeButtonFourthInactive.gameObject.SetActive(true);
        }

        nextRaceComingUpPanel.gameObject.transform.localScale = new Vector3(0, 0, 0);
        challengePanel.SetActive(true);
        challengefirstInactive.text = MainManager.playerNames[MainManager.inactivePlayers[0]];
        challengeSecondInactive.text = MainManager.playerNames[MainManager.inactivePlayers[1]];

        if (MainManager.playerNumber > 3)
        {
            challengeThirdInactive.text = MainManager.playerNames[MainManager.inactivePlayers[2]];
        }

        if (MainManager.playerNumber > 4)
        {
            challengeFourthInactive.text = MainManager.playerNames[MainManager.inactivePlayers[3]];
        }

        if (MainManager.playerInventory[MainManager.inactivePlayers[0], MainManager.currentCarIndex] < 1)
        {
            challengeButtonFirstInactive.gameObject.SetActive(false);

        }

        if (MainManager.playerInventory[MainManager.inactivePlayers[1], MainManager.currentCarIndex] < 1)
        {
            challengeButtonSecondInactive.gameObject.SetActive(false);
        }

        if (MainManager.playerNumber > 3)
        {
            if (MainManager.playerInventory[MainManager.inactivePlayers[2], MainManager.currentCarIndex] < 1)
            {
                challengeButtonThirdInactive.gameObject.SetActive(false);
            }
        }

        if (MainManager.playerNumber > 4)
        {
            if (MainManager.playerInventory[MainManager.inactivePlayers[3], MainManager.currentCarIndex] < 1)
            {
                challengeButtonFourthInactive.gameObject.SetActive(false);
            }
        }
    }

    public void ChallengeCancel()
    {
        challengePanel.SetActive(false);
    }


    public void SetDefenderAndContinue(int defender)
    {
        if(!LocalIsActivePlayer())
            nextRaceComingUpPanel.gameObject.transform.localScale = new Vector3(0, 0, 0);

        challengePanel.SetActive(false);

        if (LocalIsActivePlayer())
        {
            MainManager.defendingPlayer = MainManager.inactivePlayers[defender];
            StartRace();
        }

        raceInProgressPanelChallenge.SetActive(true);

        if (NetworkManager.Singleton.IsHost)
        {
            getAutoResultsButtonNormal.SetActive(true);
            getAutoResultsButtonNormal.gameObject.SetActive(true);
            getManualResultsButtonL2.gameObject.SetActive(true);
        }

            if (!NetworkManager.Singleton.IsHost)
        {
            getAutoResultsButtonNormal.gameObject.SetActive(false);
            getManualResultsButtonL2.gameObject.SetActive(false);
        }

        lapCountScript.FindLapData(selectedTrack);

        challengeRaceProgressCar.GetComponentInChildren<TMP_Text>().text = selectedCar;
        challengeRaceProgressTrack.GetComponentInChildren<TMP_Text>().text = selectedTrack;
        challengeProgressTextInfo.text = ("Level " + MainManager.levelCounter + ", Race " + MainManager.roundCounter + " / " + ((MainManager.raceThreshold) - 1) + " in progress");

        challengerNameL2.text = MainManager.playerNames[MainManager.activePlayer];
        defenderNameL2.text = MainManager.playerNames[MainManager.defendingPlayer];

        if(LocalIsActivePlayer())
        OnlineManager.Instance.ReportDefendingPlayerToNetworkRpc(MainManager.defendingPlayer);
    }


    public void BuyCar()
    {
        if (MainManager.playerCash[MainManager.buyer] < MainManager.carPrizes[MainManager.currentCarIndex])
        {
            emptyInventoryScript.notEnoughCashPanel.SetActive(true);
            emptyInventoryScript.emptyInvDialoguePanel.SetActive(false);
            nextRaceComingUpPanel.gameObject.transform.localScale = new Vector3(0, 0, 0);
            return;
        }

        else
        {
            MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
            PlayerWinsCar(MainManager.activePlayer);

            UpdateCashDisplay();
            UpdateInventoryDisplay();

            nextRaceComingUpPanel.gameObject.transform.localScale = new Vector3(0, 0, 0);
            l2SelectionIsOkay = true;
            buyingPossible = true;
            playerHasBoughtCarThisRound = true;

            OnActivePlayerHasBoughtCar?.Invoke();
            if(LocalIsActivePlayer())
            {
                UnlockFields();
            }

            FillInactivePlayersArray();
            Debug.Log($"Inactive players are {MainManager.inactivePlayers[0]} + {MainManager.inactivePlayers[1]} + {MainManager.inactivePlayers[2]} + {MainManager.inactivePlayers[3]}");
            PerformLevel2Check();
            
            if (activePlayerHasToynopoly == true && MainManager.fieldsLeftForCar[MainManager.currentCarIndex] > 9)
            {
                OnInactivePlayersHaveBuyOption?.Invoke();
                SetPromptText("Waiting for other players to also buy or pass");
                IdleCountdown.Instance.StartIdleCountdownMax(30f);
                //BlockFields();                
            }
        }
    }

    public void OfferBuyOption()
    {
        buyOptionPanel.SetActive(true);

        buyOptionText.text = (MainManager.playerNames[MainManager.activePlayer] + " has bought a " + MainManager.cars[MainManager.currentCarIndex] + ". Would you also like to buy it for " + MainManager.carPrizes[MainManager.currentCarIndex] + "?");

        for (int i = 0; i < GridGenerator3P.Instance.activeList.Count; i++)
        {
            if (GridGenerator3P.Instance.activeList[i] == MainManager.cars[MainManager.currentCarIndex])

            { carToBuyImage.sprite = GridGenerator3P.Instance.activeSpriteList[i]; }
        }

        IdleCountdown.Instance.StartIdleCountdownMax(30f);
    }

    public void OfferBuyOptionAfterForcedBuy(int car, int buyer)
    {
        if (MainManager.localMultiplayerName != MainManager.playerNames[buyer])
        {
            buyOptionPanel.SetActive(true);

            buyOptionText.text = (MainManager.playerNames[buyer] + " has bought a " + MainManager.cars[car] + ". Would you also like to buy it for " + MainManager.carPrizes[car] + "?");

            for (int i = 0; i < GridGenerator3P.Instance.activeList.Count; i++)
            {
                if (GridGenerator3P.Instance.activeList[i] == MainManager.cars[car])

                { carToBuyImage.sprite = GridGenerator3P.Instance.activeSpriteList[i]; }
            }
            
            MainManager.currentCarIndex = car;
        }

        IdleCountdown.Instance.StartIdleCountdownMax(30f);
    }


    public void PassBuyOption()
    {
        buyOptionPanel.SetActive(false);
        IdleCountdown.Instance.HideIdleCountdown();
        OnPlayerHasDecidedBuyOption?.Invoke();
    }

    public void ConcludeBuyOption()
    {
        int buyerIndex = 9;

        for (int i = 0; i < MainManager.playerNumber;  i++)
        {
            if(MainManager.localMultiplayerName == MainManager.playerNames[i])
            {
                buyerIndex = i;
            }
        }

        OnlineManager.Instance.ReportPurchaseToClientsRpc(buyerIndex);  
        
        buyOptionPanel.SetActive(false);
        IdleCountdown.Instance.HideIdleCountdown();

        OnPlayerHasDecidedBuyOption?.Invoke();
    }
        

    void FillInactivePlayersArray()

    {
        int InactivePlayersArrayIndex = 0;

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            if (i != MainManager.activePlayer)

            {
                MainManager.inactivePlayers[InactivePlayersArrayIndex] = i;
                InactivePlayersArrayIndex++;
            }
        }
    }


    public void PerformLevel2Check()
    {
        //emptyInventoryScript.CheckInactivePlayersInventory();

        int numberOfOwners = 0;

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            if (MainManager.playerInventory[i, MainManager.currentCarIndex] > 0)
            {
                numberOfOwners++;
            }

        }

        if (numberOfOwners == 1 && MainManager.protection[MainManager.currentCarIndex] == true)

        { buyingPossible = false; }

        if (MainManager.playerInventory[MainManager.activePlayer, MainManager.currentCarIndex] > 0 && numberOfOwners < 2)

        { activePlayerHasToynopoly = true; }

        else if (MainManager.playerInventory[MainManager.activePlayer, MainManager.currentCarIndex] < 1)

        { l2SelectionIsOkay = false; }

    }

    public void CancelRace()
    {
        nextRaceComingUpPanel.gameObject.transform.localScale = new Vector3(0, 0, 0);
        l2SelectionIsOkay = true;
        buyingPossible = true;
        protectButton.gameObject.SetActive(false);
        UnlockFields();
    }


    //SECOND NETWORK EVENT START------------------------

    private void OnRaceLevel1InProgress(bool previousValue, bool newValue)
    {
        if (newValue == true)
        {
            if (LocalIsActivePlayer())
            { return; }
            else
                StartRace();        
        } 

        if (newValue == false)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                return;
            }
            else
            {
                CSVFileReader.Instance.SetAutoResultsValidRpc();                
                CSVFileReader.Instance.LeaderboardClose();
                RegisterResults();
            }                
        }
    }

    //SECOND NETWORK EVENT END---------------------------
    //THIRD NETWORK EVENT START----------------------------
    private void OnRaceLevel2InProgress(bool previousValue, bool newValue)
    {
        Debug.Log("Race 2 In Progress Network Variable Changed To " + newValue);

        if (newValue == true)
        {
            if (LocalIsActivePlayer())
            { return; }
            else
                StartRace();
        }

        if (newValue == false)
        {
            Debug.Log("Local Is Host " + NetworkManager.Singleton.IsHost);

            if (!NetworkManager.Singleton.IsHost)
            {                
                CSVFileReader.Instance.LeaderboardClose();
                RegisterResults();
                CSVFileReader.Instance.ChallengeRaceInProgessPanelClose();

                Debug.Log("Results Registered On Clients");                
            }

            promptText.text = ("Waiting for race winner " + MainManager.playerNames[MainManager.raceWinner] + " to change the value of a car");

            CallTimeBattleWinnerDecision();
        }
    }
    //THIRD NETWORK EVENT FINISHED--------------------------------------

    public void HideNextRaceComingUpPanel()
    {
        try
        {
            nextRaceComingUpPanel.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
    }


    public void StartRace()
    {
        HideNextRaceComingUpPanel();

        protectButton.gameObject.SetActive(false);

        OnActivePlayerRaceStarted?.Invoke();

        if(MainManager.levelCounter == 2)
        {
            Debug.Log("Starting Race, Local Is Active And Will Report Level 2 Race In Progress " + LocalIsActivePlayer());

            if (LocalIsActivePlayer())
            {
                Debug.Log("Level 2 Race In progress Network Variable Set To True");
                OnlineManager.Instance.ReportRaceLevel2InProgressRpc(true);
            }

            Debug.Log("This Round Is Toynopoly Battle " + MainManager.IsToynopolyBattle);

            if (MainManager.IsToynopolyBattle == true)
            {
                raceInProgressPanel.gameObject.SetActive(true);
                lapCountScript.FindLapData(selectedTrack);
                continueButtonNormal.SetActive(false);
                getAutoResultsButtonNormal.SetActive(false);
                getAutoResultsButtonNormalL1.SetActive(false);

                if (NetworkManager.Singleton.IsHost)
                {
                    continueButtonToynopoly.SetActive(true);
                    continueButtonToynopolyAuto.SetActive(true);
                }

                audioSource.PlayOneShot(stageReady);
                currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} in progress");
                currentRaceInfoTrack.text = selectedTrack;
                currentRaceInfoCar.text = selectedCar;
                currentRaceOpponent1.text = MainManager.playerNames[MainManager.activePlayer];
            }
        }

        if (MainManager.levelCounter == 1)
        {
            if (LocalIsActivePlayer())
            {
                OnlineManager.Instance.ReportRaceLevel1InProgressRpc(true);
            }

            raceInProgressPanel.gameObject.SetActive(true);
            lapCountScript.FindLapData(selectedTrack);

            if (NetworkManager.Singleton.IsHost)
            {
                continueButtonNormal.SetActive(true);
                getAutoResultsButtonNormalL1.SetActive(true);
            }

            else
            {
                continueButtonNormal.SetActive(false);
                getAutoResultsButtonNormalL1.SetActive(false);
            }


            continueButtonToynopoly.SetActive(false);
            continueButtonToynopolyAuto.SetActive(false);
            audioSource.PlayOneShot(stageReady);

            if (MainManager.playerNumber == 5)
            {
                currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} / {(MainManager.raceThreshold) - 1} in progress");
            }

            else

            {
                currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} / {(MainManager.raceThreshold) - 1} in progress");
            }

            currentRaceInfoTrack.text = selectedTrack;
            currentRaceInfoCar.text = selectedCar;
            currentRaceOpponent1.text = MainManager.playerNames[MainManager.activePlayer];
        }


        
    }

    //Only Call For Manual Results - Auto Results Determined through CSV Reader 
    public void ShowResultsPanel()
    {
        CSVFileReader.Instance.SetAutoResultsInvalidRpc();
        Debug.Log("Host Requested Auto Results Invalid on Clients");

        raceInProgressPanel.gameObject.SetActive(false);
        raceInProgressPanelChallenge.gameObject.SetActive(false);

        if (MainManager.levelCounter == 1)
        {
            raceResultsPanelL1.SetActive(true);
        }

        else if (activePlayerHasToynopoly)
        {
            //raceResultsPanelL2T.SetActive(true);
        }

        else
        {
            raceResultsPanelL2.SetActive(true);

            challengerNameResultsChallenge.text = MainManager.playerNames[MainManager.activePlayer];

            defenderNameResultsChallenge.text = MainManager.playerNames[MainManager.defendingPlayer];
        }
    }

    public void RegisterResults()
    {
        

        if (resultsRegisteredForRound)
            return;

        resultsRegisteredForRound = true; 
        //This is reset in Round Changeover


        fields[MainManager.pendingField].gameObject.SetActive(false);
        MainManager.fieldAvailable[MainManager.pendingField] = false;

        MainManager.fieldsLeftForCar[MainManager.currentCarIndex]--;

        switch (MainManager.levelCounter)
        {
            case 1:

                if (!MainManager.autoResultsValid)
                {
                    Debug.Log(winnerDropdown.value);
                    Debug.Log(runnerUpDropdown.value);


                    if ((winnerDropdown.value) == (MainManager.activePlayer))

                    {
                        MainManager.activePlayerWins = true;
                    }

                    else

                    {
                        MainManager.activePlayerWins = false;
                    }

                    raceWinnerLevel1 = winnerDropdown.value;
                    runnerUpLevel1 = runnerUpDropdown.value;

                    if (MainManager.playerNumber > 4)
                    {
                        thirdLevel1 = thirdPlaceDropdown.value;
                        Debug.Log(thirdPlaceDropdown.value);
                    }

                    //Network Reporting Manual Results
                    if(NetworkManager.Singleton.IsHost)                       
                    OnlineManager.Instance.Level1ReportManualResultsToServerRpc(raceWinnerLevel1, runnerUpLevel1, MainManager.activePlayerWins);
                }

                else if (MainManager.autoResultsValid)
                {
                    raceInProgressPanel.SetActive(false);
                    raceInProgressPanelChallenge.SetActive(false);
                }

                raceResultsPanelL1.SetActive(false);
                                

                Level1Scoring();
                PostRaceRandomMarketProcedure();

                if (NetworkManager.Singleton.IsHost)
                    OnlineManager.Instance.ReportRaceLevel1InProgressRpc(false);

                break;

            case 2:

                //raceResultsPanelL2.SetActive(true);                               

                Debug.Log("Challenge Won Bool " + challengeWon);
                Debug.Log("Challenge Lost Bool " + challengeLost);
                if (challengeWon == true)
                {
                    PlayerWinsCar(MainManager.activePlayer);
                    PlayerLosesCar(MainManager.defendingPlayer);

                    if (!MainManager.autoResultsValid)
                    {
                        if (NetworkManager.Singleton.IsHost)
                        { OnlineManager.Instance.ReportManualChallengeGapsRpc(gapToDefender.value, gapToChallenger.value, gapStolenWin.value); }

                        if (stolenWin == false)
                        {
                            MainManager.raceWinner = MainManager.activePlayer;
                            Debug.Log("Race Winner Set : " + MainManager.raceWinner);
                            int gap;
                            gap = MainManager.manualReportingGapToDefender;
                            Debug.Log("Gap Set : " +  gap);
                            MainManager.timeBattleSeconds = (gap);
                            Debug.Log("Time Battle Seconds Set : " + MainManager.timeBattleSeconds);
                        }

                        else
                        {
                            MainManager.raceWinner = stealer;
                            int gap;
                            gap = MainManager.manualReportingGapStolenWin;
                            MainManager.timeBattleSeconds = (gap);
                        }
                    }
                }
                                
                else if (challengeLost == true)
                {
                    PlayerWinsCar(MainManager.defendingPlayer);
                    PlayerLosesCar(MainManager.activePlayer);

                    if (!MainManager.autoResultsValid)
                    {
                        if (NetworkManager.Singleton.IsHost)
                        { OnlineManager.Instance.ReportManualChallengeGapsRpc(gapToDefender.value, gapToChallenger.value, gapStolenWin.value); }

                        if (!stolenWin)
                        {
                            MainManager.raceWinner = MainManager.defendingPlayer;
                            int gap2;
                            gap2 = MainManager.manualReportingGapToChallenger;
                            MainManager.timeBattleSeconds = (gap2);
                        }

                        else
                        {
                            MainManager.raceWinner = stealer;
                            int gap;
                            gap = MainManager.manualReportingGapStolenWin;
                            MainManager.timeBattleSeconds = (gap);

                        }
                    }
                }

                raceResultsPanelL2.SetActive(false);

                if (NetworkManager.Singleton.IsHost)
                {
                    challengeOutcomePanel.SetActive(true);
                    OnlineManager.Instance.ReportChallengeAutoResultToClientsRpc(challengeWon, challengeLost, MainManager.raceWinner);

                    if (!MainManager.autoResultsValid)
                    {
                        Level2ChallengeScoring();
                    }
                }
                
                challengeCarDisplay.text = selectedCar;

                if (challengeWon == true)
                {
                    challengeWinnerDisplay.text = MainManager.playerNames[MainManager.activePlayer];
                    challengeDefeatedDisplay.text = MainManager.playerNames[MainManager.defendingPlayer];
                }

                else if (challengeLost == true)
                {
                    challengeWinnerDisplay.text = MainManager.playerNames[MainManager.defendingPlayer];
                    challengeDefeatedDisplay.text = MainManager.playerNames[MainManager.activePlayer];

                }

                protectionScript.CheckProtectionOptionAfterChallenge();

                if (activePlayerHasToynopoly && MainManager.shieldAvailable[MainManager.activePlayer] == true)
                {
                    OnActivePlayerCanProtectAfterChallenge?.Invoke();
                }

                break;
        }

        OnRaceConcluded?.Invoke();
    }

    public void SetChallengeOutcomeBools(bool won, bool lost)
    {
        challengeWon = won;
        challengeLost = lost;
    }


    public void ShowProtectionOptionAfterChallengePanel()
    {
        protectOptionPanel.SetActive(true);
        protectionOptionCarName.text = MainManager.cars[MainManager.currentCarIndex];                
    }

    public void HideProtectionOptionPanel()
    {
        protectOptionPanel.SetActive(false);
    }


    public void DisableProtectButton()
    {
        preProtectButton.gameObject.SetActive(false);
        audioSource.PlayOneShot(panelOpen);
    }

    public void SetStealerManualReporting(int stealerReported)
    {
        stealer = stealerReported;
    }

    public void RegisterSteal()
    {
        stealer = playersL2WinnerDropdown.value;

        if (NetworkManager.Singleton.IsHost)
            OnlineManager.Instance.ManualReportStealToClientsRpc(stealer);
    }


    public void OpenToynopolyTimeBattlePanel()
    {
        MainManager.autoResultsValid = false;

        raceResultsPanelL2T.SetActive(true);
        raceInProgressPanel.SetActive(false);

        toynopolyHolderName.text = MainManager.playerNames[MainManager.activePlayer];

        OnlineManager.Instance.ClientsSetToynopolyAutoResultsInvalidRpc();
        Debug.Log("Toynopoly Auto Results Set Invalid");
    }


    public void DisplayToynopolyTimeBattleGaps()
    {
        gapSecondsDisplayToLast.text = gapToLast.value.ToString();
        gapSecondsDisplayToFirst.text = gapToFirst.value.ToString();
    }


    public void ToynopolyTimeBattleResult()
    {
        float oldCarValue = MainManager.carPrizes[MainManager.currentCarIndex];


        if (!MainManager.autoResultsValid)
        {
            MainManager.changeValue = System.Convert.ToInt32(gapToLast.value) + System.Convert.ToInt32(-gapToFirst.value);                        
        }

        if (NetworkManager.Singleton.IsHost)
        {
            OnlineManager.Instance.ReportChangeValueToClientsRpc(MainManager.changeValue);
        }

        int ToynopolyTimeBattleSeconds = Mathf.Abs(Mathf.Clamp(MainManager.changeValue, -20, 20));
        Debug.Log("Time Battle Seconds value is " + ToynopolyTimeBattleSeconds);

        if (MainManager.changeValue <= 0)
        {
            MainManager.carPrizes[MainManager.currentCarIndex] -= ToynopolyTimeBattleSeconds;

            MainManager.IsToynopolyBattle = true;
            countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.currentCarIndex]);

            if (MainManager.carPrizes[MainManager.currentCarIndex] <= 0)

            {
                MainManager.carPrizes[MainManager.currentCarIndex] = 0;

                MainManager.carIsInDefault[MainManager.currentCarIndex] = true;

                CheckForDefaultCars();
            }

            //UpdateCarPrizesDisplay();

        }

        else

        {
            MainManager.carPrizes[MainManager.currentCarIndex] += ToynopolyTimeBattleSeconds;

            MainManager.IsToynopolyBattle = true;
            countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.currentCarIndex]);


            if (ToynopolyTimeBattleSeconds > 19)
            {
                audioSource.PlayOneShot(success);

            }

            //UpdateCarPrizesDisplay();

        }

        //SendSyncRequestToOtherClients
        //if (NetworkManager.Singleton.IsHost)
        //{ OnlineManager.Instance.ExecuteToynopolyTimeBattleResultsOnClientsRpc(); }

    }

    public void ToynopolyTimeBattleConclude()
    {
        fields[MainManager.pendingField].gameObject.SetActive(false);
        MainManager.fieldAvailable[MainManager.pendingField] = false;

        MainManager.fieldsLeftForCar[MainManager.currentCarIndex]--;

        if (NetworkManager.Singleton.IsHost)
        { OnlineManager.Instance.ExecuteToynopolyTimeBattleResultsOnClientsRpc(); }

        if (MainManager.roundCounter % MainManager.playerNumber == 0)
        {
            OnStartSellingRound?.Invoke();
            ReInstateRows();                        
        }

        else
        {
            StartCoroutine(WaitAfterCarFame());
        }

        raceResultsPanelL2T.SetActive(false);
        raceInProgressPanel.SetActive(false);

        //RoundChangeover();

    }

    public void GetChallengeResultWin(bool win)
    {
        if(NetworkManager.Singleton.IsHost)
        OnlineManager.Instance.ManualReportChallengeWinToServerRpc(win);

        challengeWon = win;
        Debug.Log(win);
    }

    public void GetChallengeResultLoss(bool loss)
    {
        if (NetworkManager.Singleton.IsHost)
            OnlineManager.Instance.ManualReportChallengeLossToServerRpc(loss);

        challengeLost = loss;
        Debug.Log(loss);
    }

    public void SetStolenWinBoolAfterManualReport(bool stolenWinBool)
    {
        stolenWin = stolenWinBool;
        Debug.Log("Stolen Win Set To " + stolenWinBool);
    }

    public void SetStolenWinBool(bool IsStolenWin)
    {
        if (IsStolenWin == true)
        {
            stolenWin = true;

            if (NetworkManager.Singleton.IsHost)
                OnlineManager.Instance.ManualReportStolenWinToServerRpc(true);

            playersL2WinnerDropdown.gameObject.SetActive(true);
            winnerGapSlider.SetActive(true);
            gapToDefender.gameObject.SetActive(false);
            gapToChallenger.gameObject.SetActive(false);
        }

        if (IsStolenWin == false)
        {
            stolenWin = false;

            if (NetworkManager.Singleton.IsHost)
                OnlineManager.Instance.ManualReportStolenWinToServerRpc(false);

            playersL2WinnerDropdown.gameObject.SetActive(false);
            winnerGapSlider.SetActive(false);
            gapToDefender.gameObject.SetActive(true);
            gapToChallenger.gameObject.SetActive(true);
        }
    }

    public void DisplaySecondsGap()
    {
        gapSecondsDisplaySteal.text = gapStolenWin.value.ToString();
        gapSecondsDisplayWin.text = gapToDefender.value.ToString();
        gapSecondsDisplayLoss.text = gapToChallenger.value.ToString();
    }

    public void Level2ChallengeScoring()
    {
        challengeOutcomePanel.SetActive(false);

        Debug.Log("Level 2 Race in Progress Network Variable set To False");
        OnlineManager.Instance.ReportRaceLevel2InProgressRpc(false);
    }

    public bool IsTimeBattleWinner()
    {
        ulong roundWinnerID = OnlineManager.Instance.GetPlayerID(MainManager.playerNames[MainManager.raceWinner]);

        Debug.Log("Round Winner ID is " + roundWinnerID);
        Debug.Log("Rount Winner Name is " + MainManager.playerNames[MainManager.raceWinner]);

        return NetworkManager.Singleton.LocalClientId == roundWinnerID;
    }

    private void CallTimeBattleWinnerDecision()
    {
        Debug.Log("Checking If Local Is Time Battle Winner - " + IsTimeBattleWinner());

        if (IsTimeBattleWinner())
        {
            timeBattlePanel.SetActive(true);
            TimeBattleOutcome();
        }

        IdleCountdown.Instance.StartIdleCountdownMax(60f);
    }

    void Level1Scoring()
    {
        switch (MainManager.playerNumber)

        {
            case 5:

                PlayerWinsCar(raceWinnerLevel1);
                PlayerWinsCar(runnerUpLevel1);

                if (MainManager.activePlayer != raceWinnerLevel1 && MainManager.activePlayer != runnerUpLevel1 && MainManager.activePlayer != thirdLevel1)

                {
                    MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
                    UpdateCashDisplay();
                }
                break;

            default:


                if (MainManager.activePlayerWins)
                {
                    PlayerWinsCar(MainManager.activePlayer);

                }

                else if (runnerUpLevel1 == MainManager.activePlayer)
                {
                    PlayerWinsCar(raceWinnerLevel1);
                    PlayerWinsCar(MainManager.activePlayer);

                    MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
                }

                else
                {
                    PlayerWinsCar(raceWinnerLevel1);
                    MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];


                }

                UpdateCashDisplay();

                break;
        }
    }

    public void TimeBattleOutcome()
    {
        int timeBattleWinner;

        timeBattleWinner = MainManager.raceWinner;
        {
            for (int i = 0; i < timeBattleNameDisplay.Length; i++)

            {
                timeBattleNameDisplay[i].text = MainManager.cars[i];
                timeBattlePrizeDisplay[i].text = MainManager.carPrizes[i].ToString();

                if (MainManager.carPrizes[i] < 1)

                    if (MainManager.playerNumber == 3 && MainManager.roundCounter > 3 || MainManager.playerNumber == 4 && MainManager.roundCounter > 4 || MainManager.playerNumber == 5 && MainManager.roundCounter > 5)
                    {
                        timeBattleButtons[i].gameObject.SetActive(false);
                    }
            }

        }

        timeBattleWinnerDisplay.text = MainManager.playerNames[timeBattleWinner];

        timeBattleSecondsDisplay.text = MainManager.timeBattleSeconds.ToString();
    }

    public void TimeBattleCarSelect(int whichCar)
    {
        MainManager.TimeBattleCarIndex = whichCar;
        buffNerfPanel.SetActive(true);
        audioSource.PlayOneShot(panelOpen);

        OnlineManager.Instance.ReportTimeBattleCarIndexToNetworkRpc(whichCar);
    }

    public void BuffCarAndContinue()
    {
        float oldCarValue = MainManager.carPrizes[MainManager.TimeBattleCarIndex];

        if (MainManager.timeBattleSeconds > 19)
        {
            audioSource.PlayOneShot(success);
        }

        MainManager.carPrizes[MainManager.TimeBattleCarIndex] += MainManager.timeBattleSeconds;

        countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.TimeBattleCarIndex]);

        if (MainManager.carPrizes[MainManager.TimeBattleCarIndex] < 0)

        {
            MainManager.carPrizes[MainManager.TimeBattleCarIndex] = 0;
        }
        UpdateCarPrizesDisplay();
        timeBattlePanel.SetActive(false);
        buffNerfPanel.SetActive(false);

        if (IsTimeBattleWinner())
        {            
            OnlineManager.Instance.ReportCarBuffToClientsRpc();
        }
                        
        if (MainManager.roundCounter % MainManager.playerNumber == 0)
        {
            OnStartSellingRound?.Invoke();
            ReInstateRows();
            
        }

        else

            StartCoroutine(WaitAfterCarFame());

        //RoundChangeover();

    }

    public void HideTimeBattleWindow()
    {
        timeBattlePanel.SetActive(false);
    }

    public void NerfCarAndContinue()
    {
        float oldCarValue = MainManager.carPrizes[MainManager.TimeBattleCarIndex];

        MainManager.carPrizes[MainManager.TimeBattleCarIndex] -= MainManager.timeBattleSeconds;

        countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.TimeBattleCarIndex]);

        if (MainManager.carPrizes[MainManager.TimeBattleCarIndex] <= 0)

        {
            MainManager.carPrizes[MainManager.TimeBattleCarIndex] = 0;
            CarHasDefaulted();
        }
        UpdateCarPrizesDisplay();
        timeBattlePanel.SetActive(false);
        buffNerfPanel.SetActive(false);

        if(IsTimeBattleWinner())
        OnlineManager.Instance.ReportCarNerfToClientsRpc();
                

        if (MainManager.roundCounter % MainManager.playerNumber == 0)

        {
            OnStartSellingRound?.Invoke();
            ReInstateRows();
            
        }

        else

            StartCoroutine(WaitAfterCarFame());
        //RoundChangeover();
    }


    public void UpdateCashDisplay()

    {
        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            cashDisplay[i].text = MainManager.playerCash[i].ToString();
        }

    }


    public void UpdateInventoryDisplay()

    {
        for (int i = 0; i < MainManager.cars.Length; i++)
        {
            invDisplayP1[i].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[0, i].ToString();
            invDisplayP2[i].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[1, i].ToString();
            invDisplayP3[i].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[2, i].ToString();

            if (MainManager.playerNumber > 3)
            {
                invDisplayP4[i].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[3, i].ToString();
            }

            if (MainManager.playerNumber > 4)

            {
                invDisplayP5[i].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[4, i].ToString();
            }


            if (MainManager.playerInventory[0, i] < 1)
            {
                invDisplayP1[i].gameObject.SetActive(false);
            }

            else invDisplayP1[i].gameObject.SetActive(true);

            if (MainManager.playerInventory[1, i] < 1)

            {
                invDisplayP2[i].gameObject.SetActive(false);
            }

            else invDisplayP2[i].gameObject.SetActive(true);

            if (MainManager.playerInventory[2, i] < 1)

            {
                invDisplayP3[i].gameObject.SetActive(false);
            }

            else invDisplayP3[i].gameObject.SetActive(true);

            if (MainManager.playerNumber > 3)

            {
                if (MainManager.playerInventory[3, i] < 1)

                {
                    invDisplayP4[i].gameObject.SetActive(false);
                }

                else invDisplayP4[i].gameObject.SetActive(true);
            }

            if (MainManager.playerNumber > 4)

            {
                if (MainManager.playerInventory[4, i] < 1)

                {
                    invDisplayP5[i].gameObject.SetActive(false);
                }

                else invDisplayP5[i].gameObject.SetActive(true);
            }
        }

    }

    public void UpdateCarPrizesDisplay()

    {

        for (int i = 0; i < MainManager.carPrizes.Length; i++)


        {
            carPrizeDisplays[i].text = MainManager.carPrizes[i].ToString();

        }

    }


    public void PlayerWinsCar(int winner)
    {
        MainManager.playerInventory[winner, MainManager.currentCarIndex]++;

        UpdateInventoryDisplay();

    }

    void PlayerLosesCar(int loser)

    {
        MainManager.playerInventory[loser, MainManager.currentCarIndex]--;
        UpdateInventoryDisplay();
    }

    private void PostRaceRandomMarketProcedure()
    {
        int oldCarValue = MainManager.carPrizes[MainManager.currentCarIndex];
        int randomValue = OnlineManager.Instance.GetRandomMarketDelta();
        int newCarValue = (MainManager.carPrizes[MainManager.currentCarIndex] + (carValueChangeOptions[randomValue]));
        MainManager.carPrizes[MainManager.currentCarIndex] = (MainManager.carPrizes[MainManager.currentCarIndex] + (carValueChangeOptions[randomValue]));

        if (MainManager.carPrizes[MainManager.currentCarIndex] <= 0)

        {
            MainManager.carPrizes[MainManager.currentCarIndex] = 0;

            lastChangebeforeDefault = carValueChangeOptions[randomValue];
            CarHasDefaulted();
        }


        postRaceMarketPanel.gameObject.SetActive(true);
        audioSource.PlayOneShot(coinFalling);

        currentCarNameMarketPanel.text = selectedCar;
        currentCarPrizeMarketPanel.text = newCarValue.ToString();
        carValueChangeDisplay.text = carValueChangeOptions[randomValue].ToString();

        if (carValueChangeOptions[randomValue] > 0)

        {
            priceUpArrow.SetActive(true);
            priceDownArrow.SetActive(false);
            valueChangeMessage.text = "The price of this car has gone up";

            countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.currentCarIndex]);

            if (carValueChangeOptions[randomValue] > 9)
            {
                audioSource.PlayOneShot(success);
            }
                                    
        }

        else if (carValueChangeOptions[randomValue] < 0)

        {
            priceUpArrow.SetActive(false);
            priceDownArrow.SetActive(true);
            valueChangeMessage.text = "The price of this car has gone down";

            countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.currentCarIndex]);

            
        }

        else

        {
            priceUpArrow.SetActive(false);
            priceDownArrow.SetActive(false);
            valueChangeMessage.text = "The price of this car remains unchanged";
                        
        }
               

    }


    void CarHasDefaulted()
    {
        carInDefaultPanel.SetActive(true);
        defaultDownArrow.SetActive(true);

        if (MainManager.levelCounter == 1)

        {
            MainManager.carIsInDefault[MainManager.currentCarIndex] = true;

            defaultCarValueChange.text = lastChangebeforeDefault.ToString();
            defaultCarName.text = MainManager.cars[MainManager.currentCarIndex];

            defaultPanelTextMessage.text = "This car has defaulted. If it is still in default after the first selling round, it will be eliminated from the game";

        }

        else

        {
            defaultCarName.text = MainManager.cars[MainManager.TimeBattleCarIndex];
            carPic[MainManager.TimeBattleCarIndex].image.sprite = carDefaultSprite;
            carNameButtons[MainManager.TimeBattleCarIndex].GetComponent<Image>().color = Color.grey;
            defaultPanelTextMessage.text = "This car is in default and is eliminated from the game";

            for (int i = 0; i < MainManager.playerNumber; i++)
            {
                MainManager.playerInventory[i, MainManager.TimeBattleCarIndex] = 0;
            }

            rows[MainManager.TimeBattleCarIndex].SetActive(false);
            MainManager.DefProcedureCompleted[MainManager.TimeBattleCarIndex] = true;
        }

        UpdateInventoryDisplay();
        UpdateCarPrizesDisplay();
    }


    public void CheckForDefaultCars()

    {
        for (int i = 0; i < MainManager.carIsInDefault.Length; i++)
        {
            if (MainManager.carIsInDefault[i] && MainManager.DefProcedureCompleted[i] == false && MainManager.carPrizes[i] < 1)
            {
                carInDefaultPanel.SetActive(true);
                defaultDownArrow.SetActive(true);
                defaultCarName.text = MainManager.cars[i];
                defaultPanelTextMessage.text = "This car is in default and is eliminated from the game";
                carPic[i].image.sprite = carDefaultSprite;
                carNameButtons[i].GetComponent<Image>().color = Color.grey;


                MainManager.playerInventory[0, i] = 0;
                MainManager.playerInventory[1, i] = 0;
                rows[i].SetActive(false);
                MainManager.DefProcedureCompleted[i] = true;
            }
        }

        UpdateInventoryDisplay();

    }


    public void ShowNextRoundButton()
    {
        SellDoneButton.gameObject.SetActive(true);
        carInDefaultPanel.SetActive(false);
    }

    public void CloseCarDefaultPanel()
    {
        carInDefaultPanel.gameObject.SetActive(false);
    }

    public void ReadyForRoundChangeover()
    {
        OnReadyForRoundChangeover?.Invoke();

        postRaceMarketPanel.SetActive(false);
    }

    public void RoundChangeover()
    {
        OnRoundChangeover?.Invoke();
        //Save();

        carInDefaultPanel.SetActive(false);        
        hostOptionsPanel.SetActive(false);
        pausePanel.SetActive(false);

        for (int i = 0; i < MainManager.playerNumber; i++)
        {
            playerSellButton[i].gameObject.SetActive(false);
        }

        SellDoneButton.gameObject.SetActive(false);

        MainManager.roundCounter++;
        l2SelectionIsOkay = true;
        //l3SelectionIsOkay = true;
        buyingPossible = true;
        playerHasBoughtCarThisRound = false;
        resultsRegisteredForRound = false;
        MainManager.IsToynopolyBattle = false;
        challengeWon = true;

        MainManager.activePlayer++;        

        if (MainManager.activePlayer > MainManager.playerNumber - 1)
        {
            MainManager.activePlayer = 0;
        }

        CallActivePlayer();

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            turnIndicator[i].SetActive(false);
        }

        turnIndicator[MainManager.activePlayer].SetActive(true);

        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {MainManager.raceThreshold - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");

        if (MainManager.levelCounter == 2)
        {
            if (MainManager.roundCounter < MainManager.raceThreshold)
            {
                dividendScript.DividendCheck();
                OutOfOptionsCheck();
                BankruptCheck();
            }
        }

        if (MainManager.levelCounter == 3)
        {
            //InventoryCheck();

            //toynopolyCalculatorScript.PerformToynopolyCalculations();
        }

        LevelCheck();

        int randomNumber = UnityEngine.Random.Range(1, 4);
        if (randomNumber == 3)
        {
            timerPanel.gameObject.SetActive(true);
            timerScript.DisplayToggle(true);

        }
    }


    void LevelCheck()
    {
        if (MainManager.roundCounter > MainManager.raceThreshold - 1)
        {
            if (MainManager.levelCounter == 1)
            {
                if (MainManager.roundCounter == MainManager.raceThreshold)
                {
                    level2StartPanel.gameObject.SetActive(true);
                }

                MainManager.levelCounter = 2;
                MainManager.roundCounter = 1;
            }

            else if (MainManager.levelCounter == 2)
            {
                Debug.Log("Round Counter " + MainManager.roundCounter);
                Debug.Log("Round Threshold " + MainManager.raceThreshold);

                if (MainManager.roundCounter == MainManager.raceThreshold)
                {                    
                    EndGame();
                }
            }
        }
    }


    public void StartLevel2()
    {
        ResetThresholdForLevel();

        level2StartPanel.SetActive(false);
        OnLevel2Start?.Invoke();

        MainManager.activePlayer = GetIndexOfLowestValue(MainManager.playerCash);
        CallActivePlayer();

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            turnIndicator[i].SetActive(false);
        }

        turnIndicator[MainManager.activePlayer].SetActive(true);

        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {MainManager.raceThreshold - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");

    }

    private void ResetThresholdForLevel()
    {        
      Dictionary<int, int> thresholdForPlayers = new Dictionary<int, int>
      {
        {3, 13},
        {4, 13},
        {5, 11},
       };

        MainManager.raceThreshold = thresholdForPlayers[MainManager.playerNumber];
        Debug.Log("Threshold Reset to " + thresholdForPlayers[MainManager.playerNumber]);
    }

    public int GetIndexOfLowestValue(int[] arr)
    {
        float value = float.PositiveInfinity;
        int index = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < value)
            {
                index = i;
                value = arr[i];
            }
        }
        return index;
    }

    public void ShowPreSellingPanel()
    {
        preSellingInfoPanel.SetActive(true);
        audioSource.PlayOneShot(panelOpen);
    }

    public void HidePreSellingPanel()
    {
        preSellingInfoPanel.SetActive(false);
    }


    public void StartSellingRound()

    {
        preSellingInfoPanel.SetActive(false);

        SellingHandlerP3.Instance.OpenSellingDialoguePanel();
    }

    public void AcceptDividend()
    {

        for (int i = 0; i < cashDisplay.Length; i++)

        {
            cashDisplay[i].text = MainManager.playerCash[i].ToString();
        }


    }

    void OutOfOptionsCheck()

    {
        for (int i = 0; i < MainManager.cars.Length; i++)

        {
            MainManager.playerOutofOptions[i] = false;


            if (MainManager.playerInventory[MainManager.activePlayer, i] < 1 || MainManager.fieldsLeftForCar[i] < 1)

            {
                int numberOfOwners = 0;

                for (int j = 0; j < MainManager.inactivePlayers.Length; j++)

                {

                    if (MainManager.playerInventory[MainManager.inactivePlayers[j], i] > 0)
                    { numberOfOwners++; }

                }


                if (numberOfOwners != 1 && MainManager.fieldsLeftForCar[i] > 0)
                { MainManager.playerOutofOptions[i] = false; }

                else

                    MainManager.playerOutofOptions[i] = true;
            }


        }


        if (!MainManager.playerOutofOptions.Contains(false))

        { RoundChangeover(); }


    }


    void BankruptCheck()

    {
        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            if (MainManager.playerCash[i] < 1)

            { RoundChangeover(); }

        }

    }



    public void EndGame()

    {
        MainManager.gameOver = true;
        Debug.Log("Game Over");

        gameOverPanel.SetActive(true);
        audioSource.PlayOneShot(heartbeat);

        List<float> cashRanking = new List<float>
        { };

        List<string> playerRanking = new List<string>
        { };


        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            cashRanking.Add(MainManager.playerCash[i]);
            cashRanking.Sort();
            cashRanking.Reverse();

        }

        for (int i = 0; i < MainManager.playerNumber; i++)
        {
            for (int y = 0; y < MainManager.playerNumber; y++)
            {
                if (cashRanking[i] == MainManager.playerCash[y])
                {
                    playerRanking.Add(MainManager.playerNames[y]);
                }
            }

        }


        resultsP1Name.text = playerRanking[0];
        resultsP2Name.text = playerRanking[1];
        resultsP3Name.text = playerRanking[2];
        resultsP1cashTotal.text = cashRanking[0].ToString();
        resultsP2cashTotal.text = cashRanking[1].ToString();
        resultsP3cashTotal.text = cashRanking[2].ToString();


        if (MainManager.playerNumber > 3)

        {
            resultsP4Name.text = playerRanking[3];
            resultsP4cashTotal.text = cashRanking[3].ToString();
        }

        if (MainManager.playerNumber > 4)

        {
            resultsP5Name.text = playerRanking[4];
            resultsP5cashTotal.text = cashRanking[4].ToString();
        }

        for (int i = 0; i < finale.Length; i++)
        {
            finale[i].Play();
        }


    }

    public void BackToMainMenu()

    {
        SceneManager.LoadScene(0);
    }


    private void Save()
    {
        int[] concatenatedInventory = new int[36];
        int x = 0;
        int y = 0;
        int z = 0;
        int a = 0;
        int b = 0;

        for (int i = 0; i < concatenatedInventory.Length; i++)

        {
            if (i < 6)
            { concatenatedInventory[i] = MainManager.playerInventory[0, i]; }

            else if (i < 12)
            {
                concatenatedInventory[i] = MainManager.playerInventory[1, x];
                x++;
            }

            else if (i < 18)
            {
                concatenatedInventory[i] = MainManager.playerInventory[2, y];
                y++;
            }

            else if (i < 24)
            {
                concatenatedInventory[i] = MainManager.playerInventory[3, z];
                z++;
            }

            else if (i < 30)

            {
                concatenatedInventory[i] = MainManager.playerInventory[4, a];
                a++;
            }

            else
            {
                concatenatedInventory[i] = MainManager.playerInventory[5, b];
                a++;
            }


        }




        SaveGameData playerData = new SaveGameData
        {
            playerNumber = MainManager.playerNumber,
            playerNames = MainManager.playerNames,
            playerCash = MainManager.playerCash,

            playerInventory = concatenatedInventory,
            classSelected = MainManager.classSelected,
            cars = MainManager.cars,
            carPrizes = MainManager.carPrizes,
            fieldsLeftForCar = MainManager.fieldsLeftForCar,
            fieldAvailable = MainManager.fieldAvailable,
            activeTracks = MainManager.activeTracks,
            bonusTrack = MainManager.bonusTrack,
            activePlayer = MainManager.activePlayer,
            level = MainManager.levelCounter,
            round = MainManager.roundCounter,

            matchlength = MainManager.raceThreshold,
            shields = MainManager.shieldAvailable,
            protection = MainManager.protection,
            
            //Resume N/A in Netcode Version
            //tempdividends = dividendScript.actualDividendList,


        };

        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        SaveSystem.Save(json);

        SaveGameData loadedPlayerData = JsonUtility.FromJson<SaveGameData>(json);


    }

    public void BackToMenu()
    {
        pausePanel.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void CloseHostOptionsPanel()
    {
        hostOptionsPanel.gameObject.SetActive(false);
    }

    public void OpenHostOptionsPanel()
    {
        hostOptionsPanel.gameObject.SetActive(true);
    }

    public void SetRaceStartButtonActive()
    {
        startRaceButton.SetActive(true);
    }

    public void SetRaceStartButtonInactive()
    { startRaceButton.SetActive(false); }




    public void QuitGame()
    {
        Application.Quit();
    }

    System.Collections.IEnumerator WaitAfterCarFame()
    {
        yield return new WaitForSeconds(10.0f);
        ReInstateRows();
        ReadyForRoundChangeover();
    }

    public void ReInstateRows()
    {
        for (int i = 0; i < rows.Length; i++)
        {

            if (!MainManager.DefProcedureCompleted[i] || (MainManager.levelCounter > 1 && MainManager.carPrizes[i] > 1))
            {
                rows[i].gameObject.SetActive(true);
            }
        }
    }

    

    public class SaveGameData
    {
        public int playerNumber;
        public string[] playerNames;
        public int[] playerCash;

        public int[] playerInventory;
        public int classSelected;
        public string[] cars;
        public int[] carPrizes;
        public int[] fieldsLeftForCar;
        public bool[] fieldAvailable;

        public string[] activeTracks;
        public string bonusTrack;

        public int activePlayer;
        public int level;
        public int round;

        public int matchlength;
        public bool[] shields;
        public bool[] protection;
        public List<int> tempdividends;


    }


}





