using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IdleCountdown : MonoBehaviour
{
    public static IdleCountdown Instance;

    [SerializeField] private GameObject idleCountdownPanel;
    [SerializeField] private Image idleCountdownImage;
    

    private float timerMax;
    private float timerCount;

    public event EventHandler OnCountdownExpired; 

    //public class CountdownEventArgs : EventArgs { }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
        timerMax = 999;                

        OnCountdownExpired += IdleCountdown_OnCountdownExpired;
    }

    private void IdleCountdown_OnCountdownExpired(object sender, EventArgs e)
    {
        Debug.Log("Countdown Expired - Event Invoked");
        idleCountdownPanel.SetActive(false);
        timerCount = 0;
    }
       

    // Update is called once per frame
    void Update()
    {
        timerCount += Time.deltaTime;

        idleCountdownImage.fillAmount = GetIdleCountdownNormalized();

        if(GetIdleCountdownNormalized() <= 0)
        {
            OnCountdownExpired?.Invoke(this, EventArgs.Empty);
            timerCount = 0;
        }

    }

    public float GetIdleCountdownNormalized()
    {
        return 1 - (timerCount /  timerMax);
    }

    public void StartIdleCountdownMax(float timeLimit)
    {
       timerMax = timeLimit;
       timerCount = 0;

        idleCountdownPanel.SetActive(true);
    }

    public void HideIdleCountdown()
    {
        idleCountdownPanel.SetActive(false);
        timerMax = 999;
    }


}
