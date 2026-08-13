using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellingHandler : MonoBehaviour
{
    public static SellingHandler Instance; 

    [SerializeField] Button[] nameButtons;
    [SerializeField] Button[] prizeButtons;
    [SerializeField] Button[] inventoryButtons;

    [SerializeField] TextMeshProUGUI cashP1;
    [SerializeField] TextMeshProUGUI cashP2;


    [SerializeField] TextMeshProUGUI[] SellPanelNameDisplay;
    [SerializeField] TextMeshProUGUI[] SellPanelPrizeDisplay;
    [SerializeField] TextMeshProUGUI[] SellPanelInventoryDisplay;
    [SerializeField] TextMeshProUGUI sellerNameDisplay;

    [SerializeField] GameObject sellCarDialoguePanel;
    [SerializeField] GameObject p1SellButton;
    [SerializeField] GameObject p2SellButton;
    [SerializeField] GameObject sellerDisplay;

    private int[] carsSoldFinalRound = { 0, 0, 0, 0, 0, 0 };
           

    private GameManager gameManagerScript;

    private bool[] inventoryNotEmpty = { true, true, true, true, true, true };

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        CheckSellOptions();
        UpdateDisplays();

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
            SellPanelNameDisplay[i].text = MainManager.cars[i];
            SellPanelPrizeDisplay[i].text = MainManager.carPrizes[i].ToString();
           
            SellPanelInventoryDisplay[i].text = MainManager.playerInventory[MainManager.seller,i].ToString();
                   
        }                  
                          
    }


    public void SellCar(int car)
    {
        int sellerIndex = 9;

        for (int i = 0; i < MainManager.playerNumber; i++)
        {
            if (MainManager.localMultiplayerName == MainManager.playerNames[i])
            {
                sellerIndex = i;
                Debug.Log("Seller Index " + sellerIndex);
            }
        }

        if (inventoryNotEmpty[car])
        {
            MainManager.playerInventory[sellerIndex, car]--;
            MainManager.playerCash[sellerIndex] += MainManager.carPrizes[car];

            if (MainManager.playerNumber > 2)
            {
                PlayerManager3P.Instance.UpdateInventoryDisplay();
            }
            if (MainManager.playerNumber < 3)
            {
                GameManager.Instance.UpdateInventoryDisplay();
            }

            gameManagerScript.cashDisplay[sellerIndex].text = MainManager.playerCash[sellerIndex].ToString();
            sellCarDialoguePanel.SetActive(false);

            Debug.Log("This is Round " + MainManager.roundCounter + " Last round is: " + (MainManager.raceThreshold-1));

            if (MainManager.roundCounter == (MainManager.raceThreshold - 1))
            {
                Debug.Log("This is the last selling Round");

                carsSoldFinalRound[sellerIndex]++;
                Debug.Log("Cars Sold Final Round " + carsSoldFinalRound[sellerIndex]);
                OnlineManager.Instance.ReportCarSaleToClientsRpc(car, sellerIndex);

                if (carsSoldFinalRound[sellerIndex] >= 3)
                {
                    OnlineManager.Instance.ReportReadyForNextRoundToServerRpc();
                }

                else
                {
                    sellCarDialoguePanel.SetActive(true);
                    CheckSellOptions();
                    UpdateDisplays();
                }
            }

            else
            {
                
                OnlineManager.Instance.ReportCarSaleToClientsRpc(car, sellerIndex);
                OnlineManager.Instance.ReportReadyForNextRoundToServerRpc();
                sellCarDialoguePanel.SetActive(false);
            }
        }              

    }

    public void CloseWithoutSelling()
    {       
        sellCarDialoguePanel.SetActive(false);
        OnlineManager.Instance.ReportReadyForNextRoundToServerRpc();
    }


        // Update is called once per frame
        void Update()
    {

    }
}
