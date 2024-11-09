using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerManager3P : MonoBehaviour
{
    public Button[] fields;
    public Button[] playerSellButton;

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
    private bool activePlayerHasToynopoly = false;

    private bool[] wantsToBuy = { false, false, false, false, false };
    private bool stolenWin = false;

    private bool challengeWon = true;
    private bool challengeLost = false;

    private int[] carValueChangeOptions = { -10, -7, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 7, 10 };

    //private List<int> roundsWithCarSellingOption = new List<int> { 3, 6, 9, 12 };

    public int lastChangebeforeDefault = 0;

    public AudioSource audioSource;
    public AudioClip panelOpen;
    public AudioClip stageReady;
    public AudioClip coinFalling;
    public AudioClip heartbeat;

    public TextMeshProUGUI[] cashDisplay;

    [SerializeField] Button[] invDisplayP1;
    [SerializeField] Button[] invDisplayP2;
    [SerializeField] Button[] invDisplayP3;
    [SerializeField] Button[] invDisplayP4;
    [SerializeField] Button[] invDisplayP5;

    [SerializeField] Button buyCarButton;

    public Button[] carPic;

    public TextMeshProUGUI[] carPrizeDisplays;


    public TextMeshProUGUI statusInfoTextBar;

    public string selectedTrack;
    public string selectedCar;

    [SerializeField] GameObject[] turnIndicator;

    [SerializeField] GameObject nextRaceComingUpPanel;
    [SerializeField] GameObject raceInProgressPanel;
    [SerializeField] GameObject raceInProgressPanelChallenge;
    [SerializeField] GameObject raceResultsPanelL1;
    [SerializeField] GameObject postRaceMarketPanel;

    [SerializeField] GameObject priceUpArrow;
    [SerializeField] GameObject priceDownArrow;

    [SerializeField] GameObject level2StartPanel;
    [SerializeField] GameObject startRaceButton;

    [SerializeField] GameObject continueToChallengeButton;

    [SerializeField] GameObject buyOptionPanel;

    [SerializeField] GameObject challengePanel;

    [SerializeField] GameObject continueButtonToynopoly;
    [SerializeField] GameObject continueButtonNormal;


    [SerializeField] GameObject raceResultsPanelL2;

    [SerializeField] GameObject challengeOutcomePanel;

    [SerializeField] GameObject timeBattlePanel;

    [SerializeField] GameObject buffNerfPanel;

    [SerializeField] GameObject preSellingInfoPanel;

    [SerializeField] GameObject raceResultsPanelL2T;

    [SerializeField] GameObject gameOverPanel;

    [SerializeField] GameObject pausePanel;

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

    public List<Toggle> challengeToggles = new();




    private int raceWinnerLevel1 = 0;
    private int runnerUpLevel1 = 0;
    private int thirdLevel1 = 0;

    private DividendGenerator dividendScript;



    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
                     
            statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {MainManager.raceThreshold - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");
        

        toggleGroup = GetComponent<ToggleGroup>();
        dividendScript = GameObject.Find("DividendGenerator").GetComponent<DividendGenerator>();

        if (MainManager.gameResumed)
        {
            for (int i = 0; i < MainManager.cars.Length; i++)

            {
                invDisplayP1[i].gameObject.SetActive(true);
                invDisplayP2[i].gameObject.SetActive(true);
                invDisplayP3[i].gameObject.SetActive(true);

                if (MainManager.playerNumber > 3)
                { invDisplayP4[i].gameObject.SetActive(true); }

                if (MainManager.playerNumber > 4)
                { invDisplayP5[i].gameObject.SetActive(true); }
            }

            if (MainManager.playerNumber == 5)
            {
                MainManager.raceThreshold = 11;
            }

            else

                MainManager.raceThreshold = 13;
            
            
            UpdateInventoryDisplay();
            UpdateCarPrizesDisplay();
            RoundChangeover();
            UpdateCashDisplay();
        }

        else
        {
            audioSource.PlayOneShot(heartbeat);

            for (int i = 0; i < MainManager.fieldAvailable.Length; i++)

            {
                MainManager.fieldAvailable[i] = true;
            }
        }


        SaveSystem.Init();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.gameObject.SetActive(true);
        }
    }

    public void Resume()

    {
        pausePanel.gameObject.SetActive(false);
    }

    public void FieldClicked(int fieldNumber)

    {

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

        helpText.gameObject.SetActive(false);

        ShowNextRacePanel();

    }

    void ShowNextRacePanel()
    {
        nextRaceComingUpPanel.SetActive(true);
        audioSource.PlayOneShot(panelOpen);

        nextTrackDisplay.text = selectedTrack;
        nextCarDisplay.text = selectedCar;

        switch (MainManager.levelCounter)

        {
            case 1:
                activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
                break;

            case 2:

                buyCarButton.gameObject.SetActive(true);
                activePlayerHasToynopoly = false;

                FillInactivePlayersArray();
                PerformLevel2Check();

                switch (activePlayerHasToynopoly)
                {
                    case true:
                        startRaceButton.SetActive(true);
                        continueToChallengeButton.SetActive(false);
                        break;

                    case false:
                        startRaceButton.SetActive(false);
                        continueToChallengeButton.SetActive(true);
                        break;
                }


                if (l2SelectionIsOkay == false)
                {

                    if (buyingPossible == true && playerHasBoughtCarThisRound == false)

                    {
                        activePlayerMessage.text = ("You don't own this car. Would you like to buy it?");
                        startRaceButton.SetActive(false);
                        continueToChallengeButton.SetActive(false);
                    }

                    if (buyingPossible == true && playerHasBoughtCarThisRound == true)

                    {
                        activePlayerMessage.text = ("You don't own this car.");
                        buyCarButton.gameObject.SetActive(false);
                        continueToChallengeButton.SetActive(false);
                        startRaceButton.SetActive(false);
                    }

                    else if (buyingPossible == false)

                    {
                        activePlayerMessage.text = ("An opponent has a Toynopoly for this car. Please choose a different car");
                        continueToChallengeButton.SetActive(false);
                        startRaceButton.SetActive(false);
                        buyCarButton.gameObject.SetActive(false);

                    }

                }

                else if (activePlayerHasToynopoly)

                {
                    activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
                    buyCarButton.gameObject.SetActive(false);
                    continueToChallengeButton.SetActive(false);
                    startRaceButton.SetActive(true);
                }

                else

                {
                    activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
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


        nextRaceComingUpPanel.SetActive(false);
        challengePanel.SetActive(true);
        challengefirstInactive.text = MainManager.playerNames[MainManager.inactivePlayers[0]];
        challengeSecondInactive.text = MainManager.playerNames[MainManager.inactivePlayers[1]];
        challengeThirdInactive.text = MainManager.playerNames[MainManager.inactivePlayers[2]];
        challengeFourthInactive.text = MainManager.playerNames[MainManager.inactivePlayers[3]];
                
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
        challengePanel.SetActive(false);

        MainManager.defendingPlayer = MainManager.inactivePlayers[defender];

        raceInProgressPanelChallenge.SetActive(true);
        challengeRaceProgressCar.GetComponentInChildren<TMP_Text>().text = selectedCar;
        challengeRaceProgressTrack.GetComponentInChildren<TMP_Text>().text = selectedTrack;
        challengeProgressTextInfo.text = ("Level " + MainManager.levelCounter + ", Race " + MainManager.roundCounter + " / 12 in progress");

       challengerNameL2.text = MainManager.playerNames[MainManager.activePlayer];
       defenderNameL2.text = MainManager.playerNames[MainManager.defendingPlayer];

    }


    public void BuyCar()

    {
        MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
        PlayerWinsCar(MainManager.activePlayer);

        UpdateCashDisplay();
        UpdateInventoryDisplay();

        nextRaceComingUpPanel.SetActive(false);
        l2SelectionIsOkay = true;
        buyingPossible = true;
        playerHasBoughtCarThisRound = true;

        FillInactivePlayersArray();
        Debug.Log($"Inactive players are {MainManager.inactivePlayers[0]} + {MainManager.inactivePlayers[1]} + {MainManager.inactivePlayers[2]} + {MainManager.inactivePlayers[3]}");
        PerformLevel2Check();

        if (activePlayerHasToynopoly == true)

        {
            OfferBuyOption();

        }

    }

    void OfferBuyOption()

    {
        buyOptionPanel.SetActive(true);

        firstinactivePlayerName.text = MainManager.playerNames[MainManager.inactivePlayers[0]];
        secondinactivePlayerName.text = MainManager.playerNames[MainManager.inactivePlayers[1]];

        if (MainManager.playerNumber > 3)
        {
            thirdinactivePlayerName.text = MainManager.playerNames[MainManager.inactivePlayers[2]];
        }

        if (MainManager.playerNumber > 4)

        {
            fourthinactivePlayerName.text = MainManager.playerNames[MainManager.inactivePlayers[3]];
        }


    }


    public void AcceptBuyOption(int whichPlayer)

    {
        if (wantsToBuy[whichPlayer] == false)

        { wantsToBuy[whichPlayer] = true; }

        else

        { wantsToBuy[whichPlayer] = false; }


    }


    public void ConcludeBuyOption()

    {
        for (int i = 0; i < MainManager.playerNumber - 1; i++)

        {
            if (wantsToBuy[i] == true)

            {
                PlayerWinsCar(MainManager.inactivePlayers[i]);
                MainManager.playerCash[MainManager.inactivePlayers[i]] -= MainManager.carPrizes[MainManager.currentCarIndex];

            }

        }

        UpdateInventoryDisplay();
        UpdateCashDisplay();

        buyOptionPanel.SetActive(false);

    }

    void FillInactivePlayersArray()

    { int InactivePlayersArrayIndex = 0;

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            if (i != MainManager.activePlayer)

            { MainManager.inactivePlayers[InactivePlayersArrayIndex] = i;
                InactivePlayersArrayIndex++;
            }
        }
    }


    void PerformLevel2Check()
    {
        int numberOfOwners = 0;

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            if (MainManager.playerInventory[i, MainManager.currentCarIndex] > 0)
            {
                numberOfOwners++;
            }

        }

        if (numberOfOwners == 1)

        { buyingPossible = false; }

        if (MainManager.playerInventory[MainManager.activePlayer, MainManager.currentCarIndex] > 0 && numberOfOwners < 2)

        { activePlayerHasToynopoly = true; }

        else if (MainManager.playerInventory[MainManager.activePlayer, MainManager.currentCarIndex] < 1)

        { l2SelectionIsOkay = false; }

    }

    public void CancelRace()

    {
        nextRaceComingUpPanel.SetActive(false);
        l2SelectionIsOkay = true;
        buyingPossible = true;

    }

    public void StartRace()

    {
        nextRaceComingUpPanel.SetActive(false);
        

        if (MainManager.levelCounter == 1)

        {
            raceInProgressPanel.SetActive(true);
            continueButtonNormal.SetActive(true);
            continueButtonToynopoly.SetActive(false);
            audioSource.PlayOneShot(stageReady);

            if (MainManager.playerNumber == 5)
            {
                currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} / 10 in progress");
            }

            else

            {
                currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} / 12 in progress");
            }

            currentRaceInfoTrack.text = selectedTrack;
            currentRaceInfoCar.text = selectedCar;
            currentRaceOpponent1.text = MainManager.playerNames[MainManager.activePlayer];
        }

        
        else if (activePlayerHasToynopoly)
            
            {
                raceInProgressPanel.SetActive(true);
                continueButtonNormal.SetActive(false);
                continueButtonToynopoly.SetActive(true);
                audioSource.PlayOneShot(stageReady);
                currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} in progress");
                currentRaceInfoTrack.text = selectedTrack;
                currentRaceInfoCar.text = selectedCar;
                currentRaceOpponent1.text = MainManager.playerNames[MainManager.activePlayer];


            }
                   

    }

    public void ShowResultsPanel()

    {

        raceInProgressPanel.SetActive(false);
        raceInProgressPanelChallenge.SetActive(false);

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
        
        fields[MainManager.pendingField].gameObject.SetActive(false);
        MainManager.fieldAvailable[MainManager.pendingField] = false;

        MainManager.fieldsLeftForCar[MainManager.currentCarIndex]--;



        switch (MainManager.levelCounter)

        {
            case 1:

                Debug.Log(winnerDropdown.value);

                if ((winnerDropdown.value) == (MainManager.activePlayer))

                {
                    MainManager.activePlayerWins = true;
                }

                else

                {
                    MainManager.activePlayerWins = false;
                }

                raceResultsPanelL1.SetActive(false);
                raceWinnerLevel1 = winnerDropdown.value;
                runnerUpLevel1 = runnerUpDropdown.value;

                if (MainManager.playerNumber > 4)
                {
                    thirdLevel1 = thirdPlaceDropdown.value;
                }
                                
                Level1Scoring();
                PostRaceRandomMarketProcedure();
                break;

            case 2:

                raceResultsPanelL2.SetActive(true);
                

                if (challengeWon == true)

                {
                    PlayerWinsCar(MainManager.activePlayer);
                    PlayerLosesCar(MainManager.defendingPlayer);

                    if (stolenWin == false)
                    {
                        MainManager.raceWinner = MainManager.activePlayer;
                        int gap;
                        gap = System.Convert.ToInt32(gapToDefender.value);
                        MainManager.timeBattleSeconds = (gap);
                    }

                    else

                    {
                        MainManager.raceWinner = MainManager.inactivePlayers[playersL2WinnerDropdown.value];
                        int gap;
                        gap = System.Convert.ToInt32(gapStolenWin.value);
                        MainManager.timeBattleSeconds = (gap);
                    }


                }

                else if (challengeLost == true)


                {
                    PlayerWinsCar(MainManager.defendingPlayer);
                    PlayerLosesCar(MainManager.activePlayer);


                    if (stolenWin == false)
                    {
                        MainManager.raceWinner = MainManager.defendingPlayer;
                        int gap2;
                        gap2 = System.Convert.ToInt32(gapToChallenger.value);
                        MainManager.timeBattleSeconds = (gap2);
                    }

                    else

                    {
                        MainManager.raceWinner = MainManager.inactivePlayers[playersL2WinnerDropdown.value];
                        int gap;
                        gap = System.Convert.ToInt32(gapStolenWin.value);
                        MainManager.timeBattleSeconds = (gap);

                    }

                }

                raceResultsPanelL2.SetActive(false);
                challengeOutcomePanel.SetActive(true);
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


                break;

        }

    }

    public void OpenToynopolyTimeBattlePanel()
    {
        raceResultsPanelL2T.SetActive(true);
        raceInProgressPanel.SetActive(false);

        toynopolyHolderName.text = MainManager.playerNames[MainManager.activePlayer];


    }


    public void DisplayToynopolyTimeBattleGaps()

    {
        gapSecondsDisplayToLast.text = gapToLast.value.ToString();
        gapSecondsDisplayToFirst.text = gapToFirst.value.ToString();
    }



    public void ToynopolyTimeBattleResult()
    {
        int changeValue;
        

        changeValue = System.Convert.ToInt32(gapToLast.value) + System.Convert.ToInt32(-gapToFirst.value);
        int ToynopolyTimeBattleSeconds = Mathf.Abs(changeValue);

        if (gapToLast.value < gapToFirst.value)
        {
            MainManager.carPrizes[MainManager.currentCarIndex] -= ToynopolyTimeBattleSeconds;
            
            if (MainManager.carPrizes[MainManager.currentCarIndex] <= 0)

            {
                MainManager.carPrizes[MainManager.currentCarIndex] = 0;
                CarHasDefaulted();
            }
            
            
            UpdateCarPrizesDisplay();
            
                                   
        }

        else

        {
            MainManager.carPrizes[MainManager.currentCarIndex] += ToynopolyTimeBattleSeconds;
            
            UpdateCarPrizesDisplay();
            
        }
    }

    public void ToynopolyTimeBattleConclude()
    {


        /*if (roundsWithCarSellingOption.Contains(MainManager.roundCounter))

        {
            preSellingInfoPanel.SetActive(true);
            audioSource.PlayOneShot(panelOpen);
        }
        */

        if (MainManager.roundCounter % MainManager.playerNumber == 0)

        {
            preSellingInfoPanel.SetActive(true);
            audioSource.PlayOneShot(panelOpen);
        }

        else                  

            RoundChangeover();

        raceResultsPanelL2T.SetActive(false);

    }


    


    public void GetChallengeResultWin(bool win)

    {
        challengeWon = win;
        Debug.Log(win);


    }

    public void GetChallengeResultLoss(bool loss)

    {
        challengeLost = loss;
        Debug.Log(loss);


    }


    public void SetStolenWinBool()

    {
        if (stolenWin == false)

        {
            stolenWin = true;
            playersL2WinnerDropdown.gameObject.SetActive(true);
            winnerGapSlider.SetActive(true);
            gapToDefender.gameObject.SetActive(false);
            gapToChallenger.gameObject.SetActive(false);

        }


        else

        {
            stolenWin = false;
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
        timeBattlePanel.SetActive(true);
        TimeBattleOutcome();
    }


    void Level1Scoring()

    { switch (MainManager.playerNumber)

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

    }


    public void BuffCarAndContinue()

    {
        MainManager.carPrizes[MainManager.TimeBattleCarIndex] += MainManager.timeBattleSeconds;
        if (MainManager.carPrizes[MainManager.TimeBattleCarIndex] < 0)

        {
            MainManager.carPrizes[MainManager.TimeBattleCarIndex] = 0;
        }
        UpdateCarPrizesDisplay();
        timeBattlePanel.SetActive(false);
        buffNerfPanel.SetActive(false);

        /*if (roundsWithCarSellingOption.Contains(MainManager.roundCounter))

        {
            preSellingInfoPanel.SetActive(true);
            audioSource.PlayOneShot(panelOpen);
        }

        else
        */

        if (MainManager.roundCounter % MainManager.playerNumber == 0)

        {
            preSellingInfoPanel.SetActive(true);
            audioSource.PlayOneShot(panelOpen);
        }

        else

            RoundChangeover();

    }

    public void NerfCarAndContinue()
    {
        MainManager.carPrizes[MainManager.TimeBattleCarIndex] -= MainManager.timeBattleSeconds;
        if (MainManager.carPrizes[MainManager.TimeBattleCarIndex] <= 0)

        {
            MainManager.carPrizes[MainManager.TimeBattleCarIndex] = 0;
            CarHasDefaulted();
        }
        UpdateCarPrizesDisplay();
        timeBattlePanel.SetActive(false);
        buffNerfPanel.SetActive(false);

        /*if (roundsWithCarSellingOption.Contains(MainManager.roundCounter))

        {
            preSellingInfoPanel.SetActive(true);
            audioSource.PlayOneShot(panelOpen);
        }

        else
        */

        if (MainManager.roundCounter % MainManager.playerNumber == 0)

        {
            preSellingInfoPanel.SetActive(true);
            audioSource.PlayOneShot(panelOpen);
        }

        else

            RoundChangeover();
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

    void UpdateCarPrizesDisplay()

    {

        for (int i = 0; i < MainManager.carPrizes.Length; i++)


        {
            carPrizeDisplays[i].text = MainManager.carPrizes[i].ToString();

        }

    }


    void PlayerWinsCar(int winner)
    {
        MainManager.playerInventory[winner, MainManager.currentCarIndex]++;

        UpdateInventoryDisplay();

    }

    void PlayerLosesCar(int loser)

    {
        MainManager.playerInventory[loser, MainManager.currentCarIndex]--;
        UpdateInventoryDisplay();
    }

    void PostRaceRandomMarketProcedure()

    {
        int oldCarValue = MainManager.carPrizes[MainManager.currentCarIndex];
        int randomValue = Random.Range(0, 15);
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
        currentCarPrizeMarketPanel.text = oldCarValue.ToString();
        carValueChangeDisplay.text = carValueChangeOptions[randomValue].ToString();

        if (carValueChangeOptions[randomValue] > 0)

        {
            priceUpArrow.SetActive(true);
            priceDownArrow.SetActive(false);
            valueChangeMessage.text = "The price of this car has gone up";

            UpdateCarPrizesDisplay();
        }

        else if (carValueChangeOptions[randomValue] < 0)

        {
            priceUpArrow.SetActive(false);
            priceDownArrow.SetActive(true);
            valueChangeMessage.text = "The price of this car has gone down";

            UpdateCarPrizesDisplay();

        }

        else

        {
            priceUpArrow.SetActive(false);
            priceDownArrow.SetActive(false);
            valueChangeMessage.text = "The price of this car remains unchanged";

            UpdateCarPrizesDisplay();

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
            if (MainManager.carIsInDefault[i] && MainManager.DefProcedureCompleted[i] == false)

            {
                carInDefaultPanel.SetActive(true);
                defaultDownArrow.SetActive(true);
                defaultCarName.text = MainManager.cars[i];
                defaultPanelTextMessage.text = "This car is in default and is eliminated from the game";
                carPic[i].image.sprite = carDefaultSprite;

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

    public void RoundChangeover()
    {

        Save();

        carInDefaultPanel.SetActive(false);
        postRaceMarketPanel.SetActive(false);

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

        MainManager.activePlayer++;

        if (MainManager.activePlayer > MainManager.playerNumber - 1)

        {
            MainManager.activePlayer = 0;
        }

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
                if (MainManager.roundCounter == MainManager.raceThreshold)

                {
                    EndGame();
                }

            }
        }



    }


    public void StartLevel2()

    {
        level2StartPanel.SetActive(false);
        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {MainManager.raceThreshold - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");

    }


    public void StartSellingRound()

    {
        preSellingInfoPanel.SetActive(false);

        for (int i = 0; i < MainManager.playerNumber; i++)

        { playerSellButton[i].gameObject.SetActive(true); }

        
        SellDoneButton.gameObject.SetActive(true);

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

                for (int j=0; j < MainManager.inactivePlayers.Length; j++)

                {                   

                    if (MainManager.playerInventory[MainManager.inactivePlayers[j], i] > 0)
                    { numberOfOwners++; }

                }


                if (numberOfOwners > 1 && MainManager.fieldsLeftForCar[i] > 0)
                { MainManager.playerOutofOptions[i] = false; }

                else 

                MainManager.playerOutofOptions[i] = true;
            }


        }


        if (MainManager.playerOutofOptions.Contains(false) == false)

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

        resultsP1Name.text = MainManager.playerNames[0];
        resultsP2Name.text = MainManager.playerNames[1];
        resultsP3Name.text = MainManager.playerNames[2];
        resultsP1cashTotal.text = MainManager.playerCash[0].ToString();
        resultsP2cashTotal.text = MainManager.playerCash[1].ToString();
        resultsP3cashTotal.text = MainManager.playerCash[2].ToString();
        

        if (MainManager.playerNumber > 3)

        {
            resultsP4Name.text = MainManager.playerNames[3];
            resultsP4cashTotal.text = MainManager.playerCash[3].ToString();
        }

        if (MainManager.playerNumber > 4)

        {
            resultsP5Name.text = MainManager.playerNames[4];
            resultsP5cashTotal.text = MainManager.playerCash[4].ToString();
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
            round = MainManager.roundCounter

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

    public void QuitGame()

    {
        Application.Quit();
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


    }


}





