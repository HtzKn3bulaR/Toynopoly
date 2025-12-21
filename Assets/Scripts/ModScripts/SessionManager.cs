using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{

    public static SessionManager instance { get; set; }


    public List<string> playerRoster;

    [SerializeField] private Transform[] fields;
            
    [SerializeField] GameObject nextRaceComingUpPanel;
    [SerializeField] GameObject raceInProgressPanel;

    [SerializeField] TextMeshProUGUI statusInfoTextBar;

    [SerializeField] TextMeshProUGUI activePlayerMessage;
    [SerializeField] TextMeshProUGUI nextTrackDisplay;
    [SerializeField] TextMeshProUGUI nextCarDisplay;

    [SerializeField] TextMeshProUGUI currentRaceInfoTrack;
    [SerializeField] TextMeshProUGUI currentRaceInfoCar;
    [SerializeField] TextMeshProUGUI currentRaceInfoRound;

    [SerializeField] private LapDataReader lapCountScript;
    [SerializeField] private LogFileReader logFileReader;
    [SerializeField] private PlayerPanelManager playerPanelManager;


    private string selectedTrack;
    private string selectedCar;

   

    private void Awake()
    {
        instance = this;
        playerRoster = new List<string>();
    }

    private void Start()
    {
        statusInfoTextBar.text = ($"Races completed: {MainManager.roundCounter - 1} / Players connected: {playerRoster.Count}");
    }
        

    public void ConcludeRace()

    {
        raceInProgressPanel.SetActive(false);
        MainManager.roundCounter++;
               
        logFileReader.ReadCSVFileCurrentRound();

        logFileReader.MoveCursorPosAfterSuccessfulRead();
                
        
        fields[MainManager.pendingField].gameObject.SetActive(false);
        MainManager.fieldAvailable[MainManager.pendingField] = false;

        MainManager.fieldsLeftForCar[MainManager.currentCarIndex]--;

        //playerPanelManager.StartRandomizerVisual();

        statusInfoTextBar.text = ($"Races completed: {MainManager.roundCounter - 1} / Players connected: {playerRoster.Count}");

    }


    public void UpdateRosterList(List<string> list)
    {
        playerRoster.Clear();
        playerRoster.AddRange(list);
        Debug.Log("Players on List " + list.Count);

        playerPanelManager.UpdateRosterVisual();
    }

    public List<string> GetRosterList()
    {
        return playerRoster;
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

        //helpText.gameObject.SetActive(false);

        ShowNextRacePanel();

       

    


    void ShowNextRacePanel()

    {       
       nextRaceComingUpPanel.gameObject.SetActive(true);

        activePlayerMessage.text = ($"Next Race Selection:"); }

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
                
        raceInProgressPanel.SetActive(true);
        
        currentRaceInfoRound.text = ($"RVGL Race in Progress...");
        currentRaceInfoTrack.text = selectedTrack;
        currentRaceInfoCar.text = selectedCar;
        
        lapCountScript.FindLapData(selectedTrack);

    }



}
