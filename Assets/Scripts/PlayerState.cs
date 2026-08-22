using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    [SerializeField] private GameObject idleCountdown;

    private enum State
    {
        NewRound,
        FieldClicked,
        Buying,
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
        PlayerManager3P.OnActivePlayerHasBoughtCar += PlayerManager3P_OnActivePlayerHasBoughtCar;
        PlayerManager3P.OnInactivePlayersHaveBuyOption += PlayerManager3P_OnInactivePlayersHaveBuyOption;
        PlayerManager3P.OnPlayerHasDecidedBuyOption += PlayerManager3P_OnPlayerHasDecidedBuyOption;
        EmptyInventoryHandler.OnPlayerHasEmptyInventory += EmptyInventoryHandler_OnPlayerHasEmptyInventory;
        EmptyInventoryHandler.OnPlayerHasMadeForcedPurchase += EmptyInventoryHandler_OnPlayerHasMadeForcedPurchase;
        SellingHandlerP3.OnCarSold += SellingHandlerP3_OnCarSold;

        PlayerManager3P.OnStartSellingRound += PlayerManager3P_OnStartSellingRound;

        OnlineManager.Instance.skippingRoundNetworkVariable.OnValueChanged += TriggerRoundSkipOnClients;

        idleCountdown.GetComponent<IdleCountdown>().OnCountdownExpired += IdleCountdown_OnCountdownExpired;
    }

    private void TriggerRoundSkipOnClients(bool previousValue, bool newValue)
    {
        if(newValue == true)
        {
            if(!NetworkManager.Singleton.IsHost)
            {
                SkipPlayerRpc();
            }

            if (NetworkManager.Singleton.IsHost)
            {
                OnlineManager.Instance.SkippingRoundNetworkVariableChangeRpc(false);
            }
        }

        if (newValue == false)
        {
            return;
        }
    }

    private void EmptyInventoryHandler_OnPlayerHasMadeForcedPurchase()
    {
        state = State.ReadyForRace;
    }

    private void EmptyInventoryHandler_OnPlayerHasEmptyInventory()
    {
        state = State.Buying;
    }

    private void PlayerManager3P_OnPlayerHasDecidedBuyOption()
    {
        state = State.ReadyForRace;
        Debug.Log("State : Ready For Race");
    }

    private void PlayerManager3P_OnInactivePlayersHaveBuyOption()
    {
        if (MainManager.levelCounter == 2)
        {
            if (!PlayerManager3P.Instance.LocalIsActivePlayer())
            {
                state = State.Buying;
                Debug.Log("State: Buying");
            }
                        
        }
    }

    private void PlayerManager3P_OnActivePlayerHasBoughtCar()
    {
        if(MainManager.levelCounter == 2)
        {
            if(PlayerManager3P.Instance.LocalIsActivePlayer())
            {
                state = State.Buying;
                Debug.Log("State: Buying");
            }
        }
    }

    private void IdleCountdown_OnCountdownExpired(object sender, EventArgs e)
    {
        Debug.Log("Countdown Expired Event Received");

        switch (state)
        {
            case State.NewRound:
                if(PlayerManager3P.Instance.LocalIsActivePlayer())
                {
                    PlayerManager3P.Instance.UnlockFields();
                }
                break;

            case State.FieldClicked:
                if (PlayerManager3P.Instance.LocalIsActivePlayer())
                {
                    PlayerManager3P.Instance.StartRace();
                }
                break;

            case State.RaceConcluded:

                PlayerManager3P.Instance.ReadyForRoundChangeover();
                PlayerManager3P.Instance.ReInstateRows();
                PlayerManager3P.Instance.HideProtectionOptionPanel();
                IdleCountdown.Instance.HideIdleCountdown();
                break;

            case State.Buying:
                if (PlayerManager3P.Instance.LocalIsActivePlayer())
                {
                    IdleCountdown.Instance.HideIdleCountdown();
                    PlayerManager3P.Instance.UnlockFields();
                    state = State.NewRound;
                    PlayerManager3P.Instance.SetPromptText("Select a field to start the next race");
                }
                
                if (!PlayerManager3P.Instance.LocalIsActivePlayer())
                {
                    state = State.ReadyForRace;
                    PlayerManager3P.Instance.SetPromptText("Waiting for next race");

                    if (MainManager.levelCounter == 2)
                    {
                        EmptyInventoryHandler.Instance.Spectate();
                        PlayerManager3P.Instance.PassBuyOption();
                    }

                }
                break;

            case State.InTimeBattleWindow:
                {
                    state = State.ReadyForNextRound;
                    PlayerManager3P.Instance.HideTimeBattleWindow();
                    PlayerManager3P.Instance.ReadyForRoundChangeover();
                }
                break;

            case State.SellingCars:
                {
                    state = State.ReadyForNextRound;
                    Debug.Log("State: Ready For Next Round");

                    OnlineManager.Instance.ReportReadyForNextRoundToServerRpc();

                    IdleCountdown.Instance.HideIdleCountdown();

                    SellingHandlerP3.Instance.HideSellingDialoguePanel();

                }
                break;

            case State.ReadyForRace:
                IdleCountdown.Instance.HideIdleCountdown();
                break;


            default:

                IdleCountdown.Instance.HideIdleCountdown();

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

            if (MainManager.levelCounter == 2)
            {
                if (state == State.NewRound)
                {
                    state = State.FieldClicked;
                }                
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

        if(MainManager.levelCounter == 2 && MainManager.IsToynopolyBattle == false && PlayerManager3P.Instance.IsTimeBattleWinner() == true)
        {
            state = State.InTimeBattleWindow;
        }

        if (MainManager.levelCounter == 2 && MainManager.IsToynopolyBattle == false && PlayerManager3P.Instance.IsTimeBattleWinner() == false)
        {
            state = State.RaceConcluded;
        }
    }

    private void PlayerManager3P_OnStartSellingRound()
    {
        state = State.SellingCars;

        PlayerManager3P.Instance.SetPromptText("Players Are Selling Cars");

        PlayerManager3P.Instance.ShowPreSellingPanel();
               
    }

    private void SellingHandlerP3_OnCarSold()
    {
        state = State.ReadyForNextRound;
        Debug.Log("State: Ready For Next Round");

        OnlineManager.Instance.ReportReadyForNextRoundToServerRpc();

        IdleCountdown.Instance.HideIdleCountdown();
    }


    private void PlayerManager3P_OnReadyForRoundChangeover()
    {
        state = State.ReadyForNextRound;
        PlayerManager3P.Instance.HideNextRaceComingUpPanel();
        Debug.Log("State: Ready For Next Round");
        PlayerManager3P.Instance.SetPromptText("Waiting for round to conclude");

        OnlineManager.Instance.ReportReadyForNextRoundToServerRpc();

        IdleCountdown.Instance.HideIdleCountdown();
    }

    public bool GetPlayerReadinessStatus() 
    {
        return (state == State.NewRound);          
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void SkipPlayerRpc()
    {
        if(NetworkManager.Singleton.IsHost)
        {            
            OnlineManager.Instance.SkippingRoundNetworkVariableChangeRpc(true);
        }

        switch (state)
        { case State.NewRound:
                PlayerManager3P.Instance.ReadyForRoundChangeover();
                break;

            case State.FieldClicked:
                PlayerManager3P.Instance.ReadyForRoundChangeover();
                break;

            case State.Buying:
                if (PlayerManager3P.Instance.LocalIsActivePlayer())
                {
                    IdleCountdown.Instance.HideIdleCountdown();
                    PlayerManager3P.Instance.ReadyForRoundChangeover();
                }
                else
                {
                    PlayerManager3P.Instance.ReadyForRoundChangeover();
                    PlayerManager3P.Instance.PassBuyOption();
                }
                    break;

            case State.RaceConcluded:                
                PlayerManager3P.Instance.ReInstateRows();
                PlayerManager3P.Instance.HideProtectionOptionPanel();
                IdleCountdown.Instance.HideIdleCountdown();
                PlayerManager3P.Instance.ReadyForRoundChangeover();
                break;

            case State.InTimeBattleWindow:
                PlayerManager3P.Instance.HideTimeBattleWindow();
                IdleCountdown.Instance.HideIdleCountdown();
                PlayerManager3P.Instance.ReadyForRoundChangeover();
                break;

            case State.SellingCars:
                Debug.Log("State: Ready For Next Round");
                SellingHandlerP3.Instance.HideSellingDialoguePanel();

                PlayerManager3P.Instance.HidePreSellingPanel();
                IdleCountdown.Instance.HideIdleCountdown();
                PlayerManager3P.Instance.ReadyForRoundChangeover();
                break;

            default:
                PlayerManager3P.Instance.ReadyForRoundChangeover();
                break;
        }
    }

}
