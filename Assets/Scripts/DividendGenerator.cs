using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class DividendGenerator : MonoBehaviour
{

    List<int> dividendList = new List<int> {  };

    List<int> actualDividendList = new List<int> {  };

    int[] dividendsTenRounds = { 7, 7, 7, 7, 0, 1, 2, 3, 4, 5 };
    int[] dividendsTwelveRounds = { 7, 7, 7, 7, 7, 7, 0, 1, 2, 3, 4, 5 };
       
    private GameManager gameManagerScript;
    private PlayerManager3P playerManagerScript;

    [SerializeField] GameObject dividendPayPanel;

    [SerializeField] TextMeshProUGUI dividendCarNamePanel;
    [SerializeField] TextMeshProUGUI dividendCarPrizePanel;

    public AudioSource divGenAudio;
    public AudioClip dividend;



    List<T> GetUniqueRandomElements<T>(List<T> inputList, int count)

    {
        List<T> inputListClone = new List<T>(inputList);
        Shuffle(inputListClone);
        return inputListClone.GetRange(0, count);

    }

    // Start is called before the first frame update


    void Shuffle<T>(List<T> inputList)

    {
        for (int i = 0; i < inputList.Count - 1; i++)

        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }

    void Awake()
    {
        if (MainManager.playerNumber < 3)
        {
            gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        else

        playerManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();

        if (MainManager.playerNumber == 5)
        {
            dividendList.AddRange(dividendsTenRounds);
            actualDividendList.AddRange(dividendsTenRounds);
        }

        else
        {
            dividendList.AddRange(dividendsTwelveRounds);
            actualDividendList.AddRange(dividendsTwelveRounds);
        }

    }

    public void RandomizeDividend()

    {
        if (MainManager.playerNumber == 5)

        {
            MainManager.raceThreshold = 11;
        }

        else

        {
            MainManager.raceThreshold = 13;
        }


        var uniqueRandomList = GetUniqueRandomElements(dividendList, MainManager.raceThreshold-1);

        for (int i = 0; i < uniqueRandomList.Count; i++)

        {
            actualDividendList[i] = uniqueRandomList[i];
            Debug.Log(MainManager.raceThreshold - 1);
            Debug.Log(actualDividendList[i]);

        }


        if (MainManager.playerNumber < 3)

        {
            gameManagerScript.SetLevelChangePanelInactive();
        }

        if (MainManager.playerNumber > 2)
        {
            playerManagerScript.StartLevel2();
        }
    }


    public void DividendCheck()

    {
        if (actualDividendList[MainManager.roundCounter - 1] < 7)

            PayDividend();

    }

    public void PayDividend()

    {
        int amountToPay;

        dividendPayPanel.gameObject.SetActive(true);
        divGenAudio.PlayOneShot(dividend);

        dividendCarNamePanel.text = MainManager.cars[actualDividendList[MainManager.roundCounter - 1]];
        dividendCarPrizePanel.text = MainManager.carPrizes[actualDividendList[MainManager.roundCounter - 1]].ToString();

        MainManager.currentCarIndex = actualDividendList[MainManager.roundCounter - 1];
        amountToPay = MainManager.carPrizes[actualDividendList[MainManager.roundCounter - 1]];

        for (int i = 0; i < MainManager.playerNumber; i++)

        {
            MainManager.playerCash[i] += (MainManager.playerInventory[i, MainManager.currentCarIndex]) * amountToPay;
        }
       
              
        }

    public void DividendAccepted()

    {
        if (MainManager.playerNumber < 3)
        {
            gameManagerScript.AcceptDividend();
            dividendPayPanel.gameObject.SetActive(false);
        }

        else
        {
            playerManagerScript.AcceptDividend();
            dividendPayPanel.gameObject.SetActive(false);
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
