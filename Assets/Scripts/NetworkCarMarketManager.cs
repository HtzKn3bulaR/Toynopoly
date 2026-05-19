using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkCarMarketManager : NetworkBehaviour
{
    public static NetworkCarMarketManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        
    }

    private void PlayerManager3P_OnActivePlayerHasBoughtCar()
    {
        AckCarPurchaseToClientsRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void AckCarPurchaseToClientsRpc()
    {
        if(!PlayerManager3P.Instance.LocalIsActivePlayer())
        {
            MainManager.playerCash[MainManager.activePlayer] -= MainManager.carPrizes[MainManager.currentCarIndex];
            PlayerManager3P.Instance.PlayerWinsCar(MainManager.activePlayer);

            PlayerManager3P.Instance.UpdateCashDisplay();
            PlayerManager3P.Instance.UpdateInventoryDisplay();

            PlayerManager3P.Instance.SetPromptText(MainManager.playerNames[MainManager.activePlayer] + " has bought a " + MainManager.cars[MainManager.currentCarIndex]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
