using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class LobbyUIHandler : MonoBehaviour
{

    public static LobbyUIHandler Instance;

    [SerializeField] private TextMeshProUGUI roomCode;
    [SerializeField] private TextMeshProUGUI matchTitle;

    [SerializeField] private TextMeshProUGUI carClass;
    [SerializeField] private TextMeshProUGUI playersInLobby;
    [SerializeField] private TextMeshProUGUI matchLength;

    [SerializeField] private GameObject[] playerPanels;
    [SerializeField] private TextMeshProUGUI[] playerNameTags;

    [SerializeField] private Button startGameButton;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        LobbyHandler.OnLobbyJoined += LobbyHandler_OnLobbyJoined;                
    }

    private void LobbyHandler_OnLobbyJoined()
    {
        roomCode.text = LobbyHandler.Instance.ReturnJoinedLobby().LobbyCode;
        carClass.text = LobbyHandler.Instance.ReturnJoinedLobby().Data["CarClass"].Value;
        matchLength.text = LobbyHandler.Instance.ReturnJoinedLobby().Data["MatchLength"].Value;
        matchTitle.text = LobbyHandler.Instance.ReturnJoinedLobby().Name;
        playersInLobby.text = LobbyHandler.Instance.ReturnJoinedLobby().Players.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStartGameButton()
    {
        startGameButton.gameObject.SetActive(true);
    }


    public void SetLobbyParameters(int playerNumber, string lobbyTitle)
    {
        Debug.Log("Lobby Title " + lobbyTitle);

        playersInLobby.text = playerNumber.ToString();
        matchTitle.text = lobbyTitle.ToString();
        roomCode.text = MainManager.roomCode;
    }

    public void SetPlayerBox(int playerIndex, string playerName)
    {
        playerPanels[playerIndex].gameObject.SetActive(true);
        playerNameTags[playerIndex].text = playerName;
    }

    public void UpdateLobbyCarClass(string classSelected)
    {
        carClass.text = classSelected;
    }

    public void UpdateLobbyDuration(string duration)
    {
        matchLength.text = duration;
    }

    public void UpdatePlayerNumber(int playerNumber)
    {
        playersInLobby.text = playerNumber.ToString();
    }
        
}
