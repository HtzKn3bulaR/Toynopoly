using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreGameFlowManager : MonoBehaviour
{

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


    [SerializeField] GameObject playerNamesPanel2P;
    [SerializeField] GameObject playerNamesPanel3P;
    [SerializeField] GameObject playerNamesPanel4P;
    [SerializeField] GameObject playerNamesPanel5P;

    [SerializeField] TextMeshProUGUI p1NameInputFieldP2;
    [SerializeField] TextMeshProUGUI p2NameInputFieldP2;

    [SerializeField] TextMeshProUGUI p1NameInputFieldP3;
    [SerializeField] TextMeshProUGUI p2NameInputFieldP3;   
    [SerializeField] TextMeshProUGUI p3NameInputFieldP3;

    [SerializeField] TextMeshProUGUI p1NameInputFieldP4;
    [SerializeField] TextMeshProUGUI p2NameInputFieldP4;
    [SerializeField] TextMeshProUGUI p3NameInputFieldP4;
    [SerializeField] TextMeshProUGUI p4NameInputFieldP4;

    [SerializeField] TextMeshProUGUI p1NameInputFieldP5;
    [SerializeField] TextMeshProUGUI p2NameInputFieldP5;
    [SerializeField] TextMeshProUGUI p3NameInputFieldP5;
    [SerializeField] TextMeshProUGUI p4NameInputFieldP5;
    [SerializeField] TextMeshProUGUI p5NameInputFieldP5;
    
    [SerializeField] TextMeshProUGUI p6NameInputField;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ShowNewGameBox()

    {
        welcomePanel.SetActive(false);
        newGamePanel.SetActive(true);

    }


    public void EnterPlayerNames()

    {
        MainManager.classSelected = (carClassMenu.value);
        MainManager.playerNumber = (playerNumberMenu.value) + 2;

        switch (MainManager.playerNumber)

        {
            case 2:
                playerNamesPanel2P.SetActive(true);
                
                if(MainManager.shortMatch)
                {
                    MainManager.raceThreshold = 9;
                }

                else
                                
                MainManager.raceThreshold = 13;
                break;

            case 3:
                playerNamesPanel3P.SetActive(true);

                if (MainManager.shortMatch)
                {
                    MainManager.raceThreshold = 7;
                }

                else

                MainManager.raceThreshold = 13;
                break;

            case 4:
                playerNamesPanel4P.SetActive(true);
                if (MainManager.shortMatch)
                {
                    MainManager.raceThreshold = 9;
                }

                else
                    MainManager.raceThreshold = 13;
                break;

            case 5:
                playerNamesPanel5P.SetActive(true);
                if (MainManager.shortMatch)
                {
                    MainManager.raceThreshold = 6;
                }

                else
                    MainManager.raceThreshold = 11;
                break;
        }
        
              
                             
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

        switch (MainManager.playerNumber)

            {
                case 2:
                    MainManager.playerNames[0] = p1NameInputFieldP2.text.ToUpper();
                    MainManager.playerNames[1] = p2NameInputFieldP2.text.ToUpper();
                    break;

                case 3:
                    MainManager.playerNames[0] = p1NameInputFieldP3.text.ToUpper();
                    MainManager.playerNames[1] = p2NameInputFieldP3.text.ToUpper();
                    MainManager.playerNames[2] = p3NameInputFieldP3.text.ToUpper();
                    break;

            case 4:
                MainManager.playerNames[0] = p1NameInputFieldP4.text.ToUpper();
                MainManager.playerNames[1] = p2NameInputFieldP4.text.ToUpper();
                MainManager.playerNames[2] = p3NameInputFieldP4.text.ToUpper();
                MainManager.playerNames[3] = p4NameInputFieldP4.text.ToUpper();
                break;

            case 5:
                MainManager.playerNames[0] = p1NameInputFieldP5.text.ToUpper();
                MainManager.playerNames[1] = p2NameInputFieldP5.text.ToUpper();
                MainManager.playerNames[2] = p3NameInputFieldP5.text.ToUpper();
                MainManager.playerNames[3] = p4NameInputFieldP5.text.ToUpper();
                MainManager.playerNames[4] = p5NameInputFieldP5.text.ToUpper();
                break;
        }


            SceneManager.LoadScene(MainManager.playerNumber - 1);
    }

    public void ResumePreviousGame()
    {
        Load();
        MainManager.gameResumed = true;
        SceneManager.LoadScene(MainManager.playerNumber - 1);
    }

    private void Load()
    {
        int x = 0;
        int y = 0;
        int z = 0;
        int a = 0;
        int b = 0;

        string saveString = SaveSystem.Load();

        if (saveString != null)
        {

            GameManager.SaveGameData playerData = JsonUtility.FromJson<GameManager.SaveGameData>(saveString);

            MainManager.playerNumber = playerData.playerNumber;
            MainManager.playerNames = playerData.playerNames;
            MainManager.playerCash = playerData.playerCash;
                        
            MainManager.classSelected = playerData.classSelected;
            MainManager.cars = playerData.cars;
            MainManager.carPrizes = playerData.carPrizes;
            MainManager.fieldsLeftForCar = playerData.fieldsLeftForCar;
            MainManager.fieldAvailable = playerData.fieldAvailable;
            MainManager.activeTracks = playerData.activeTracks;
            MainManager.bonusTrack = playerData.bonusTrack;
            MainManager.activePlayer = playerData.activePlayer;
            MainManager.levelCounter = playerData.level;
            MainManager.roundCounter = playerData.round;

            for (int i = 0; i < playerData.playerInventory.Length; i++)

            {
                if (i < 6)
                { MainManager.playerInventory[0, i] = playerData.playerInventory[i]; }

                else if (i < 12)
                {
                    MainManager.playerInventory[1, x] = playerData.playerInventory[i];
                    x++;
                }

                else if (i < 18)
                {
                    MainManager.playerInventory[2, y] = playerData.playerInventory[i];
                    y++;
                }

                else if (i < 24)
                {
                    MainManager.playerInventory[3, z] = playerData.playerInventory[i];
                    z++;
                }

                else if (i < 30)

                {
                    MainManager.playerInventory[4, a] = playerData.playerInventory[i];
                    a++;
                }

                else
                {
                    MainManager.playerInventory[5, b] = playerData.playerInventory[i];
                    a++;
                }


            }


        }

        else

            ContinueToMain();
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
}
