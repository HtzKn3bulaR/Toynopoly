using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;


public class CSVFileReader : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI[] LBplayers;
    [SerializeField] TextMeshProUGUI[] LBcars;
    [SerializeField] TextMeshProUGUI[] LBtimes;
    [SerializeField] TextMeshProUGUI[] LBgaps;

    [SerializeField] GameObject ResultsPanelAuto;

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
        

    void Start()
    {
        selectedFilePath = MainManager.selectedFilePath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadCSVFileCurrentRound()
    {
        string[] lines = File.ReadAllLines(selectedFilePath);

        Debug.Log(lines.Length);

        resultLines.Clear();
        Debug.Log("Cursor position before reading" + cursorPos);

        for (int i = cursorPos; i < (cursorPos + MainManager.playerNumber); i++)
        {

            resultLines.Add(lines[i]);

        }

        cursorPos += (MainManager.playerNumber);
        cursorPos += 3;
        Debug.Log("Cursor position after reading " + cursorPos);

        ExtractResultsData();
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

            
            playerNames.Add(lineData[1].Trim(chars));
            carNames.Add(lineData[2].Trim(chars));
            times.Add(lineData[3].Trim(chars));
                      
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

            Debug.Log(minutes[i]);
            Debug.Log(seconds[i]);

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
                    if ((((minutes[i] - minutes[0]) * 60 ) + (seconds[i] - seconds[0])) > 20)
                    {
                        gaps.Add(20);
                    }

                    else
                    {
                        gaps.Add((minutes[i] - minutes[0]) * 60  + (seconds[i] - seconds[0]));
                    
                    }
                    
                }
                else if ((seconds[i] - seconds[0]) > 20)

                {
                    gaps.Add(20);
                }

                else
                {
                    gaps.Add(seconds[i] - seconds[0]);
                }
            }
        }
    }

    public void PopulateLeaderboard()
    {

        ResultsPanelAuto.gameObject.SetActive(true);

        for (int i = 0; i < MainManager.playerNumber; i++)
        {
            
            LBplayers[i].text = playerNames[i].ToString();
            LBcars[i].text = carNames[i].ToString();
            LBtimes[i].text = times[i].ToString();
            
        }

        for (int i = 0; i < MainManager.playerNumber -1; i++)
        {
            LBgaps[i].text = gaps[i].ToString();
        }

        ResultsValidate();
    }

    void ResultsValidate()
    {
        bool resultsValid = true;
           
        for (int i = 0; i < playerNames.Count; i++)
        {

            switch (playerNames.IndexOf(MainManager.playerNames[i]))

                {
                case 0:
                    Debug.Log(MainManager.playerNames[i] + " is the winner");
                    if (MainManager.activePlayer == i)
                        MainManager.activePlayerWins = true;
                    break;

                case 1:
                    Debug.Log(MainManager.playerNames[i] + "has second place");
                    if (MainManager.activePlayer == i)
                        MainManager.activePlayerWins = false;
                    break;

                case 2:
                    Debug.Log(MainManager.playerNames[i] + "has third place");
                    break;

                case -1:
                    Debug.Log("Error: Player not found! Results not valid!");
                    resultsValid = false;
                    MainManager.autoResultsValid = false;
                    break;

            }

        if (resultsValid)
            { Debug.Log("All results validated");
                MainManager.autoResultsValid = true;
                  
            }


        }
        
    }

    


    public void LeaderboardClose()
    {
        ResultsPanelAuto.gameObject.SetActive(false);
    }

}
