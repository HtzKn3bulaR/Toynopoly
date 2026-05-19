using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    [SerializeField] private GameObject idleCountdown;

    private enum State
    {
        NewRound,
        FieldClicked,
        ReadyForRace,
        RaceConcluded,
        InTimeBattleWindow,
        SellingCars,
        ReadyForNextRound

    }

    private State state;

    private void Start()
    {        

        Instance = this;
        state = State.NewRound;

        OnlineManager.Instance.pendingFieldNetwork.OnValueChanged += SetFieldClickedState;
        PlayerManager3P.OnActivePlayerRaceStarted += PlayerManager3P_OnActivePlayerRaceStarted;
        PlayerManager3P.OnRaceConcluded += PlayerManager3P_OnRaceConcluded;
        PlayerManager3P.OnReadyForRoundChangeover += PlayerManager3P_OnReadyForRoundChangeover;
        PlayerManager3P.OnRoundChangeover += PlayerManager3P_OnRoundChangeover;

        idleCountdown.GetComponent<IdleCountdown>().OnCountdownExpired += IdleCountdown_OnCountdownExpired;
    }

    private void IdleCountdown_OnCountdownExpired(object sender, EventArgs e)
    {
        Debug.Log("Countdown Expired Event Received");

        switch (state)
        {
            case State.FieldClicked:
                if (PlayerManager3P.Instance.LocalIsActivePlayer())
                {
                    PlayerManager3P.Instance.StartRace();
                }
                break;

            case State.RaceConcluded:

                PlayerManager3P.Instance.ReadyForRoundChangeover();
                PlayerManager3P.Instance.ReInstateRows();
                break;

            default:

                break;
        }
    }
       

    private void PlayerManager3P_OnRoundChangeover()
    {
        state = State.NewRound;

        if(NetworkManager.Singleton.IsHost)
        OnlineManager.Instance.CheckAllPlayersSyncedToNewRoundRpc();
    }



    //PRE-RACE

   
    private void SetFieldClickedState(int previousValue, int newValue)
    {
        if (PlayerManager3P.Instance.LocalIsActivePlayer())
        {
            if (MainManager.levelCounter == 1)
            {
                if (state == State.NewRound)
                {
                    state = State.FieldClicked;
                    IdleCountdown.Instance.StartIdleCountdownMax(30f);
                }
            }

            else
                if (state == State.NewRound)
            {
                state = State.FieldClicked;                
            }




        }

        else
        {
            //Check if inactive player has empty inventory
            //->Disable Start Button

            //Else:
            state = State.ReadyForRace;
        }
    }

    private void PlayerManager3P_OnActivePlayerRaceStarted()
    {
        if (PlayerManager3P.Instance.LocalIsActivePlayer())
        {
            state = State.ReadyForRace;
            Debug.Log("State: Ready For Race");
            IdleCountdown.Instance.HideIdleCountdown();
        }
    }


    //POST-RACE

    private void PlayerManager3P_OnRaceConcluded()
    {
        if (MainManager.levelCounter == 1)
        {
            state = State.RaceConcluded;
            IdleCountdown.Instance.StartIdleCountdownMax(15f);
        }

        if(MainManager.levelCounter == 2 && MainManager.IsToynopolyBattle == false && PlayerManager3P.Instance.IsTimeBattleWinner())
        {
            state = State.InTimeBattleWindow;
        }
    }

    private void PlayerManager3P_OnReadyForRoundChangeover()
    {
        state = State.ReadyForNextRound;
        Debug.Log("State: Ready For Next Round");

        OnlineManager.Instance.ReportReadyForNextRoundToServerRpc();

        IdleCountdown.Instance.HideIdleCountdown();
    }

    public bool GetPlayerReadinessStatus() 
    {
        return (state == State.NewRound);          
    }

}
