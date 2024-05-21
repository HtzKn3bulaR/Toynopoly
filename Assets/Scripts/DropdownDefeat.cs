using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownDefeat : MonoBehaviour
{
    //public TMP_Dropdown winnerDropdown;
    public TMP_Dropdown defeatDropdown;
    private void Awake()
    {
        //winnerDropdown = GetComponent<TMP_Dropdown>();
        defeatDropdown = GetComponent<TMP_Dropdown>();

    }

    public void LoadPlayerOptions()

    {
        //winnerDropdown.ClearOptions();
        defeatDropdown.ClearOptions();

        var option1 = new TMP_Dropdown.OptionData("N/A");
        //winnerDropdown.options.Add(option1);
        defeatDropdown.options.Add(option1);

        if (MainManager.activePlayer == 0)
        {
            var option2 = new TMP_Dropdown.OptionData(MainManager.playerNames[1]);
            //winnerDropdown.options.Add(option2);
            defeatDropdown.options.Add(option2);

        }

        else if (MainManager.activePlayer == 1)

        {
            var option2 = new TMP_Dropdown.OptionData(MainManager.playerNames[0]);
            //winnerDropdown.options.Add(option2);
            defeatDropdown.options.Add(option2);
            

        }


        //winnerDropdown.RefreshShownValue();
        defeatDropdown.RefreshShownValue();
    }


}
