using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunnerUpDropdown : MonoBehaviour
{
    public TMP_Dropdown runnerUpDropdown;
    private void Awake()
    {
        runnerUpDropdown = GetComponent<TMP_Dropdown>();

    }

    public void LoadPlayerOptions()

    {
        runnerUpDropdown.ClearOptions();

        var option1 = new TMP_Dropdown.OptionData(MainManager.playerNames[0]);
        runnerUpDropdown.options.Add(option1);

        var option2 = new TMP_Dropdown.OptionData(MainManager.playerNames[1]);
        runnerUpDropdown.options.Add(option2);

        var option3 = new TMP_Dropdown.OptionData(MainManager.playerNames[2]);
        runnerUpDropdown.options.Add(option3);

        if (MainManager.playerNumber > 3)

        {
            var option4 = new TMP_Dropdown.OptionData(MainManager.playerNames[3]);
            runnerUpDropdown.options.Add(option4);
        }


        runnerUpDropdown.RefreshShownValue();


    }

}