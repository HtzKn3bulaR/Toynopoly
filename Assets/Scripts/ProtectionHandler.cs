using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProtectionHandler : MonoBehaviour
{
    public Sprite shieldIcon;

    [SerializeField] List<GameObject> unusedShields = new List<GameObject>();

    [SerializeField] List<Button> p1Inventory = new List<Button>();
    [SerializeField] List<Button> p2Inventory = new List<Button>();

    [SerializeField] List<Button> p3Inventory = new List<Button>();

    [SerializeField] List<Button> p4Inventory = new List<Button>();

    [SerializeField] List<Button> p5Inventory = new List<Button>();

    private PlayerManager3P playerManagerScript;

    // Start is called before the first frame update
    void Awake()
    {
        if (MainManager.playerNumber > 2)
        {
            playerManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProtectionEnable()
    {
        MainManager.protection[MainManager.currentCarIndex] = true;
        unusedShields[MainManager.activePlayer].gameObject.SetActive(false);
        MainManager.shieldAvailable[MainManager.activePlayer] = false;

        switch (MainManager.activePlayer)
        {
            case 0:
                p1Inventory[MainManager.currentCarIndex].image.sprite = shieldIcon;
                break;
            case 1:
                p2Inventory[MainManager.currentCarIndex].image.sprite = shieldIcon;
                break;
            case 2:
                p3Inventory[MainManager.currentCarIndex].image.sprite = shieldIcon;
                break;
            case 3:
                p4Inventory[MainManager.currentCarIndex].image.sprite = shieldIcon;
                break;
            case 4:
                p5Inventory[MainManager.currentCarIndex].image.sprite = shieldIcon;
                break;

        }



    }

    public void CheckProtectionOptionAfterChallenge()
    {
        int numberOfOwners = 0;

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            if (MainManager.playerInventory[i, MainManager.currentCarIndex] > 0)
            {
                numberOfOwners++;
            }

        }

        

        if (MainManager.playerInventory[MainManager.activePlayer, MainManager.currentCarIndex] > 0 && numberOfOwners < 2)

        { playerManagerScript.activePlayerHasToynopoly = true; }
                

    }



}







