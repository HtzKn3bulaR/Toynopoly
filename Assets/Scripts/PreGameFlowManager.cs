using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreGameFlowManager : MonoBehaviour
{
    public static PreGameFlowManager Instance;

    [SerializeField] Button newGame;
    [SerializeField] Button help;
    [SerializeField] Button goToHelp2;
    [SerializeField] Button goToHelp3;
    [SerializeField] Button closeHelp;
    [SerializeField] GameObject welcomePanel;
    [SerializeField] GameObject newGamePanel;
    [SerializeField] GameObject helpPanel1;
    [SerializeField] GameObject helpPanel2;
    [SerializeField] GameObject helpPanel3;
    [SerializeField] TMP_Dropdown carClassMenu;
    [SerializeField] TMP_Dropdown playerNumberMenu;
    [SerializeField] TMP_Dropdown matchLengthMenu;


    [SerializeField] GameObject clientSignUpWindow;
    [SerializeField] GameObject playerNameHost;
    [SerializeField] GameObject playerNamesPanel4P;
    [SerializeField] GameObject lobbyWindow;

    [SerializeField] TextMeshProUGUI roomCodeInfo;
    [SerializeField] GameObject roomCodeEntryField;
    [SerializeField] TextMeshProUGUI advisoryText;
        
    [SerializeField] Button[] StartGame;

    [SerializeField] GameObject[] inputFieldsP2;
    [SerializeField] GameObject[] inputFieldsP3;
    [SerializeField] GameObject[] inputFieldsP4;
    [SerializeField] GameObject[] inputFieldsP5;

    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject startModButton;


    public string tempPlayerName;

    // Start is called before the first frame update
    void Start()
    {
       Instance = this; 
    }


    public void ShowNewGameBox()

    {
        welcomePanel.SetActive(false);
        newGamePanel.SetActive(true);

    }

    public void ShowClientSignUpPanel()
    {
        clientSignUpWindow.gameObject.SetActive(true);
    }

    public void ShowLobbyWindow()
    {
        lobbyWindow.SetActive(true);
        playerNameHost.SetActive(false);
        
    }


    public void ShowHostSignUpPanel()
    {
        newGamePanel.SetActive(false);
        playerNameHost.SetActive(true);

        MainManager.classSelected = (carClassMenu.value);
                                     
    }

    public void SetLobbyTitle(string name)
    {
        MainManager.matchTitle = name;
    }

    public void StartMod()
    {
        SceneManager.LoadScene(5);
    }



    public void SetMatchLength()
    {
        switch (matchLengthMenu.value)
        {
            case 0:
                MainManager.shortMatch = false;
                break;

            case 1:
                MainManager.shortMatch = true;
                break;
        }
    }

    public void ContinueToMain()
    {      
     //SceneManager.LoadScene(LobbyHandler.Instance.ReturnJoinedLobby().Players.Count - 1);
    }
        

    public void PrepareNameString(string name)
    {
        string entry;

        entry = name.TrimEnd(new char[] { '\r', ' ' });
        entry = entry.TrimStart(new char[] { '\r', ' ' });
        entry = entry.ToUpper();
        tempPlayerName = entry;
        LobbyHandler.Instance.SetLobbyPlayerName(tempPlayerName);
    }

    public void ShowRoomCodeEntryField()
    {
        roomCodeEntryField.SetActive(true);
        advisoryText.text = "Please type in the Room Code for the Lobby and confirm with Enter";
    }

    

    public void BackToMainMenu()
    {
        tempPlayerName = null;
        
        CloseLobbyWindow();
    }


    public void OpenHelp()
    {
        helpPanel1.SetActive(true);

    }

    public void OpenHelp2()
    {
        helpPanel1.SetActive(false);
        helpPanel2.SetActive(true);

    }

    public void OpenHelp3()
    {
        helpPanel2.SetActive(false);
        helpPanel3.SetActive(true);

    }

    public void CloseHelp()
    {
        helpPanel3.SetActive(false);
    }


    // Update is called once per frame

    public void QuitGame()

    {
        Application.Quit();

    }
    void Update()
    {
        
    }
        

    public void CloseLobbyWindow()
    {
        lobbyWindow.SetActive(false);
        clientSignUpWindow.SetActive(false);
    }
}
