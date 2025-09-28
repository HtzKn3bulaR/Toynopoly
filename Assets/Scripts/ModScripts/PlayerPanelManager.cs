using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerPanelManager : MonoBehaviour
{
    [SerializeField] private Transform playerPanel;
    [SerializeField] private Transform playerTemplate;

    [SerializeField] private Transform reversePanel;

    [SerializeField] private SessionManager sessionManager;

    [SerializeField] private Transform shuffleButton;

    private List<Transform> playerNametagTransformList;

    private float timer = 0.0f;
    private float timerMax = 10.0f;


    private void Awake()
    {
        playerTemplate.gameObject.SetActive(false);
    }


    private void Start()
    {
        playerNametagTransformList = new List<Transform>();

        GridGenerator.OnTrackPanelPopulate += GridGenerator_OnTrackPanelPopulate;
        sessionManager = GetComponent<SessionManager>();
    }

    private void GridGenerator_OnTrackPanelPopulate()
    {
        playerPanel.gameObject.SetActive(true);
        shuffleButton.gameObject.SetActive(true);
        reversePanel.gameObject.SetActive(true);

    }


    public void UpdateRosterVisual()
    {

        playerNametagTransformList.Clear();

        foreach (Transform child in playerPanel)
        {
            if (child == playerTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (string player in SessionManager.instance.GetRosterList())
        {
            Debug.Log("Player is " + player);
            Transform playerNametag = Instantiate(playerTemplate, playerPanel);
            playerNametag.gameObject.SetActive(true);
            playerNametag.GetComponent<UITemplateName>().SetName(player);
            playerNametagTransformList.Add(playerNametag);
                        
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }


    public void StartRandomizerVisual()
    {
        timer = 0.0f;
               
            InvokeRepeating("ChooseRandomNametag", 1.0f, 0.2f);
             
    }


    private void ChooseRandomNametag()
    {
        if (timer < timerMax)
        {

            foreach (Transform playerNametag in playerNametagTransformList)
            {
                playerNametag.GetComponent<UITemplateName>().SetDefaultColor();
            }

            Transform chosenNametag = playerNametagTransformList[Random.Range(0, playerNametagTransformList.Count)];
            chosenNametag.GetComponent<UITemplateName>().SetChosenColor();
        }

        else CancelInvoke("ChooseRandomNametag");

    }
    
}
