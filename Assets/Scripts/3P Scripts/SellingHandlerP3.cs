using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SellingHandlerP3 : MonoBehaviour
{
    public static SellingHandlerP3 Instance;

    [SerializeField] Button[] nameButtons;
    [SerializeField] Button[] prizeButtons;
    [SerializeField] Button[] inventoryButtons;
       
    [SerializeField] TextMeshProUGUI[] sellPanelNameDisplay;
    [SerializeField] TextMeshProUGUI[] sellPanelPrizeDisplay;
    [SerializeField] TextMeshProUGUI[] sellPanelInventoryDisplay;
    [SerializeField] TextMeshProUGUI sellerNameDisplay;

    [SerializeField] GameObject sellCarDialoguePanel;
    [SerializeField] GameObject sellerDisplay;

    [SerializeField] GameObject[] sellButtons = { };


    private int[] carsSoldFinalRound = { 0, 0, 0, 0, 0, 0 };
    private bool[] inventoryNotEmpty = { true, true, true, true, true, true };
     
    private PlayerManager3P gameManagerScript;

    public static event Action OnCarSold;

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();
    }

    private void Start()
    {
        Instance = this;
    }


    public void OpenSellingDialoguePanel()
    {
        sellCarDialoguePanel.SetActive(true);

        int myIndex = 9;

        for (int i = 0; i < MainManager.playerNumber; i++)
        {
            if (MainManager.localMultiplayerName == MainManager.playerNames[i])
            {
                myIndex = i;
            }
        }

        MainManager.seller = myIndex;
        IdleCountdown.Instance.StartIdleCountdownMax(60f);

        CheckSellOptions();
        UpdateDisplays();

    }

    public void HideSellingDialoguePanel()
    {
        sellCarDialoguePanel.SetActive(false);
    }

    public void CloseWithoutSelling()
    {
        OnCarSold?.Invoke();
    }


    private void CheckSellOptions()
    {
        for (int i = 0; i < MainManager.cars.Length; i++)
            
            {
            
            inventoryNotEmpty[i] = true;
            
            if (MainManager.playerInventory[MainManager.seller, i] < 1)
            {
                inventoryNotEmpty[i] = false;
            }

            }
    }


    public void UpdateDisplays()

    {
        sellerNameDisplay.text = MainManager.playerNames[MainManager.seller];

        for (int i = 0; i < MainManager.cars.Length; i++)

        {
            sellPanelNameDisplay[i].text = ($"{MainManager.cars[i]}");
            sellPanelPrizeDisplay[i].text = ($"{MainManager.carPrizes[i]}");

            sellPanelInventoryDisplay[i].text = MainManager.playerInventory[MainManager.seller, i].ToString();

        }


    }


    public void SellCar(int car)
    {
        if (inventoryNotEmpty[car])
        {
            MainManager.playerInventory[MainManager.seller, car]--;
            MainManager.playerCash[MainManager.seller] += MainManager.carPrizes[car];
            gameManagerScript.UpdateInventoryDisplay();
            gameManagerScript.cashDisplay[MainManager.seller].text = MainManager.playerCash[MainManager.seller].ToString();
            sellCarDialoguePanel.SetActive(false);

            if (MainManager.roundCounter == MainManager.raceThreshold - 1)

            {

                carsSoldFinalRound[MainManager.seller]++;

                if (carsSoldFinalRound[MainManager.seller] >= 3)

                {
                    sellButtons[MainManager.seller].SetActive(false);
                }

            }

            else

            { sellButtons[MainManager.seller].SetActive(false); }

        }

        else

            sellCarDialoguePanel.SetActive(false);

        OnCarSold?.Invoke();
        OnlineManager.Instance.ReportCarSaleToClientsRpc(car, MainManager.seller);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
