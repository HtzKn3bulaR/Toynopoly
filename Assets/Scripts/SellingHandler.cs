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

    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            SellPanelNameDisplay[i].text = MainManager.cars[i];
            SellPanelPrizeDisplay[i].text = MainManager.carPrizes[i].ToString();
           
        }

        switch (MainManager.seller)

        {
            case 0:
                for (int i = 0; i < MainManager.p1Inventory.Length; i++)

                {
                    SellPanelInventoryDisplay[i].text = MainManager.p1Inventory[i].ToString();

                    //if (MainManager.p1Inventory[i] < 1)
                    //{
                    //    nameButtons[i].gameObject.SetActive(false);
                    //    prizeButtons[i].gameObject.SetActive(false);
                    //    inventoryButtons[i].gameObject.SetActive(false);
                    //}
                }

                 


                break;

            case 1:
                for (int i = 0; i < MainManager.p2Inventory.Length; i++)
                {
                    SellPanelInventoryDisplay[i].text = MainManager.p2Inventory[i].ToString();

                    //if (MainManager.p2Inventory[i] < 1)
                    //{
                    //    nameButtons[i].gameObject.SetActive(false);
                    //    prizeButtons[i].gameObject.SetActive(false);
                    //    inventoryButtons[i].gameObject.SetActive(false);
                    //}
                }
                break;
         }
        
    }


    public void SellCar(int car)

    {
        switch (MainManager.seller)
        {
            case 0:
                MainManager.p1Inventory[car]--;
                MainManager.player1Cash += MainManager.carPrizes[car];
                gameManagerScript.UpdateInventoryDisplay();
                cashP1.text = MainManager.player1Cash.ToString();
                sellCarDialoguePanel.SetActive(false);
                p1SellButton.SetActive(false);
                break;

            case 1:
                MainManager.p2Inventory[car]--;
                MainManager.player2Cash += MainManager.carPrizes[car];
                gameManagerScript.UpdateInventoryDisplay();
                cashP2.text = MainManager.player2Cash.ToString();
                sellCarDialoguePanel.SetActive(false);
                p2SellButton.SetActive(false);
                break;

        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
