using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThirdPlaceDropdown : MonoBehaviour
{
    public TMP_Dropdown thirdPlaceDropdown;
    private void Awake()
    {
        thirdPlaceDropdown = GetComponent<TMP_Dropdown>();

    }

    public void LoadPlayerOptions()

    {
        thirdPlaceDropdown.ClearOptions();

        var option1 = new TMP_Dropdown.OptionData(MainManager.playerNames[0]);
        thirdPlaceDropdown.options.Add(option1);

        var option2 = new TMP_Dropdown.OptionData(MainManager.playerNames[1]);
        thirdPlaceDropdown.options.Add(option2);

        var option3 = new TMP_Dropdown.OptionData(MainManager.playerNames[2]);
        thirdPlaceDropdown.options.Add(option3);

        if (MainManager.playerNumber > 3)

        {
            var option4 = new TMP_Dropdown.OptionData(MainManager.playerNames[3]);
            thirdPlaceDropdown.options.Add(option4);
        }

        if (MainManager.playerNumber > 4)

        {

            var option5 = new TMP_Dropdown.OptionData(MainManager.playerNames[4]);
            thirdPlaceDropdown.options.Add(option5);

        }

        thirdPlaceDropdown.RefreshShownValue();


    }

}