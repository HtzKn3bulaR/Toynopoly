using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class DividendGenerator : MonoBehaviour
{
    List<int> dividendList = new List<int> { 7, 7, 7, 7, 7, 7, 0, 1, 2, 3, 4, 5 };

    List<int> actualDividendList = new List<int> { 7, 7, 7, 7, 7, 7, 0, 1, 2, 3, 4, 5 };

    private GameManager gameManagerScript;

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

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    public void RandomizeDividend()

    {
        var uniqueRandomList = GetUniqueRandomElements(dividendList, 12);

        for (int i = 0; i < uniqueRandomList.Count; i++)

        {
            actualDividendList[i] = uniqueRandomList[i];


        }

        gameManagerScript.SetLevelChangePanelInactive();
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

        MainManager.player1Cash += (MainManager.p1Inventory[MainManager.currentCarIndex]) * amountToPay;
        MainManager.player2Cash += (MainManager.p2Inventory[MainManager.currentCarIndex]) * amountToPay;
        }

    public void DividendAccepted()

    {
        gameManagerScript.AcceptDividend();
        dividendPayPanel.gameObject.SetActive(false);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
