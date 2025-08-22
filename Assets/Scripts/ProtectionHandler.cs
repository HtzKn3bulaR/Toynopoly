using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProtectionHandler : MonoBehaviour
{
    public AudioClip shieldDeployed;
    public AudioSource protectionAudio;

    public Sprite shieldIcon;

    public ParticleSystem shieldEmitter;
    [SerializeField] GameObject shieldObject;

    [SerializeField] List<GameObject> unusedShields = new List<GameObject>();

    [SerializeField] List<Button> p1Inventory = new List<Button>();
    [SerializeField] List<Button> p2Inventory = new List<Button>();

    [SerializeField] List<Button> p3Inventory = new List<Button>();

    [SerializeField] List<Button> p4Inventory = new List<Button>();

    [SerializeField] List<Button> p5Inventory = new List<Button>();

    private PlayerManager3P playerManagerScript;
    private GameManager gameManagerScript;


    [SerializeField] TextMeshProUGUI activeCar;
    [SerializeField] TextMeshProUGUI activePlayer;

    [SerializeField] GameObject P2protectionPanel;
    [SerializeField] GameObject shieldDeployedPanel;

    [SerializeField] Button shieldCarPicture;

    private GridGenerator3P gridGeneratorScript;
    private GridGenerator gridGenerate2PScript;

    // Start is called before the first frame update
    void Awake()
    {
        if (MainManager.playerNumber > 2)
        {
            playerManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();
        }

        if (MainManager.playerNumber < 3)
        {
            gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
            gridGenerate2PScript = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
        }

        else
        {

            playerManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();
            gridGeneratorScript = GameObject.Find("GridGenerator3P").GetComponent<GridGenerator3P>();
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

        shieldDeployedPanel.gameObject.SetActive(true);
        DisplayShieldDeployedPanel();

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

    public void GetInformation()
    {
        activeCar.text = MainManager.cars[MainManager.TimeBattleCarIndex].ToString();
        activePlayer.text = MainManager.playerNames[MainManager.activePlayer].ToString();
    }

    public void P2ProtectionPanelClose()
    {
        P2protectionPanel.gameObject.SetActive(false);
    }

    public void DisplayShieldDeployedPanel()
    {
        shieldObject.gameObject.SetActive(true);
        shieldEmitter.Play();
        protectionAudio.PlayOneShot(shieldDeployed);



        if (MainManager.playerNumber > 2)
        {
            for (int i = 0; i < gridGeneratorScript.activeList.Count; i++)

            {
                if (gridGeneratorScript.activeList[i] == MainManager.cars[MainManager.currentCarIndex])

                { shieldCarPicture.image.sprite = gridGeneratorScript.activeSpriteList[i]; }
            }

        }

        else

            for (int i = 0; i < gridGenerate2PScript.activeList.Count; i++)

            {
                if (gridGenerate2PScript.activeList[i] == MainManager.cars[MainManager.currentCarIndex])

                { shieldCarPicture.image.sprite = gridGenerate2PScript.activeSpriteList[i]; }
            }

    }

    public void ShieldInfoPanelClose()
    {
        shieldDeployedPanel.gameObject.SetActive(false);
        shieldEmitter.Stop();
        shieldObject.gameObject.SetActive(false);
    }

}







