using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreGameFlowManager : MonoBehaviour
{

    [SerializeField] Button newGame;
    [SerializeField] GameObject welcomePanel;
    [SerializeField] GameObject newGamePanel;
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
