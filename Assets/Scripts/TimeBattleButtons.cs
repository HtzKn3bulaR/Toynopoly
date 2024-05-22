using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeBattleButtons : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] timeBattleNameDisplay;
    [SerializeField] TextMeshProUGUI[] timeBattlePrizeDisplay;



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
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
