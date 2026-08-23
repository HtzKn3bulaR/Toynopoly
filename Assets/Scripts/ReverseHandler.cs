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
        Debug.Log("Reverse Icons Remaining " + reverseIconsRemaining);

        if (MainManager.playerNumber < 3)
        {

            if (GridGenerator.Instance.TrackHasReverseVersion(GameManager.Instance.ReturnSelectedTrack()) && reverseIconsRemaining > 0)
            {
                ShowReversePanel();
            }

            else
                HideReversePanel();
        }

        if (MainManager.playerNumber > 2)
        {
            if (GridGenerator3P.Instance.TrackHasReverseVersion(PlayerManager3P.Instance.ReturnSelectedTrack()) && reverseIconsRemaining > 0)
            {
                ShowReversePanel();
            }

            else
                HideReversePanel();
        }

    }
       

    public void ShowReversePanel()
    {
        reverseOptionPanel.SetActive(true);

        if (MainManager.playerNumber < 3)
        {

            if (GameManager.Instance.LocalIsActivePlayer())
            {
                reverseButton.SetActive(true);
            }
            else
            {
                reverseButton.SetActive(false);
            }
        }

        if (MainManager.playerNumber > 2)
        {

            if (PlayerManager3P.Instance.LocalIsActivePlayer())
            {
                reverseButton.SetActive(true);
            }
            else
            {
                reverseButton.SetActive(false);
            }
        }

    }

    public void HideReversePanel()
    {
        reverseOptionPanel.SetActive(false);
    }

    public void UseReverseIcon()
    {
        if (MainManager.playerNumber < 3)
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

        if (MainManager.playerNumber > 2)
        {
            if (PlayerManager3P.Instance.LocalIsActivePlayer())
            {
                MainManager.nextTrackReverse = true;
                ReportReverseTrackToServer();
                RemoveIcon();
                reverseButton.SetActive(false);
                Debug.Log("Next Track Reversed");
            }
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
        if (MainManager.playerNumber < 3)
        {
            if (GameManager.Instance.LocalIsActivePlayer())
                OnlineManager.Instance.ReportReverseTrackToAllPlayersRpc();
        }

        if (MainManager.playerNumber > 2)
        {
            if (PlayerManager3P.Instance.LocalIsActivePlayer())
                OnlineManager.Instance.ReportReverseTrackToAllPlayersRpc();
        }
    }
}
