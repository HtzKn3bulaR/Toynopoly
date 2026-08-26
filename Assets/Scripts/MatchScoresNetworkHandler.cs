using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

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

    [Rpc(SendTo.ClientsAndHost)]
    internal void SendCarReplacementToClientRpc(int newCarPosition, int newCarIndex)
    {
        if(!NetworkManager.Singleton.IsHost)
        { 
        MainManager.cars[newCarPosition] = GridGenerator.Instance.activeList[newCarIndex].ToSafeString();

            switch (newCarPosition)
            {
                case 0:
                    GridGenerator.Instance.carAText.text = MainManager.cars[0];
                    GridGenerator.Instance.carPicA.image.sprite = GridGenerator.Instance.activeSpriteList[newCarIndex];
                    break;

                case 1:
                    GridGenerator.Instance.carBText.text = MainManager.cars[1];
                    GridGenerator.Instance.carPicB.image.sprite = GridGenerator.Instance.activeSpriteList[newCarIndex];
                    break;

                case 2:
                    GridGenerator.Instance.carCText.text = MainManager.cars[2];
                    GridGenerator.Instance.carPicC.image.sprite = GridGenerator.Instance.activeSpriteList[newCarIndex];
                    break;

                case 3:
                    GridGenerator.Instance.carDText.text = MainManager.cars[3];
                    GridGenerator.Instance.carPicD.image.sprite = GridGenerator.Instance.activeSpriteList[newCarIndex];
                    break;

                case 4:
                    GridGenerator.Instance.carEText.text = MainManager.cars[4];
                    GridGenerator.Instance.carPicE.image.sprite = GridGenerator.Instance.activeSpriteList[newCarIndex];
                    break;

                case 5:
                    GridGenerator.Instance.carFText.text = MainManager.cars[5];
                    GridGenerator.Instance.carPicF.image.sprite = GridGenerator.Instance.activeSpriteList[newCarIndex];
                    break;
            }
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    internal void ReportTrackReplacementToClientRpc(int trackPosition, string newTrackName)
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            if (trackPosition != 9)
            {
                MainManager.activeTracks[trackPosition] = newTrackName;
            }

            else
            {
                MainManager.bonusTrack = newTrackName;
            }
            
            ReplaceTrackHandler.Instance.UpdateTrackPanel();
        }
    }
}
