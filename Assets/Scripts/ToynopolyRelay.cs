using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class ToynopolyRelay : MonoBehaviour
{
    public static ToynopolyRelay Instance;
        

    private void Start()
    {
        Instance = this;

        DontDestroyOnLoad(this.gameObject);        

    }
        

    public async Task<string> CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();

            Debug.Log("Host started");

            Debug.Log("Local Client ID " + NetworkManager.Singleton.LocalClientId);

            return joinCode;                       
            
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e.Message);
            return null;
        }              

    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            Debug.Log("Joining Relay with code " +  joinCode);

            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();

            //LobbyHandler.Instance.LeaveLobby();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e.Message);
        }

    }
}
