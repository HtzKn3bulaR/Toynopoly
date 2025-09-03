using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LapDataReader : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI lapDisplay;
    [SerializeField] TextMeshProUGUI lapDisplayChallenge;

    private int carClass = 9;
    public TextAsset lapCountFile;
    private int tableSize = -1;

    private int currentLapCount = 99;

    [System.Serializable]

    public class LapCount
    {
        public string track;
        public int lapsRookie;
        public int lapsAmateur;
        public int lapsAdvanced;
        public int lapsSemiPro;
        public int lapsPro;
        public int lapsSuperPro;
    }

    [System.Serializable]
    public class LapDataList
    {
        public LapCount[] laps;
    }

    public LapDataList lapsList = new LapDataList();

    // Start is called before the first frame update
    void Start()
    {
        ReadLapsFile();
        carClass = MainManager.classSelected;
    }


    void ReadLapsFile()
    {
        string[] data = lapCountFile.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        tableSize = data.Length / 7 - 1;

        lapsList.laps = new LapCount[tableSize];


        for (int i = 0; i < tableSize; i++)
        {
            lapsList.laps[i] = new LapCount();

            lapsList.laps[i].track = data[7 * (i + 1)];
            lapsList.laps[i].lapsRookie = int.Parse(data[7 * (i + 1) + 1]);
            lapsList.laps[i].lapsAmateur = int.Parse(data[7 * (i + 1) + 2]);
            lapsList.laps[i].lapsAdvanced = int.Parse(data[7 * (i + 1) + 3]);
            lapsList.laps[i].lapsSemiPro = int.Parse(data[7 * (i + 1) + 4]);
            lapsList.laps[i].lapsPro = int.Parse(data[7 * (i + 1) + 5]);
            lapsList.laps[i].lapsSuperPro = int.Parse(data[7 * (i + 1) + 6]);
        }
    }

    public void FindLapData(string trk)
    {
        string trackSelected = trk;

        Debug.Log(trackSelected);
        Debug.Log("Table size is " + tableSize);

        int index = -1;

        for (int i = 0; i < tableSize; i++)
        {

            Debug.Log(lapsList.laps[i].track);


            if(lapsList.laps[i].track.Equals(trackSelected, StringComparison.InvariantCultureIgnoreCase))
                
            {
                index = i;
                
            }


        }

        if (index == -1)
        {
            currentLapCount = 99;
        }

        else
        {
            switch (carClass)
            {
                case 0:
                    currentLapCount = lapsList.laps[index].lapsRookie;
                    break;

                case 1:
                    currentLapCount = lapsList.laps[index].lapsAmateur;
                    break;

                case 2:
                    currentLapCount = lapsList.laps[index].lapsAdvanced;
                    break;
                case 3:
                    currentLapCount = lapsList.laps[index].lapsSemiPro;
                    break;
                case 4:
                    currentLapCount = lapsList.laps[index].lapsPro;
                    break;
                case 5:
                    currentLapCount = lapsList.laps[index].lapsSuperPro;
                    break;

            }

        }

        Debug.Log("Lap Count is " + currentLapCount);
        Debug.Log("Index is " + index);

        ShowLapData();

    }


    private void ShowLapData()
    {
        lapDisplay.text = currentLapCount.ToString();

        if (MainManager.playerNumber > 2)
        {
            lapDisplayChallenge.text = currentLapCount.ToString();
        }
    }

    
}
