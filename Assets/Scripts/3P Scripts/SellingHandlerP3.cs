using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellingHandlerP3 : MonoBehaviour
{

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
     
    private PlayerManager3P gameManagerScript;

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();
    }


    public void OpenSellingDialoguePanel(int seller)

    {
        sellCarDialoguePanel.SetActive(true);
        MainManager.seller = seller;

        UpdateDisplays();

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
                MainManager.playerInventory[MainManager.seller, car]--;
                MainManager.playerCash[MainManager.seller] += MainManager.carPrizes[car];
                gameManagerScript.UpdateInventoryDisplay();
                gameManagerScript.cashDisplay[MainManager.seller].text = MainManager.playerCash[MainManager.seller].ToString();
                sellCarDialoguePanel.SetActive(false);

                if (MainManager.roundCounter == 12)

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


    // Update is called once per frame
    void Update()
    {

    }
}
