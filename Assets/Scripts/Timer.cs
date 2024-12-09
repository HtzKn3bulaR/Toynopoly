using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;
    private DateTime dateTimeOfPause;
    

    private void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
            elapsedTime += Time.deltaTime;
            int hours = Mathf.FloorToInt(elapsedTime / 3600);
            int minutes = Mathf.FloorToInt(elapsedTime % 3600) / 60;
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
     }


    public void DisplayToggle(bool displayOn)
    {
               
        if (displayOn)
        {
            MainManager.matchTimeDisplayed = true;
            elapsedTime += (float)(DateTime.Now - dateTimeOfPause).TotalSeconds;
            Debug.Log("Elapsed Time" + elapsedTime);
        }

        else
        {
            MainManager.matchTimeDisplayed = false;
            dateTimeOfPause = DateTime.Now;
            Debug.Log("Elapsed Time" + elapsedTime);
        }
               
    }


    private void OnApplicationPause(bool isPaused)
    {
                     
        if(isPaused)

        {
            if (!MainManager.matchTimeDisplayed)

            {
                elapsedTime += (float)(DateTime.Now - dateTimeOfPause).TotalSeconds;
                Debug.Log("Elapsed Time" + elapsedTime);
            }

            dateTimeOfPause = DateTime.Now;
        }

        else

        {
            elapsedTime += (float)(DateTime.Now - dateTimeOfPause).TotalSeconds;
            Debug.Log("Elapsed Time" + elapsedTime);

            if (!MainManager.matchTimeDisplayed)

            {
                dateTimeOfPause = DateTime.Now;
            }
        }

    }

}
