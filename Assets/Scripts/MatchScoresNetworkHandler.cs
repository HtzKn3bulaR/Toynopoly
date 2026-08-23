using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MatchScoresNetworkHandler : NetworkBehaviour
{
    public static MatchScoresNetworkHandler instance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnNetworkSpawn()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
            

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar1Rpc(string price)
    {
        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[0] = tempPrize;

        MatchScoreEditor.instance.EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else

            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 0);

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar2Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[1] = tempPrize;


        MatchScoreEditor.instance.EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else

            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 1);

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar3Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[2] = tempPrize;


        MatchScoreEditor.instance.EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else

            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 2);

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar4Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[3] = tempPrize;


        MatchScoreEditor.instance.EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else

            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 3);

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar5Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[4] = tempPrize;

        MatchScoreEditor.instance.EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else

            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 4);

    }


    [Rpc(SendTo.ClientsAndHost)]
    public void NewDataCar6Rpc(string price)
    {

        int tempPrize = int.Parse(price);

        Debug.Log(price);
        Debug.Log(tempPrize);
        MainManager.carPrizes[5] = tempPrize;

        MatchScoreEditor.instance.EditorPanelHide();

        if (MainManager.playerNumber < 3)
        {
            GameManager.Instance.UpdateCarPrizesDisplay();
        }

        else

            PlayerManager3P.Instance.UpdateCarPrizesDisplay();

        OnlineManager.Instance.ReportCarValueChangeToClientsRpc(tempPrize, 5);

    }



}
