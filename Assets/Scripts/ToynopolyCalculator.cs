using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToynopolyCalculator : MonoBehaviour
{
    private GameManager gameManagerScript;

    private int[] maxToynopolyIncome = { 0, 0, 0, 0, 0, 0 };

    public TextMeshProUGUI[] toynopolyMaxIncomeDisplay;
    public TextMeshProUGUI incomeText;
    public GameObject[] maxIncomeButtons;

    public GameObject endGameButton;



    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformToynopolyCalculations()

    {
        incomeText.gameObject.SetActive(true);
        endGameButton.SetActive(true);


        for (int i = 0; i < MainManager.cars.Length; i++)

        {
            int inventoryCheck = 0;

            for (int j = 0; j < MainManager.playerNumber; j++)

            {
                inventoryCheck += MainManager.playerInventory[j, i];
                Debug.Log(inventoryCheck);
            }


            maxToynopolyIncome[i] = MainManager.carPrizes[i] * MainManager.fieldsLeftForCar[i];

            if (maxToynopolyIncome[i] > 0 && inventoryCheck > 0)

            {
                toynopolyMaxIncomeDisplay[i].gameObject.SetActive(true);
                maxIncomeButtons[i].SetActive(true);
                toynopolyMaxIncomeDisplay[i].text = maxToynopolyIncome[i].ToString();
            }

            else if (inventoryCheck < 1)

            {
                toynopolyMaxIncomeDisplay[i].gameObject.SetActive(false);
            }

        }
                     


    }

}
