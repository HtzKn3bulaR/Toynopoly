using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinnerDropdown : MonoBehaviour
{
    public TMP_Dropdown winnerDropdown;
    private void Awake()
    {
        winnerDropdown = GetComponent<TMP_Dropdown>();
        
    }

    public void LoadPlayerOptions()

    {
        winnerDropdown.ClearOptions();
        
        var option1 = new TMP_Dropdown.OptionData(MainManager.playerNames[0]);
        winnerDropdown.options.Add(option1);
        
        var option2 = new TMP_Dropdown.OptionData(MainManager.playerNames[1]);
        winnerDropdown.options.Add(option2);

        var option3 = new TMP_Dropdown.OptionData(MainManager.playerNames[2]);
        winnerDropdown.options.Add(option3);


        winnerDropdown.RefreshShownValue();


    }

}
