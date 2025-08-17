using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeBattleButtons : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] timeBattleNameDisplay;
    [SerializeField] TextMeshProUGUI[] timeBattlePrizeDisplay;

    [SerializeField] GameObject[] timeBattleNameButtons;



    // Start is called before the first frame update
    void Awake()
    {
        
    }

    public void UpdateDisplays()

    {
        for (int i=0; i < timeBattleNameDisplay.Length; i++)

        {
            timeBattleNameDisplay[i].text = MainManager.cars[i];
            timeBattlePrizeDisplay[i].text = MainManager.carPrizes[i].ToString();

            if(MainManager.carPrizes[i] < 1)
            {
                timeBattleNameDisplay[i].gameObject.SetActive(false);
                timeBattlePrizeDisplay[i].gameObject.SetActive(false);
                timeBattleNameButtons[i].gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
