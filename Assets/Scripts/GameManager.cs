using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Button[] fields;

    private string selectedTrack;
    private string selectedCar;

    [SerializeField] GameObject nextRaceComingUpPanel;
    [SerializeField] GameObject raceInProgressPanel;
    [SerializeField] TextMeshProUGUI activePlayerMessage;
    [SerializeField] TextMeshProUGUI nextTrackDisplay;
    [SerializeField] TextMeshProUGUI nextCarDisplay;

    [SerializeField] GameObject turnIndicatorP1;
    [SerializeField] GameObject turnIndicatorP2;
    [SerializeField] TextMeshProUGUI statusInfoTextBar;

    [SerializeField] TextMeshProUGUI currentRaceInfoTrack;
    [SerializeField] TextMeshProUGUI currentRaceInfoCar;
    [SerializeField] TextMeshProUGUI currentRaceInfoRound;
    [SerializeField] TextMeshProUGUI currentRaceOpponent1;
    [SerializeField] TextMeshProUGUI currentRaceOpponent2;

    [SerializeField] Button cancelRace;
    [SerializeField] Button startRace;

    // Start is called before the first frame update
    void Awake()
    {
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

        { selectedCar = MainManager.cars[0]; }

        else if (fieldNumber <= 19)

        { selectedCar = MainManager.cars[1]; }

        else if (fieldNumber <= 29)
        { selectedCar = MainManager.cars[2]; }

        else if (fieldNumber <= 39)
        { selectedCar = MainManager.cars[3]; }

        else if (fieldNumber <= 49)
        { selectedCar = MainManager.cars[4]; }

        else

        { selectedCar = MainManager.cars[5]; }

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

        activePlayerMessage.text = ($"{ MainManager.playerNames[MainManager.activePlayer]} has made their selection:");

        nextTrackDisplay.text = selectedTrack;
        nextCarDisplay.text = selectedCar;



    }

    public void CancelRace()

    {
        nextRaceComingUpPanel.SetActive(false);

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
        currentRaceInfoRound.text = ($"Level {MainManager.levelCounter}, Race {MainManager.roundCounter} / 12 in progress");
        currentRaceInfoTrack.text = selectedTrack;
        currentRaceInfoCar.text = selectedCar;
        currentRaceOpponent1.text = MainManager.playerNames[MainManager.activePlayer];

        currentRaceOpponent2.text = MainManager.playerNames[nonActivePlayer];


    }


}
