using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellingHandler : MonoBehaviour
{

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
       

    private int P1carsSoldFinalRound = 0;
    private int P2carsSoldFinalRound = 0;

    private GameManager gameManagerScript;

    private bool[] inventoryNotEmpty = { true, true, true, true, true, true };

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void OpenSellingDialoguePanel(int seller)

    {
        sellCarDialoguePanel.SetActive(true);
        MainManager.seller = seller;

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
        if (inventoryNotEmpty[car])

        {

            switch (MainManager.seller)
            {

                case 0:
                    MainManager.playerInventory[0, car]--;
                    MainManager.playerCash[0] += MainManager.carPrizes[car];
                    gameManagerScript.UpdateInventoryDisplay();
                    gameManagerScript.cashDisplay[0].text = MainManager.playerCash[0].ToString();
                    sellCarDialoguePanel.SetActive(false);

                    if (MainManager.roundCounter == 12)

                    {
                        P1carsSoldFinalRound++;

                        if (P1carsSoldFinalRound >= 3)

                        {
                            p1SellButton.SetActive(false);
                        }

                    }

                    else

                    { p1SellButton.SetActive(false); }

                    break;

                case 1:
                    MainManager.playerInventory[1, car]--;
                    MainManager.playerCash[1] += MainManager.carPrizes[car];
                    gameManagerScript.UpdateInventoryDisplay();
                    gameManagerScript.cashDisplay[1].text = MainManager.playerCash[1].ToString();
                    sellCarDialoguePanel.SetActive(false);

                    if (MainManager.roundCounter == 12)

                    {
                        P2carsSoldFinalRound++;

                        if (P2carsSoldFinalRound >= 3)

                        {
                            p2SellButton.SetActive(false);
                        }

                    }

                    else

                    { p2SellButton.SetActive(false); }
                    break;
            
            }

        }

        else

        { sellCarDialoguePanel.SetActive(false); }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
