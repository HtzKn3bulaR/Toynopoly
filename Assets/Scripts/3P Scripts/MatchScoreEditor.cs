using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.Netcode;

public class MatchScoreEditor : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] carPrizeFields;
    [SerializeField] TextMeshProUGUI[] carNames;
    [SerializeField] TextMeshProUGUI[] carPrizePlaceholders;

    [SerializeField] TextMeshProUGUI[] playerNames;
    [SerializeField] TextMeshProUGUI[] playerCash;
    [SerializeField] TextMeshProUGUI[] playerCashPlaceholders;
         
    [SerializeField] GameObject editorPanel;

    [SerializeField] TMP_Dropdown playerSelect;
    [SerializeField] TMP_Dropdown carSelect;

    int inventoryPlayerIndex;
    int inventoryCarIndex;
    int inventoryContent;

    [SerializeField] TextMeshProUGUI inventoryContentDisplay;

    int[] tempPrize = { 20, 20, 20, 20, 20, 20 };
    int[] tempCash = { 150, 150, 150, 150, 150, 150 };

    private PlayerManager3P playerManagerScript;
    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Awake()
    {

        if (MainManager.playerNumber > 2)
        {
            playerManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();
        }
        else
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMatchScoreEditor()
    {
        editorPanel.gameObject.SetActive(true);
    }

    public void RefreshData()
    {

        for (int i = 0; i < MainManager.carPrizes.Length; i++)
        {
            tempPrize[i] = MainManager.carPrizes[i];
            carPrizeFields[i].text = MainManager.carPrizes[i].ToString();
            carNames[i].text = MainManager.cars[i].ToString();
            carPrizePlaceholders[i].text = MainManager.carPrizes[i].ToString();
        }

        for (int i = 0; i < MainManager.playerNumber; i++)
        {
            tempCash[i] = MainManager.playerCash[i];
            playerNames[i].text = MainManager.playerNames[i].ToString();
            playerCashPlaceholders[i].text = MainManager.playerCash[i].ToString();

        }

        PopulatePlayerDropdown();
        PopulateCarDropdown();

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataPlayer1Rpc (string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[0] = tempCash;

        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.AcceptDividend();
        }

        else
            playerManagerScript.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 0);
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataPlayer2Rpc(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[1] = tempCash;

        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.AcceptDividend();
        }

        else
            playerManagerScript.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 1);
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataPlayer3Rpc(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[2] = tempCash;

        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.AcceptDividend();
        }

        else

            playerManagerScript.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 2);
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataPlayer4Rpc(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[3] = tempCash;

        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.AcceptDividend();
        }

        else

            playerManagerScript.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 3);
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataPlayer5Rpc(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[4] = tempCash;

        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.AcceptDividend();
        }

        else

            playerManagerScript.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 4);
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar1Rpc(string price)
    {
        int tempPrize = int.Parse(price);
                    
        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[0] = tempPrize;
    

        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.UpdateCarPrizesDisplay();
        }

        else

            playerManagerScript.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 0);

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar2Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[1] = tempPrize;


        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.UpdateCarPrizesDisplay();
        }

        else

            playerManagerScript.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 1);

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar3Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[2] = tempPrize;


        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.UpdateCarPrizesDisplay();
        }

        else

            playerManagerScript.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 2);

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar4Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[3] = tempPrize;


        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.UpdateCarPrizesDisplay();
        }

        else

            playerManagerScript.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 3);

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar5Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[4] = tempPrize;

        editorPanel.gameObject.SetActive(false);

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.UpdateCarPrizesDisplay();
        }

        else

            playerManagerScript.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 4);

    }


    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar6Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[5] = tempPrize;

        editorPanel.gameObject.SetActive(false);

        if(MainManager.playerNumber < 3)
        {
            gameManagerScript.UpdateCarPrizesDisplay();
        }

        else

        playerManagerScript.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 5);

    }


    private void PopulatePlayerDropdown()
    {
        playerSelect.ClearOptions();

        var option1 = new TMP_Dropdown.OptionData(MainManager.playerNames[0]);
        playerSelect.options.Add(option1);

        var option2 = new TMP_Dropdown.OptionData(MainManager.playerNames[1]);
        playerSelect.options.Add(option2);


        if (MainManager.playerNumber > 2)
        {

            var option3 = new TMP_Dropdown.OptionData(MainManager.playerNames[2]);
            playerSelect.options.Add(option3);
        }


        if (MainManager.playerNumber > 3)

        {
            var option4 = new TMP_Dropdown.OptionData(MainManager.playerNames[3]);
            playerSelect.options.Add(option4);
        }

        if (MainManager.playerNumber > 4)

        {
            var option5 = new TMP_Dropdown.OptionData(MainManager.playerNames[4]);
            playerSelect.options.Add(option5);

        }


        playerSelect.RefreshShownValue();
    }

    private void PopulateCarDropdown()
    {
        carSelect.ClearOptions();

        var option1 = new TMP_Dropdown.OptionData(MainManager.cars[0]);
        var option2 = new TMP_Dropdown.OptionData(MainManager.cars[1]);
        var option3 = new TMP_Dropdown.OptionData(MainManager.cars[2]);
        var option4 = new TMP_Dropdown.OptionData(MainManager.cars[3]);
        var option5 = new TMP_Dropdown.OptionData(MainManager.cars[4]);
        var option6 = new TMP_Dropdown.OptionData(MainManager.cars[5]);
        carSelect.options.Add(option1);
        carSelect.options.Add(option2);
        carSelect.options.Add(option3);
        carSelect.options.Add(option4);
        carSelect.options.Add(option5);
        carSelect.options.Add(option6);


        carSelect.RefreshShownValue();
    }


    public void GetPlayerIndex()
    {
        inventoryPlayerIndex = playerSelect.value;
    }

    public void GetCarIndex()
    {
        inventoryCarIndex = carSelect.value;
    }


    public void LookupInventoryContent()
    {
        inventoryContent = MainManager.playerInventory[inventoryPlayerIndex, inventoryCarIndex];

        inventoryContentDisplay.text = inventoryContent.ToString();

    }

    public void InventoryIncrease()
    {
        inventoryContent++;
        inventoryContentDisplay.text = inventoryContent.ToString();
    }

    public void InventoryDecrease()
    {
        inventoryContent--;
        inventoryContentDisplay.text = inventoryContent.ToString();
    }

    public void InventorySubmit()
    {
        OnlineManager.Instance.InventorySubmitRpc(inventoryPlayerIndex, inventoryCarIndex, inventoryContent);
    }
        

    public void ClosePanel()
    {
        editorPanel.gameObject.SetActive(false);
    }

}
