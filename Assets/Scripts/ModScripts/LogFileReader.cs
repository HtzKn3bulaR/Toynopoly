using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogFileReader : MonoBehaviour
{
    public string selectedFilePath;
    private bool CSVfileIsNew = true;

    private int cursorPos = 4;
    private int trackCursor;
    private int linesInCSV;

    private string[] lines;

    private string[] trackData;
    private string trackInfo;

    private List<string> resultLines = new List<string>();

    private List<string> resultPlayerNames = new List<string>();
    private List<string> resultTimes = new List<string>();
    private List<int> gaps = new List<int>();

    private List<string> stringsecs = new List<string>();
    private List<string> stringmins = new List<string>();

    private List<int> seconds = new List<int>();
    private List<int> minutes = new List<int>();

    [SerializeField] private CSVFileSelector fileSelectorScript;

    [SerializeField] private SessionManager sessionManager;

    private List<string> validLineMarkers = new List<string> { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16" };

    public static event Action OnRaceResultsCalculated;


    // Start is called before the first frame update
    void Start()
    {
        
        if (selectedFilePath != null)
        {
            selectedFilePath = MainManager.selectedFilePath;
        }

        else
            Debug.Log("Error: Session Log File Not Set!");

        //SessionManager.OnRoundComplete += MoveCursorPosAfterSuccessfulRead;

    }
        

    public void ReadCSVFileCurrentRound()
    {
        if (MainManager.selectedFilePath != null)
        {

            lines = File.ReadAllLines(MainManager.selectedFilePath);


            if (CSVfileIsNew)
            {
                cursorPos = 4;
                trackCursor = 2;
                CSVfileIsNew = false;
            }

            else
            {
                trackCursor = cursorPos - 2;
                Debug.Log("Track cursor set to " + trackCursor);
            }

            Debug.Log("Number of lines in file " + lines.Length);
            linesInCSV = lines.Length;

            resultLines.Clear();
            Debug.Log("Cursor position before reading" + cursorPos);
            Debug.Log("Track Cursor before reading" + trackCursor);

            if (cursorPos > lines.Length)
            {
                Debug.Log("No more session data found in file");
                resultPlayerNames.Clear();
                resultTimes.Clear();
                gaps.Clear();

                //setupPanel.gameObject.SetActive(true);
                fileSelectorScript.GetAllCSVFiles();

                CSVfileIsNew = true;

            }

            else
            {

                CheckForValidLines();

            }

        }

        else
        {
            //setupPanel.gameObject.SetActive(true);

        }


    }

    private void CheckForValidLines()
    {

        resultPlayerNames.Clear();
        resultTimes.Clear();

        char[] chars = { '"', '#' };
        bool checkOK = true;

        string[] lineMarkers = lines[cursorPos].Split(",");

        Debug.Log(lineMarkers[0]);

        if (lineMarkers[0].Trim(chars) == "01")
        {
            Debug.Log("Valid race result set found!");
            checkOK = true;
        }

        else
        {
            Debug.Log("Line Invalid! Looking Up next line");
            checkOK = false;
        }


        switch (checkOK)
        {
            case true:

                int linesLeftInFile = linesInCSV - cursorPos;
                Debug.Log("Lines Left in File " + linesLeftInFile);
                int scope = 16;

                if (linesLeftInFile < scope)
                {
                    scope = linesLeftInFile;
                }

                for (int i = cursorPos; i < cursorPos + scope; i++)
                {

                    if (i <= linesInCSV)
                    {

                        string[] lineSet = lines[i].Split(",");

                        //Read Track Line
                        trackData = lines[trackCursor].Split(",");





                        if ("Session".Contains(lineSet[0].Trim(chars)))
                        {
                            Debug.Log("End of Results Set Detected");
                            break;

                        }


                        else if (validLineMarkers.Contains(lineSet[0].Trim(chars)))

                        {
                            resultLines.Add(lines[i]);

                        }
                    }


                }



                foreach (string s in resultLines)
                {
                    Debug.Log(s);
                }


                trackInfo = trackData[1].Trim(chars);
                Debug.Log("Results for race on track " + trackInfo);

                for (int i = 0; i < resultLines.Count; i++)

                {
                    string[] lineData = resultLines[i].Split(",");

                    resultPlayerNames.Add(lineData[1].Trim(chars));
                    resultTimes.Add(lineData[3].Trim(chars));
                    Debug.Log("Line " + i + "was read");
                    Debug.Log(resultPlayerNames[i]);

                }
                CleanCSVNames();
                ExtractSeconds();
                break;

            case false:
                cursorPos++;
                ReadCSVFileCurrentRound();
                break;
        }

    }


    void CleanCSVNames()
    {
        for (int i = 0; i < resultPlayerNames.Count; i++)
        {
            resultPlayerNames[i] = resultPlayerNames[i].TrimEnd(new char[] { '\r', ' ' });
            resultPlayerNames[i] = resultPlayerNames[i].TrimStart(new char[] { '\r', ' ' });
            resultPlayerNames[i] = resultPlayerNames[i].ToUpper();
            Debug.Log(resultPlayerNames[i]);

        }
    }


    void ExtractSeconds()
    {

        stringsecs.Clear();
        stringmins.Clear();

        seconds.Clear();
        minutes.Clear();

        for (int i = 0; i < resultTimes.Count; i++)
        {
            string[] timeElements = resultTimes[i].Split(":");
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
            if (i > 0)
            {
                if (minutes[i] > minutes[0])
                {
                    gaps.Add((minutes[i] - minutes[0]) * 60 + (seconds[i] - seconds[0]));
                }

                else if (minutes[i] == minutes[0] || minutes[i] < minutes[0])
                {
                    gaps.Add(seconds[i] - seconds[0]);
                }
            }
        }

        SendResultsToMainManager();

    }


    void SendResultsToMainManager()
    {
              
        sessionManager.UpdateRosterList(resultPlayerNames);                    
             
        OnRaceResultsCalculated?.Invoke();

    }



    public void MoveCursorPosAfterSuccessfulRead()
    {

        cursorPos += (resultLines.Count);
        cursorPos += 2;

        trackCursor += (resultLines.Count);
        trackCursor += 2;

        Debug.Log("Cursor position after reading " + cursorPos);

    }


    public void SetupPanelShow()
    {
        //setupPanel.gameObject.SetActive(true);
    }

    public void SetNewLogFile()
    {
        CSVfileIsNew = true;
    }

} 

