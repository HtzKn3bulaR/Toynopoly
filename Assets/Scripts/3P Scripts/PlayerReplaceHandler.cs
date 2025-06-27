using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerReplaceHandler : MonoBehaviour
{
    private GridGenerator3P gridGeneratorScript;

    [SerializeField] GameObject playerReplaceDialoguePanel;

    [SerializeField] Button[] playerNames;

    [SerializeField] TextMeshProUGUI [] playerNamesDisplay;

    private int playerPos;
    private string newPlayerName;



    // Start is called before the first frame update
    void Start()
    {
        gridGeneratorScript = GameObject.Find("GridGenerator3P").GetComponent<GridGenerator3P>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePlayerReplaceProcedure()
    {
        playerReplaceDialoguePanel.gameObject.SetActive(true);

        for (int i=0; i < MainManager.playerNumber; i++)
        {
            playerNamesDisplay[i].text = MainManager.playerNames[i];
        }

        switch(MainManager.playerNumber)
        {
            case 3:
                playerNames[3].gameObject.SetActive(false);
                playerNames[4].gameObject.SetActive(false);
                break;
            case 4:
                playerNames[4].gameObject.SetActive(false);
                break;
        }


    }

    public void GetPlayerPosition(int position)
    {
        playerPos = position;

    }

    public void GetNewPlayerName(string name)
    {
        newPlayerName = name;
    }

    public void PlayerReplaceExecute()
    {
        MainManager.playerNames[playerPos] = newPlayerName;
        playerNamesDisplay[playerPos].text = newPlayerName;
        
        switch(playerPos)
        {
            case 0:
                gridGeneratorScript.player1NameField.text = newPlayerName;
                break;
            case 1:
                gridGeneratorScript.player2NameField.text = newPlayerName;
                break;
            case 2:
                gridGeneratorScript.player3NameField.text = newPlayerName;
                break;
            case 3:
                gridGeneratorScript.player4NameField.text = newPlayerName;
                break;
            case 4:
                gridGeneratorScript.player5NameField.text = newPlayerName;
                break;
        }
               

        playerReplaceDialoguePanel.gameObject.SetActive(false);

    }

    public void PlayerReplaceCancel()
    {
        playerReplaceDialoguePanel.gameObject.SetActive(false);
    }

}
