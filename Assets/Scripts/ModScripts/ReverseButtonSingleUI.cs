using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseButtonSingleUI : MonoBehaviour
{
    [SerializeField] private Transform button;
    [SerializeField] private ReverseOptionPanelManager reverseOptionPanelManager;

    public void ReportThisButton()
    {
        reverseOptionPanelManager.DisableReverseButton(button);
    }

}
