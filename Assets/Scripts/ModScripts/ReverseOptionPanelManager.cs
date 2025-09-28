using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseOptionPanelManager : MonoBehaviour
{
     

    public void DisableReverseButton(Transform pressedButton)
    {
        Transform buttonToDisable = pressedButton;
        buttonToDisable.gameObject.SetActive(false);
    }

}
