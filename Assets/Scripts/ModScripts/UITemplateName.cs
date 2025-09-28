using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITemplateName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;


    public void SetName(string name)
    {
        playerNameText.text = name;
    }


    public void SetChosenColor()
    {
        playerNameText.color = new Color(1.0f, 0.6588f, 0.00392f, 0.9411f);
    }

    public void SetDefaultColor()
    {
        playerNameText.color = new Color(0.1294f, 0.2666f, 0.3764f, 0.9411f);
    }


}
