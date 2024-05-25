using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager3P : MonoBehaviour
{
    public Button[] fields;
    public Button[] playerSellButton;
        
    public GameObject[] rows;
    
    private bool l2SelectionIsOkay = true;
    private bool l3SelectionIsOkay = true;
    private bool buyingPossible = true;
    private bool playerHasBoughtCarThisRound = false;

    private int[] carValueChangeOptions = { -10, -7, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 7, 10 };

    public int lastChangebeforeDefault = 0;

    public AudioSource audioSource;
    public AudioClip panelOpen;
    public AudioClip stageReady;
    public AudioClip coinFalling;

    public TextMeshProUGUI[] cashDisplay;

    [SerializeField] Button[] invDisplayP1;
    [SerializeField] Button[] invDisplayP2;
    [SerializeField] Button[] invDisplayP3;

    public Button[] carPic;

    public TextMeshProUGUI[] carPrizeDisplays;


    public TextMeshProUGUI statusInfoTextBar;

    private string selectedTrack;
    private string selectedCar;

    [SerializeField] GameObject[] turnIndicator;

    [SerializeField] GameObject nextRaceComingUpPanel;
    [SerializeField] GameObject raceInProgressPanel;
    [SerializeField] GameObject raceResultsPanelL1;
    [SerializeField] GameObject postRaceMarketPanel;

    [SerializeField] GameObject priceUpArrow;
    [SerializeField] GameObject priceDownArrow;



    public TMP_Dropdown winnerDropdown;
    public TMP_Dropdown runnerUpDropdown;


    [SerializeField] TextMeshProUGUI activePlayerMessage;



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




    [SerializeField] Sprite carDefaultSprite;


    private int raceWinnerLevel1 = 0;
    private int runnerUpLevel1 = 0;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {13 - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");
    }

    // Update is called once per frame
    void Update()
    {
        
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
                //fill for Level 2
                break;

            case 3:
                //fill for Level 3
                break;
        }
                       
               

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
        raceInProgressPanel.SetActive(true);
        audioSource.PlayOneShot(stageReady);
        currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} / 12 in progress");
        currentRaceInfoTrack.text = selectedTrack;
        currentRaceInfoCar.text = selectedCar;
        

        switch (MainManager.levelCounter)

        {
            case 1:
                currentRaceOpponent1.text = MainManager.playerNames[MainManager.activePlayer];
                break;

            case 2:
                //fill for Level 2
                break;

            case 3:
                //fill for Level 3
                break;
        }

    }

    public void ShowResultsPanel()

    {
        

            raceInProgressPanel.SetActive(false);
            raceResultsPanelL1.SetActive(true);
                                        

     }

    public void RegisterResults()

    {
        Debug.Log(winnerDropdown.value);
        
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

        
        fields[MainManager.pendingField].gameObject.SetActive(false);

        MainManager.fieldsLeftForCar[MainManager.currentCarIndex]--;

        raceResultsPanelL1.SetActive(false);

        switch(MainManager.levelCounter)

        {
            case 1:
                Level1Scoring();
                PostRaceRandomMarketProcedure();
                break;

            case 2:
                    //FillforLevel2
                    break;

            case 3:
                //Fill for Level3
                break;


        }
                
    }

    void Level1Scoring()

    {
        if(MainManager.activePlayerWins)

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

    void PostRaceRandomMarketProcedure()

    {
        int oldCarValue = MainManager.carPrizes[MainManager.currentCarIndex];
        int randomValue = Random.Range(0, 14);
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
        carInDefaultPanel.SetActive(false);
        postRaceMarketPanel.SetActive(false);

        for (int i = 0; i < MainManager.playerNumber; i++)
        {
            playerSellButton[i].gameObject.SetActive(false);
        }
        
        SellDoneButton.gameObject.SetActive(false);
                    
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
                   
        statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {13 - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");

        if (MainManager.levelCounter == 2)
        {
            if (MainManager.roundCounter < 13)

            {
                //dividendScript.DividendCheck();
                //OutOfOptionsCheck();
            }
        }

        if (MainManager.levelCounter == 3)

        {
            //InventoryCheck();

            //toynopolyCalculatorScript.PerformToynopolyCalculations();
        }

        LevelCheck();

        //BankruptCheck();

    }


    void LevelCheck()

    {
        if (MainManager.roundCounter > 12)

        {
            if (MainManager.levelCounter == 1)

            {
                if (MainManager.roundCounter == 13)

                {
                    //level2StartPanel.gameObject.SetActive(true);
                }

                MainManager.levelCounter = 2;
                MainManager.roundCounter = 1;


            }

            else if (MainManager.levelCounter == 2)

            {
                if (MainManager.roundCounter == 13)

                {
                    MainManager.levelCounter = 3;
                    MainManager.roundCounter = 1;
                    //StartLevel3();
                }

            }
        }

    }



}



