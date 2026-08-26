using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class MatchScoreEditor : MonoBehaviour
{
    public static MatchScoreEditor instance;

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

    public void Start()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public void Awake()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditorPanelHide()
    {
        editorPanel.SetActive(false);
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
        Debug.Log("Submitting Inventory Player Index " + inventoryPlayerIndex);
        Debug.Log("Submitting Inventory Car Index " + inventoryCarIndex);
        Debug.Log("Submitting Inventory Content " + inventoryContent);

        OnlineManager.Instance.InventorySubmitRpc(inventoryPlayerIndex, inventoryCarIndex, inventoryContent);
                
    }
        

    public void ClosePanel()
    {
        editorPanel.gameObject.SetActive(false);
    }


    //SET VALUES ON THE NETWORK

    public void NewDataPlayer1(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[0] = tempCash;

        EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.AcceptDividend();
        }

        else
            PlayerManager3P.Instance.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 0);
    }

    public void NewDataPlayer2(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[1] = tempCash;

        EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.AcceptDividend();
        }

        else
            PlayerManager3P.Instance.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 1);
    }

    public void NewDataPlayer3(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[2] = tempCash;

        EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.AcceptDividend();
        }

        else
            PlayerManager3P.Instance.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 2);
    }
    public void NewDataPlayer4(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[3] = tempCash;

        EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.AcceptDividend();
        }

        else
            PlayerManager3P.Instance.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 3);
    }

    public void NewDataPlayer5(string cash)
    {
        int tempCash = int.Parse(cash);

        MainManager.playerCash[4] = tempCash;

        EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.AcceptDividend();
        }

        else
            PlayerManager3P.Instance.AcceptDividend();

        OnlineManager.Instance.ReportPlayerCashBalanceChangeToClientsRpc(tempCash, 4);
    }

    public void NewDataCar1(string price)
    {
        int tempPrize = int.Parse(price);

        MainManager.carPrizes[0] = tempPrize;


        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else
            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        MatchScoresNetworkHandler.instance.NewDataCar1Rpc(price);
    }

    public void NewDataCar2(string price)
    {
        int tempPrize = int.Parse(price);

        MainManager.carPrizes[1] = tempPrize;


        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else
            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        MatchScoresNetworkHandler.instance.NewDataCar2Rpc(price);
    }

    public void NewDataCar3(string price)
    {
        int tempPrize = int.Parse(price);

        MainManager.carPrizes[2] = tempPrize;


        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else
            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        MatchScoresNetworkHandler.instance.NewDataCar3Rpc(price);
    }

    public void NewDataCar4(string price)
    {
        int tempPrize = int.Parse(price);

        MainManager.carPrizes[3] = tempPrize;


        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else
            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        MatchScoresNetworkHandler.instance.NewDataCar4Rpc(price);
    }

    public void NewDataCar5(string price)
    {
        int tempPrize = int.Parse(price);

        MainManager.carPrizes[4] = tempPrize;


        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else
            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        MatchScoresNetworkHandler.instance.NewDataCar5Rpc(price);
    }

    public void NewDataCar6(string price)
    {
        int tempPrize = int.Parse(price);

        MainManager.carPrizes[5] = tempPrize;


        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else
            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        MatchScoresNetworkHandler.instance.NewDataCar6Rpc(price);
    }


}
