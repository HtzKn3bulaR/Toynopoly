using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;



public class EmptyInventoryHandler : MonoBehaviour
{
    public static EmptyInventoryHandler Instance;

    [SerializeField] Button[] emptyInvNameButtons;
    [SerializeField] Button[] emptyInvPrizeButtons;
    [SerializeField] Button spectate;

    [SerializeField] TextMeshProUGUI[] emptyInvPanelNameDisplay;
    [SerializeField] TextMeshProUGUI[] emptyInvPanelPrizeDisplay;
    [SerializeField] TextMeshProUGUI buyerNameDisplay;

    [SerializeField] TextMeshProUGUI firstinactivePlayerName;
    [SerializeField] TextMeshProUGUI secondinactivePlayerName;
    [SerializeField] TextMeshProUGUI thirdinactivePlayerName;
    [SerializeField] TextMeshProUGUI fourthinactivePlayerName;
    [SerializeField] TextMeshProUGUI activePlayerName;

    public GameObject emptyInvDialoguePanel;
    [SerializeField] GameObject emptyInvBuyerDisplay;
    [SerializeField] GameObject buyOptionPanelAfterForcedBuy;
    [SerializeField] GameObject nextRacePanel;
    public GameObject notEnoughCashPanel;

    private PlayerManager3P gameManagerScript;

    public static event Action OnPlayerHasEmptyInventory;
    public static event Action OnPlayerHasMadeForcedPurchase;
    
    private bool[] buyingPossible = { true, true, true, true, true, true };

    private bool[] wantsToBuy = { false, false, false, false, false };


    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();
    }

    private void Start()
    {
        Instance = this;

        PlayerManager3P.OnActivePlayerCalled += CheckInactivePlayersInventory;
    }

    public void CheckInactivePlayersInventory()
    {
        if (MainManager.levelCounter == 2)
        {
            int myIndex = 9;

            for (int i = 0; i < MainManager.playerNumber; i++)
            {
                if (MainManager.localMultiplayerName == MainManager.playerNames[i])
                {
                    myIndex = i;
                }
            }
                       
            int inventoryCount = 0;

            for (int i = 0; i < MainManager.cars.Length; i++)
                {
                    if (MainManager.playerInventory[myIndex, i] > 0)
                    {
                        inventoryCount++;
                    }
                }

                if (inventoryCount == 0 && myIndex != MainManager.activePlayer && !MainManager.spectator[myIndex])
                {
                    EmptyInventoryProcedure(myIndex);
                }
            
        }
    }

    public void EmptyInventoryProcedure(int emptyPlayer)    
    {
        OnlineManager.Instance.ReportEmptyInventoryToClientsRpc(emptyPlayer);
        IdleCountdown.Instance.StartIdleCountdownMax(60f);
        PlayerManager3P.Instance.SetPromptText("Your Inventory is Empty!");
        OnPlayerHasEmptyInventory?.Invoke();

        MainManager.buyer = emptyPlayer;

        CheckBuyOptions();
                       
        UpdateDisplays();

        emptyInvDialoguePanel.gameObject.SetActive(true);

        for (int i = 0; i < MainManager.cars.Length; i++)

        {
            if (!buyingPossible[i])

            {
                emptyInvNameButtons[i].gameObject.SetActive(false);
                emptyInvPrizeButtons[i].gameObject.SetActive(false);

            }
        }
    }


    public void CheckBuyOptions ()
    {
        for (int i = 0; i < MainManager.cars.Length; i++)

        {
            int numberOfOwners = 0;

            for (int j = 0; j < MainManager.playerNumber; j++)

            { if (MainManager.playerInventory[j, i] > 0)

                { 
                    numberOfOwners++;
                }
            }

            if (numberOfOwners == 1 && MainManager.protection[i] || MainManager.fieldsLeftForCar[i] < 1 || MainManager.DefProcedureCompleted[i] || MainManager.carPrizes[i] > MainManager.playerCash[MainManager.buyer])

            { buyingPossible[i] = false; }
        }
    }

    public void UpdateDisplays()
    {
        buyerNameDisplay.text = MainManager.playerNames[MainManager.buyer];

        for (int i = 0; i < MainManager.cars.Length; i++)
        {
            emptyInvPanelNameDisplay[i].text = ($"{MainManager.cars[i]}");
            emptyInvPanelPrizeDisplay[i].text = ($"{MainManager.carPrizes[i]}");                      
        }
    }

    public void BuyCar(int car)
    {
        if (MainManager.playerCash[MainManager.buyer] < MainManager.carPrizes[car])
        {
            notEnoughCashPanel.SetActive(true);
            emptyInvDialoguePanel.SetActive(false);
            nextRacePanel.SetActive(false);
            return;
        }
        else
        {
            OnlineManager.Instance.ReportForcedCarPurchaseToClientsRpc(car, MainManager.buyer);
            IdleCountdown.Instance.HideIdleCountdown();
            OnPlayerHasMadeForcedPurchase?.Invoke();
                        
        }
    }

    public void Spectate()
    {
        emptyInvDialoguePanel.SetActive(false);
        MainManager.spectator[MainManager.buyer] = true;
        gameManagerScript.PerformLevel2Check();
        IdleCountdown.Instance.HideIdleCountdown();
    }

    /*
    void OfferBuyOption()
    {
        buyOptionPanelAfterForcedBuy.SetActive(true);

        firstinactivePlayerName.text = MainManager.playerNames[MainManager.inactivePlayers[0]];
        secondinactivePlayerName.text = MainManager.playerNames[MainManager.inactivePlayers[1]];
        activePlayerName.text = MainManager.playerNames[MainManager.activePlayer];

        if (MainManager.playerNumber > 3)
        {
            thirdinactivePlayerName.text = MainManager.playerNames[MainManager.inactivePlayers[2]];
        }

        if (MainManager.playerNumber > 4)

        {
            fourthinactivePlayerName.text = MainManager.playerNames[MainManager.inactivePlayers[3]];
        }

    }

    public void AcceptBuyOption(int whichPlayer)

    {
        if (wantsToBuy[whichPlayer] == false)

        { wantsToBuy[whichPlayer] = true; }

        else

        { wantsToBuy[whichPlayer] = false; }


    }


    public void ConcludeBuyOption()

    {
        for (int i = 0; i < MainManager.playerNumber - 1; i++)

        {
            if (wantsToBuy[i] == true)

            {
                MainManager.playerInventory[MainManager.inactivePlayers[i], carBought]++;
                MainManager.playerCash[MainManager.inactivePlayers[i]] -= MainManager.carPrizes[carBought];

            }

        }

        switch (MainManager.playerNumber)

        {
            case 3:
                if (wantsToBuy[2] == true)

                {
                    MainManager.playerInventory[MainManager.activePlayer, carBought]++;
                    MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[carBought];

                }
                break;

            case 4:

                if (wantsToBuy[3] == true)

                {
                    MainManager.playerInventory[MainManager.activePlayer, carBought]++;
                    MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[carBought];
                }
                break;

            case 5:

                if (wantsToBuy[4] == true)

                {
                    MainManager.playerInventory[MainManager.activePlayer, carBought]++;
                    MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[carBought];
                }
                break;

        }

        

        gameManagerScript.UpdateInventoryDisplay();
        gameManagerScript.UpdateCashDisplay();

        buyOptionPanelAfterForcedBuy.SetActive(false);

    }
    */


    public void HideEmptyInventoryPanel()
    {
        emptyInvDialoguePanel.SetActive(false);
    }

    public void NotEnoughCashPanelClose()
    {
        notEnoughCashPanel.gameObject.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
