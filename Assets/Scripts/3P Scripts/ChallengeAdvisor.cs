using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeAdvisor : MonoBehaviour
{

    [SerializeField] GameObject challengeAdvisorPanel;
    [SerializeField] TextMeshProUGUI defenderMessage;
    [SerializeField] Button acceptButton;

    private void Start()
    {
        OnlineManager.Instance.defenderPlayerIDNetworkVariable.OnValueChanged += SetDefenderLocally;

        PlayerManager3P.OnRoundChangeover += PlayerManager3P_OnRoundChangeover;
    }

    private void PlayerManager3P_OnRoundChangeover()
    {
        OnlineManager.Instance.ReportDefendingPlayerToNetworkRpc(9);
    }

    private void SetDefenderLocally(int previousValue, int newValue)
    {
        if (newValue != 9)
        {
            ulong defenderID = OnlineManager.Instance.GetPlayerID(MainManager.playerNames[newValue]);

            CallDefender(defenderID);
        }

    }

    private void CallDefender(ulong defenderID)
    {
        if (NetworkManager.Singleton.LocalClientId == defenderID)
        {
            ShowChallengeAdvisorPanel();
        }
    }

    private void ShowChallengeAdvisorPanel()
    {
        challengeAdvisorPanel.gameObject.SetActive(true);

        defenderMessage.text = "You have been challenged by " + MainManager.playerNames[MainManager.activePlayer] + " for your " + MainManager.cars[MainManager.currentCarIndex] + "! Finish ahead of "
            + MainManager.playerNames[MainManager.activePlayer] + " in the upcoming race to defend your car.";

    }

    public void HideChallengeAdvisorPanel()
    {
        challengeAdvisorPanel.gameObject.SetActive(false);
    }
}
