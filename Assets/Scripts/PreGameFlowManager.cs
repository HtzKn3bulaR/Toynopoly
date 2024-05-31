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




    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ShowNewGameBox()

    {
        welcomePanel.SetActive(false);
        newGamePanel.SetActive(true);

    }


    public void ContinueToMain()

    {
        MainManager.classSelected = (carClassMenu.value);
        MainManager.playerNumber = (playerNumberMenu.value) + 2;

        SceneManager.LoadScene(MainManager.playerNumber - 1);

        
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
    void Update()
    {
        
    }
}
