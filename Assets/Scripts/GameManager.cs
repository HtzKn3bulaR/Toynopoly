using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{

    

    public AudioClip panelOpen;
    public AudioClip stageReady;
    public AudioClip coinFalling;
    public AudioClip jewelHit;
    public AudioClip transition;
    public AudioClip heartbeat;
    public AudioClip newGame;
    public AudioClip success;
    public AudioSource audioSource;

    [SerializeField] ParticleSystem[] finaleStars;

    public GameObject[] rows;

    [SerializeField] GameObject[] carNameButtons;

    public Button[] fields;

    public Button[] p1InventoryDisplay;
    public Button[] p2InventoryDisplay;

    private string selectedTrack;
    private string selectedCar;

    private bool l2SelectionIsOkay = true;
    private bool l3SelectionIsOkay = true;
    private bool buyingPossible = true;
    private bool playerHasBoughtCarThisRound = false;
    private bool activePlayerhasToynopoly = false;
    

    [SerializeField] GameObject nextRaceComingUpPanel;
    [SerializeField] GameObject raceInProgressPanel;
    [SerializeField] GameObject raceResultsPanel;
    [SerializeField] GameObject postRaceMarketPanel;
    [SerializeField] GameObject priceUpArrow;
    [SerializeField] GameObject priceDownArrow;
    [SerializeField] GameObject defautlDownArrow;
    [SerializeField] GameObject level2StartPanel;
    [SerializeField] GameObject timeBattlePanel;
    [SerializeField] GameObject buffNerfPanel;
    [SerializeField] GameObject preSellingInfoPanel;
    [SerializeField] GameObject sellingDialoguePanel;
    [SerializeField] GameObject level3StartPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject carInDefaultPanel;
    [SerializeField] GameObject buyOptionPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject timerPanel;
    [SerializeField] GameObject protectionEnablePanel;
    

    [SerializeField] GameObject sliderDefeat;
    [SerializeField] GameObject sliderWin;

    [SerializeField] Slider defeatGap;
    [SerializeField] Slider winGap;

    [SerializeField] TextMeshProUGUI activePlayerMessage;
    [SerializeField] TextMeshProUGUI nextTrackDisplay;
    [SerializeField] TextMeshProUGUI nextCarDisplay;
    [SerializeField] TextMeshProUGUI resultPanelActivePlayer;
    [SerializeField] TextMeshProUGUI valueChangeMessage;
    [SerializeField] TextMeshProUGUI defaultPanelTextMessage;

    [SerializeField] TextMeshProUGUI timeBattleWinnerDisplay;
    [SerializeField] TextMeshProUGUI timeBattleSecondsDisplay;



    [SerializeField] TextMeshProUGUI priceCarA;
    [SerializeField] TextMeshProUGUI priceCarB;
    [SerializeField] TextMeshProUGUI priceCarC;
    [SerializeField] TextMeshProUGUI priceCarD;
    [SerializeField] TextMeshProUGUI priceCarE;
    [SerializeField] TextMeshProUGUI priceCarF;

    public TextMeshProUGUI[] carPrizeDisplays;

    public Button[] carPic;
    [SerializeField] Sprite carDefaultSprite;

    public TextMeshProUGUI cashP1;
    public TextMeshProUGUI cashP2;
    public TextMeshProUGUI[] cashDisplay;

    [SerializeField] TextMeshProUGUI gapDefeatSecondsDisplay;
    [SerializeField] TextMeshProUGUI gapWinSecondsDisplay;


    //New
    [SerializeField] Button[] invDisplayP1;
    [SerializeField] Button[] invDisplayP2;
    [SerializeField] Button[] invDisplayP3;

    //

    [SerializeField] GameObject startRaceButton;

    [SerializeField] TextMeshProUGUI currentCarNameMarketPanel;
    [SerializeField] TextMeshProUGUI currentCarPrizeMarketPanel;
    [SerializeField] TextMeshProUGUI carValueChangeDisplay;

    [SerializeField] TextMeshProUGUI defaultCarName;
    [SerializeField] TextMeshProUGUI defaultCarValueChange;



    public TMP_Dropdown winnerDropdown;
    public TMP_Dropdown defeatDropdown;


    [SerializeField] GameObject[] turnIndicator;
    public TextMeshProUGUI statusInfoTextBar;
    public TextMeshProUGUI helpText;

    [SerializeField] TextMeshProUGUI currentRaceInfoTrack;
    [SerializeField] TextMeshProUGUI currentRaceInfoCar;
    [SerializeField] TextMeshProUGUI currentRaceInfoRound;
    [SerializeField] TextMeshProUGUI currentRaceOpponent1;
    [SerializeField] TextMeshProUGUI currentRaceOpponent2;

    [SerializeField] Button cancelRace;
    [SerializeField] Button startRace;
    [SerializeField] Button buyCarButton;
    [SerializeField] Button protectButton;

    [SerializeField] Button SellDoneButton;
    [SerializeField] Button p1SellButton;
    [SerializeField] Button p2SellButton;

    [SerializeField] TextMeshProUGUI resultsP1Name;
    [SerializeField] TextMeshProUGUI resultsP2Name;
    [SerializeField] TextMeshProUGUI resultsP1cashTotal;
    [SerializeField] TextMeshProUGUI resultsP2cashTotal;



    private int[] carValueChangeOptions = { -10, -7, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 7, 10 };

    public int lastChangebeforeDefault = 0;

    private List<int> roundsWithCarSellingOption = new List<int> { 2, 4, 6, 8, 10, 12 };

        
    private TimeBattleButtons timeBattlePanelScript;
    private DividendGenerator dividendScript;
    private ToynopolyCalculator toynopolyCalculatorScript;
    private Timer timerScript;
    private CountUpHandler countUpScript;
    private ProtectionHandler protectionHandlerScript;
    private LapDataReader lapCountScript;

    public static event Action Onlevel2Start;
    public static event Action Onlevel3Start;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        dividendScript = GameObject.Find("DividendGenerator").GetComponent<DividendGenerator>();
        toynopolyCalculatorScript = GameObject.Find("ToynopolyCalculator").GetComponent<ToynopolyCalculator>();
        timerScript = GameObject.Find("Timer").GetComponent<Timer>();
        countUpScript = GameObject.Find("CountUpHandler").GetComponent<CountUpHandler>();
        protectionHandlerScript = GameObject.Find("ProtectionHandler").GetComponent<ProtectionHandler>();
        lapCountScript = GameObject.Find("LapDataReader").GetComponent<LapDataReader>();
        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {MainManager.raceThreshold - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");

        //timerPanel.gameObject.SetActive(true);
        


        if (MainManager.gameResumed)
        {
            for (int i = 0; i < MainManager.cars.Length; i++)

            {
                invDisplayP1[i].gameObject.SetActive(true);
                invDisplayP2[i].gameObject.SetActive(true);
            }
                        
            UpdateInventoryDisplay();
            UpdateCarPrizesDisplay();
            RoundChangeover();
            UpdateCashDisplay();
        }

        else { audioSource.PlayOneShot(newGame);

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
        
        if(Input.GetKeyDown(KeyCode.Escape))
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

        if (MainManager.matchTimeDisplayed)

        {
            timerScript.DisplayToggle(false);
            timerPanel.gameObject.SetActive(false);
        }

    }

    void ShowNextRacePanel()

    {


        if (MainManager.levelCounter == 2)

        {
            startRaceButton.SetActive(true);
            buyCarButton.gameObject.SetActive(true);
            activePlayerhasToynopoly = false;

            PerformLevel2Check();

            if (l2SelectionIsOkay == false)
            {

                if (buyingPossible == true && playerHasBoughtCarThisRound == false)

                {
                    activePlayerMessage.text = ("You don't own this car. Would you like to buy it?");
                    buyCarButton.gameObject.SetActive(true);
                    startRaceButton.SetActive(false);
                    protectButton.gameObject.SetActive(false);
                }

                if (buyingPossible == true && playerHasBoughtCarThisRound == true)

                {
                    activePlayerMessage.text = ("You don't own this car.");
                    buyCarButton.gameObject.SetActive(false);
                    startRaceButton.SetActive(false);
                    protectButton.gameObject.SetActive(false);
                }

                else if (buyingPossible == false)

                {
                    activePlayerMessage.text = ("An opponent has a protected Toynopoly for this car. Please choose a different car");
                    startRaceButton.SetActive(false);
                    buyCarButton.gameObject.SetActive(false);
                    protectButton.gameObject.SetActive(false);

                }

            }

            else if (playerHasBoughtCarThisRound == true)

            {
                activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
                buyCarButton.gameObject.SetActive(false);
            }

            else if (activePlayerhasToynopoly == true && MainManager.protection[MainManager.currentCarIndex] == false && MainManager.shieldAvailable[MainManager.activePlayer])
            {
                activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
                buyCarButton.gameObject.SetActive(true);
                protectButton.gameObject.SetActive(true);
            }

            else if (activePlayerhasToynopoly == true && MainManager.protection[MainManager.currentCarIndex] == false)
            {
                activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
                buyCarButton.gameObject.SetActive(true);
                protectButton.gameObject.SetActive(false);
            }

            else if (activePlayerhasToynopoly == true)
            {
                activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
                buyCarButton.gameObject.SetActive(false);
            }

            else

            {
                activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
                protectButton.gameObject.SetActive(false);

            }



        }

        nextRaceComingUpPanel.SetActive(true);
        audioSource.PlayOneShot(panelOpen);


        if (MainManager.levelCounter == 1)

        { activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:"); }


        if (MainManager.levelCounter == 3)

        {
            startRaceButton.SetActive(true);
            buyCarButton.gameObject.SetActive(false);

            PerformLevel3Check();

            if (l3SelectionIsOkay == false)

            {
                activePlayerMessage.text = ("You don't own this car.");
                startRaceButton.SetActive(false);
            }

            else

            {
                activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");
            }



        }

        nextTrackDisplay.text = selectedTrack;
        nextCarDisplay.text = selectedCar;



    }

    void PerformLevel3Check()
    {

        if (MainManager.playerInventory[MainManager.activePlayer, MainManager.currentCarIndex] < 1)

        {
            l3SelectionIsOkay = false;
        }

        else

        {
            l3SelectionIsOkay = true;
        }



    }

    void PerformLevel2Check()

    {
        switch (MainManager.activePlayer)
        {
            case 0:
                if (MainManager.playerInventory[0, MainManager.currentCarIndex] == 0)
                {
                    l2SelectionIsOkay = false;

                }

                if (MainManager.playerInventory[1, MainManager.currentCarIndex] > 0 && MainManager.protection[MainManager.currentCarIndex])

                {
                    buyingPossible = false;
                }

                if (MainManager.playerInventory[0, MainManager.currentCarIndex] > 0 && MainManager.playerInventory[1, MainManager.currentCarIndex] < 1)

                {
                    activePlayerhasToynopoly = true;
                }

                break;

            case 1:
                if (MainManager.playerInventory[1, MainManager.currentCarIndex] == 0)
                {
                    l2SelectionIsOkay = false;

                }

                if (MainManager.playerInventory[0, MainManager.currentCarIndex] > 0 && MainManager.protection[MainManager.currentCarIndex])

                {
                    buyingPossible = false;
                }

                if (MainManager.playerInventory[1, MainManager.currentCarIndex] > 0 && MainManager.playerInventory[0, MainManager.currentCarIndex] < 1)

                {
                    activePlayerhasToynopoly = true;
                }


                break;

        }
    }

    public void CancelRace()

    {
        nextRaceComingUpPanel.SetActive(false);
        l2SelectionIsOkay = true;
        buyingPossible = true;
        protectButton.gameObject.SetActive(false);

    }


    public void BuyCar()

    {
        switch (MainManager.activePlayer)
        {
            case 0:

                MainManager.playerCash[0] -= MainManager.carPrizes[MainManager.currentCarIndex];
                cashDisplay[0].text = MainManager.playerCash[0].ToString();
                PlayerWinsCar(0);

                if (MainManager.levelCounter == 2 && MainManager.playerInventory[1, MainManager.currentCarIndex] + MainManager.playerInventory[0, MainManager.currentCarIndex] < 2 )
                {
                    MainManager.playerWithBuyOption = 1;
                    OfferBuyOption();
                }

                break;


            case 1:

                MainManager.playerCash[1] -= MainManager.carPrizes[MainManager.currentCarIndex];
                cashDisplay[1].text = MainManager.playerCash[1].ToString();
                PlayerWinsCar(1);

                if (MainManager.levelCounter == 2 && MainManager.playerInventory[1, MainManager.currentCarIndex] + MainManager.playerInventory[0, MainManager.currentCarIndex] < 2 )
                {
                    MainManager.playerWithBuyOption = 0;
                    OfferBuyOption();
                }
                break;
        }

        nextRaceComingUpPanel.SetActive(false);
        l2SelectionIsOkay = true;
        buyingPossible = true;
        playerHasBoughtCarThisRound = true;


    }

    public void OfferBuyOption()
    {
        buyOptionPanel.SetActive(true);

    }

    public void AcceptBuyOption()

    {

        MainManager.playerCash[MainManager.playerWithBuyOption] -= MainManager.carPrizes[MainManager.currentCarIndex];
        cashDisplay[MainManager.playerWithBuyOption].text = MainManager.playerCash[MainManager.playerWithBuyOption].ToString();
        PlayerWinsCar(MainManager.playerWithBuyOption);


        buyOptionPanel.SetActive(false);
    }

    public void PassBuyOption()
    {
        buyOptionPanel.SetActive(false);
    }



    public void SetStateRaceInProgress()

    {

        nextRaceComingUpPanel.SetActive(false);

        int nonActivePlayer;

        if
        (MainManager.activePlayer == 0)
            nonActivePlayer = 1;

        else
            nonActivePlayer = 0;

        raceInProgressPanel.SetActive(true);
        audioSource.PlayOneShot(stageReady);
        currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} / 12 in progress");
        currentRaceInfoTrack.text = selectedTrack;
        currentRaceInfoCar.text = selectedCar;
        currentRaceOpponent1.text = MainManager.playerNames[MainManager.activePlayer];

        currentRaceOpponent2.text = MainManager.playerNames[nonActivePlayer];

        lapCountScript.FindLapData(selectedTrack);

    }

    public void ShowResultsPanel()

    {

        raceInProgressPanel.SetActive(false);
        raceResultsPanel.SetActive(true);
        resultPanelActivePlayer.text = MainManager.playerNames[MainManager.activePlayer];

        if (MainManager.levelCounter == 2)

        {
            sliderDefeat.SetActive(true);
            sliderWin.SetActive(true);

        }

        else

        {
            sliderDefeat.SetActive(false);
            sliderWin.SetActive(false);
        }



    }


    public void DisplaySecondsGap()

    {
        gapDefeatSecondsDisplay.text = defeatGap.value.ToString();
        gapWinSecondsDisplay.text = winGap.value.ToString();

    }



    public void RegisterResults()

    {
        fields[MainManager.pendingField].gameObject.SetActive(false);
        MainManager.fieldAvailable[MainManager.pendingField] = false;

        MainManager.fieldsLeftForCar[MainManager.currentCarIndex]--;

        if (!MainManager.autoResultsValid)
        {
            Debug.Log(winnerDropdown.value);
            if ((winnerDropdown.value) == 1)

            {
                MainManager.activePlayerWins = false;
            }

            else if ((defeatDropdown.value) == 1)

            {
                MainManager.activePlayerWins = true;
            }

            else Debug.Log("Please set the race results!");

        }

        else
        {
            raceInProgressPanel.SetActive(false);
        }
        
        raceResultsPanel.SetActive(false);

        if (MainManager.levelCounter == 1)

        {
            PostRaceScoring();
            PostRaceRandomMarketProcedure();

        }

        else if (MainManager.levelCounter == 2)

        {
            Level2Scoring();

        }

        else

        {
            Level3Scoring();

        }

    }


    public void PostRaceScoring()

    {
        if (MainManager.activePlayerWins == true)

        {

            PlayerWinsCar(MainManager.activePlayer);

        }

        if (MainManager.activePlayerWins == false)

        {
            if (MainManager.activePlayer == 0)

            {
                PlayerWinsCar(1);
                MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
                cashDisplay[MainManager.activePlayer].text = MainManager.playerCash[MainManager.activePlayer].ToString();
            }

            else if (MainManager.activePlayer == 1)

            {
                PlayerWinsCar(0);
                MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
                cashDisplay[MainManager.activePlayer].text = MainManager.playerCash[MainManager.activePlayer].ToString();
            }
        }

    }


    void PlayerWinsCar(int winner)
    {
        MainManager.playerInventory[winner, MainManager.currentCarIndex]++;

        switch (winner)

        {
            case 0:

                invDisplayP1[MainManager.currentCarIndex].gameObject.SetActive(true);
                invDisplayP1[MainManager.currentCarIndex].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[winner, MainManager.currentCarIndex].ToString();
                break;

            case 1:
                invDisplayP2[MainManager.currentCarIndex].gameObject.SetActive(true);
                invDisplayP2[MainManager.currentCarIndex].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[winner, MainManager.currentCarIndex].ToString();
                break;
        }

    }


    void PostRaceRandomMarketProcedure()

    {
        int oldCarValue = MainManager.carPrizes[MainManager.currentCarIndex];
        int randomValue = UnityEngine.Random.Range(0, 15);
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

            countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.currentCarIndex]);

            if (carValueChangeOptions[randomValue] > 9)
            {
                audioSource.PlayOneShot(success);
                
            }

            //SetNewCarPrizes();
        }

        else if (carValueChangeOptions[randomValue] < 0)

        {
            priceUpArrow.SetActive(false);
            priceDownArrow.SetActive(true);
            valueChangeMessage.text = "The price of this car has gone down";

            countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.currentCarIndex]);

            //SetNewCarPrizes();

        }

        else

        {
            priceUpArrow.SetActive(false);
            priceDownArrow.SetActive(false);
            valueChangeMessage.text = "The price of this car remains unchanged";

            //SetNewCarPrizes();

        }



    }

    void SetNewCarPrizes()

    {

        for (int i = 0; i < MainManager.carPrizes.Length; i++)


        {
            switch (i)

            {
                case 0:
                    priceCarA.text = MainManager.carPrizes[i].ToString();
                    break;

                case 1:
                    priceCarB.text = MainManager.carPrizes[i].ToString();
                    break;

                case 2:
                    priceCarC.text = MainManager.carPrizes[i].ToString();
                    break;

                case 3:
                    priceCarD.text = MainManager.carPrizes[i].ToString();
                    break;

                case 4:
                    priceCarE.text = MainManager.carPrizes[i].ToString();
                    break;

                case 5:
                    priceCarF.text = MainManager.carPrizes[i].ToString();
                    break;


            }



        }


    }

    void CarHasDefaulted()

    {
        carInDefaultPanel.SetActive(true);
        defautlDownArrow.SetActive(true);

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

        SetNewCarPrizes();
    }


    public void CheckForDefaultCars()

    {
        for (int i = 0; i < MainManager.carIsInDefault.Length; i++)
        {
            if (MainManager.carIsInDefault[i] && MainManager.DefProcedureCompleted[i] == false)

            {
                carInDefaultPanel.SetActive(true);
                defautlDownArrow.SetActive(true);
                defaultCarName.text = MainManager.cars[i];
                defaultPanelTextMessage.text = "This car is in default and is eliminated from the game";
                carPic[i].image.sprite = carDefaultSprite;
                carPrizeDisplays[i].color = Color.grey;
                carNameButtons[i].GetComponent<Image>().color = Color.grey;
                

                MainManager.playerInventory[0, i] = 0;
                MainManager.playerInventory[1, i] = 0;
                rows[i].SetActive(false);
                MainManager.DefProcedureCompleted[i] = true;

            }
        }

    }


    public void Level2Scoring()

    {
        if (MainManager.activePlayerWins == true)

        {
            if (MainManager.activePlayer == 0)

            {
                if (MainManager.playerInventory[1, MainManager.currentCarIndex] > 0)

                {
                    PlayerWinsCar(0);
                    MainManager.playerInventory[1, MainManager.currentCarIndex]--;
                }

            }

            else if (MainManager.activePlayer == 1)

            {
                if (MainManager.playerInventory[0, MainManager.currentCarIndex] > 0)

                {
                    PlayerWinsCar(1);
                    MainManager.playerInventory[0, MainManager.currentCarIndex]--;
                }
            }

        }

        if (MainManager.activePlayerWins == false)

        {
            if (MainManager.activePlayer == 0)

            {
                if (MainManager.playerInventory[1, MainManager.currentCarIndex] > 0)

                {
                    PlayerWinsCar(1);
                    MainManager.playerInventory[0, MainManager.currentCarIndex]--;
                }
            }

            else if (MainManager.activePlayer == 1)

            {
                if (MainManager.playerInventory[0, MainManager.currentCarIndex] > 0)

                {
                    PlayerWinsCar(0);
                    MainManager.playerInventory[1, MainManager.currentCarIndex]--;
                }
            }
        }

        UpdateInventoryDisplay();

        raceResultsPanel.SetActive(false);

        timeBattlePanelScript = GameObject.Find("TimeBattleHandler").GetComponent<TimeBattleButtons>();

        TimeBattleOutcome();

    }

    public void UpdateInventoryDisplay()

    {
        for (int i = 0; i < MainManager.cars.Length; i++)

        {
                        
            invDisplayP1[i].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[0, i].ToString();
            invDisplayP2[i].GetComponentInChildren<TMP_Text>().text = MainManager.playerInventory[1, i].ToString();

            if (MainManager.playerInventory[0, i] < 1)
            {
                invDisplayP1[i].gameObject.SetActive(false);
            }

            if (MainManager.playerInventory[1, i] < 1)

            {
                invDisplayP2[i].gameObject.SetActive(false);
            }
        }

    }

    public void UpdateCarPrizesDisplay()

    {
        for (int i = 0; i < carPrizeDisplays.Length; i++)

        { carPrizeDisplays[i].text = MainManager.carPrizes[i].ToString(); }
    }


    public void TimeBattleOutcome()

    {
        int timeBattleWinner;

        if (MainManager.activePlayerWins == true)

        {
            timeBattleWinner = MainManager.activePlayer;

        }

        else

            if (MainManager.activePlayer == 0)
        {
            timeBattleWinner = 1;
        }

        else

        {
            timeBattleWinner = 0;
        }



        timeBattlePanel.SetActive(true);

        timeBattlePanelScript.UpdateDisplays();

        timeBattleWinnerDisplay.text = MainManager.playerNames[timeBattleWinner];

        if (!MainManager.autoResultsValid)
        {

            if (timeBattleWinner == MainManager.activePlayer)

            {
                int gap;
                gap = System.Convert.ToInt32(winGap.value);
                MainManager.timeBattleSeconds = (gap);
            }

            else

            {
                int gap;
                gap = System.Convert.ToInt32(defeatGap.value);
                MainManager.timeBattleSeconds = (gap);

            }
        }

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
        float oldCarValue = MainManager.carPrizes[MainManager.TimeBattleCarIndex];
               
        MainManager.carPrizes[MainManager.TimeBattleCarIndex] += MainManager.timeBattleSeconds;

        countUpScript.AddValue(oldCarValue, MainManager.carPrizes[MainManager.TimeBattleCarIndex]);

        if (MainManager.timeBattleSeconds > 19)
        {
            audioSource.PlayOneShot(success);
            
        }


        if (MainManager.carPrizes[MainManager.TimeBattleCarIndex] < 0)

        {
            MainManager.carPrizes[MainManager.TimeBattleCarIndex] = 0;
        }
        UpdateCarPrizesDisplay();
        timeBattlePanel.SetActive(false);
        buffNerfPanel.SetActive(false);


        if (MainManager.playerInventory[MainManager.activePlayer,MainManager.TimeBattleCarIndex] > 0 && MainManager.shieldAvailable[MainManager.activePlayer] == true)

        {
            int totalInventory = 0;
            totalInventory = MainManager.playerInventory[0, MainManager.TimeBattleCarIndex] + MainManager.playerInventory[1, MainManager.TimeBattleCarIndex];

            if (totalInventory == MainManager.playerInventory[MainManager.activePlayer,MainManager.TimeBattleCarIndex])
            {
                protectionEnablePanel.gameObject.SetActive(true);
                protectionHandlerScript.GetInformation();

            }
        }


        if (roundsWithCarSellingOption.Contains(MainManager.roundCounter))

        {
            ReInstateRows();
            preSellingInfoPanel.SetActive(true);
            audioSource.PlayOneShot(panelOpen);
        }

        else
            
            StartCoroutine(WaitAfterCarFame());
        

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

        if (roundsWithCarSellingOption.Contains(MainManager.roundCounter))

        {
            ReInstateRows();
            preSellingInfoPanel.SetActive(true);
            audioSource.PlayOneShot(panelOpen);
        }

        else

            StartCoroutine(WaitAfterCarFame());
            //RoundChangeover();
    }


    public void StartSellingRound()

    {
        preSellingInfoPanel.SetActive(false);
        p1SellButton.gameObject.SetActive(true);
        p2SellButton.gameObject.SetActive(true);
        SellDoneButton.gameObject.SetActive(true);

    }

    public void ShowNextRoundButton()
    {
        SellDoneButton.gameObject.SetActive(true);
        carInDefaultPanel.SetActive(false);
    }



    public void RoundChangeover()

    {

        Save();

        carInDefaultPanel.SetActive(false);
        p1SellButton.gameObject.SetActive(false);
        p2SellButton.gameObject.SetActive(false);
        SellDoneButton.gameObject.SetActive(false);


        postRaceMarketPanel.SetActive(false);



        MainManager.roundCounter++;
        l2SelectionIsOkay = true;
        l3SelectionIsOkay = true;
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
            if (MainManager.roundCounter < 13)

            {
                dividendScript.DividendCheck();
                OutOfOptionsCheck();
            }
        }

        if (MainManager.levelCounter == 3)

        {
            InventoryCheck();

            toynopolyCalculatorScript.PerformToynopolyCalculations();
        }

        LevelCheck();
                
        BankruptCheck();

        int randomNumber = UnityEngine.Random.Range(1, 4);
        if (randomNumber == 3)
        {
                timerPanel.gameObject.SetActive(true);
                timerScript.DisplayToggle(true);
                  
        }

    }

    void UpdateCashDisplay()
    {
        cashDisplay[0].text = MainManager.playerCash[0].ToString();
        cashDisplay[1].text = MainManager.playerCash[1].ToString();
    }




    void LevelCheck()

    {
        if (MainManager.roundCounter > MainManager.raceThreshold-1)

        {
            if (MainManager.levelCounter == 1)

            {
                if (MainManager.roundCounter == MainManager.raceThreshold)

                {
                    level2StartPanel.gameObject.SetActive(true);
                    Onlevel2Start?.Invoke();


                    MainManager.levelCounter = 2;
                    MainManager.roundCounter = 1;
                }

            }

            else if (MainManager.levelCounter == 2)

            {
                if (MainManager.roundCounter == 13)

                {
                    MainManager.levelCounter = 3;
                    MainManager.roundCounter = 1;
                    StartLevel3();
                }

            }
        }

    }

    public void SetLevelChangePanelInactive()

    {
        level2StartPanel.SetActive(false);

        MainManager.activePlayer = GetIndexOfLowestValue(MainManager.playerCash);

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            turnIndicator[i].SetActive(false);
        }

        turnIndicator[MainManager.activePlayer].SetActive(true);


        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {13 - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");

    }

    public void AcceptDividend()

    {
        for (int i = 0; i < cashDisplay.Length; i++)

        {
            cashDisplay[i].text = MainManager.playerCash[i].ToString();
        }

    }


    public void StartLevel3()

    {
        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {13 - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");

        for (int i = 0; i < MainManager.cars.Length; i++)

        {
            if (MainManager.playerInventory[0, i] > 0 && MainManager.playerInventory[1, i] > 0)

            {
                MainManager.playerInventory[0, i] = 0;
                MainManager.playerInventory[1, i] = 0;
                MainManager.carIsInDefault[i] = true;
                rows[i].SetActive(false);
                UpdateInventoryDisplay();
            }

        }

        level3StartPanel.gameObject.SetActive(true);
        Onlevel3Start?.Invoke();
        audioSource.PlayOneShot(transition);

    }

    public void CloseLevel3EntryPanel()

    {
        level3StartPanel.gameObject.SetActive(false);
    }


    public void Level3Scoring()

    {
        if (MainManager.activePlayerWins == true)

        {
            if (MainManager.activePlayer == 0)

            {
                MainManager.playerCash[MainManager.activePlayer] += MainManager.carPrizes[MainManager.currentCarIndex];
                MainManager.playerCash[1] -= MainManager.carPrizes[MainManager.currentCarIndex];

                for (int i = 0; i < MainManager.playerNumber; i++)

                {
                    cashDisplay[i].text = MainManager.playerCash[i].ToString();
                }


                RoundChangeover();

            }

            else if (MainManager.activePlayer == 1)

            {
                MainManager.playerCash[1] += MainManager.carPrizes[MainManager.currentCarIndex];
                MainManager.playerCash[0] -= MainManager.carPrizes[MainManager.currentCarIndex];

                for (int i = 0; i < MainManager.playerNumber; i++)

                {
                    cashDisplay[i].text = MainManager.playerCash[i].ToString();
                }
                RoundChangeover();
            }

        }


        if (MainManager.activePlayerWins == false)

        {
            if (MainManager.activePlayer == 0)

            {
                {
                    MainManager.playerInventory[0, MainManager.currentCarIndex]--;
                    UpdateInventoryDisplay();

                    RoundChangeover();
                }
            }

            else if (MainManager.activePlayer == 1)

            {

                {
                    MainManager.playerInventory[1, MainManager.currentCarIndex]--;
                    UpdateInventoryDisplay();

                    RoundChangeover();
                }
            }
        }

    }

    void OutOfOptionsCheck()

    {
        for (int i = 0; i < MainManager.cars.Length; i++)

        {
            MainManager.playerOutofOptions[i] = false;


            if (MainManager.playerInventory[MainManager.activePlayer, i] < 1 && MainManager.protection[i] || MainManager.fieldsLeftForCar[i] < 1)

            {
                MainManager.playerOutofOptions[i] = true;
            }


        }

        if (!MainManager.playerOutofOptions.Contains(false))

        {
            RoundChangeover();
        }
                
    }


    void BankruptCheck()

    {
        for (int i = 0; i < MainManager.playerNumber - 1; i++)

        {
            if (MainManager.playerCash[i] < 1)

            { EndGame(); }

        }

    }

    void InventoryCheck()

    {
        int sumInventoryp1 = 0;
        int sumInventoryp2 = 0;

        for (int i = 0; i < MainManager.cars.Length; i++)
        {
            sumInventoryp1 += MainManager.playerInventory[0, i];
            sumInventoryp2 += MainManager.playerInventory[1, i];
        }

        if (sumInventoryp1 + sumInventoryp2 == 0)
        {
            EndGame();
        }

        else if (sumInventoryp1 == 0 && MainManager.activePlayer == 0)
        {
            RoundChangeover();

        }

        else if (sumInventoryp2 == 0 && MainManager.activePlayer == 1)
        {
            RoundChangeover();
        }



    }

    public void EndGame()

    {
        MainManager.gameOver = true;
        audioSource.PlayOneShot(heartbeat);
        Debug.Log("Game Over");

        gameOverPanel.SetActive(true);

        resultsP1Name.text = MainManager.playerNames[0];
        resultsP2Name.text = MainManager.playerNames[1];
        resultsP1cashTotal.text = MainManager.playerCash[0].ToString();
        resultsP2cashTotal.text = MainManager.playerCash[1].ToString();

        for (int i = 0; i < finaleStars.Length; i++)
        {
            finaleStars[i].Play();
        }


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


    System.Collections.IEnumerator WaitAfterCarFame()

    {
        yield return new WaitForSeconds(10.0f);
        ReInstateRows();
        RoundChangeover();


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


    private void Save()
    {
        int[] concatenatedInventory = new int[36];
        int x = 0;
        int y = 0;
        int z = 0;
        int a = 0;
        int b = 0;

        for (int i=0; i<concatenatedInventory.Length; i++)

        {
            if (i<6)
            { concatenatedInventory[i] = MainManager.playerInventory[0, i]; }

            else if (i<12)
            { concatenatedInventory[i] = MainManager.playerInventory[1, x];
                x++;
            }

            else if (i<18)
            {
                concatenatedInventory[i] = MainManager.playerInventory[2, y];
                y++;
            }

            else if (i<24)
            {
                concatenatedInventory[i] = MainManager.playerInventory[3, z];
                z++;
            }

            else if (i<30)

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
            tempdividends = dividendScript.actualDividendList,

        };

        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        SaveSystem.Save(json);

        SaveGameData loadedPlayerData = JsonUtility.FromJson<SaveGameData>(json);
           
        
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


