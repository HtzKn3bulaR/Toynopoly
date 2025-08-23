using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.Events;


public class CSVFileReader : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI[] LBplayers;
    [SerializeField] TextMeshProUGUI[] LBcars;
    [SerializeField] TextMeshProUGUI[] LBtimes;
    [SerializeField] TextMeshProUGUI[] LBgaps;

    [SerializeField] GameObject ResultsPanelAuto;

    [SerializeField] GameObject SetNewLogFilePanel;

    [SerializeField] GameObject validateButtonLevel1;
    [SerializeField] GameObject validateButtonLevel2;
    [SerializeField] GameObject validateButtonLevel3;
    [SerializeField] GameObject validateButtonToynopoly;

    [SerializeField] GameObject getResultsLevel1Button;
    [SerializeField] GameObject manualLevel1Button;

    [SerializeField] GameObject raceInProgressPanel;
    [SerializeField] GameObject raceInProgressPanelChallenge;

    private bool[] playerIsChallenger = { false, false, false, false, false };
    private bool[] playerIsDefender = { false, false, false, false, false };

    private int finisherPos2 = 9;
    private int finisherPos3 = 9;
    private int finisherPos4 = 9;
    private int finisherPos5 = 9;

    private char[] trimchar;

    private int cursorPos = 4;
    public string selectedFilePath;

    private List<string> resultLines = new List<string>();

    private List<string> playerNames = new List<string>();
    private List<string> carNames = new List<string>();
    private List<string> times = new List<string>();

    private List<string> stringsecs = new List<string>();
    private List<string> stringmins = new List<string>();
    private List<int> seconds = new List<int>();
    private List<int> minutes = new List<int>();
    private List<int> gaps = new List<int>();

    private List<string> cleanedNames = new List<string>();

    private List<string> validLineMarkers = new List<string>() { "01", "02", "03", "04", "05" };

    private CSVFileSelector fileSelectorScript;

    private PlayerManager3P playerManagerScript;

    public int changeValue;

    [SerializeField] GameObject[] challengerButtonsLB;
    [SerializeField] GameObject[] defenderButtonsLB;


    void Start()
    {
        selectedFilePath = MainManager.selectedFilePath;

        fileSelectorScript = GameObject.Find("CSVFileSelector").GetComponent<CSVFileSelector>();
        playerManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();

        GameManager.Onlevel2Start += ValidateButtonsChange;
        GameManager.Onlevel2Start += GetResultsButtonsChange;
        GameManager.Onlevel3Start += ValidateButtonsLevel3;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadCSVFileCurrentRound()
    {
               

        string[] lines = File.ReadAllLines(selectedFilePath);

        Debug.Log("Number of lines in file " + lines.Length);

        resultLines.Clear();
        Debug.Log("Cursor position before reading" + cursorPos);

        if (cursorPos > lines.Length)
        {
            Debug.Log("No more session data found in file");
            playerNames.Clear();
            carNames.Clear();
            times.Clear();
            gaps.Clear();

            LeaderboardClear();

            cursorPos = 4;
            SetNewLogFilePanel.gameObject.SetActive(true);
            fileSelectorScript.GetAllCSVFiles();                 

        }

        else if ((cursorPos + MainManager.playerNumber) > lines.Length)
        {

            for (int i = cursorPos; i < (lines.Length - cursorPos); i++)
            {

                resultLines.Add(lines[i]);

            }

            cursorPos += (lines.Length - cursorPos);
            cursorPos += 3;

        }

        else
        {


            for (int i = cursorPos; i < (cursorPos + MainManager.playerNumber); i++)
            {

                resultLines.Add(lines[i]);

            }

            cursorPos += (MainManager.playerNumber);
            cursorPos += 3;
            Debug.Log("Cursor position after reading " + cursorPos);

            ExtractResultsData();
        }
    }

    void ExtractResultsData()
    {

        playerNames.Clear();
        carNames.Clear();
        times.Clear();

        char[] chars = { '"', '#' };

        for (int i = 0; i < MainManager.playerNumber; i++)
        {

            string[] lineData = resultLines[i].Split(",");



            if (i == 0 && lineData[0].Trim(chars) != "01")
            {
                Debug.Log("Error! No valid session data found");
                cursorPos++;
                ReadCSVFileCurrentRound();
            }

            else
            {
                Debug.Log(lineData[0].Trim(chars));

                if (validLineMarkers.Contains(lineData[0].Trim(chars)))

                {
                    playerNames.Add(lineData[1].Trim(chars));
                    carNames.Add(lineData[2].Trim(chars));
                    times.Add(lineData[3].Trim(chars));
                }
                else

                    cursorPos--;
            } 
        }

        CleanCSVNames();
        ExtractSeconds();

    }

    void CleanCSVNames()
    {
        for (int i = 0; i < playerNames.Count; i++)
        {
            playerNames[i] = playerNames[i].TrimEnd(new char[] { '\r', ' ' });
            playerNames[i] = playerNames[i].TrimStart(new char[] { '\r', ' ' });
            playerNames[i] = playerNames[i].ToUpper();
            Debug.Log(playerNames[i]);

        }
    }


    void ExtractSeconds()
    {

        stringsecs.Clear();
        stringmins.Clear();

        seconds.Clear();
        minutes.Clear();

        for (int i = 0; i < times.Count; i++)
        {
            string[] timeElements = times[i].Split(":");
            stringsecs.Add(timeElements[1]);
            stringmins.Add(timeElements[0]);

            
            seconds.Add(Convert.ToInt32(stringsecs[i]));
            minutes.Add(Convert.ToInt32(stringmins[i]));

            Debug.Log("Minutes " + minutes[i]);
            Debug.Log("Seconds " + seconds[i]);

        }

        GapCalculate();

    }

    void GapCalculate()
    {
        gaps.Clear();

        for (int i = 0; i < seconds.Count; i++)
        {
            if ( i > 0)
            {
                if (minutes[i] > minutes[0])
                {                    
                    gaps.Add((minutes[i] - minutes[0]) * 60  + (seconds[i] - seconds[0]));                  
                }
                
                else if (minutes[i] == minutes[0] || minutes[i] < minutes[0])
                {
                    gaps.Add(seconds[i] - seconds[0]);
                }
            }
        }
    }

    void PopulateLeaderboard()
    {

        ResultsPanelAuto.gameObject.SetActive(true);

        for (int i = 0; i < playerNames.Count; i++)
        {
            
            LBplayers[i].text = playerNames[i].ToString();
            LBcars[i].text = carNames[i].ToString();
            LBtimes[i].text = times[i].ToString();
            
        }

        for (int i = 0; i < playerNames.Count -1; i++)
        {
            LBgaps[i].text = gaps[i].ToString();
        }

        SetLeaderboardIcons();
    }

    public void ResultsValidate()
    {
        GetAllFinisherPositions();
        ResetChallengerDefenderButtonsArrays();

        bool resultsValid = true;
           
        for (int i = 0; i < playerNames.Count; i++)
        {                      

            switch (playerNames.IndexOf(MainManager.playerNames[i]))

            {
                case 0:
                    Debug.Log(MainManager.playerNames[i] + " is the winner");                                  
                    
                    if (MainManager.playerNumber > 2)
                    {
                        playerManagerScript.raceWinnerLevel1 = i;
                        MainManager.raceWinner = i;                                              
                    }
                    if (MainManager.activePlayer == i)
                    {
                        MainManager.activePlayerWins = true;
                        

                        if (MainManager.levelCounter > 1)
                        {
                            playerIsChallenger[0] = true;
                            playerManagerScript.challengeWon = true;
                            playerManagerScript.challengeLost = false;
                            playerManagerScript.stolenWin = false;
                        }
                    }
                    else if (MainManager.levelCounter > 1)
                    {
                        if (MainManager.defendingPlayer == i)
                        {
                            playerIsDefender[0] = true;
                        }
                        else
                        {
                            playerManagerScript.stolenWin = true;
                            playerManagerScript.stealer = i;
                        }
                    }
                    break;

                case 1:
                    Debug.Log(MainManager.playerNames[i] + " has second place");
                   
                    if (MainManager.playerNumber > 2)
                    {
                        playerManagerScript.runnerUpLevel1 = i;
                    }
                    if (MainManager.activePlayer == i)
                    {
                        MainManager.activePlayerWins = false;

                        if (MainManager.levelCounter > 1)
                        {
                            playerIsChallenger[1] = true;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[0], -20, 20);
                         }

                        if (MainManager.levelCounter > 1 && MainManager.raceWinner == MainManager.defendingPlayer)
                        {
                            playerManagerScript.challengeLost = true;
                            playerManagerScript.challengeWon = false;

                        }

                        else if (MainManager.levelCounter > 1 && MainManager.defendingPlayer != MainManager.raceWinner)
                        {
                            playerManagerScript.challengeLost = false;
                            playerManagerScript.challengeWon = true;
                            playerManagerScript.stolenWin = true;
                        }                                                             
                        
                    }

                    else if (MainManager.defendingPlayer == i && MainManager.levelCounter > 1)
                    {
                        playerIsDefender[1] = true;

                        if (MainManager.levelCounter > 1 && MainManager.activePlayer == MainManager.raceWinner)
                        {
                            playerManagerScript.challengeWon = true;
                            playerManagerScript.challengeLost = false;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[0], -20, 20);
                        }
                        else if (MainManager.levelCounter > 1 && MainManager.activePlayer != MainManager.raceWinner)
                        {
                            playerManagerScript.challengeWon = false;
                            playerManagerScript.challengeLost = true;
                            playerManagerScript.stolenWin = true;                                                     
                        }
                    }

                    if (MainManager.playerNumber < 3)
                    {
                        MainManager.timeBattleSeconds = Math.Clamp(gaps[0], -20, 20);
                    }

                    break;

                case 2:
                    Debug.Log(MainManager.playerNames[i] + "has third place");
                    

                    if (MainManager.levelCounter < 2)
                    {
                        playerManagerScript.thirdLevel1 = i;
                    }                   
                    
                    if (MainManager.activePlayer == i)
                    {
                        MainManager.activePlayerWins = false;


                        if (MainManager.levelCounter > 1)
                        {
                            playerIsChallenger[2] = true;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[1], -20, 20);
                        }

                        if (MainManager.defendingPlayer == MainManager.raceWinner)

                        {
                            playerManagerScript.challengeLost = true;
                            playerManagerScript.challengeWon = false;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[1], -20, 20);
                        }

                        if (MainManager.defendingPlayer == finisherPos2)

                        {
                            playerManagerScript.challengeLost = true;
                            playerManagerScript.challengeWon = false;
                        }

                        else if (MainManager.defendingPlayer == finisherPos4 || MainManager.defendingPlayer == finisherPos5)
                        {
                            playerManagerScript.challengeLost = false;
                            playerManagerScript.challengeWon = true;
                        }                           
                            
                        
                    }
                    else if (MainManager.defendingPlayer == i && MainManager.levelCounter > 1)
                    {
                        playerIsDefender[2] = true;

                        if (MainManager.activePlayer == MainManager.raceWinner)
                        {
                            playerManagerScript.challengeWon = true;
                            playerManagerScript.challengeLost = true;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[1], -20, 20);
                        }

                        else if (MainManager.activePlayer == finisherPos2)
                        {
                            playerManagerScript.challengeWon = true;
                            playerManagerScript.challengeLost = false;

                        }

                        else if (MainManager.activePlayer == finisherPos4 || MainManager.defendingPlayer == finisherPos5)
                        {
                            playerManagerScript.challengeWon = false;
                            playerManagerScript.challengeLost = true;
                        }
                        
                        
                    }
                    break;

                case 3:
                    Debug.Log(MainManager.playerNames[i] + "has fourth place");
                                                        

                    if (MainManager.activePlayer == i)
                    {
                        MainManager.activePlayerWins = false;

                        if (MainManager.levelCounter > 1)
                        {
                            playerIsChallenger[3] = true;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[2], -20, 20);
                        }

                        if (MainManager.defendingPlayer == MainManager.raceWinner || MainManager.defendingPlayer == finisherPos2 || MainManager.defendingPlayer == finisherPos3)

                        {
                            playerManagerScript.challengeLost = true;
                            playerManagerScript.challengeWon = false;
                        }

                        else if (MainManager.defendingPlayer == finisherPos5)
                        {
                            playerManagerScript.challengeLost = false;
                            playerManagerScript.challengeWon = true;
                        }
                    }
                    else if (MainManager.defendingPlayer == i && MainManager.levelCounter > 1)
                    {
                        playerIsDefender[3] = true;

                        if (MainManager.activePlayer == MainManager.raceWinner)
                        {
                            playerManagerScript.challengeWon = true;
                            playerManagerScript.challengeLost = false;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[2], -20, 20);
                        }

                        else if (MainManager.activePlayer == finisherPos2 || MainManager.activePlayer == finisherPos3)
                        {
                            playerManagerScript.challengeWon = true;
                            playerManagerScript.challengeLost = false;

                        }

                        else if (MainManager.activePlayer == finisherPos5)
                        {
                            playerManagerScript.challengeWon = false;
                            playerManagerScript.challengeLost = true;
                        }
                    }
                    break;

                case 4:
                    Debug.Log(MainManager.playerNames[i] + "has fifth place");
                    
                    if (MainManager.activePlayer == i)
                    {
                        MainManager.activePlayerWins = false;

                        if (MainManager.levelCounter > 1)
                        {
                            playerIsChallenger[4] = true;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[3], -20, 20);
                            playerManagerScript.challengeLost = true;
                            playerManagerScript.challengeWon = false;
                        }                                            
                                                
                    }
                    else if (MainManager.defendingPlayer == i && MainManager.levelCounter > 1)
                    {
                        playerIsDefender[4] = true;

                        if (MainManager.activePlayer == MainManager.raceWinner)
                        {
                            playerManagerScript.challengeWon = true;
                            playerManagerScript.challengeLost = false;
                            MainManager.timeBattleSeconds = Math.Clamp(gaps[3], -20, 20);
                        }
                                                
                    }
                    break;

                case -1:

                    if (MainManager.activePlayer == i)
                    {
                        MainManager.activePlayerWins = false;
                                               
                        if(MainManager.levelCounter > 1 && MainManager.playerNumber > 2)
                        {
                            playerManagerScript.challengeLost = true;
                            playerManagerScript.challengeWon = false;
                            MainManager.timeBattleSeconds = 20;
                        }
                    }

                    else if
                        (MainManager.levelCounter > 1 && MainManager.playerNumber > 2)
                    {
                        if (MainManager.defendingPlayer == i)
                        {
                            if (MainManager.activePlayer == MainManager.raceWinner)
                            {
                                playerManagerScript.challengeWon = true;
                                playerManagerScript.challengeLost = false;
                                MainManager.timeBattleSeconds = 20;
                            }
                            else
                            {
                                playerManagerScript.challengeWon = true;
                                playerManagerScript.challengeLost = false;
                            }

                        }
                    }

                    playerNames.Add(MainManager.playerNames[i]);
                    carNames.Add("Unknown");
                    times.Add("DNF");
                    gaps.Add(99);

                    if (MainManager.playerNumber < 3)
                    {
                        MainManager.timeBattleSeconds = 20;
                    }                    
                    break;

            }

        if (resultsValid)
            { Debug.Log("All results validated");
                
            }
         
        }

        PopulateLeaderboard();
    }

    private void GetAllFinisherPositions()
    {
        for (int i = 0; i < MainManager.playerNumber; i++)
        {
            switch (playerNames.IndexOf(MainManager.playerNames[i]))
            {
                case 0:
                    MainManager.raceWinner = i;
                    if (MainManager.activePlayer == i)
                    {
                        switch (MainManager.playerNumber)
                        {
                            case 3:

                                Debug.Log("Gap Array contains " + gaps.Count + " values");
                                Debug.Log("Second gap value is " + gaps[1]);
                                
                                if (gaps[1] > 20)
                                {
                                    MainManager.changeValue = 20;
                                }
                                else
                                {
                                    MainManager.changeValue = gaps[1];
                                    Mathf.Clamp(changeValue, -20, 20);
                                }
                                break;
                            case 4:
                                if (gaps[2] > 20)
                                {
                                    MainManager.changeValue = 20;
                                }
                                else
                                {
                                    MainManager.changeValue = gaps[2];
                                    Mathf.Clamp(changeValue, -20, 20);
                                }
                                break;
                            case 5:
                                if (gaps[3] > 20)
                                {
                                    MainManager.changeValue = 20;
                                }
                                else
                                {
                                    MainManager.changeValue = gaps[3];
                                    Mathf.Clamp(changeValue, -20, 20);
                                }
                                break;
                        }
                                                
                    }
                    break;
                case 1:
                    finisherPos2 = i;
                    if (MainManager.activePlayer == i)
                    {
                        switch(MainManager.playerNumber)
                        {
                            case 3:
                                MainManager.changeValue = ((gaps[1] - gaps[0]) - gaps[0]);
                                MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                                break;

                            case 4:
                                MainManager.changeValue = ((gaps[2] - gaps[0]) - gaps[0]);
                                MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                                break;

                            case 5:
                                MainManager.changeValue = ((gaps[3] - gaps[0]) - gaps[0]);
                                MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                                break;
                        }
                        
                    }
                    break;
                case 2:
                    finisherPos3 = i;
                    if (MainManager.activePlayer == i)
                    {

                        switch(MainManager.playerNumber)
                        {
                            case 3:
                                MainManager.changeValue = -gaps[1];
                                MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                                break;

                            case 4:
                                MainManager.changeValue = ((gaps[2] - gaps[1]) - gaps[1]);
                                MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                                break;
                            case 5:
                                MainManager.changeValue = ((gaps[3] - gaps[1]) - gaps[1]);
                                MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                                break;

                        }
                                                
                    }
                    break;
                case 3:
                    finisherPos4 = i;
                    if (MainManager.activePlayer == i)

                        switch (MainManager.playerNumber)
                        {
                            case 4:
                                MainManager.changeValue = -gaps[2];
                                MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                                break;
                            case 5:
                                MainManager.changeValue = ((gaps[3] - gaps[2]) - gaps[2]);
                                MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                                break;
                        }
                                           
                    
                    break;
                case 4:
                    finisherPos5 = i;
                    if (MainManager.activePlayer == i)
                    {
                        MainManager.changeValue = -gaps[3];
                        MainManager.changeValue = Mathf.Clamp(changeValue, -20, 20);
                    }
                    break;
                case -1:
                    if (MainManager.activePlayer == i)
                    {
                        MainManager.changeValue = -20;
                    }
                    break;

            }

        }
    }

    public void SetAutoResultsValid()
    {
        MainManager.autoResultsValid = true;
    }

    public void SetAutoResultsInvalid()
    {
        MainManager.autoResultsValid = false;
    }

    


    public void LeaderboardClose()
    {
        ResultsPanelAuto.gameObject.SetActive(false);

        LeaderboardClear();
    }

    private void LeaderboardClear()
    {
        for (int i = 0; i < 5; i++)
        {

            LBplayers[i].text = "";
            LBcars[i].text = "";
            LBtimes[i].text = "";
        }

        for (int i = 0; i < 4; i++)
        {
            LBgaps[i].text = "";
        }

        Debug.Log("Leaderboard cleared");
                
    }

    public void SetNewFilePanelClose()
    {
        SetNewLogFilePanel.gameObject.SetActive(false);

    }

    private void ValidateButtonsChange()
    {
        validateButtonLevel1.gameObject.SetActive(false);
        validateButtonLevel2.gameObject.SetActive(true);

     }

    private void GetResultsButtonsChange()
    {
        getResultsLevel1Button.gameObject.SetActive(false);
        manualLevel1Button.gameObject.SetActive(false);

    }

    public void ActivateButtonToynopolyRound()
    {
        validateButtonLevel2.gameObject.SetActive(false);
        validateButtonToynopoly.gameObject.SetActive(true);
    }

    public void ResetButtonAfterToynopolyRound()
    {
        validateButtonToynopoly.gameObject.SetActive(false);
        validateButtonLevel2.gameObject.SetActive(true);
    }

    private void ValidateButtonsLevel3()
    {
        validateButtonLevel2.gameObject.SetActive(false);
        validateButtonLevel3.gameObject.SetActive(true);
    }
       
    public void RaceInProgessPanelClose()
    {
        raceInProgressPanel.gameObject.SetActive(false);
    }

    public void ChallengeRaceInProgessPanelClose()
    {
        raceInProgressPanelChallenge.gameObject.SetActive(false);
    }

    private void SetLeaderboardIcons()
    {
        for (int i = 0; i < playerNames.Count; i++)
            {
                defenderButtonsLB[i].gameObject.SetActive(false);
                challengerButtonsLB[i].gameObject.SetActive(false);

                if (MainManager.levelCounter > 1 && !MainManager.IsToynopolyBattle)
                {

                    if (playerIsChallenger[i])
                    {
                        challengerButtonsLB[i].gameObject.SetActive(true);
                    }

                    if (playerIsDefender[i])
                    {
                        defenderButtonsLB[i].gameObject.SetActive(true);
                    }
                }
            }
        
    }

    private void ResetChallengerDefenderButtonsArrays()
    {
        for (int i = 0; i < playerIsChallenger.Length; i++)
        {
            playerIsChallenger[i] = false;
            playerIsDefender[i] = false;
        }
    }


}
