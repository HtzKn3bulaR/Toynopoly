using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseHandler : MonoBehaviour
{
    [SerializeField] private GameObject reverseOptionPanel;

    [SerializeField] private GameObject[] reverseIcons;

    [SerializeField] private GameObject reverseButton;

    private int reverseIconsRemaining = 3;

    // Start is called before the first frame update
    void Start()
    {
        OnlineManager.Instance.pendingFieldNetwork.OnValueChanged += OnPendingFieldChanged;
    }

    private void OnPendingFieldChanged(int previousValue, int newValue)
    {
        Debug.Log("Track is " + GameManager.Instance.ReturnSelectedTrack());
        Debug.Log("Reverse Icons Remaining " + reverseIconsRemaining);
        Debug.Log("Track Has Reversed Version " + GridGenerator.Instance.TrackHasReverseVersion(GameManager.Instance.ReturnSelectedTrack()));

        if(GridGenerator.Instance.TrackHasReverseVersion(GameManager.Instance.ReturnSelectedTrack()) && reverseIconsRemaining > 0)
        {
            ShowReversePanel();            
        }

        else
            HideReversePanel();
    }
       

    public void ShowReversePanel()
    {
        reverseOptionPanel.SetActive(true);

        if(GameManager.Instance.LocalIsActivePlayer())
        {
            reverseButton.SetActive(true);
        }
        else
        {
            reverseButton.SetActive(false);
        }

    }

    public void HideReversePanel()
    {
        reverseOptionPanel.SetActive(false);
    }

    public void UseReverseIcon()
    {
        if (GameManager.Instance.LocalIsActivePlayer())
        {
            MainManager.nextTrackReverse = true;
            ReportReverseTrackToServer();
            RemoveIcon();
            reverseButton.SetActive(false);
            Debug.Log("Next Track Reversed");
        }
    }

    public void RemoveIcon()
    {
        reverseIcons[reverseIconsRemaining - 1].gameObject.SetActive(false);
        reverseIconsRemaining--;
        Debug.Log("Reverse Icons Remaining " + reverseIconsRemaining);
    }

    private void ReportReverseTrackToServer()
    {
        if(GameManager.Instance.LocalIsActivePlayer())
        OnlineManager.Instance.ReportReverseTrackToAllPlayersRpc();
    }
}
